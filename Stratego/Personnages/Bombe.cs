using System.Drawing;

namespace Stratego.Personnages
{
    public class Bombe : Personnage
    {
        public Bombe(int id, Point point) : base(id, point)
        {
            puissance = 11;
            type = "bombe";

            piece = new Pieces(type);
        }
    }
}