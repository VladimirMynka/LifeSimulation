using System.Collections.Generic;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObject
    {
        public Cell Cell;
        protected internal World.World world;
        protected List<WorldObjectComponent> components;
        private readonly List<WorldObjectComponent> _removingComponents;
        private readonly List<WorldObjectComponent> _addingComponents;

        public bool evenCycle;
        private bool _isDestroyed = false;

        protected WorldObject(Cell keeper)
        {
            Cell = keeper;
            world = keeper.World;
            evenCycle = false;
            components = new List<WorldObjectComponent>();
            _removingComponents = new List<WorldObjectComponent>();
            _addingComponents = new List<WorldObjectComponent>();

            Cell.AddObject(this);
        }
        
        public void Start()
        {
            foreach (var component in components)
            {
                component.Start();
            }
        }

        public void Update()
        {
            evenCycle = !evenCycle;
            foreach (var component in components)
            {
                component.Update();
                if (_isDestroyed) return;
            }

            foreach (var component in _removingComponents)
            {
                components.Remove(component);
            }

            foreach (var component in _addingComponents)
            {
                components.Add(component);
                component.Start();
            }
        }

        public void Destroy()
        {
            _isDestroyed = true;
            foreach (var component in components)
            {
                component.Destroy();
            }
            Cell.RemoveObject(this);
            Cell = null;
        }

        public bool DestroyComponent<T>() where T : WorldObjectComponent
        {
            var destroyingComponent = GetComponent<T>();
            if (destroyingComponent == null) 
                return false;
            destroyingComponent.Destroy();
            return true;
        }

        public void AddComponent<T>(T component) where T : WorldObjectComponent
        {
            _addingComponents.Add(component);
        }

        public void RemoveComponent<T>(T component) where T : WorldObjectComponent
        {
            _removingComponents.Add(component);
        }
        
        public T GetComponent<T>() where T : WorldObjectComponent
        {
            foreach (var component in components)
            {
                if (component is T) 
                    return component as T;
            }
            return null;
        }
    }
}