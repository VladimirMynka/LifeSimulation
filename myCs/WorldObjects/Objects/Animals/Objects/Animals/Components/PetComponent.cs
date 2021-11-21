using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class PetComponent: WorldObjectComponent, IHaveInformation
    {
        public PetsOwnerComponent PetOwner;
        private EaterComponent _eaterComponent;
        private int _toPresentTimer;
        private readonly int _defaultToPresentTimer;
        
        public PetComponent(WorldObject owner, int timer) : base(owner)
        {
            _toPresentTimer = timer;
            _defaultToPresentTimer = timer;
        }

        public override void Start()
        {
            base.Start();
            _eaterComponent = GetComponent<EaterComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (CheckWereDestroyed(PetOwner))
            {
                PetOwner = null;
                _toPresentTimer = _defaultToPresentTimer;
            }
            UpdateTimer();
            if (IsHungry())
                PetOwner.GetSignal(this, IsVeryHungry());
            if (_toPresentTimer == 0)
            {
                if (OwnerHere())
                    GivePresent();
                else
                    PetOwner.GetSignal(this);
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
            return !CheckWereDestroyed(PetOwner) &&
                   Direction.CheckEqual(WorldObject.Cell.Coords, PetOwner.WorldObject.Cell.Coords);
        }

        private void GivePresent()
        {
            PetOwner.GetPresent(20, GetMealType());
        }

        public bool HasPresent()
        {
            return _toPresentTimer == 0;
        }

        public MealType GetMealType()
        {
            return _eaterComponent.MealType;
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

        public string GetInformation()
        {
            var info = "";
            info += "Owner: ";
            if (CheckWereDestroyed(PetOwner))
                info += "none";
            else
                info += "on " + InformationComponent.GetInfoAboutCoords(PetOwner);
            return info;
        }
    }
}