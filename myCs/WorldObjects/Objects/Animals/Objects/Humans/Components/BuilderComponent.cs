using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class BuilderComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private IBuilding<Resource> _targetBuilding;
        public BuilderComponent(WorldObject owner) : base(owner)
        {
        }

        public int GetInformationPriority()
        {
            throw new System.NotImplementedException();
        }

        public int GetPriorityInBehaviour()
        {
            throw new System.NotImplementedException();
        }

        public WorldObject GetTarget()
        {
            if (_targetBuilding == null || CheckWereDestroyed(_targetBuilding.GetWorldObject()))
                return null;
            return _targetBuilding.GetWorldObject();
        }
    }
}