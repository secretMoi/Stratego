using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Lieutenant : Personnage
    {
        public Lieutenant()
        {
            puissance = 5;
            type = "lieutenant";
        }
        
        // deserialise
        public Lieutenant(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Lieutenant";
        }
    }
}