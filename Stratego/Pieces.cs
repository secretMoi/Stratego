using System.Collections.Generic;
using System.Drawing;

namespace Stratego
{
    public class Pieces
    {
        public const int DimensionX = 58;
        public const int DimensionY = 50;
        
        private const string prefixeSource = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\";
        private const string format = ".jpg";
        private readonly Bitmap image;
        
        private string imageSource; // liste des images

        public Pieces(string type)
        {
            imageSource = prefixeSource + type + format;
            image = new Bitmap(imageSource);
        }

        public string Chemin => imageSource;

        public Point Dimension => new Point(DimensionX, DimensionY);

        public Bitmap Image => image;

        public int Longueur => DimensionX;
        public int Hauteur => DimensionY;

        public void CentrePiece(ref Point point)
        {
            point.X -= Longueur / 2;
            point.Y -= Hauteur / 2;
        }
    }
}