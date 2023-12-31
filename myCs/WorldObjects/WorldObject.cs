﻿using System.Collections.Generic;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects
{
    public abstract class WorldObject
    {
        public Cell Cell;
        protected internal World world;
        protected List<WorldObjectComponent> components;
        private readonly List<WorldObjectComponent> _removingComponents;
        private readonly List<WorldObjectComponent> _addingComponents;

        public bool evenCycle;
        public bool IsDestroyed = false;

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
                if (IsDestroyed) return;
                component.Update();
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
            
            _addingComponents.Clear();
            _removingComponents.Clear();
        }

        public void Destroy()
        {
            IsDestroyed = true;
            foreach (var component in components)
            {
                component.OnDestroy();
            }
            Cell.RemoveObject(this);
            Cell = null;
        }
        
        public static bool CheckWereDestroyed(WorldObject worldObject)
        {
            return worldObject == null ||
                   worldObject.Cell == null;
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
        
        public T GetComponent<T>() where T : class
        {
            foreach (var component in components)
            {
                if (component is T) 
                    return component as T;
            }
            return null;
        }
        
        public List<T> GetComponents<T>() where T : class
        {
            var componentsOfType = new List<T>();
            foreach (var component in components)
            {
                if (component is T) 
                    componentsOfType.Add(component as T);
            }
            return componentsOfType;
        }

        public static int GetSqrLengthBetween(WorldObject object1, WorldObject object2)
        {
            if (CheckWereDestroyed(object1) || CheckWereDestroyed(object2))
                return -1;
            return Direction.SqrLength(object1.Cell.Coords, object2.Cell.Coords);
        }
        
        public static bool CheckOnOneCell(WorldObject object1, WorldObject object2)
        {
            if (CheckWereDestroyed(object1) || CheckWereDestroyed(object2))
                return false;
            return Direction.CheckEqual(object1.Cell.Coords, object2.Cell.Coords);
        }
    }
}