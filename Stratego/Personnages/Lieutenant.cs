using System.Drawing;

namespace Stratego.Personnages
{
    public class Lieutenant : Personnage
    {
        public Lieutenant(int id, Point point) : base(id, point)
        {
            puissance = 5;
            type = "lieutenant";

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Lieutenant";
        }
    }
}