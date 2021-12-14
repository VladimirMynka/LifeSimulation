using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldStructure;

namespace LifeSimulation.myCs.Drawing
{
    public class Drawer
    {
        private readonly List<Cell> _updatingCells;
        private readonly List<DrawableComponent> _currentDrawables;
        private Graphics _graphics;
        private readonly int _size;
        private readonly int _bitmapSize;
        public int ZoomInPixelSize = 50;
        public int ZoomOutPixelSize = 10;
        private int _pixelSize;
        public int OffsetLeft;
        public int OffsetTop;
        private int _length;

        private Brush _brush = Brushes.Bisque;
        public bool UpdateAll;

        public Drawer(
            Graphics graphics,
            int size = 1000,
            int bitmapSize = 1000,
            int pixelSize = 10, 
            int offsetLeft = 0, 
            int offsetTop = 0
            )
        {
            _updatingCells = new List<Cell>();
            _currentDrawables = new List<DrawableComponent>();
            _graphics = graphics;
            SetCellSize(pixelSize);
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
            _size = size;
            _bitmapSize = bitmapSize;
        }

        public void Update()
        {
            UpdateCells();
        }

        private void UpdateCells()
        {
            foreach (var cell in _updatingCells)
                UpdateCell(cell);
        }

        private void UpdateCell(Cell cell)
        {
            Fill(cell.Coords[0], cell.Coords[1], _brush);
            FillCurrentDrawables(cell);
            DrawObjects(_currentDrawables, cell.Coords[0], cell.Coords[1]);
            _currentDrawables.Clear();
        }

        private void FillCurrentDrawables(Cell cell)
        {
            _currentDrawables.Clear();
            foreach (var worldObject in cell.CurrentObjects)
            {
                var drawable = worldObject.GetComponent<DrawableComponent>();
                if (drawable != null)
                    _currentDrawables.Add(drawable);
            }
            _currentDrawables.Sort();
        }

        private void DrawObjects(List<DrawableComponent> drawables, int x, int y)
        {
            foreach (var drawable in drawables)
                DrawPicture(x, y, drawable.Image);
        }

        private void Fill(int x, int y, Brush color)
        {
            _graphics.FillRectangle(
                color, 
                new Rectangle(
                    _pixelSize * (x - OffsetLeft),
                    _pixelSize * (y - OffsetTop),
                    _pixelSize,
                    _pixelSize
                    )
                );
        }

        private void DrawPicture(int x, int y, Image image)
        {
            if (_pixelSize <= 5)
                Fill(x, y, Pictures.GetBrushFor(image));
            else
            {
                _graphics.DrawImage(
                    image,
                    new Rectangle(
                        _pixelSize * (x - OffsetLeft),
                        _pixelSize * (y - OffsetTop),
                        _pixelSize,
                        _pixelSize
                    )
                );
            }
        }

        public void DrawOffsets()
        {
            DrawLeftOffset();
            DrawBottomOffset();
            DrawTopOffset();
            DrawRightOffset();
            UpdateAll = true;
        }

        private void DrawLeftOffset()
        {
            if (OffsetLeft < 0)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        0,
                        0,
                        -_pixelSize * OffsetLeft,
                        _bitmapSize
                    )
                );
            }
        }
        
        private void DrawTopOffset()
        {
            if (OffsetTop < 0)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        0,
                        0,
                        _bitmapSize,
                        -_pixelSize * OffsetTop
                    )
                );
            }

        }
        
        private void DrawRightOffset()
        {
            var offsetRight = _size - OffsetLeft - _bitmapSize / _pixelSize;
            if (offsetRight < 0)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        _bitmapSize + offsetRight * _pixelSize,
                        0,
                        -_pixelSize * offsetRight,
                        _bitmapSize
                    )
                );
            }
        }

        private void DrawBottomOffset()
        {
            var offsetBottom = _size - OffsetTop - _bitmapSize / _pixelSize;
            if (offsetBottom < 0)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        0, 
                        _bitmapSize + offsetBottom * _pixelSize,
                        _bitmapSize,
                        -_pixelSize * offsetBottom
                    )
                );
            }
        }

        public void UpdateGraphics(Graphics graphics)
        {
            _graphics = graphics;
            ClearList();
        }

        private void ClearList()
        {
            _updatingCells.Clear();
        }

        public void AddCell(Cell cell)
        {
            if (!CheckVisible(cell)) return;
                
            int index = _updatingCells.BinarySearch(cell);
            if (index < 0)
            {
                _updatingCells.Insert(~index, cell);
            }
        }
        
        private bool CheckVisible(Cell cell)
        {
            return CheckVisible(cell.Coords[0], cell.Coords[1]);
        }

        private bool CheckVisible(int x, int y)
        {
            return (OffsetLeft <= x && x <= OffsetLeft + _length &&
                    OffsetTop <= y && y <= OffsetTop + _length);
        }
        
        public Point CellCoordsFromPixelCoords(Point pixelCoords)
        {
            return new Point(pixelCoords.X / _pixelSize + OffsetLeft,
                pixelCoords.Y / _pixelSize + OffsetTop);
        }

        public void ZoomOnCell(Point coords)
        {
            if (_pixelSize > ZoomOutPixelSize)
                _pixelSize = ZoomOutPixelSize;
            else
                _pixelSize = ZoomInPixelSize;
            SetOffsetsWithCenterIn(coords);
        }

        public void SetOffsetsWithCenterIn(Point coords)
        {
            OffsetLeft = coords.X - _bitmapSize / 2 / _pixelSize;
            OffsetTop = coords.Y - _bitmapSize / 2 / _pixelSize;
        }

        public int GetX()
        {
            return OffsetLeft;
        }

        public int GetY()
        {
            return OffsetTop;
        }

        public int GetLength()
        {
            return _length;
        }

        public void SetBackground(Brush brush)
        {
            _brush = brush;
            UpdateAll = true;
        }

        public void SetCellSize(int size)
        {
            _pixelSize = size;
            _length = _bitmapSize / size;
        }

        public int GetCellSize()
        {
            return _pixelSize;
        }
    }
}