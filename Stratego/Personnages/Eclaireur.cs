using System.Drawing;

namespace Stratego.Personnages
{
    public class Eclaireur : Personnage
    {
        public Eclaireur()
        {
            puissance = 2;
                
            type = "eclaireur";
            piece = new Pieces(type);
        }

        public override void Hydrate(int id,  int deplacement, Point point)
        {
            base.Hydrate(id, deplacement, point);
            this.deplacement = deplacement - 1;
        }

        public override string ToString()
        {
            return "Eclaireur";
        }
    }
}