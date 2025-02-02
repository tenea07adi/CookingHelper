using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class PreparationStepsService : GenericEntityService<PreparationStep>, IPreparationStepsService
    {
        public PreparationStepsService(IGenericRepo<PreparationStep> genericRepo) : base(genericRepo)
        {
            this.onAddAction = OnAddAction;
            this.afterDeleteAction = AfterDeleteAction;
        }

        public void MoveUp(int id)
        {
            var searchedStep = _genericRepo.Get(id);

            if (searchedStep == null)
            {
                throw new KeyNotFoundException(Constants.Exceptions.EntityNotFound);
            }

            ChangeStepPosition(searchedStep, true);
        }

        public void MoveDown(int id)
        {
            var searchedStep = _genericRepo.Get(id);

            if (searchedStep == null)
            {
                throw new KeyNotFoundException(Constants.Exceptions.EntityNotFound);
            }

            ChangeStepPosition(searchedStep, false);
        }

        private void OnAddAction(PreparationStep entity)
        {
            var lastStep = _genericRepo.Get(c => c.RecipeId == entity.RecipeId).OrderByDescending(c => c.OrderNumber).FirstOrDefault();

            if (lastStep != null)
            {
                entity.OrderNumber = lastStep.OrderNumber + 1;
            }
            else
            {
                entity.OrderNumber = 0;
            }
        }

        private void AfterDeleteAction(PreparationStep entity)
        {
            var steps = _genericRepo.Get(c => c.RecipeId == entity.RecipeId).OrderBy(c => c.OrderNumber).ToList();

            for (int i = 0; i < steps.Count(); i++)
            {
                if (steps[i].OrderNumber != i)
                {
                    steps[i].OrderNumber = i;
                    _genericRepo.Update(steps[i]);
                }
            }
        }

        private void ChangeStepPosition(PreparationStep searchedStep, bool isUp)
        {
            var steps = _genericRepo.Get(c => c.RecipeId == searchedStep.RecipeId);

            steps = steps.OrderBy(c => c.OrderNumber).ToList();

            for (int i = 0; i < steps.Count(); i++)
            {
                if (steps[i].Id == searchedStep.Id)
                {
                    if (isUp)
                    {
                        if (steps[i].OrderNumber == 0)
                        {
                            return;
                        }

                        steps[i].OrderNumber--;
                        steps[i - 1].OrderNumber++;

                        _genericRepo.Update(steps[i]);
                        _genericRepo.Update(steps[i - 1]);

                        return;
                    }
                    else
                    {
                        if (steps[i].OrderNumber == steps.Count() - 1)
                        {
                            return;
                        }

                        steps[i].OrderNumber++;
                        steps[i + 1].OrderNumber--;

                        _genericRepo.Update(steps[i]);
                        _genericRepo.Update(steps[i + 1]);

                        return;
                    }
                }
            }
        }
    }
}
