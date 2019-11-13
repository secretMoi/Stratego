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

        public override void Hydrate(int id, Point point, int deplacement)
        {
            base.Hydrate(id, point, deplacement);
            this.deplacement = deplacement - 1;
        }

        public override string ToString()
        {
            return "Eclaireur";
        }
    }
}