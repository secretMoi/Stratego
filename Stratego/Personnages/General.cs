using System.Drawing;

namespace Stratego.Personnages
{
    public class General : Personnage
    {
        public General(int id, Point point) : base(id, point)
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