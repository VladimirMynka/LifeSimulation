using System;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents
{
    public class BehaviourChangerComponent : WorldObjectComponent
    {
        private HumanEaterComponent _eaterComponent;
        private MovingComponent _movingComponent;
        private IHaveTarget _matingComponent;
        private PetsOwnerComponent _petsOwnerComponent;
        private AgeComponent _ageComponent;
        public BehaviourChangerComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _eaterComponent = GetComponent<HumanEaterComponent>();
            _movingComponent = GetComponent<MovingComponent>();
            _petsOwnerComponent = GetComponent<PetsOwnerComponent>();
            _ageComponent = GetComponent<AgeComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (_ageComponent.AgeStage == AgeStage.Mother && _matingComponent == null)
                _matingComponent = GetComponent<MatingComponent>() as IHaveTarget;
            
            _movingComponent.SetTarget(GetTarget());
        }

        private WorldObject GetTarget()
        {
            var eaterPriority = _eaterComponent.GetPriority();
            var matingPriority = _matingComponent.GetPriority();
            var petsPriority = _petsOwnerComponent.GetPriority();
            var max = Math.Max(Math.Max(eaterPriority, matingPriority), petsPriority);
            return max == eaterPriority 
                ? _eaterComponent.GetTarget() 
                : max == matingPriority 
                    ? _matingComponent.GetTarget() 
                    : _petsOwnerComponent.GetTarget();
        }
    }
}