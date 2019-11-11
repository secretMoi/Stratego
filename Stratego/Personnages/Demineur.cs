using System.Drawing;

namespace Stratego.Personnages
{
    public class Demineur : Personnage
    {
        public Demineur(int id, Point point) : base(id, point)
        {
            puissance = 3;
            type = "demineur";

            piece = new Pieces(type);
        }
    }
}