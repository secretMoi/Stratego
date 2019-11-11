using System.Drawing;

namespace Stratego.Personnages
{
    public class Bombe : Personnage
    {
        public Bombe(int id, Point point) : base(id, point)
        {
            puissance = 11;
            type = "bombe";
            deplacement = 0;

            piece = new Pieces(type);
        }
        
        
    }
}