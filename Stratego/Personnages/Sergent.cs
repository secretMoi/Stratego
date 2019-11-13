using System.Drawing;

namespace Stratego.Personnages
{
    public class Sergent : Personnage
    {
        public Sergent()
        {
            puissance = 4;
            type = "sergent";

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Sergent";
        }
    }
}