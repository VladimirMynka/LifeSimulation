using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving
{
    public class MovingComponent : WorldObjectComponent, IHaveInformation
    {
        private int _ticksToStep;
        public int Pace;
        public int RunPace;
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
            MovingToTargetState movingState = MovingToTargetState.UsualMoving,
            int pace = Defaults.TicksToStep) : base(owner)
        {
            _ticksToStep = pace;
            Pace = pace;
            RunPace = pace / 2;
            _walkingState = walkingState;
            _movingToTargetState = movingState;
            _lastDirection = new[]{0, 0};
        }

        public override void Start()
        {
            base.Start();
            _cell = WorldObject.Cell;
        }

        public override void Update()
        {
            base.Update();
            TryChangeStates();
            TryStep();
            if (_target != null && CheckWereDestroyed(_target))
            {
                _target = null;
                _speedState = SpeedState.SlowDown;
            }
        }

        private void TryChangeStates()
        {
            var random = World.Random.Next(100);
            if (random < 5)
                ChangeStates();
        }

        private void ChangeStates()
        {
            _walkingState = (WalkingState) World.Random.Next(4);
            _movingToTargetState = (MovingToTargetState) World.Random.Next(2);
        }

        private void TryStep()
        {
            if (_ticksToStep <= 0)
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
            _lastPace++;
            _ticksToStep = _lastPace;
            if (_ticksToStep >= Pace)
            {
                _speedState = SpeedState.Walk;
                _ticksToStep = Pace;
                _lastPace = Pace;
            }
        }
        
        private void UpdateTickWhenSlowUp()
        {
            _lastPace--;
            _ticksToStep = _lastPace;
            if (_ticksToStep <= RunPace)
            {
                _speedState = SpeedState.Run;
                _ticksToStep = RunPace;
                _lastPace = RunPace;
            }
        }

        private void Step()
        {
            DeleteTargetIfAchieved();

            var direction = GetDirection();
            var cell = GetCell(direction);
            if (cell == null) 
                return;
            GoToCell(cell);
            _lastDirection = direction;
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
            return _target != null ? GetDirectionToTarget() : GetRandomDirection();
        }

        private int[] GetDirectionToTarget()
        {
            switch (_movingToTargetState)
            {
                case MovingToTargetState.OrthogonalMoving:
                    return GetOrthogonalDirection(_target.Cell.Coords);
                case MovingToTargetState.SnakeMoving:
                    return GetSnakeDirection(_target.Cell.Coords);
                default:
                    return GetUsualDirection(_target.Cell.Coords);
            }
        }

        private int[] GetOrthogonalDirection(int[] targetCoords)
        {
            return Direction.GetOrthogonalDirection(_cell.Coords, targetCoords);
        }
        
        private int[] GetUsualDirection(int[] targetCoords)
        {
            return Direction.GetDirectionVector(_cell.Coords, targetCoords);
        }
        
        private int[] GetSnakeDirection(int[] targetCoords)
        {
            var direction = Direction.GetOrthogonalDirection(_cell.Coords, targetCoords);
            return
                World.Random.Next(10) < 3
                    ? direction
                    : Direction.GetNormalDirection(direction);
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
            int direction = World.Random.Next(10);
            if (direction < 1) 
                return Direction.GetDirectionVector(Direction.BottomLeft);
            if (direction < 3) 
                return Direction.GetDirectionVector(Direction.Left);
            if (direction < 7) 
                return Direction.GetDirectionVector(Direction.TopLeft);
            if (direction < 9) 
                return Direction.GetDirectionVector(Direction.Top);
            return Direction.GetDirectionVector(Direction.TopRight);
        }
        
        private int[] GetRandomRightBottomDirection()
        {
            int direction = World.Random.Next(10);
            if (direction < 1) 
                return Direction.GetDirectionVector(Direction.TopRight);
            if (direction < 3) 
                return Direction.GetDirectionVector(Direction.Right);
            if (direction < 7) 
                return Direction.GetDirectionVector(Direction.BottomRight);
            if (direction < 9) 
                return Direction.GetDirectionVector(Direction.Bottom);
            return Direction.GetDirectionVector(Direction.BottomLeft);
        }

        private int[] GetRandomWithoutTurnsDirection()
        {
            int[] direction = new int[]{_lastDirection[0], _lastDirection[1]};
            int index = World.Random.Next(2);
            if (direction[index] == -1)
            {
                int delta = World.Random.Next(2);
                direction[index] += delta;
            }
            else if (direction[index] == 1)
            {
                int delta = World.Random.Next(2);
                direction[index] -= delta;
            }
            else
            {
                int delta = World.Random.Next(3);
                direction[index] += delta - 1;
            }
            return direction;
        }

        public void DeleteTargetIfAchieved()
        {
            if (!CheckTargetMustBeDeleted())
                return;
            _target = null;
            _speedState = SpeedState.SlowDown;
        }
        
        public bool CheckTargetMustBeDeleted()
        {
            if (_target == null) 
                return false;
            return _target.Cell == null || 
                   Direction.CheckEqual(WorldObject.Cell.Coords, _target.Cell.Coords);
        }

        private void GoAway()
        {
            _cell.RemoveObject(WorldObject);
            _cell.ReportAboutUpdating();
        }

        private void GoToCell(Cell cell)
        {
            GoAway();
            WorldObject.Cell = cell;
            _cell = cell;
            cell.AddObject(WorldObject);
            cell.ReportAboutUpdating();
        }

        public void SetTarget(WorldObject target)
        {
            _target = target;
            _speedState = SpeedState.SlowUp;
        }

        public void WaitFor(int ticks)
        {
            _ticksToStep += ticks;
        }

        public string GetInformation()
        {
            var info = "Ticks to step: " + _ticksToStep + '\n';
            info += "Speed state: " + _speedState + '\n';
            info += "Walking state: " + _movingToTargetState + '\n';
            info += "Go to target state: " + _movingToTargetState + '\n';
            info += "Target: ";
            
            if (CheckWereDestroyed(_target))
                info += "none";
            else
                info += "on " + InformationComponent.GetInfoAboutCoords(_target);
            
            return info;
        }
    }
}