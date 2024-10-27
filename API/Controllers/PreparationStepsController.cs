using API.Controllers.ActionFilters;
using API.Controllers.Generics;
using API.Models.DBModels;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public class PreparationStepsController : GenericController<PreparationStepDBM>
    {
        private readonly IGenericRepo<PreparationStepDBM> _preparationStepsRepo;
        public PreparationStepsController(IGenericRepo<PreparationStepDBM> repo) : base(repo, "RecipeId")
        {
            _preparationStepsRepo = repo;

            this.onAddAction = OnAddAction;
            this.afterDeleteAction = AfterDeleteAction;
        }

        [HttpGet("{id}/MoveUp")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult MoveUp(int id)
        {
            var searchedStep = _preparationStepsRepo.Get(id);

            if (searchedStep == null)
            {
                return NotFound();
            }

            ChangeStepPosition(searchedStep, true);

            return Ok();
        }

        [HttpGet("{id}/MoveDown")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult MoveDown(int id)
        {
            var searchedStep = _preparationStepsRepo.Get(id);

            if(searchedStep == null)
            {
                return NotFound();
            }

            ChangeStepPosition(searchedStep, false);

            return Ok();
        }

        private void OnAddAction(PreparationStepDBM entity)
        {
            var lastStep = _preparationStepsRepo.Get(c => c.RecipeId == entity.RecipeId).OrderByDescending(c => c.OrderNumber).FirstOrDefault();

            if(lastStep != null)
            {
                entity.OrderNumber = lastStep.OrderNumber + 1;
            }
            else
            {
                entity.OrderNumber = 0;
            }
        }

        private void AfterDeleteAction(PreparationStepDBM entity)
        {
            var steps = _preparationStepsRepo.Get(c => c.RecipeId == entity.RecipeId).OrderBy(c => c.OrderNumber).ToList();

            for(int i = 0; i < steps.Count(); i++)
            {
                if(steps[i].OrderNumber != i)
                {
                    steps[i].OrderNumber = i;
                    _preparationStepsRepo.Update(steps[i]);
                }
            }
        }

        private void ChangeStepPosition(PreparationStepDBM searchedStep, bool isUp)
        {
            var steps = _preparationStepsRepo.Get(c => c.RecipeId == searchedStep.RecipeId);

            steps = steps.OrderBy(c => c.OrderNumber).ToList();

            for (int i = 0; i < steps.Count(); i++)
            {
                if (steps[i].Id == searchedStep.Id)
                {
                    if (isUp)
                    {
                        if(steps[i].OrderNumber == 0)
                        {
                            return;
                        }

                        steps[i].OrderNumber--;
                        steps[i - 1].OrderNumber++;

                        _preparationStepsRepo.Update(steps[i]);
                        _preparationStepsRepo.Update(steps[i - 1]);

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

                        _preparationStepsRepo.Update(steps[i]);
                        _preparationStepsRepo.Update(steps[i + 1]);

                        return;
                    }
                }
            }
        }
    }
}
