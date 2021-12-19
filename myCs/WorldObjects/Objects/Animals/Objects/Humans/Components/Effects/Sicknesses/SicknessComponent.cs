using System.Linq;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components.Effects.Sicknesses
{
    public abstract class SicknessComponent : EffectComponent, IHaveInformation
    {
        private readonly Pill[] _canBeDecreasedByList;
        private readonly Pill[] _canBeTreatedByList;

        protected SicknessComponent(WorldObject owner, int period, Pill[] canBeTreated, Pill[] canBeDecreased) 
            : base(owner, period)
        {
            _canBeTreatedByList = canBeTreated;
            _canBeDecreasedByList = canBeDecreased;
        }

        public void Heal(Pill pill)
        {
            if (_canBeTreatedByList != null && _canBeTreatedByList.Contains(pill))
                Destroy();
            else if (_canBeDecreasedByList != null && _canBeDecreasedByList.Contains(pill))
                timer /= 2;
        }

        public abstract SicknessComponent Clone(WorldObject worldObject);
        
        public int GetInformationPriority()
        {
            return Defaults.InfoPrioritySickness;
        }

        public override string ToString()
        {
            var name = this.GetType().Name;
            return "Ill: " + name.Substring(0, name.Length - 9);
        }
    }
}