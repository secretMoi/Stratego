using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Espion : Personnage
    {
        public Espion()
        {
            puissance = 1;
            type = "espion";
        }
        
        // deserialise
        public Espion(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
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