using System.Drawing;

namespace Stratego.Personnages
{
    public class Eclaireur : Personnage
    {
        public Eclaireur(int id, Point point) : base(id, point)
        {
            puissance = 2;
            type = "eclaireur";

            piece = new Pieces(type);
        }
    }
}