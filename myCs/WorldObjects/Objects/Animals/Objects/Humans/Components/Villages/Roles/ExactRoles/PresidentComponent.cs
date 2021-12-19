using System;
using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects.Objects.Buildings;
using LifeSimulation.myCs.WorldStructure;
using LifeSimulation.myCs.WorldStructure.Weather;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Villages.Roles.ExactRoles
{
    public class PresidentComponent : ProfessionalComponent
    {
        private readonly Village _village;
        private readonly List<CitizenComponent> _citizens;
        
        
        public PresidentComponent(WorldObject owner, Village village, List<CitizenComponent> citizens) 
            : base(owner, InfinityPeriod)
        {
            _village = village;
            _citizens = citizens;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _village.RemoveCitizen(citizenComponent);
            _village.StartElection(World.Random.Next(_citizens.Count));
        }

        protected override void ConfigureBehaviour()
        {
            ConfigureEaterBehaviour(20, 10, 0);
            ConfigureMatingBehaviour(5, 3, 15);
            ConfigurePetsOwnerBehaviour(8, 2, 6, 0);
            ConfigureWarehousesOwnerBehaviour(50, 4);
        }

        public ProfessionalComponent AskNewJob(CitizenComponent requester, Type lastJobType)
        {
            var resting = TryCreateResting(requester, lastJobType);
            return resting != null 
                ? resting 
                : CreateNotResting(requester, lastJobType);
        }

        private static ProfessionalComponent TryCreateResting(CitizenComponent requester, Type lastJobType)
        {
            var requesterPartner = requester.GetPartner();
            int restingChance = (World.Random.Next(0, 1) + (
                        requesterPartner == null 
                            ? 0 
                            : requesterPartner.GetRoleType() == typeof(RestingComponent) ? 1 : 0)
                ) * (lastJobType == typeof(RestingComponent) ? 0 : 1);
            
            return restingChance != 0 
                ? CreateComponentForRole(ProfessionalRole.Resting, requester.WorldObject, 50) 
                : null;
        }

        private ProfessionalComponent CreateNotResting(CitizenComponent requester, Type lastJobType)
        {
            bool isMale = requester.IsMale();

            var accessArray = GetGenderList(requester.IsMale());
            var noSeasonArray = GetNoSeasonList(world.Weather.GetSeason() == Season.Winter);

            var currentDistribution = GetCurrentDistribution(requester, accessArray);
            ConfigureDistributionByWeather(currentDistribution, accessArray, noSeasonArray);
            ConfigureDistributionByLastJob(currentDistribution, accessArray, lastJobType);

            return CreateComponentForRole(
                accessArray[GetIndexOfMin(currentDistribution)],
                requester.WorldObject,
                100);
        }

        private ProfessionalRole[] GetGenderList(bool isMale)
        {
            return isMale 
                ? MenRoles 
                : WomenRoles;
        }

        private ProfessionalRole[] GetNoSeasonList(bool isWinter)
        {
            return !isWinter
                ? OnlyWinterRoles
                : OnlySummerRoles;
        }

        private int[] GetCurrentDistribution(CitizenComponent requester, ProfessionalRole[] accessArray)
        {
            var currentDistribution = new int[accessArray.Length];

            foreach (var citizen in _citizens)
            {
                if (citizen == requester) continue;
                int index = Array.IndexOf(accessArray, GetEnumByType(citizen.GetRoleType()));
                if (index != -1)
                    currentDistribution[index]++;
            }

            return currentDistribution;
        }

        private void ConfigureDistributionByWeather(
            int[] currentDistribution,
            ProfessionalRole[] accessArray,
            ProfessionalRole[] noSeasonArray
        )
        {
            foreach (var role in noSeasonArray)
            {
                int seasonIndex = Array.IndexOf(accessArray, role);
                if (seasonIndex != -1)
                    currentDistribution[seasonIndex] += _citizens.Count;
            }
        }
        
        private static void ConfigureDistributionByLastJob(
            int[] currentDistribution,
            ProfessionalRole[] accessArray,
            Type lastJobType
        )
        {
            int lastJobIndex = Array.IndexOf(accessArray, lastJobType);
            if (lastJobIndex != -1)
                currentDistribution[lastJobIndex] += 2;
        }

        private static int GetIndexOfMin(IReadOnlyList<int> array)
        {
            int index = 0;
            int min = array[0];
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] >= min) 
                    continue;
                index = i;
                min = array[i];
            }

            return index;
        }

        public List<ProfessionalComponent> GetRolesOfType(Type roleType)
        {
            var list = new List<ProfessionalComponent>();
            foreach (var citizen in _citizens)
            {
                if (citizen.GetRole().GetType() == roleType)
                    list.Add(citizen.GetRole());
            }

            return list;
        }
    }
}