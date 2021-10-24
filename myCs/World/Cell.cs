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

            if (!updateInAnyKeys && !_wereUpdated) return;
            _drawer.AddCell(new CellDrawer(Coords[0], Coords[1], _color));
            _wereUpdated = false;
        }

        public void AddObject(WorldObject addingObject)
        {
            CurrentObjects.Add(addingObject);
            UpdateColor();
        }

        public void RemoveObject(WorldObject removingObject)
        {
            CurrentObjects.Remove(removingObject);
            UpdateColor();
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
            var neighCell = World.GetCell(localCoords[0] + Coords[0], localCoords[1] + Coords[1]);
            return neighCell ?? World.GetCell(localCoords[0] - Coords[0], localCoords[1] - Coords[1]);
        }
    }
}