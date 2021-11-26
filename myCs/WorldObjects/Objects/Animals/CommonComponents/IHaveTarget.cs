﻿namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents
{
    public interface IHaveTarget
    {
        int GetPriorityInBehaviour();
        WorldObject GetTarget();
    }
}