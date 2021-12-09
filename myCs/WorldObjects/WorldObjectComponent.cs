using System.Collections.Generic;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObjectComponent
    {
        public WorldObject WorldObject;
        protected readonly World world;

        protected WorldObjectComponent(WorldObject owner)
        {
            WorldObject = owner;
            world = owner.world;
        }

        public static bool CheckWereDestroyed(WorldObjectComponent component)
        {
            return component == null ||
                   component.WorldObject == null ||
                   component.WorldObject.Cell == null;
        }

        public static bool CheckWereDestroyed(WorldObject worldObject)
        {
            return WorldObject.CheckWereDestroyed(worldObject);
        }

        public T GetComponent<T>() where T : WorldObjectComponent
        {
            return WorldObject.GetComponent<T>();
        }
        
        public List<T> GetComponents<T>() where T : class
        {
            return WorldObject.GetComponents<T>();
        }

        public void Destroy()
        {
            OnDestroy();
            if (WorldObject != null)
                WorldObject.RemoveComponent(this);
            WorldObject = null;
        }
        
        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void OnDestroy()
        {
            
        }

        protected static int GetSqrLengthBetween(WorldObjectComponent component1, WorldObjectComponent component2)
        {
            return WorldObject.GetSqrLengthBetween(component1.WorldObject, component2.WorldObject);
        }

        protected static int GetSqrLengthBetween(WorldObjectComponent component, WorldObject worldObject)
        {
            return WorldObject.GetSqrLengthBetween(component.WorldObject, worldObject);
        }

        protected int GetSqrLengthWith(WorldObjectComponent other)
        {
            return GetSqrLengthBetween(this, other);
        }

        protected int GetSqrLengthWith(WorldObject other)
        {
            return GetSqrLengthBetween(this, other);
        }

        protected static bool CheckOnOneCell(WorldObjectComponent component1, WorldObjectComponent component2)
        {
            return WorldObject.CheckOnOneCell(component1.WorldObject, component2.WorldObject);
        }

        protected static bool CheckOnOneCell(WorldObjectComponent component, WorldObject worldObject)
        {
            return WorldObject.CheckOnOneCell(component.WorldObject, worldObject);
        }

        protected bool OnOneCellWith(WorldObjectComponent other)
        {
            return CheckOnOneCell(this, other);
        }
        
        protected bool OnOneCellWith(WorldObject other)
        {
            return CheckOnOneCell(this, other);
        }
    }
}