using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Bombe : Personnage
    {
        public Bombe()
        {
            puissance = 11;
            type = "bombe";
            deplacement = 0;
        }
        
        // deserialise
        public Bombe(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }

        public override string ToString()
        {
            return "Bombe";
        }
    }
}