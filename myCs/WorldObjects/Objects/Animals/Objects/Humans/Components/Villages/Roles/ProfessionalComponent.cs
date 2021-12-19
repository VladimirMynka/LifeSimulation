using System;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles;
using LifeSimulation.myCs.WorldStructure;

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
            if (WorldObject.IsDestroyed)
                return;
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

        protected static ProfessionalComponent CreateComponentForRole(
            ProfessionalRole role,
            WorldObject owner,
            int period
        )
        {
            switch (role)
            {
                case ProfessionalRole.Doctor:
                    return new DoctorComponent(owner, period);
                case ProfessionalRole.Gardener:
                    return new GardenerComponent(owner, period);
                case ProfessionalRole.Hunter:
                    return new HunterComponent(owner, period);
                case ProfessionalRole.Mainer:
                    return new MainerComponent(owner, period);
                case ProfessionalRole.Builder:
                    return new ProfessionalBuilderComponent(owner, period);
                case ProfessionalRole.Collector:
                    return new ProfessionalCollectorComponent(owner, period);
                case ProfessionalRole.Resting:
                    return new RestingComponent(owner, period);
                case ProfessionalRole.Shepherd:
                    return new ShepherdComponent(owner, period);
                default:
                    return null;
            }
        }

        public static ProfessionalRole GetEnumByType(Type roleAsType)
        {
            return roleAsType == typeof(DoctorComponent) ? ProfessionalRole.Doctor
                : roleAsType == typeof(GardenerComponent) ? ProfessionalRole.Gardener
                : roleAsType == typeof(HunterComponent) ? ProfessionalRole.Hunter
                : roleAsType == typeof(MainerComponent) ? ProfessionalRole.Mainer
                : roleAsType == typeof(PresidentComponent) ? ProfessionalRole.President
                : roleAsType == typeof(ProfessionalBuilderComponent) ? ProfessionalRole.Builder
                : roleAsType == typeof(ProfessionalCollectorComponent) ? ProfessionalRole.Collector
                : roleAsType == typeof(ShepherdComponent) ? ProfessionalRole.Shepherd
                : ProfessionalRole.Resting;
        }

        public static ProfessionalRole[] MenRoles = new ProfessionalRole[]{
            ProfessionalRole.Hunter,
            ProfessionalRole.Mainer,
            ProfessionalRole.Builder,
            ProfessionalRole.Shepherd,
            ProfessionalRole.Doctor
        };

        public static ProfessionalRole[] WomenRoles = new ProfessionalRole[]{
            ProfessionalRole.Doctor,
            ProfessionalRole.Gardener,
            ProfessionalRole.Collector,
            ProfessionalRole.Shepherd
        };

        public static ProfessionalRole[] OnlyWinterRoles = new ProfessionalRole[]{
            ProfessionalRole.Doctor
        };
        
        public static ProfessionalRole[] OnlySummerRoles = new ProfessionalRole[]{
            ProfessionalRole.Gardener
        }

        public static ProfessionalComponent CreateRandomWithGender(
            bool isMale,
            WorldObject owner,
            int period
        )
        {
            if (isMale)
                return CreateComponentForRole(MenRoles[World.Random.Next(MenRoles.Length)], owner, period);
            return CreateComponentForRole(WomenRoles[World.Random.Next(WomenRoles.Length)], owner, period);
        }
    }
}