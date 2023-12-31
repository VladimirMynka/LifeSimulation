﻿using System;
using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Information;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings.Components
{
    public class BuildingComponent<T> : WorldObjectComponent, IHaveInformation, IBuilding<T> where T : Resource
    {
        public Village Village;
        private readonly Image[] _images;
        private int _stage;
        private readonly int _buildingTypeNumber;
        private DrawableComponent _drawableComponent;
        private int _size;

        private static readonly Resource[][][] Resources;
        
        public BuildingComponent(WorldObject worldObject, 
            int buildingTypeNumber, 
            Image[] images,
            int size = 250,
            Village village = null) 
            : base(worldObject)
        {
            _stage = 0;
            _buildingTypeNumber = buildingTypeNumber;
            _images = images;
            _size = size;
            Village = village;
        }

        static BuildingComponent()
        {
            Resources = new Resource[][][]{
                new Resource[][]{
                    new Resource[]{new WoodResource(20)},
                    new Resource[]{new WoodResource(20)}
                },
                new Resource[][]{
                    new Resource[]{new WoodResource(20)},
                    new Resource[]{new WoodResource(20)},
                    new Resource[]{new WoodResource(20)}
                },
                new Resource[][]{
                    new Resource[]{new WoodResource(20)},
                    new Resource[]{new WoodResource(20)},
                    new Resource[]{new WoodResource(20)}
                }
            };
        }

        public override void Start()
        {
            base.Start();
            _drawableComponent = GetComponent<DrawableComponent>();
        }

        public IInventory<Resource> TryBuildNextStage(InventoryComponent<Resource> builderInventory)
        {
            if (_stage == Resources[_buildingTypeNumber].Length - 1)
                return null;
            if (!builderInventory.RemoveIfHave(Resources[_buildingTypeNumber][_stage])) 
                return null;
            _stage++;
            _drawableComponent.Image = _images[_stage];
            return _stage == Resources[_buildingTypeNumber].Length - 1 
                ? ToLastStage() 
                : null;
        }
        
        public Resource GetNeedSource(InventoryComponent<Resource> builderInventory)
        {
            return builderInventory.FirstOrDefaultLackCounts(Resources[_buildingTypeNumber][_stage]);
        }

        public WorldObject GetWorldObject()
        {
            return WorldObject;
        }

        public Type KeepResourceOfType()
        {
            return typeof(T);
        }

        public string GetTypeAsString()
        {
            var name = typeof(T).Name;
            return name.Substring(0, name.Length - 8);
        }

        public void SetVillage(Village citizenComponentVillage)
        {
            Village = citizenComponentVillage;
        }

        public Village GetVillage()
        {
            return Village;
        }

        public bool IsEnded()
        {
            return _stage == Resources[_buildingTypeNumber].Length - 1;
        }

        private IInventory<T> ToLastStage()
        {
            var inventory = new InventoryComponent<T>(WorldObject, _size);
            WorldObject.AddComponent(inventory);
            return inventory;
        }

        public override string ToString()
        {
            return typeof(T).Name + "Warehouse " + _stage +
                   "\nVillage: " + (Village == null ? "none" : '\n' + Village.ToString());
        }

        public int GetInformationPriority()
        {
            return Defaults.InfoPriorityAge;
        }
    }
}