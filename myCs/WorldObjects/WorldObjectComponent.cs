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
            if (CheckWereDestroyed(component1) || CheckWereDestroyed(component2))
                return -1;
            return WorldObject.GetSqrLengthBetween(component1.WorldObject, component2.WorldObject);
        }

        protected static int GetSqrLengthBetween(WorldObjectComponent component, WorldObject worldObject)
        {
            if (CheckWereDestroyed(component) || CheckWereDestroyed(worldObject))
                return -1;
            return WorldObject.GetSqrLengthBetween(component.WorldObject, worldObject);
        }

        protected int GetSqrLengthWith(WorldObjectComponent other)
        {
            if (CheckWereDestroyed(other))
                return -1;
            return GetSqrLengthBetween(this, other);
        }

        protected int GetSqrLengthWith(WorldObject other)
        {
            if (CheckWereDestroyed(other))
                return -1;
            return GetSqrLengthBetween(this, other);
        }

        protected static bool CheckOnOneCell(WorldObjectComponent component1, WorldObjectComponent component2)
        {
            if (CheckWereDestroyed(component1) || CheckWereDestroyed(component2))
                return false;
            return WorldObject.CheckOnOneCell(component1.WorldObject, component2.WorldObject);
        }

        protected static bool CheckOnOneCell(WorldObjectComponent component, WorldObject worldObject)
        {
            if (CheckWereDestroyed(component) || CheckWereDestroyed(worldObject))
                return false;
            return WorldObject.CheckOnOneCell(component.WorldObject, worldObject);
        }

        protected bool OnOneCellWith(WorldObjectComponent other)
        {
            return !CheckWereDestroyed(other) && CheckOnOneCell(this, other);
        }
        
        protected bool OnOneCellWith(WorldObject other)
        {
            return !CheckWereDestroyed(other) && CheckOnOneCell(this, other);
        }
    }
}