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

        public const int AnimalTeenagePeriod = 5;
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
        
        public const int BehaviourHaveNotPriority = 0;
        public const int BehaviourSearching = 1;
        public const int BehaviourInstrumentsTriggered = 2;
        public const int BehaviourPetHasPresent = 2;
        public const int BehaviourPetOwnerThereArePresents = 2;
        public const int BehaviourItIsTimeToMating = 3;
        public const int BehaviourPetOwnerPetHungry = 4;
        public const int BehaviourAnimalMating = 5;
        public const int BehaviourPartnerIsHungry = 5;
        public const int BehaviourPetOwnerPetVeryHungry = 6;
        public const int BehaviourPartnerIsVeryHungry = 7;
        public const int BehaviourHungry = 10;
        public const int BehaviourPetHungry = 11;
        public const int BehaviourVeryHungry = 20;
        public const int BehaviourPetVeryHungry = 21;
    }
}