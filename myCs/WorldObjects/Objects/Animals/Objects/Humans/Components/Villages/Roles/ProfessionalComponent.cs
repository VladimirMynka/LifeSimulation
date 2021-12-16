using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles
{
    public abstract class ProfessionalComponent : WorldObjectComponent
    {
        protected HumanEaterComponent humanEaterComponent;
        protected MatingComponent matingComponent;
        protected PetsOwnerComponent petsOwnerComponent;
        protected WarehousesOwnerComponent warehousesOwnerComponent;
        protected BuilderComponent builderComponent;
        protected InstrumentsOwnerComponent instrumentsOwnerComponent;

        protected ProfessionalComponent(WorldObject owner) : base(owner)
        {
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
            humanEaterComponent.PriorityWhenVeryHungry = Defaults.BehaviourVeryHungry;
            humanEaterComponent.PriorityWhenHungry = Defaults.BehaviourHungry;
            humanEaterComponent.PrioritySearching = Defaults.BehaviourSearching;
        }

        protected void ConfigureMatingBehaviour(
            int forPartnerVeryHungry = Defaults.BehaviourVeryHungry,
            int forPartnerHungry = Defaults.BehaviourHungry,
            int forTimeToMate = Defaults.BehaviourSearching,
            int forWaitInHouse = Defaults.BehaviourWait
        )
        {
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
            int forGetPresent = Defaults.BehaviourPetOwnerThereArePresents
        )
        {
            petsOwnerComponent.PriorityWhenPetIsVeryHungry = forPetIsVeryHungry;
            petsOwnerComponent.PriorityWhenPetIsHungry = forPetIsHungry;
            petsOwnerComponent.PriorityGetPresent = forGetPresent;
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
            instrumentsOwnerComponent.ResourcesSearchingTriggered = forTriggered;
            instrumentsOwnerComponent.UneatableSearching = forUsualSearching;
        }

        protected void ConfigureBuilderBehaviour(
            int forBuildingProcess = Defaults.BehaviourBuilder
        )
        {
            builderComponent.BuildingProcessPriority = forBuildingProcess;
        }
    }
}