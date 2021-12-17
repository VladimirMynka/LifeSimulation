namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public class RestingComponent : ProfessionalComponent
    {
        public RestingComponent(WorldObject owner, int period) 
            : base(owner, period)
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