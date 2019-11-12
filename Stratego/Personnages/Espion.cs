using System.Drawing;

namespace Stratego.Personnages
{
    public class Espion : Personnage
    {
        public Espion(int id, Point point) : base(id, point)
        {
            puissance = 1;
            type = "espion";

            piece = new Pieces(type);
        }
        
        public override int Collision(Personnage attaquant, Personnage defenseur)
        {
            int resultat = base.Collision(attaquant, defenseur);

            if (defenseur is Marechal)
                resultat = Attaquant;

            return resultat;
        }
        
        public override string ToString()
        {
            return "Espion";
        }
    }
}