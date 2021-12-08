using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class BuildingComponent<T> : WorldObjectComponent, IBuilding<T> where T : Resource
    {
        private readonly List<WarehousesOwnerComponent> _owners;
        private readonly Image[] _images;
        private int _stage;
        private readonly int _buildingTypeNumber;
        private DrawableComponent _drawableComponent;

        private static readonly Resource[][][] Resources;
        
        public BuildingComponent(WorldObject owner, int buildingTypeNumber, Image[] images) : base(owner)
        {
            _owners = new List<WarehousesOwnerComponent>();
            _stage = 0;
            _buildingTypeNumber = buildingTypeNumber;
            _images = images;
        }

        static BuildingComponent()
        {
            Resources = new Resource[][][]{
                new Resource[][]{
                    new Resource[]{new CompostResource(20)},
                    new Resource[]{new WoodResource(20), new IronResource(10)}
                },
                new Resource[][]{
                    new Resource[]{new CompostResource(20)},
                    new Resource[]{new CompostResource(10), new WoodResource(10)},
                    new Resource[]{new IronResource(15), new GoldResource(5)}
                }
            };
        }

        public override void Start()
        {
            base.Start();
            _drawableComponent = GetComponent<DrawableComponent>();
        }

        public override void Update()
        {
            base.Update();
            CheckAndRemoveOwners();
        }

        public void TryBuildNextStage(InventoryComponent<Resource> builderInventory)
        {
            if (_stage == Resources[_buildingTypeNumber].Length - 1)
                return;
            if (!builderInventory.Remove(Resources[_buildingTypeNumber][_stage])) 
                return;
            _stage++;
            _drawableComponent.Image = _images[_stage];
            if (_stage == Resources[_buildingTypeNumber].Length - 1)
                ToLastStage();
        }

        public WorldObject GetWorldObject()
        {
            return WorldObject;
        }

        private void ToLastStage()
        {
            WorldObject.AddComponent(new InventoryComponent<T>(WorldObject, 250));
        }

        private void CheckAndRemoveOwners()
        {
            for (int i = _owners.Count; i >= 0; i--)
            {
                if (CheckWereDestroyed(_owners[i]))
                    _owners.Remove(_owners[i]);
            }
        }
    }
}