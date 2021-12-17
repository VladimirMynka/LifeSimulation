namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public class MainerComponent : ProfessionalComponent
    {
        public MainerComponent(WorldObject owner, int period) 
            : base(owner, period)
        {
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(20, 10, 0);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureInstrumentsOwnerBehaviour(35, 35);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(50, 40);
            ConfigureBuilderBehaviour(0);
        }
    }
}