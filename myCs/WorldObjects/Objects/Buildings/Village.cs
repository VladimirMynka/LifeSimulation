using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components;
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
        private readonly List<IInventory<Resource>> _warehouses;
        private readonly List<CitizenComponent> _citizens;
        private PresidentComponent _president;
        public readonly string Name;
        private int _amountOfHouses = 0;

        public Village()
        {
            _buildings = new List<IBuilding<Resource>>();
            _warehouses = new List<IInventory<Resource>>();
            _citizens = new List<CitizenComponent>();
            Name = Names[World.Random.Next(Names.Length)];
        }
        
        public void AddNewCitizen(CitizenComponent citizen)
        {
            if (!_citizens.Contains(citizen))
                _citizens.Add(citizen);
            if (_citizens.Count == 1)
                _president = citizen.BecomePresident(_citizens);
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
        }

        public PresidentComponent GetPresident()
        {
            return _president;
        }

        public void StartElection(int probate)
        {
            if (_citizens.Count == 0)
                return;
            var candidates = _citizens.Where(citizen => citizen.CanParticipate()).ToArray();
            if (candidates.Length == 0)
            {
                _president = _citizens[0].BecomePresident(_citizens);
                return;
            }

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

        private static int GetIndexOfMax(IReadOnlyList<int> array)
        {
            int max = array[0];
            int index = 0;
            for (var i = 1; i < array.Count; i++)
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

        public bool IsActive()
        {
            return _amountOfHouses >= HousesCountForCreateVillage;
        }

        public override string ToString()
        {
            var info = "Name: " + Name;
            info += "\nCitizens: " + _citizens.Aggregate("", (common, current)
                => common + '\n' + current.GetGenderString() + ' ' + current.GetAge() + " on " +
                   InformationComponent.GetInfoAboutCoords(current) + 
                   "\n   Role: " + current.GetRoleString());
            info += "\nHouses: " + _buildings.Aggregate("", (common, current)
                => common + (current.GetWorldObject() is House
                    ? "\non " + InformationComponent.GetInfoAboutCoords(current.GetWorldObject())
                    : ""));
            info += "\nWarehouses: " + _buildings.Aggregate("", (common, current) 
                => common + (current.GetWorldObject() is House
                    ? ""
                    : '\n' + current.GetTypeAsString() + " on " + 
                      InformationComponent.GetInfoAboutCoords(current.GetWorldObject())));
            return info;
        }

        public List<IInventory<Resource>> GetWarehouses()
        {
            return _warehouses;
        }
    }
}