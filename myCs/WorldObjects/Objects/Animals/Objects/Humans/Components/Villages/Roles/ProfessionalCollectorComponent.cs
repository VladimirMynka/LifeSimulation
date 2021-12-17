using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public class ProfessionalCollectorComponent : ProfessionalComponent
    {
        public ProfessionalCollectorComponent(WorldObject owner, int period) 
            : base(owner, period)
        {
        }

        public override void Start()
        {
            base.Start();
            humanEaterComponent.CollectingTypes.Clear();
            humanEaterComponent.CollectingTypes.Add(MealType.Plant);
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(30, 30, 30);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureInstrumentsOwnerBehaviour(0, 0);
            ConfigurePetsOwnerBehaviour(0, 0, 0, 0);
            ConfigureWarehousesOwnerBehaviour(50, 35);
            ConfigureBuilderBehaviour(0);
        }
    }
}