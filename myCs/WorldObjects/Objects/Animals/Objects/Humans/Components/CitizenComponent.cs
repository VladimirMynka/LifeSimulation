using System.Collections.Generic;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class CitizenComponent : WorldObjectComponent, IHaveInformation
    {
        public Village Village;
        private CitizenRole _role;
        private HumanAgeComponent _ageComponent;
        private MatingComponent _matingComponent;
        private WarehousesOwnerComponent _warehousesOwnerComponent;
        private int _startDay;

        public CitizenComponent(WorldObject owner, Village village = null) 
            : base(owner)
        {
            Village = village;
        }

        public override void Start()
        {
            base.Start();
            _ageComponent = GetComponent<HumanAgeComponent>();
            _matingComponent = GetComponent<MatingComponent>();
            _warehousesOwnerComponent = GetComponent<WarehousesOwnerComponent>();
            _startDay = _ageComponent.GetAge();
        }

        public void ToActiveMod()
        {
            throw new System.NotImplementedException();
        }

        public bool CanParticipate()
        {
            return GetAge() > 100;
        }

        public bool CanVote()
        {
            return GetAge() > 50;
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
            if (candidate._role == _role)
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

        public int GetAge()
        {
            return _ageComponent.GetAge();
        }

        private bool LiveIn(House house)
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
            WorldObject.AddComponent(presidentComponent);
            return presidentComponent;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Village.RemoveCitizen(this);
        }

        public int GetRiches()
        {
            throw new System.NotImplementedException();
        }

        public int GetTimeOfResidence()
        {
            return GetAge() - _startDay;
        }

        public bool IsVeryOld()
        {
            return GetAge() < 600;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityCitizen;
        }

        public override string ToString()
        {
            return "Village: " + Village.Name;
        }
    }
}