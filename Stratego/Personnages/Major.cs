namespace Stratego.Personnages
{
    public class Major : Personnage
    {
        public Major()
        {
            puissance = 7;
            type = "major";
        }
        
        public override string ToString()
        {
            return "Major";
        }
    }
}