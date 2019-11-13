using System.Drawing;

namespace Stratego.Personnages
{
    public class Marechal : Personnage
    {
        public Marechal()
        {
            puissance = 10;
            type = "marechal";
            
            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Maréchal";
        }
    }
}