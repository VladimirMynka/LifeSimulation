using System;
using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public class PresidentComponent : ProfessionalComponent
    {
        private readonly Village _village;
        private readonly List<CitizenComponent> _citizens;
        
        
        public PresidentComponent(WorldObject owner, Village village, List<CitizenComponent> citizens) 
            : base(owner, InfinityPeriod)
        {
            _village = village;
            _citizens = citizens;
        }

        public override void Start()
        {
            base.Start();
            if (_village.IsActive())
                ToActiveMod();
        }

        public void ToActiveMod()
        {
            WorldObject.RemoveComponent(builderComponent);
            WorldObject.RemoveComponent(petsOwnerComponent);
            WorldObject.RemoveComponent(instrumentsOwnerComponent);
            builderComponent = null;
            petsOwnerComponent = null;
            instrumentsOwnerComponent = null;;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _village.StartElection(World.Random.Next(_citizens.Count));
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(20, 10, 0);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigureWarehousesOwnerBehaviour(50, 4);
        }

        public void AskNewJob(CitizenComponent citizenComponent, Type lastJobType)
        {

            citizenComponent.SetJob(new ProfessionalComponent(citizenComponent.WorldObject));
        }
    }
}