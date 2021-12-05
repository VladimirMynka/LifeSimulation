using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class PetComponent: WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private PetsOwnerComponent _petOwner;
        private EaterComponent _eaterComponent;
        private int _toPresentTimer;
        private readonly int _defaultToPresentTimer;
        private CreatureType _creatureType;
        private readonly PetEffect _effect;
        private readonly int _presentValue;
        
        public PetComponent(WorldObject owner, int timer, PetEffect effect, int presentValue) : base(owner)
        {
            _toPresentTimer = timer;
            _defaultToPresentTimer = timer;
            _effect = effect;
            _presentValue = presentValue;
        }

        public override void Start()
        {
            base.Start();
            _eaterComponent = GetComponent<EaterComponent>();
            _creatureType = GetComponent<EatableComponent>().CreatureType;
        }

        public override void Update()
        {
            base.Update();
            if (CheckWereDestroyed(_petOwner))
            {
                _petOwner = null;
                _toPresentTimer = _defaultToPresentTimer;
                return;
            }
            UpdateTimer();
            if (IsHungry())
                _petOwner.GetSignal(this, IsVeryHungry());
            if (_toPresentTimer == 0)
            {
                if (OwnerHere())
                    GivePresent();
                else
                    _petOwner.GetSignal(this);
            }
        }

        private void UpdateTimer()
        {
            _toPresentTimer--;
            if (_toPresentTimer < 0)
                _toPresentTimer = 0;
        }

        private bool OwnerHere()
        {
            return !CheckWereDestroyed(_petOwner) &&
                   Direction.CheckEqual(WorldObject.Cell.Coords, _petOwner.WorldObject.Cell.Coords);
        }

        private void GivePresent()
        {
            _petOwner.GetPresent(_presentValue, _effect, GetMealType());
            _toPresentTimer = _defaultToPresentTimer;
        }

        public bool HasPresent()
        {
            return _toPresentTimer == 0;
        }

        public EatableResource GetMealType(int quantity = 20)
        {
            return EatableResource.CreateResource(_eaterComponent.MealType, quantity);
        }

        public CreatureType GetCreatureType()
        {
            return _creatureType;
        }

        public void AddSatiety(int delta)
        {
            _eaterComponent.AddSatiety(delta);
        }

        public bool IsHungry()
        {
            return _eaterComponent.IsHungry();
        }

        public bool IsVeryHungry()
        {
            return _eaterComponent.IsVeryHungry();
        }

        public void SetOwner(PetsOwnerComponent petsOwnerComponent)
        {
            if (CheckWereDestroyed(petsOwnerComponent))
                return;
            _petOwner = petsOwnerComponent;
            _eaterComponent.Exclude(CreatureType.Human);
        }

        public PetsOwnerComponent GetOwner()
        {
            return _petOwner;
        }

        public void RemoveOwner()
        {
            _petOwner = null;
            _eaterComponent.Include(CreatureType.Human);
        }
        
        public override string ToString()
        {
            var info = "";
            info += "Owner: ";
            if (CheckWereDestroyed(_petOwner))
                info += "none";
            else
                info += "on " + InformationComponent.GetInfoAboutCoords(_petOwner);

            info += "\nPet effect: " + _effect;
            info += "\nTo present ticks: " + _toPresentTimer;
            return info;
        }

        public int GetInformationPriority()
        {
            return 70;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 12 if it's very hungry and owner has meal,
        /// 6 if it's hungry and owner has meal,
        /// 2 if it has present
        /// </returns>
        public int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(_petOwner) ? 0
                : IsVeryHungry() && _petOwner.HasMealFor(GetMealType(50)) ? 21
                : IsHungry() && _petOwner.HasMealFor(GetMealType(20)) ? 11
                : HasPresent() ? 2
                : 0;
        }

        public WorldObject GetTarget()
        {
            return CheckWereDestroyed(_petOwner) ? null : _petOwner.WorldObject;
        }
    }
}