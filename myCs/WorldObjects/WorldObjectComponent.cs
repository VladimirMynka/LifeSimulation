namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObjectComponent
    {
        public WorldObject WorldObject;
        protected readonly World.World world;

        protected WorldObjectComponent(WorldObject owner)
        {
            WorldObject = owner;
            world = owner.world;
        }

        public T GetComponent<T>() where T : WorldObjectComponent
        {
            return WorldObject.GetComponent<T>();
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