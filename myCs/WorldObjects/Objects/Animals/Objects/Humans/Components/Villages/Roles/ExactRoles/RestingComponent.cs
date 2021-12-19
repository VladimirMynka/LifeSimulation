namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class RestingComponent : ProfessionalComponent
    {
        public RestingComponent(WorldObject owner, CitizenComponent citizenComponent, int period) 
            : base(owner, citizenComponent, period)
        {
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(10, 5, 0);
            ConfigureMatingBehaviour(20, 15, 30);
            ConfigureInstrumentsOwnerBehaviour(0, 0);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(50, 5);
            ConfigureBuilderBehaviour(0);
        }
    }
}