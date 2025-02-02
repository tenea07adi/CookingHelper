using Core.Entities.Persisted;

namespace Core.Ports.Driving
{
    public interface IPreparationStepsService : IGenericEntityService<PreparationStep>
    {
        public void MoveUp(int id);
        public void MoveDown(int id);
    }
}
