using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LifeSimulation.myCs
{
    public class Drawer
    {
        private List<CellDrawer> _updatingCells;
        private Graphics _graphics;
        public int PixelSize;
        public int OffsetLeft;
        public int OffsetTop;

        public Drawer(
            Graphics graphics, 
            int pixelSize = 10, 
            int offsetLeft = 0, 
            int offsetTop = 0)
        {
            _updatingCells = new List<CellDrawer>();
            _graphics = graphics;
            PixelSize = pixelSize;
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
        }

        public void Update()
        {
            foreach (var cell in _updatingCells.Where(cell => 
                cell.PositionX - OffsetLeft >= 0 && 
                cell.PositionY - OffsetTop >= 0 && 
                PixelSize * (cell.PositionX - OffsetLeft) <= 1000 && 
                PixelSize * (cell.PositionY - OffsetTop) <= 1000)
            )
            {
                _graphics.FillRectangle(Colors.GetBrush(cell.Color),
                new Rectangle(
                    PixelSize * (cell.PositionX - OffsetLeft), 
                    PixelSize * (cell.PositionY - OffsetTop),
                    PixelSize,
                    PixelSize
                    )
                );
            }
        }

        public void ConfigureOffsets()
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
            if (OffsetLeft * PixelSize >= 1000)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        PixelSize * OffsetLeft,
                        0,
                        1000 - PixelSize * OffsetLeft,
                        1000
                    )
                );
            }
            if (OffsetTop * PixelSize >= 1000)
            {
                _graphics.FillRectangle(Colors.Black,
                    new Rectangle(
                        0,
                        PixelSize * OffsetTop,
                        1000,
                        1000 - PixelSize * OffsetTop
                    )
                );
            }
        }

        public void Clear()
        {
            _updatingCells = new List<CellDrawer>();
        }

        public void UpdateGraphics(Graphics graphics)
        {
            _graphics = graphics;
            Clear();
        }

        public void AddCell(CellDrawer cell)
        {
            _updatingCells.Add(cell);
        }
    }
}