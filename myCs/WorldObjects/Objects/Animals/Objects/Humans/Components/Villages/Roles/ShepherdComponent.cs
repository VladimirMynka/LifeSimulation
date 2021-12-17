namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public class ShepherdComponent : ProfessionalComponent
    {
        public ShepherdComponent(WorldObject owner, int period) 
            : base(owner, period)
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