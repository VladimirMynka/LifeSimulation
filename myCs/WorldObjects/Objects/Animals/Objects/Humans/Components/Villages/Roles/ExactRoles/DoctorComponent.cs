using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class DoctorComponent : ProfessionalComponent // not ended
    {
        public DoctorComponent(WorldObject owner, CitizenComponent citizenComponent, int period) 
            : base(owner, citizenComponent, period)
        {
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(30, 30, 30);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureInstrumentsOwnerBehaviour(0, 0);
            ConfigurePetsOwnerBehaviour(8, 2, 6, 0);
            ConfigureWarehousesOwnerBehaviour(50, 35);
            ConfigureBuilderBehaviour(0);
            humanEaterComponent.CollectingTypes.Clear();
            humanEaterComponent.CollectingTypes.Add(MealType.Plant);
        }
    }
}