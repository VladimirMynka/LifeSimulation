using System;
using System.Drawing;

namespace LifeSimulation.myCs.WorldObjects
{
    public class DrawableComponent : WorldObjectComponent, IComparable
    {
        public Image Image;
        public int Layer;
        public DrawableComponent(WorldObject owner, Image image, int layer) 
            : base(owner)
        {
            Image = image;
            Layer = layer;
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