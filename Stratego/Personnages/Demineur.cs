using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Demineur : Personnage
    {
        public Demineur()
        {
            puissance = 3;
            type = "demineur";
        }
        
        // deserialise
        public Demineur(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }

        public override int Collision(Personnage attaquant, Personnage defenseur)
        {
            int resultat = base.Collision(attaquant, defenseur);

            if (defenseur is Bombe)
                resultat = Attaquant;

            return resultat;
        }
        
        public override string ToString()
        {
            return "Démineur";
        }
    }
}