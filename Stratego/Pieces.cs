using System.Collections.Generic;

namespace Stratego
{
    public class Pieces
    {
        public const int DimensionX = 58;
        public const int DimensionY = 50;
        
        private const string prefixeSource = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\";
        private const string format = ".jpg";
        
        private string imageSource; // liste des images

        public Pieces(string type)
        {
            imageSource = prefixeSource + type + format;
        }

        public string Chemin => imageSource;

        public int Longueur => DimensionX;
        public int Hauteur => DimensionY;
    }
}