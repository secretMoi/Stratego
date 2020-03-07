using System.Drawing;

namespace Stratego.Personnages
{
    public class Eclaireur : Personnage
    {
        public Eclaireur()
        {
            puissance = 2;
                
            type = "eclaireur";
        }

        public override void Hydrate(int id,  int deplacement, Point point, bool equipe)
        {
            base.Hydrate(id, deplacement, point, equipe);
            this.deplacement = deplacement - 1;
        }

        public override string ToString()
        {
            return "Eclaireur";
        }
    }
}