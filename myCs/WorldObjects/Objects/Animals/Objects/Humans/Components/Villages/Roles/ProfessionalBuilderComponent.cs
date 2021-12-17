namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public class ProfessionalBuilderComponent : ProfessionalComponent
    {
        public ProfessionalBuilderComponent(WorldObject owner, int period) 
            : base(owner, period)
        {
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(20, 10, 0);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureInstrumentsOwnerBehaviour(25, 0);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(50, 45);
            ConfigureBuilderBehaviour(40);
        }
    }
}