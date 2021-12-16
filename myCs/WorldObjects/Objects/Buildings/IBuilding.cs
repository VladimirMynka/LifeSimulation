﻿using System;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public interface IBuilding<out T> where T : Resource
    {
        IInventory<Resource> TryBuildNextStage(InventoryComponent<Resource> builderInventory);
        
        Resource GetNeedSource(InventoryComponent<Resource> builderInventory);

        WorldObject GetWorldObject();

        Type KeepResourceOfType();

    }
}