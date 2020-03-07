namespace Stratego.Personnages
{
    public class Marechal : Personnage
    {
        public Marechal()
        {
            puissance = 10;
            type = "marechal";
        }
        
        public override string ToString()
        {
            return "Maréchal";
        }
    }
}