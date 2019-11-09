using System.Collections.Generic;

namespace Stratego.Personnages
{
    public class Personnage
    {
        protected readonly int[] dimensionPiece;
        public readonly int X = 0;
        public readonly int Y = 1;
        
        private List<string> imageSource;

        public Personnage()
        {
            dimensionPiece = new int[2];
            dimensionPiece[X] = 58;
            dimensionPiece[Y] = 50;
            
            imageSource = new List<string>();
            ReferencePersonnage();
        }

        private void ReferencePersonnage()
        {
            string prefixeSource = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\";
            
            imageSource.Add(prefixeSource + "marechal.jpg");
        }

        public int DimensionPieceX => dimensionPiece[X];
        public int DimensionPieceY => dimensionPiece[Y];
    }
}