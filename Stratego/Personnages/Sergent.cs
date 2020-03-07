namespace Stratego.Personnages
{
    public class Sergent : Personnage
    {
        public Sergent()
        {
            puissance = 4;
            type = "sergent";
        }
        
        public override string ToString()
        {
            return "Sergent";
        }
    }
}