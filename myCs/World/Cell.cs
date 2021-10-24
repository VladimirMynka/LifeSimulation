using System.Collections.Generic;
using System.Linq;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs.World
{
    public class Cell
    {
        public readonly World World;
        public readonly int[] Coords;
        public List<WorldObject> CurrentObjects;
        private List<WorldObject> _removingObjects;
        private List<WorldObject> _addingObjects;
        private int _color;
        public int DefaultColor;

        private readonly Drawer.Drawer _drawer;
        public bool evenCycle;
        private bool _wereUpdated;

        public Cell(World world, Drawer.Drawer drawer, int[] coords, int color = 0)
        {
            World = world;
            Coords = coords;
            DefaultColor = color;
            _color = color;
            CurrentObjects = new List<WorldObject>();
            _removingObjects = new List<WorldObject>();
            _addingObjects = new List<WorldObject>();
            _drawer = drawer;
            evenCycle = false;
            
            drawer.AddCell(new CellDrawer(Coords[0], Coords[1], color));
        }

        public void Update(bool updateInAnyKeys)
        {
            evenCycle = !evenCycle;
            foreach (var worldObject in CurrentObjects)
            {
                if (evenCycle == worldObject.evenCycle)
                    worldObject.Update();
            }

            foreach (var worldObject in _removingObjects)
            {
                CurrentObjects.Remove(worldObject);
            }

            foreach (var worldObject in _addingObjects)
            {
                CurrentObjects.Add(worldObject);
            }
            
            if (_removingObjects.Count != 0 || _addingObjects.Count != 0)
                UpdateColor();

            _removingObjects.Clear();
            _addingObjects.Clear();

            if (!updateInAnyKeys && !_wereUpdated) return;
            _drawer.AddCell(new CellDrawer(Coords[0], Coords[1], _color));
            _wereUpdated = false;
        }

        public void AddObject(WorldObject addingObject)
        {
            _addingObjects.Add(addingObject);
        }

        public void RemoveObject(WorldObject removingObject)
        {
            _removingObjects.Add(removingObject);
        }

        private void UpdateColor()
        {
            if (CurrentObjects.Count == 0)
                ThrowOffColor();
            else 
                SetColor(CurrentObjects.Last().Color);
        }

        public void SetColor(int color)
        {
            _color = color;
            _wereUpdated = true;
        }
        
        public void ThrowOffColor()
        {
            SetColor(DefaultColor);
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
    }
}