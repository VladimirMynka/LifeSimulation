using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Mating
{
    public abstract class PregnantComponent : WorldObjectComponent, IHaveInformation
    {
        private int _ticksToBirthday;

        protected PregnantComponent(WorldObject owner, 
            int ticksToBirthday = Defaults.PregnantPeriod) 
            : base(owner)
        {
            _ticksToBirthday = ticksToBirthday;
        }

        public override void Update()
        {
            base.Update();
            if (_ticksToBirthday > 0)
                _ticksToBirthday--;
            
            if (IsReadyToGiveBirth())
                Destroy();
        }

        protected virtual bool IsReadyToGiveBirth()
        {
            return _ticksToBirthday <= 0;
        }

        public override string ToString()
        {
            var info = "";
            info += "Ticks to birthday: " + _ticksToBirthday;
            return info;
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityPregnant;
        }
    }
}