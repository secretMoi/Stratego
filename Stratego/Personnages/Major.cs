using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Major : Personnage
    {
        public Major()
        {
            puissance = 7;
            type = "major";
        }
        
        // deserialise
        public Major(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Major";
        }
    }
}