using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Colonel : Personnage
    {
        public Colonel()
        {
            puissance = 8;
            type = "colonel";

            piece = new Pieces(type);
        }
        
        // deserialise
        public Colonel(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Colonel";
        }
    }
}