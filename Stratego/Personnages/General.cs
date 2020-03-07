namespace Stratego.Personnages
{
    public class General : Personnage
    {
        public General()
        {
            puissance = 9;
            type = "general";
        }
        
        public override string ToString()
        {
            return "Général";
        }
    }
}