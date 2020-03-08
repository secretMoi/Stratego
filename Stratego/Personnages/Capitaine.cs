using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Capitaine : Personnage
    {
        public Capitaine()
        {
            puissance = 6;
            type = "capitaine";
        }
        
        // deserialise
        public Capitaine(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Capitaine";
        }
    }
}