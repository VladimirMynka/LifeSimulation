using System;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class BehaviourChangerComponent : WorldObjectComponent
    {
        private HumanEaterComponent _eaterComponent;
        private MovingComponent _movingComponent;
        private IHumanMating _matingComponent;
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
                _matingComponent = GetComponent<MatingComponent>() as IHumanMating;
            
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