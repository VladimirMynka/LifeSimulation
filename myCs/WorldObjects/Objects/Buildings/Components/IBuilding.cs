﻿using System;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components
{
    public interface IBuilding<out T> where T : Resource
    {
        IInventory<Resource> TryBuildNextStage(InventoryComponent<Resource> builderInventory);
        
        Resource GetNeedSource(InventoryComponent<Resource> builderInventory);

        WorldObject GetWorldObject();

        Type KeepResourceOfType();

        string GetTypeAsString();

        void SetVillage(Village citizenComponentVillage);

        bool IsEnded();
        
        Village GetVillage();
    }
}