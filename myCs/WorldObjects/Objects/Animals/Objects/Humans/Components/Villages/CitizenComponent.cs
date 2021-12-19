using System;
using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.PetsOwner;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages
{
    public class CitizenComponent : WorldObjectComponent, IHaveInformation
    {
        public Village Village;
        private ProfessionalComponent _professionalComponent;
        private HumanAgeComponent _ageComponent;
        private MatingComponent _matingComponent;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private InventoryComponent<Resource> _inventoryComponent;
        private PetsOwnerComponent _petsOwnerComponent;
        private InstrumentsOwnerComponent _instrumentsOwnerComponent;
        private int _startDay;
        private bool _started;
        private bool _active; 

        public CitizenComponent(WorldObject owner, Village village = null) 
            : base(owner)
        {
            Village = village;
            if (village != null)
                village.AddNewCitizen(this);
        }

        public override void Start()
        {
            base.Start();
            _ageComponent = GetComponent<HumanAgeComponent>();
            _matingComponent = GetComponent<MatingComponent>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            _instrumentsOwnerComponent = GetComponent<InstrumentsOwnerComponent>();
            _petsOwnerComponent = GetComponent<PetsOwnerComponent>();
            _inventoryComponent = GetComponent<InventoryComponent<Resource>>();
            _startDay = _ageComponent.GetAge();
            _started = true;
        }

        public override void Update()
        {
            base.Update();
            if (Village.IsActive() && !_active)
                ToActiveMod();
        }

        public void ToActiveMod()
        {
            if (!_started)
                return;
            if (_professionalComponent == null)
                SetJob(GetPresident().AskNewJob(this, null));
            Expropriate();
            _active = false;
        }

        private void Expropriate()
        {
            var oldList = _warehousesOwnerComponent.GetWarehouses();
            _warehousesOwnerComponent.SetWarehouses(Village.GetWarehouses());
            foreach (var warehouse in oldList)
                _warehousesOwnerComponent.AddWarehouse(warehouse);
        }

        public bool CanParticipate()
        {
            return _started && GetAge() > 100;
        }

        public bool CanVote()
        {
            return _started && GetAge() > 50;
        }

        private PresidentComponent GetPresident()
        {
            return Village.GetPresident();
        }

        public int Vote(CitizenComponent[] candidates, int[] objectiveScore)
        {
            var maxScore = 0;
            var choice = 0;
            for (int i = 0; i < candidates.Length; i++)
            {
                var score = CalculateScore(candidates[i], objectiveScore[i]);
                if (maxScore >= score) 
                    continue;
                maxScore = score;
                choice = i;
            }
            return choice;
        }

        private int CalculateScore(CitizenComponent candidate, int objectiveScore)
        {
            if (candidate == this)
                return 0;
            var score = 0;
            if (candidate.GetRoleType() == GetRoleType())
                score += 2;
            if (candidate.AgeAbout(GetAge()))
                score += 2;
            if (candidate.GenderAs(_matingComponent))
                score += 1;
            if (candidate.LiveIn(_warehousesOwnerComponent.House))
                score += 2;
            score += objectiveScore;
            return score;
        }

        public Type GetRoleType()
        {
            return _professionalComponent == null 
                ? null 
                : _professionalComponent.GetType();
        }

        public int GetAge()
        {
            return _ageComponent == null 
                ? 0 
                : _ageComponent.GetAge();
        }

        private bool LiveIn(IInventory<Resource> house)
        {
            return _warehousesOwnerComponent.House == house;
        }

        private bool GenderAs(MatingComponent other)
        {
            return other.GetType() == _matingComponent.GetType();
        }

        private bool AgeAbout(int otherAge)
        {
            return otherAge - _ageComponent.GetAge() < 100 && 
                   otherAge - _ageComponent.GetAge() > -100;
        }

        public PresidentComponent BecomePresident(List<CitizenComponent> citizens)
        {
            var presidentComponent = new PresidentComponent(WorldObject, Village, citizens);
            SetJob(presidentComponent);
            return presidentComponent;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Village.RemoveCitizen(this);
        }

        public int GetRiches()
        {
            var count = _warehousesOwnerComponent.House == null ? 0 : 
                _warehousesOwnerComponent.House.GetAllCount();
            count += _inventoryComponent.GetAllCount();
            count += _instrumentsOwnerComponent == null 
                ? 0 
                : _instrumentsOwnerComponent.GetInstrumentsCount() * 20;
            count += _petsOwnerComponent == null
                ? 0
                : _petsOwnerComponent.GetPetsCount() * 10;
            return count;
        }

        public int GetTimeOfResidence()
        {
            return GetAge() - _startDay;
        }

        public bool IsVeryOld()
        {
            return GetAge() < 600;
        }
        
        public void DeclareEndOfWork()
        {
            SetJob(GetPresident().AskNewJob(this, _professionalComponent.GetType()));
        }

        public void SetJob(ProfessionalComponent professionalComponent)
        {
            _professionalComponent = professionalComponent;
            WorldObject.AddComponent(professionalComponent);
        }

        public string GetGenderString()
        {
            return _matingComponent is ManComponent ? "Man" : "Woman";
        }

        public bool IsMale()
        {
            return _matingComponent is ManComponent;
        }

        public string GetRoleString()
        {
            if (_professionalComponent == null) 
                return "none";
            var name = _professionalComponent.GetType().Name;
            return name.Substring(0, name.Length - 9);
        }

        public ProfessionalComponent GetRole()
        {
            return _professionalComponent;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityCitizen;
        }

        public override string ToString()
        {
            return "Village: " + Village.Name;
        }

        public CitizenComponent GetPartner()
        {
            return _matingComponent.GetPartner() == null
                ? null
                : _matingComponent.GetPartner().GetComponent<CitizenComponent>();
        }
    }
}