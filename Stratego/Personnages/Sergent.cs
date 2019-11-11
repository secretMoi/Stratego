using System.Drawing;

namespace Stratego.Personnages
{
    public class Sergent : Personnage
    {
        public Sergent(int id, Point point) : base(id, point)
        {
            puissance = 4;
            type = "sergent";

            piece = new Pieces(type);
        }
    }
}