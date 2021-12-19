namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class ShepherdComponent : ProfessionalComponent
    {
        public ShepherdComponent(WorldObject owner, CitizenComponent citizenComponent, int period) 
            : base(owner, citizenComponent, period)
        {
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(10, 5, 0);
            ConfigureMatingBehaviour(6, 2, 15);
            ConfigureInstrumentsOwnerBehaviour(0, 0);
            ConfigurePetsOwnerBehaviour(40, 20, 10, 30);
            ConfigureWarehousesOwnerBehaviour(50, 12);
            ConfigureBuilderBehaviour(0);
        }
    }
}