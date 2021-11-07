using System.Drawing;

namespace LifeSimulation.myCs.Drawer
{
    public class ObjectDrawer
    {
        public readonly int PositionX;
        public readonly int PositionY;
        public readonly int Layer;
        public readonly Image Image;
        

        public ObjectDrawer(int x, int y, int layer, Image image)
        {
            PositionX = x;
            PositionY = y;
            Layer = layer;
            Image = image;
        }

        public static bool operator <(ObjectDrawer od1, ObjectDrawer od2)
        {
            return (od1.Layer < od2.Layer);
        }

        public static bool operator >(ObjectDrawer od1, ObjectDrawer od2)
        {
            return (od1.Layer > od2.Layer);
        }
        
        public static bool operator ==(ObjectDrawer od1, ObjectDrawer od2)
        {
            return od2 != null && od1 != null && (od1.Layer == od2.Layer);
        }

        public static bool operator !=(ObjectDrawer od1, ObjectDrawer od2)
        {
            return !(od1 == od2);
        }
    }
}