using System.Drawing;

namespace Stratego.Personnages
{
    public class Espion : Personnage
    {
        public Espion(int id, Point point) : base(id, point)
        {
            puissance = 1;
            type = "espion";

            piece = new Pieces(type);
        }
    }
}