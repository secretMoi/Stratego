using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Sergent : Personnage
    {
        public Sergent()
        {
            puissance = 4;
            type = "sergent";
        }
        
        // deserialise
        public Sergent(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Sergent";
        }
    }
}