namespace Stratego.Personnages
{
    public class Bombe : Personnage
    {
        public Bombe()
        {
            puissance = 11;
            type = "bombe";
            deplacement = 0;
        }

        public override string ToString()
        {
            return "Bombe";
        }
    }
}