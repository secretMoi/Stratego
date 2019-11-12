using System.Drawing;

namespace Stratego.Personnages
{
    public class Capitaine : Personnage
    {
        public Capitaine(int id, Point point) : base(id, point)
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