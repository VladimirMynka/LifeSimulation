using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.UneatableResources;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;

namespace LifeSimulation.myCs.WorldObjects.Objects.Buildings
{
    public class BuildingComponent<T> : WorldObjectComponent, IBuilding<T> where T : Resource
    {
        private readonly Image[] _images;
        private int _stage;
        private readonly int _buildingTypeNumber;
        private DrawableComponent _drawableComponent;

        private static readonly Resource[][][] Resources;
        
        public BuildingComponent(WorldObject owner, int buildingTypeNumber, Image[] images) : base(owner)
        {
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

        public WorldObject GetWorldObject()
        {
            return WorldObject;
        }

        private IInventory<T> ToLastStage()
        {
            var inventory = new InventoryComponent<T>(WorldObject, 250);
            WorldObject.AddComponent(inventory);
            Destroy();
            return inventory;
        }
    }
}