using System.Drawing;

namespace Stratego.Personnages
{
    public class Marechal : Personnage
    {
        public Marechal(int id, Point point) : base(id, point)
        {
            puissance = 10;
            type = "marechal";
            
            piece = new Pieces(type);
        }
    }
}