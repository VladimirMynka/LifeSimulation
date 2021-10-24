namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObjectComponent
    {
        protected WorldObject worldObject;
        protected readonly World.World world;

        protected WorldObjectComponent(WorldObject owner)
        {
            worldObject = owner;
            world = owner.world;
        }

        public T GetComponent<T>() where T : WorldObjectComponent
        {
            return worldObject.GetComponent<T>();
        }

        public void Destroy()
        {
            OnDestroy();
            worldObject.RemoveComponent(this);
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