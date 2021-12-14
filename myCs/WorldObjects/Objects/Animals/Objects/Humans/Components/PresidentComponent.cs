using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class PresidentComponent : WorldObjectComponent
    {
        private readonly Village _village;
        private readonly List<CitizenComponent> _citizens;
        
        
        public PresidentComponent(WorldObject owner, Village village, List<CitizenComponent> citizens) 
            : base(owner)
        {
            _village = village;
            _citizens = citizens;
        }

        public void ToActiveMod()
        {
            throw new System.NotImplementedException();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _village.StartElection(World.Random.Next(_citizens.Count));
        }
    }
}