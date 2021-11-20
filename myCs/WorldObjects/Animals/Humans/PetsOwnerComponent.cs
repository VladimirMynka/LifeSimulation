using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class PetsOwnerComponent : WorldObjectComponent, IHaveInformation
    {
        private readonly List<PetComponent> _pets;
        private PetComponent _targetPet;
        private VisibilityComponent _visibility;
        private InventoryComponent _inventory;

        public PetsOwnerComponent(WorldObject owner) : base(owner)
        {
            _pets = new List<PetComponent>();
        }

        public override void Start()
        {
            base.Start();
            _visibility = GetComponent<VisibilityComponent>();
            _inventory = GetComponent<InventoryComponent>();
        }

        public override void Update()
        {
            base.Update();
            if (CheckWereDestroyed(_targetPet))
            {
                _targetPet = null;
                SearchPet();
            }
            
            TryTame();
        }

        private void TryTame()
        {
            if (!CheckItIsTamable(_targetPet))
            {
                _targetPet = null;
                return;
            }

            if (CheckPetHere())
                Tame();
        }

        private bool Tame()
        {
            var someEat = _inventory.Remove(20, _targetPet.GetMealType());
            
            if (someEat < 20)
            {
                _targetPet.PetOwner = null;
                _inventory.Add(someEat, _targetPet.GetMealType());
                _targetPet = null;
                return false;
            }
            
            _targetPet.AddSatiety(20);
            AddPet(_targetPet);
            _targetPet = null;
            return true;
        }

        private void SearchPet()
        {
            SetTargetPet(_visibility.Search<PetComponent>(CheckItIsTamable));
        }

        private bool CheckItIsTamable(PetComponent pet)
        {
            return !CheckWereDestroyed(pet) 
                   && pet.PetOwner == null 
                   && _inventory.CheckHave(20, pet.GetMealType());
        }

        private bool CheckPetHere()
        {
            return !CheckWereDestroyed(_targetPet) &&
                Direction.CheckEqual(WorldObject.Cell.Coords, _targetPet.WorldObject.Cell.Coords);
        }

        private void SetTargetPet(PetComponent pet)
        {
            _targetPet = pet;
            pet.PetOwner = this;
        }

        private void AddPet(PetComponent pet)
        {
            _pets.Add(pet);
            pet.PetOwner = this;
        }

        public void GetSignal(PetComponent pet, bool isVeryHungry = false)
        {
            if (isVeryHungry && !TargetPetIsMyAndVeryHungry() || !isVeryHungry && !TargetPetIsMyAndHungry())
                _targetPet = pet;
        }

        private bool TargetPetIsMyAndHungry()
        {
            return !CheckWereDestroyed(_targetPet) &&
                   _targetPet.PetOwner == this &&
                   _targetPet.IsHungry();
        }

        private bool TargetPetIsMyAndVeryHungry()
        {
            return !CheckWereDestroyed(_targetPet) &&
                   _targetPet.PetOwner == this &&
                   _targetPet.IsVeryHungry();
        }

        public void GetPresent(int quantity, MealType mealType)
        {
            _inventory.Add(quantity, mealType);
        }

        public int GetPriority()
        {
            return CheckWereDestroyed(_targetPet) ? 0
                : _targetPet.IsVeryHungry() ? 6
                : _targetPet.IsHungry() ? 4
                : _targetPet.HasPresent() ? 2
                : 0;


        }
        
        public WorldObject GetTarget()
        {
            return _targetPet == null 
                ? null 
                : _targetPet.WorldObject;
        }

        public string GetInformation()
        {
            var info = 
                _pets.Aggregate("Pets: ", 
                    (current, pet) => 
                        current + ("\non " + InformationComponent.GetInfoAboutCoords(pet)));
            info += "Target pet: on " + InformationComponent.GetInfoAboutCoords(_targetPet);
            return info;
        }
    }
}