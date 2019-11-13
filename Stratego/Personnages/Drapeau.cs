using System.Drawing;

namespace Stratego.Personnages
{
    public class Drapeau : Personnage
    {
        public Drapeau()
        {
            puissance = 0;
            type = "drapeau";
            deplacement = 0;

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Drapeau";
        }
    }
}