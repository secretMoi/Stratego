using System.Drawing;

namespace Stratego.Personnages
{
    public class Colonel : Personnage
    {
        public Colonel(int id, Point point) : base(id, point)
        {
            puissance = 8;
            type = "colonel";

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Colonel";
        }
    }
}