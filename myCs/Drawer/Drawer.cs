using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects;

namespace LifeSimulation.myCs.Drawer
{
    public class Drawer
    {
        private readonly List<Cell> _updatingCells;
        private readonly List<DrawableComponent> _currentDrawables;
        private Graphics _graphics;
        public int PixelSize;
        public int OffsetLeft;
        public int OffsetTop;

        private Brush _brush = Brushes.Bisque;
        public bool UpdateAll;

        public Drawer(
            Graphics graphics, 
            int pixelSize = 10, 
            int offsetLeft = 0, 
            int offsetTop = 0)
        {
            _updatingCells = new List<Cell>();
            _currentDrawables = new List<DrawableComponent>();
            _graphics = graphics;
            PixelSize = pixelSize;
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
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
                    PixelSize * (x - OffsetLeft),
                    PixelSize * (y - OffsetTop),
                    PixelSize,
                    PixelSize
                    )
                );
        }

        private void DrawPicture(int x, int y, Image image)
        {
            _graphics.DrawImage(
                image, 
                new Rectangle(
                    PixelSize * (x - OffsetLeft),
                    PixelSize * (y - OffsetTop),
                    PixelSize,
                    PixelSize
                )
            );        
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
                        -PixelSize * OffsetLeft,
                        1000
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
                        1000,
                        -PixelSize * OffsetTop
                    )
                );
            }

        }
        
        private void DrawRightOffset()
        {
            if (OffsetLeft * PixelSize >= 1000)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        PixelSize * OffsetLeft,
                        0,
                        1000 - PixelSize * (1000 - OffsetLeft),
                        1000
                    )
                );
            }

        }

        private void DrawBottomOffset()
        {
            if (OffsetTop * PixelSize >= 1000)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        0,
                        PixelSize * OffsetTop,
                        1000,
                        1000 - PixelSize * (1000 - OffsetTop)
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
            return CheckVisible(cell.Coords[0], cell.Coords[0]);
        }

        private bool CheckVisible(int x, int y)
        {
            return (OffsetLeft < x + PixelSize && x < 1000 &&
                    OffsetTop < y + PixelSize && y < 1000);
        }

        public Point CellCoordsFromPixelCoords(Point pixelCoords)
        {
            return new Point(pixelCoords.X / PixelSize + OffsetLeft,
                pixelCoords.Y / PixelSize + OffsetTop);
        }

        public void ZoomOnCell(Point coords)
        {
            if (PixelSize >= 30)
                PixelSize = 10;
            else
                PixelSize = 50;
            SetOffsetsWithCenterIn(coords);
        }

        public void SetOffsetsWithCenterIn(Point coords)
        {
            OffsetLeft = coords.X - 500 / PixelSize;
            OffsetTop = coords.Y - 500 / PixelSize;
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
            return 1000 / PixelSize;
        }

        public void SetBackground(Brush brush)
        {
            _brush = brush;
            UpdateAll = true;
        }
    }
}