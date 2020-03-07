namespace Stratego.Personnages
{
    public class Capitaine : Personnage
    {
        public Capitaine()
        {
            puissance = 6;
            type = "capitaine";
        }
        
        public override string ToString()
        {
            return "Capitaine";
        }
    }
}