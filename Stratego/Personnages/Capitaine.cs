using System.Drawing;

namespace Stratego.Personnages
{
    public class Capitaine : Personnage
    {
        public Capitaine()
        {
            puissance = 6;
            type = "capitaine";

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Capitaine";
        }
    }
}