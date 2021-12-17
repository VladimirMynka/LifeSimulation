using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public abstract class ProfessionalComponent : WorldObjectComponent, IHaveInformation
    {
        protected const int InfinityPeriod = -1;
        protected HumanEaterComponent humanEaterComponent;
        protected MatingComponent matingComponent;
        protected PetsOwnerComponent petsOwnerComponent;
        protected WarehousesOwnerComponent warehousesOwnerComponent;
        protected BuilderComponent builderComponent;
        protected InstrumentsOwnerComponent instrumentsOwnerComponent;
        protected CitizenComponent citizenComponent;
        private int _timer;

        protected ProfessionalComponent(WorldObject owner, int period) : base(owner)
        {
            _timer = period;
        }
        
        public override void Start()
        {
            base.Start();
            humanEaterComponent = GetComponent<HumanEaterComponent>();
            matingComponent = GetComponent<MatingComponent>();
            petsOwnerComponent = GetComponent<PetsOwnerComponent>();
            warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            builderComponent = GetComponent<BuilderComponent>();
            instrumentsOwnerComponent = GetComponent<InstrumentsOwnerComponent>();
            citizenComponent = GetComponent<CitizenComponent>();
            ConfigureBehaviour();
        }

        public override void Update()
        {
            base.Update();
            if (_timer < 0) 
                return;
            if (_timer == 0)
            {
                Destroy();
                return;
            }
            _timer--;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ConfigureWithDefaults();
            citizenComponent.DeclareEndOfWork();
        }

        protected abstract void ConfigureBehaviour();

        protected void ConfigureWithDefaults()
        {
            ConfigureEaterBehaviour();
            ConfigureMatingBehaviour();
            ConfigurePetsOwnerBehaviour();
            ConfigureInstrumentsOwnerBehaviour();
            ConfigureWarehousesOwnerBehaviour();
            ConfigureBuilderBehaviour();
        }

        protected void ConfigureEaterBehaviour(
            int forVeryHungry = Defaults.BehaviourVeryHungry,
            int forHungry = Defaults.BehaviourHungry,
            int forSearching = Defaults.BehaviourSearching
        )
        {
            humanEaterComponent.PriorityWhenVeryHungry = forVeryHungry;
            humanEaterComponent.PriorityWhenHungry = forHungry;
            humanEaterComponent.PrioritySearching = forSearching;
        }

        protected void ConfigureMatingBehaviour(
            int forPartnerVeryHungry = Defaults.BehaviourVeryHungry,
            int forPartnerHungry = Defaults.BehaviourHungry,
            int forTimeToMate = Defaults.BehaviourSearching,
            int forWaitInHouse = Defaults.BehaviourWait
        )
        {
            if (matingComponent == null)
                return;
            var womanComponent = matingComponent as WomanComponent;
            if (womanComponent != null)
            {
                womanComponent.PriorityWhenPartnerIsVeryHungry = forPartnerVeryHungry;
                womanComponent.PriorityWhenPartnerIsHungry = forPartnerHungry;
                womanComponent.PriorityWhenItIsTimeToMating = forTimeToMate;
            }
            else
            {
                var manComponent = matingComponent as ManComponent;
                manComponent.PriorityWhenPartnerIsVeryHungry = forPartnerVeryHungry;
                manComponent.PriorityWhenPartnerIsHungry = forPartnerHungry;
                manComponent.PriorityWhenItIsTimeToMating = forTimeToMate;
                manComponent.PriorityWaitInHouse = forWaitInHouse;
            }
        }

        protected void ConfigurePetsOwnerBehaviour(
            int forPetIsVeryHungry = Defaults.BehaviourPetOwnerPetVeryHungry,
            int forPetIsHungry = Defaults.BehaviourPetOwnerPetHungry,
            int forGetPresent = Defaults.BehaviourPetOwnerThereArePresents,
            int forTame = Defaults.BehaviourTame
        )
        {
            if (petsOwnerComponent == null)
                return;
            petsOwnerComponent.PriorityWhenPetIsVeryHungry = forPetIsVeryHungry;
            petsOwnerComponent.PriorityWhenPetIsHungry = forPetIsHungry;
            petsOwnerComponent.PriorityGetPresent = forGetPresent;
            petsOwnerComponent.PriorityTame = forTame;
        }

        protected void ConfigureWarehousesOwnerBehaviour(
            int forTakeEat = Defaults.BehaviourWarehouseTakeMeal,
            int forPutOrTakeResource = Defaults.BehaviourWarehouseTakeOrPut
        )
        {
            warehousesOwnerComponent.TakeEatPriority = forTakeEat;
            warehousesOwnerComponent.PutOrTakeResourcePriority = forPutOrTakeResource;
        }

        protected void ConfigureInstrumentsOwnerBehaviour(
            int forTriggered = Defaults.BehaviourResourcesSearchingTriggered,
            int forUsualSearching = Defaults.BehaviourUneatableSearching
        )
        {
            if (instrumentsOwnerComponent == null)
                return;
            instrumentsOwnerComponent.ResourcesSearchingTriggered = forTriggered;
            instrumentsOwnerComponent.UneatableSearching = forUsualSearching;
        }

        protected void ConfigureBuilderBehaviour(
            int forBuildingProcess = Defaults.BehaviourBuilder
        )
        {
            if (builderComponent == null)
                return;
            builderComponent.BuildingProcessPriority = forBuildingProcess;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityProfessional;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}