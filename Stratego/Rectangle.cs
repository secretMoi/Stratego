using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Stratego
{
    // cette classe permet de manipuler la struct Rectangle de c# grâce à un objet
    [Serializable]
    public class Rectangle : ISerializable
    {
        private System.Drawing.Rectangle rectangle;
        
        // serialise
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Rectangle", rectangle, typeof(System.Drawing.Rectangle));
        }
        
        // deserialise
        public Rectangle(SerializationInfo info, StreamingContext context)
        {
            rectangle = (System.Drawing.Rectangle) info.GetValue("Rectangle", typeof(System.Drawing.Rectangle));
        }
        
        public Rectangle(int posX, int posY, int longueur, int hauteur)
        {
            rectangle = new System.Drawing.Rectangle(posX, posY, longueur, hauteur);
        }
        
        public Rectangle(Point position, Point dimension)
        {
            rectangle = new System.Drawing.Rectangle(position, new Size(dimension));
        }

        public int X
        {
            set { rectangle.X = value; }
        }
        public int Y
        {
            set { rectangle.Y = value; }
        }

        public Point Point
        {
            set { rectangle.Location = value; }
        }
        
        public System.Drawing.Rectangle Rect
        {
            get { return rectangle; }
        }
    }
}