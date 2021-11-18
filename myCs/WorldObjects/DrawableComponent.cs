using System;
using System.Drawing;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects
{
    public class DrawableComponent : WorldObjectComponent, IComparable
    {
        public Image Image;
        public Image DefaultImage;
        public int Layer;
        public DrawableComponent(WorldObject owner, Image image, int layer) 
            : base(owner)
        {
            Image = image;
            DefaultImage = image;
            Layer = layer;
        }

        public void SetImage(Image image)
        {
            Image = image;
            WorldObject.Cell.ReportAboutUpdating();
        }
        
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherDrawable = obj as DrawableComponent;
            if (otherDrawable == null) 
                throw new ArgumentException("Object is not a DrawableComponent");
            if (Layer > otherDrawable.Layer)
                return 1;
            if (Layer < otherDrawable.Layer)
                return -1;
            return 0;
        }
    }
}