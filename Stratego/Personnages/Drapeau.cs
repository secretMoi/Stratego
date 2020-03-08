using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Drapeau : Personnage
    {
        public Drapeau()
        {
            puissance = 0;
            type = "drapeau";
            deplacement = 0;
        }
        
        // deserialise
        public Drapeau(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Drapeau";
        }
    }
}