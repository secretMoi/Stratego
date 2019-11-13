using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Stratego
{
    public class Pieces
    {
        private const int DimensionX = 58;
        private const int DimensionY = 50;
        
        private string prefixeSource = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\images\";
        private const string format = ".jpg";
        private readonly Bitmap image;
        
        private string imageSource; // liste des images

        public Pieces(string type)
        {
            imageSource = prefixeSource + type + format;
            image = new Bitmap(imageSource);
        }

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