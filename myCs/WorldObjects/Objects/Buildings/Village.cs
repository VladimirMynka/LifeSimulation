using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class Village
    {
        private static readonly string[] Names = new string[]{
            "Mynkberg", "Mynkburg", "Mynkvill",
            "Mynklend", "New Mynka", "Mynk-Lake Village",
            "Mynkopol", "Mynkas", "Mynkin Hills"
        };

        private const int HousesCountForCreateVillage = Defaults.VillageStartHousesCount;

        private readonly List<IBuilding<Resource>> _buildings;
        private readonly List<CitizenComponent> _citizens;
        private PresidentComponent _president;
        public readonly string Name;
        private int _amountOfHouses = 0;

        public Village(CitizenComponent firstCitizen)
        {
            _buildings = new List<IBuilding<Resource>>();
            _citizens = new List<CitizenComponent>();
            firstCitizen.SetVillage(this);
            _president = firstCitizen.BecomePresident(_citizens);
            _citizens.Add(firstCitizen);
            Name = Names[World.Random.Next(Names.Length)];
        }

        public void AddNewCitizen(CitizenComponent citizen)
        {
            if (!_citizens.Contains(citizen))
                _citizens.Add(citizen);
        }

        public void RemoveCitizen(CitizenComponent citizen)
        {
            _citizens.Remove(citizen);
        }

        public void AddNewBuilding(IBuilding<Resource> building)
        {
            if (!_buildings.Contains(building))
                _buildings.Add(building);
            if (!(building.GetWorldObject() is House))
                return;
            _amountOfHouses++;
            if (_amountOfHouses == HousesCountForCreateVillage)
                Start();
        }

        private void Start()
        {
            foreach (var citizenComponent in _citizens)
                citizenComponent.ToActiveMod();
            _president.ToActiveMod();

        }

        public void StartElection(int probate)
        {
            if (_citizens.Count == 0)
                return;
            var candidates = _citizens.Where(citizen => citizen.CanParticipate()).ToArray();
            var objectiveScores = GetObjectiveScores(candidates);
            var votes = new int[candidates.Length];

            var voters = _citizens.Where(citizen => citizen.CanVote());
            foreach (var voter in voters)
            {
                votes[voter.Vote(candidates, objectiveScores)]++;
            }
            if (probate > 0)
                votes[probate % votes.Length] += 2;

            
            _president = candidates[GetIndexOfMax(votes)].BecomePresident(_citizens);
        }

        private static int GetIndexOfMax(int[] array)
        {
            int max = array[0];
            int index = 0;
            for (var i = 1; i < array.Length; i++)
            {
                if (max >= array[i]) 
                    continue;
                max = array[i];
                index = i;
            }

            return index;
        }

        private static int[] GetObjectiveScores(CitizenComponent[] candidates)
        {
            var scores = new int[candidates.Length];
            scores[GetIndexOfMax(candidates, 
                citizenComponent => citizenComponent.GetRiches())] += 2;
            scores[GetIndexOfMax(candidates, 
                citizenComponent => citizenComponent.GetTimeOfResidence())] += 2;
            for (var i = 0; i < candidates.Length; i++)
            {
                if (!candidates[i].IsVeryOld())
                    scores[i]++;
            }
            return scores;
        }

        private delegate int GetCharacteristic(CitizenComponent candidate);

        private static int GetIndexOfMax(CitizenComponent[] candidates,
            GetCharacteristic getCharacteristic)
        {
            int maxValue = getCharacteristic(candidates[0]);
            int index = 0;
            for (var i = 1; i < candidates.Length; i++)
            {
                int value = getCharacteristic(candidates[i]);
                if (maxValue >= value) 
                    continue;
                maxValue = value;
                index = i;
            }

            return index;
        }
        
    }
}