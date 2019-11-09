using System.Collections.Generic;
using System.Drawing;

namespace Stratego.Personnages
{
    public class Personnage
    {
        protected readonly Map map; // contient un objet map pour intéragir
        public const int DimensionPieceX = 58;
        public const int DimensionPieceY = 50;
        public readonly int X = 0;
        public readonly int Y = 1;
        
        private List<string> imageSource; // liste des images

        protected int deplacement;
        protected Point position;

        public Personnage(Map map)
        {
            imageSource = new List<string>();
            ReferencePersonnage(); // référence toutes les images de personnages 

            this.map = map;

            deplacement = 1;
        }

        private void ReferencePersonnage()
        {
            string prefixeSource = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\";
            
            imageSource.Add(prefixeSource + "marechal.jpg");
        }

        public virtual bool Deplacement(Point point)
        {
            if (deplacement >= map.Distance(position, point))
                return true;
            
            return false;
        }

        public List<string> ListePersonnage
        {
            get { return imageSource; }
        }
    }
}