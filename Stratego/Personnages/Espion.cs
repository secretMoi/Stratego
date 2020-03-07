namespace Stratego.Personnages
{
    public class Espion : Personnage
    {
        public Espion()
        {
            puissance = 1;
            type = "espion";
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