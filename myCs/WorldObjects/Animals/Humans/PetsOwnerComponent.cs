﻿namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class PetsOwnerComponent : WorldObjectComponent
    {
        public PetsOwnerComponent(WorldObject owner) : base(owner)
        {
        }
        
        public int GetPriority()
        {
            throw new System.NotImplementedException();
        }
        
        public WorldObject GetTarget()
        {
            throw new System.NotImplementedException();
        }
    }
}