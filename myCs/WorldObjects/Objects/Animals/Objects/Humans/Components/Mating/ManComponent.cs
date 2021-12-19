using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating
{
    public class ManComponent : MaleComponent
    {
        private InventoryComponent<Resource> _inventory;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private BuilderComponent _builderComponent;
        private WomanComponent _woman;

        public ManComponent(WorldObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent<Resource>>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            _builderComponent = GetComponent<BuilderComponent>();
        }

        public override void Update()
        {
            base.Update();

            if (CheckWereDestroyed(partner))
                return;

            if (_woman == null)
                _woman = (WomanComponent) partner;

            if (_warehousesOwnerComponent.House == null)
                _builderComponent.StartBuildHouse();
            else
                _woman.SetHouse(_warehousesOwnerComponent.House);

            if (base.CheckPartnerHere() && (_woman.IsHungry() || IsHungry()))
                _woman.AverageEatWith(_inventory);
        }

        protected override bool CheckPartnerHere()
        {
            return base.CheckPartnerHere() && CheckHouseHere();
        }

        private bool CheckHouseHere()
        {
            return OnOneCellWith(_warehousesOwnerComponent.House != null 
                ? _warehousesOwnerComponent.House.GetWorldObject() : null);
        }

        public int PriorityWhenPartnerIsVeryHungry = Defaults.BehaviourPartnerIsVeryHungry;
        public int PriorityWhenPartnerIsHungry = Defaults.BehaviourPartnerIsHungry;
        public int PriorityWhenItIsTimeToMating = Defaults.BehaviourItIsTimeToMating;
        public int PriorityWaitInHouse = Defaults.BehaviourWait;
        public override int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(_woman)
                ? Defaults.BehaviourHaveNotPriority
                : _woman.IsVeryHungry()
                    ? PriorityWhenPartnerIsVeryHungry
                    : _woman.IsHungry()
                        ? PriorityWhenPartnerIsHungry
                        : _woman.IsReady() && IsReady()
                            ? CheckPartnerHere() ? PriorityWaitInHouse
                            : PriorityWhenItIsTimeToMating
                            : Defaults.BehaviourHaveNotPriority;
        }

        public override WorldObject GetTarget()
        {
            return CheckWereDestroyed(_woman)
                ? null
                : IsReady() && _woman.IsReady() && _warehousesOwnerComponent.House != null
                    ? _warehousesOwnerComponent.House.GetWorldObject()
                    : _woman.WorldObject;
        }

        public bool IsHungry()
        {
            return eaterComponent.IsHungry();
        }

        public bool IsVeryHungry()
        {
            return eaterComponent.IsVeryHungry();
        }

        public override bool IsReady()
        {
            return base.IsReady() && _warehousesOwnerComponent.House != null;
        }

        protected override bool FemaleCheckInSearch(FemaleComponent component)
        {
            var woman = component as WomanComponent;
            return base.FemaleCheckInSearch(component) && woman != null &&
                   (_warehousesOwnerComponent.House != woman.GetHouse()
                    || _warehousesOwnerComponent.House == null
                    || woman.GetHouse() == null);
        }

        protected override bool CanMateWith(FemaleComponent female)
        {
            return female != null && female.IsReady();
        }
    }
}