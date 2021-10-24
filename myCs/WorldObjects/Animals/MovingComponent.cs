using System.Collections.Generic;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class MovingComponent : WorldObjectComponent
    {
        private int _ticksToStep;
        public int ReverseSpeed;
        private Stack<int[]> _targets;

        private Cell _cell;
        
        public MovingComponent(WorldObject owner) : base(owner)
        {
            _ticksToStep = Defaults.TicksToStep;
            ReverseSpeed = Defaults.TicksToStep;
            _targets = new Stack<int[]>();
        }

        public override void Start()
        {
            base.Start();
            _cell = worldObject.Cell;
        }

        public override void Update()
        {
            base.Update();
            TryStep();
        }

        private void TryStep()
        {
            if (_ticksToStep == 0)
            {
                Step();
                _ticksToStep = ReverseSpeed;
            }
            else
            {
                _ticksToStep--;
            }
        }

        private void Step()
        {
            var cell = GetCell(GetDirection());
            if (cell != null) 
                GoToCell(cell);
        }

        private Cell GetCell(int[] localCoords)
        {
            return world.GetCell(
                _cell.Coords[0] + localCoords[0],
                _cell.Coords[1] + localCoords[1]
            );
        }

        private int[] GetDirection()
        {
            if (_targets.Count != 0)
                return Direction.GetDirectionVector(_cell.Coords, _targets.Peek());
            else
                return Direction.GetRandomDirectionVector();
        }

        public bool CheckTargetAchieved()
        {
            return Direction.CheckEqual(worldObject.Cell.Coords, _targets.Peek());
        }

        private void GoAway()
        {
            _cell.RemoveObject(worldObject);
        }

        private void GoToCell(Cell cell)
        {
            GoAway();
            worldObject.Cell = cell;
            cell.AddObject(worldObject);
        }
    }
}