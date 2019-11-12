using System.Drawing;

namespace Stratego.Personnages
{
    public class Eclaireur : Personnage
    {
        public Eclaireur(int id, Point point, int taillePlateau) : base(id, point)
        {
            puissance = 2;
            deplacement = taillePlateau - 1;
                
            type = "eclaireur";
            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Eclaireur";
        }
    }
}