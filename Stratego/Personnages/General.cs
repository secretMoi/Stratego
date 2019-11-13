using System.Drawing;

namespace Stratego.Personnages
{
    public class General : Personnage
    {
        public General()
        {
            puissance = 9;
            type = "general";

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Général";
        }
    }
}