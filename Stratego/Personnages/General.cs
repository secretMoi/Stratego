using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class General : Personnage
    {
        public General()
        {
            puissance = 9;
            type = "general";
        }
        
        // deserialise
        public General(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Général";
        }
    }
}