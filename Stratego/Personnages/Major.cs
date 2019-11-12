using System.Drawing;

namespace Stratego.Personnages
{
    public class Major : Personnage
    {
        public Major(int id, Point point) : base(id, point)
        {
            puissance = 7;
            type = "major";

            piece = new Pieces(type);
        }
        
        public override string ToString()
        {
            return "Major";
        }
    }
}