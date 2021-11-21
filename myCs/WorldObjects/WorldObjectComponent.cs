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
        
        public List<WorldObjectComponent> GetComponents<T>()
        {
            return WorldObject.GetComponents<T>();
        }

        public void Destroy()
        {
            OnDestroy();
            WorldObject.RemoveComponent(this);
        }
        
        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            
        }

        protected virtual void OnDestroy()
        {
            
        }
    }
}