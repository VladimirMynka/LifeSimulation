using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class FemaleMatingComponent : MatingComponent
    {
        public MaleMatingComponent Partner;
        
        public FemaleMatingComponent(WorldObject owner, int ticksToMating = Defaults.AnimalNormalTicksToMating) 
            : base(owner, ticksToMating)
        {
            
        }

        public bool IsEaterOfType(MealType type)
        {
            return (type == eaterComponent.MealType);
        }

        public void Mate(MaleMatingComponent partner)
        {
            BecomePregnant();
            ToWaitingStage();
            Partner = null;
        }

        private void BecomePregnant()
        {
            WorldObject.AddComponent(new PregnantComponent(WorldObject));
        }
    }
}