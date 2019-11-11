using System.Drawing;

namespace Stratego.Personnages
{
    public class Drapeau : Personnage
    {
        public Drapeau(int id, Point point) : base(id, point)
        {
            puissance = 0;
            type = "drapeau";

            piece = new Pieces(type);
        }
    }
}