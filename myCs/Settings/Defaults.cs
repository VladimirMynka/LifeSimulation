namespace LifeSimulation.myCs.Settings
{
    public static class Defaults
    {
        public const int NutritionalValue = 50;
        public const int PoisonDamage = 50;
        public const int SeedPeriod = 5;
        public const int PlantTeenagePeriod = 20;
        public const int PlantDieAge = 60;
        public const int PlantRotAge = 100;
        public const int ReproduceChance = 20;

        public const int FruitLivePeriod = 20;
        public const int FruitRotAge = 30;

        public const int AnimalTeenagePeriod = 2;
        public const int AnimalDiedAge = 700;
        public const int AnimalRotAge = 750;
        public const int AnimalEggPeriod = 20;
        
        public const int AnimalHealth = 100;
        public const int AnimalHealthRegeneration = 5;
        public const int AnimalHealthDestruction = 3;
        public const int AnimalSatiety = 100;
        public const int AnimalSatietyDestruction = 1;
        public const int AnimalVisibleArea = 10;
        public const int AnimalPlantProbability = 30;

        public const int AnimalNormalTicksToMating = 20;
        public const int TicksToStep = 4;
        public const int PregnantPeriod = 0;

        public const int InfoPriorityRotMeat = 1;
        public const int InfoPriorityEgg = 5;
        public const int InfoPriorityPregnant = 6;
        public const int InfoPrioritySleeper = 7;
        public const int InfoPriorityHealth = 10;
        public const int InfoPriorityInventory = 15;
        public const int InfoPriorityEater = 20;
        public const int InfoPriorityAge = 30;
        public const int InfoPriorityVisibility = 40;
        public const int InfoPriorityMoving = 50;
        public const int InfoPriorityMating = 60;
        public const int InfoPriorityPetsOwner = 70;
        public const int InfoPriorityPet = 70;
        public const int InfoPriorityBehaviourChanger = 80;
        public const int InfoPriorityInstrumentsOwner = 110;
        public const int InfoPriorityResourceKeeper = 120;
        public const int InfoPriorityWarehouseOwner = 120;
        public const int InfoPriorityBuilder = 130;
        public const int InfoPriorityCitizen = 140;


        public const int BehaviourWait = -1;
        public const int BehaviourHaveNotPriority = 0;
        public const int BehaviourSearching = 10;
        public const int BehaviourUneatableSearching = 20;
        public const int BehaviourPetHasPresent = 21;
        public const int BehaviourPetOwnerThereArePresents = 22;
        public const int BehaviourResourcesSearchingTriggered = 33;
        public const int BehaviourWarehouseTakeOrPut = 35;
        public const int BehaviourPetOwnerPetHungry = 40;
        public const int BehaviourItIsTimeToMating = 45;
        public const int BehaviourAnimalMating = 50;
        public const int BehaviourPartnerIsHungry = 51;
        public const int BehaviourPetOwnerPetVeryHungry = 60;
        public const int BehaviourPartnerIsVeryHungry = 70;
        public const int BehaviourHungry = 100;
        public const int BehaviourPetHungry = 110;
        public const int BehaviourVeryHungry = 200;
        public const int BehaviourVeryHungryAndPet = 210;
        public const int BehaviourWarehouseTakeMeal = 211;
        public const int BehaviourBuilder = 250;
        public const int BehaviourHumanPregnant = 260;

        public const int InstrumentsMax = 5;
        public const int VillageStartHousesCount = 3;
        public const int CellSearchRadius = 100;
    }
}