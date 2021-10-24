using System;
using System.Collections.Generic;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class MovingComponent : WorldObjectComponent
    {
        private int _ticksToStep;
        public int Pace;
        public int RunPace;
        private Stack<int[]> _targets;
        private WorldObject _target;

        private Cell _cell;
        private WalkingState _walkingState;
        private MovingToTargetState _movingToTargetState;
        private SpeedState _speedState;

        private int[] _lastDirection;
        private int _lastPace;

        public MovingComponent(
            WorldObject owner,
            WalkingState walkingState = WalkingState.UsualWalking, 
            MovingToTargetState movingState = MovingToTargetState.UsualMoving) : base(owner)
        {
            _ticksToStep = Defaults.TicksToStep;
            Pace = Defaults.TicksToStep;
            RunPace = Pace / 2;
            _targets = new Stack<int[]>();
            _walkingState = walkingState;
            _movingToTargetState = movingState;
        }

        public override void Start()
        {
            base.Start();
            _cell = WorldObject.Cell;
        }

        public override void Update()
        {
            base.Update();
            TryStep();
            if (_target == null) return;
            _targets.Clear();
            _targets.Push(_target.Cell.Coords);
        }

        private void TryStep()
        {
            if (_ticksToStep == 0)
            {
                Step();
                UpdateTickToStep();
            }
            else
            {
                _ticksToStep--;
            }
        }

        private void UpdateTickToStep()
        {
            switch (_speedState)
            {
                case SpeedState.Walk:
                    UpdateTicksWhenWalk();
                    return;
                case SpeedState.Run:
                    UpdateTicksWhenRun();
                    return;
                case SpeedState.SlowDown:
                    UpdateTickWhenSlowDown();
                    return;
                case SpeedState.SlowUp:
                    UpdateTickWhenSlowUp();
                    return;
            }
        }

        private void UpdateTicksWhenWalk()
        {
            _ticksToStep = Pace;
            _lastPace = Pace;
        }

        private void UpdateTicksWhenRun()
        {
            _ticksToStep = RunPace;
            _lastPace = RunPace;
        }

        private void UpdateTickWhenSlowDown()
        {
            _ticksToStep = --_lastPace;
            if (_ticksToStep <= Pace)
            {
                _speedState = SpeedState.Walk;
                _ticksToStep = Pace;
            }
        }
        
        private void UpdateTickWhenSlowUp()
        {
            _ticksToStep = ++_lastPace;
            if (_ticksToStep >= RunPace)
            {
                _speedState = SpeedState.Run;
                _ticksToStep = Pace;
            }
        }

        private void Step()
        {
            var direction = GetDirection();
            var cell = GetCell(direction);
            if (cell == null) 
                return;
            GoToCell(cell);
            _lastDirection = direction;
            if (_targets.Count != 0 && CheckTargetAchieved())
            {
                _targets.Pop();
                if (_targets.Count == 0) _speedState = SpeedState.SlowDown;
            }

            if (CheckDynamicTargetAchieved())
            {
                _target = null;
            }
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
                return GetDirectionToTarget();
            return GetRandomDirection();
        }

        private int[] GetDirectionToTarget()
        {
            switch (_movingToTargetState)
            {
                case MovingToTargetState.OrthogonalMoving:
                    return Direction.GetOrthogonalDirection(_cell.Coords, _targets.Peek());
                default:
                    return Direction.GetDirectionVector(_cell.Coords, _targets.Peek());
            }
        }

        private int[] GetRandomDirection()
        {
            switch (_walkingState)
            {
                case WalkingState.LeftTopWalking:
                    return GetRandomLeftTopDirection();
                case WalkingState.RightBottomWalking:
                    return GetRandomRightBottomDirection();
                case WalkingState.NoSharpTurns:
                    return GetRandomWithoutTurnsDirection();
                default:
                    return Direction.GetRandomDirectionVector();
            }
        }

        private int[] GetRandomLeftTopDirection()
        {
            int direction = World.World.Random.Next(10);
            if (direction < 1) return Direction.GetDirectionVector(Direction.BottomLeft);
            if (direction < 3) return Direction.GetDirectionVector(Direction.Left);
            if (direction < 7) return Direction.GetDirectionVector(Direction.TopLeft);
            if (direction < 9) return Direction.GetDirectionVector(Direction.Top);
            return Direction.GetDirectionVector(Direction.TopRight);
        }
        
        private int[] GetRandomRightBottomDirection()
        {
            int direction = World.World.Random.Next(10);
            if (direction < 1) return Direction.GetDirectionVector(Direction.TopRight);
            if (direction < 3) return Direction.GetDirectionVector(Direction.Right);
            if (direction < 7) return Direction.GetDirectionVector(Direction.BottomRight);
            if (direction < 9) return Direction.GetDirectionVector(Direction.Bottom);
            return Direction.GetDirectionVector(Direction.BottomLeft);
        }

        private int[] GetRandomWithoutTurnsDirection()
        {
            int[] direction = new int[]{_lastDirection[0], _lastDirection[1]};
            int index = World.World.Random.Next(2);
            if (direction[index] == -1)
            {
                int delta = World.World.Random.Next(2);
                direction[index] += delta;
            }
            else if (direction[index] == 1)
            {
                int delta = World.World.Random.Next(2);
                direction[index] -= delta;
            }
            else
            {
                int delta = World.World.Random.Next(3);
                direction[index] += delta - 1;
            }
            return direction;
        }

        public bool CheckTargetAchieved()
        {
            return _targets.Count != 0 && Direction.CheckEqual(WorldObject.Cell.Coords, _targets.Peek());
        }
        
        public bool CheckDynamicTargetAchieved()
        {
            return (_target != null && Direction.CheckEqual(WorldObject.Cell.Coords, _target.Cell.Coords));
        }

        private void GoAway()
        {
            _cell.RemoveObject(WorldObject);
        }

        private void GoToCell(Cell cell)
        {
            GoAway();
            WorldObject.Cell = cell;
            cell.AddObject(WorldObject);
        }

        public void SetTarget(WorldObject target, bool targetIsDynamic)
        {
            if (targetIsDynamic)
            {
                _target = target;
                _speedState = SpeedState.SlowUp;
            }
            else
                BuildRoute(target.Cell);
        }

        private void BuildRoute(Cell targetCell)
        {
            _targets.Push(targetCell.Coords);
        }
    }
}