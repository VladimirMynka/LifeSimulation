using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents
{
    public class BehaviourChangerComponent : WorldObjectComponent, IHaveInformation
    {
        private List<IHaveTarget> _competitors;
        private MovingComponent _movingComponent;
        private int _lastPriority;
        
        public BehaviourChangerComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _movingComponent = GetComponent<MovingComponent>();
            _competitors = GetComponents<IHaveTarget>();
        }

        public override void Update()
        {
            base.Update();
            
            _movingComponent.SetTarget(GetTarget());
        }

        private WorldObject GetTarget()
        {
            var component = ComponentWithLargestPriority();
            return component == null 
                ? null 
                : component.GetTarget();
        }

        private IHaveTarget ComponentWithLargestPriority()
        {
            _lastPriority = 0;
            IHaveTarget component = null;
            
            foreach (var competitor in _competitors)
            {
                var priority = competitor.GetPriority();
                if (priority > _lastPriority)
                {
                    _lastPriority = priority;
                    component = competitor;
                }
            }

            return component;
        }

        public void Add(IHaveTarget component)
        {
            _competitors.Add(component);
        }

        public override string ToString()
        {
            return "Last priority: " + _lastPriority;
        }
    }
}