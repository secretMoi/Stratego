namespace Stratego.Personnages
{
    public class Lieutenant : Personnage
    {
        public Lieutenant()
        {
            puissance = 5;
            type = "lieutenant";
        }
        
        public override string ToString()
        {
            return "Lieutenant";
        }
    }
}