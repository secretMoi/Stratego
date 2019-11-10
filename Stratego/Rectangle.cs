using System.Drawing;

namespace Stratego
{
    // cette classe permet de manipuler la struct Rectangle de c# grâce à un objet
    public class Rectangle
    {
        private System.Drawing.Rectangle rectangle;
        public Rectangle(int posX, int posY, int longueur, int hauteur)
        {
            rectangle = new System.Drawing.Rectangle(posX, posY, longueur, hauteur);
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