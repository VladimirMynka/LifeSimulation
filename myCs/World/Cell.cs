using System;
using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs.World
{
    public class Cell : IComparable
    {
        public readonly World World;
        public readonly int[] Coords;
        public List<WorldObject> CurrentObjects;
        private readonly List<WorldObject> _removingObjects;
        private readonly List<WorldObject> _addingObjects;

        public readonly Drawer.Drawer Drawer;
        public bool evenCycle;
        private bool _wereUpdated;

        public Cell(World world, Drawer.Drawer drawer, int[] coords)
        {
            World = world;
            Coords = coords;
            CurrentObjects = new List<WorldObject>();
            _removingObjects = new List<WorldObject>();
            _addingObjects = new List<WorldObject>();
            Drawer = drawer;
            evenCycle = false;
            
            drawer.AddCell(this);
        }

        public void Update()
        {
            evenCycle = !evenCycle;
            foreach (var worldObject in CurrentObjects)
            {
                if (evenCycle == worldObject.evenCycle)
                    worldObject.Update();
            }
            ApplyChanges();
        }

        public void AfterUpdate(bool updateInAnyKeys)
        {
            if (!updateInAnyKeys && !_wereUpdated) 
                return;
            Drawer.AddCell(this);
            _wereUpdated = false;
        }

        public void ApplyChanges()
        {
            foreach (var worldObject in _removingObjects)
            {
                CurrentObjects.Remove(worldObject);
            }
            foreach (var worldObject in _addingObjects)
            {
                CurrentObjects.Add(worldObject);
            }
            
            if (_removingObjects.Count != 0 || _addingObjects.Count != 0)
                _wereUpdated = true;

            _removingObjects.Clear();
            _addingObjects.Clear();
        }

        public void AddObject(WorldObject addingObject)
        {
            _addingObjects.Add(addingObject);
        }

        public void RemoveObject(WorldObject removingObject)
        {
            _removingObjects.Add(removingObject);
        }

        public void ReportAboutUpdating()
        {
            _wereUpdated = true;
        }
        
        public Cell GetRandomNeighbour()
        {
            var localCoords = Direction.GetRandomDirectionVector();
            if (localCoords[0] == 0 && localCoords[1] == 0)
                localCoords[0] = 1;
            
            var neighCell = World.GetCell(Coords[0] + localCoords[0], Coords[1] + localCoords[1]);
            if (neighCell != null)
                return neighCell;
            neighCell = World.GetCell(Coords[0] - localCoords[0], Coords[1] - localCoords[1]);
            if (neighCell != null)
                return neighCell;
            neighCell = World.GetCell(Coords[0] + localCoords[0], Coords[1] - localCoords[1]);
            if (neighCell != null)
                return neighCell;
            neighCell = World.GetCell(Coords[0] - localCoords[0], Coords[1] + localCoords[1]);
            return neighCell;
        }

        public List<InformationComponent> GetAllInformation()
        {
            var list = new List<InformationComponent>();
            foreach (var wo in CurrentObjects)
            {
                var informationComponent = wo.GetComponent<InformationComponent>();
                if (informationComponent != null)
                    list.Add(informationComponent);
            }

            return list;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherCell = obj as Cell;
            if (otherCell == null) 
                throw new ArgumentException("Object is not a Cell");
            for (var i = 0; i < 2; i++)
            {
                if (this.Coords[i] > otherCell.Coords[i])
                    return 1;
                if (this.Coords[i] < otherCell.Coords[i])
                    return -1;
            }
            return 0;
        }
        
        
    }
}