using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner
{
    public class PetsOwnerComponent : WorldObjectComponent, IHaveInformation, IHaveTarget
    {
        private readonly List<PetComponent> _pets;
        private PetComponent _targetPet;
        private VisibilityComponent _visibility;
        private InventoryComponent<Resource> _inventory;
        private EaterComponent _eaterComponent;

        public PetsOwnerComponent(WorldObject owner) : base(owner)
        {
            _pets = new List<PetComponent>();
        }

        public override void Start()
        {
            base.Start();
            _visibility = GetComponent<VisibilityComponent>();
            _inventory = GetComponent<InventoryComponent<Resource>>();
            _eaterComponent = GetComponent<EaterComponent>();
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
            if (CheckWereDestroyed(_targetPet))
            {
                _targetPet = null;
                return;
            }

            if (CheckPetHere())
                Tame();
        }

        private void Tame()
        {
            if (_pets.Contains(_targetPet))
                return;
            var result = _inventory.RemoveIfHave(_targetPet.GetMealType(20));
            
            if (!result)
                return;
            
            _targetPet.AddSatiety(20);
            AddPet(_targetPet);
            _targetPet = null;
        }

        private void SearchPet()
        {
            SetTargetPet(_visibility.Search<PetComponent>(CheckItIsTamable));
        }

        private bool CheckItIsTamable(PetComponent pet)
        {
            return !CheckWereDestroyed(pet) 
                   && pet.GetOwner() == null 
                   && _inventory.CheckHave(pet.GetMealType());
        }

        private bool CheckPetHere()
        {
            return !CheckWereDestroyed(_targetPet) &&
                Direction.CheckEqual(WorldObject.Cell.Coords, _targetPet.WorldObject.Cell.Coords);
        }

        private void SetTargetPet(PetComponent pet)
        {
            if (CheckWereDestroyed(pet))
                return;
            _targetPet = pet;
            pet.SetOwner(this);
            _eaterComponent.Exclude(pet.WorldObject);
        }

        private void AddPet(PetComponent pet)
        {
            _pets.Add(pet);
            pet.SetOwner(this);
        }

        public void GetSignal(PetComponent pet, bool isVeryHungry = false)
        {
            if (isVeryHungry && !TargetPetIsMyAndVeryHungry() || !isVeryHungry && !TargetPetIsMyAndHungry())
                _targetPet = pet;
        }

        public bool HasMealFor(EatableResource resource)
        {
            return _inventory.CheckHave(resource);
        }

        private bool TargetPetIsMyAndHungry()
        {
            return !CheckWereDestroyed(_targetPet) &&
                   _targetPet.GetOwner() == this &&
                   _targetPet.IsHungry();
        }

        private bool TargetPetIsMyAndVeryHungry()
        {
            return !CheckWereDestroyed(_targetPet) &&
                   _targetPet.GetOwner() == this &&
                   _targetPet.IsVeryHungry();
        }

        public void GetPresent(int value, PetEffect effect, EatableResource resource)
        {
            switch (effect)
            {
                case PetEffect.WarmClothes:
                    WorldObject.DestroyComponent<WarmClothesComponent>();
                    WorldObject.AddComponent(new WarmClothesComponent(WorldObject, value));
                    return;
                case PetEffect.Protection:
                    WorldObject.DestroyComponent<ProtectionComponent>();
                    WorldObject.AddComponent(new ProtectionComponent(WorldObject, value));
                    return;
                case PetEffect.AddMeal:
                default:
                    _inventory.Add(resource);
                    return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 6 if pet is very hungry, 4 if pet is hungry, 2 if pet has present, 0 in others
        /// </returns>
        public int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(_targetPet) ? 0
                : _targetPet.IsVeryHungry() 
                  && _inventory.CheckHave(_targetPet.GetMealType(50)) ? 6
                : _targetPet.IsHungry() 
                  && _inventory.CheckHave(_targetPet.GetMealType(20)) ? 4
                : _targetPet.HasPresent() ? 2
                : 0;
        }
        
        public WorldObject GetTarget()
        {
            return CheckWereDestroyed(_targetPet) 
                ? null 
                : _targetPet.WorldObject;
        }

        public override string ToString()
        {
            var info = _pets.Where(
                pet => !CheckWereDestroyed(pet)
                ).Aggregate(
                "Pets: ", (current, pet) => current + 
                                            ("\n" 
                                             + pet.GetCreatureType()
                                             + " on " 
                                             + InformationComponent.GetInfoAboutCoords(pet)));

            if (!CheckWereDestroyed(_targetPet))
                info += "\nTarget pet: " + _targetPet.GetCreatureType() + " on "
                        + InformationComponent.GetInfoAboutCoords(_targetPet);
            return info;
        }

        public int GetInformationPriority()
        {
            return 70;
        }
    }
}