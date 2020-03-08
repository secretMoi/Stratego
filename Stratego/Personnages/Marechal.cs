using System;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Marechal : Personnage
    {
        public Marechal()
        {
            puissance = 10;
            type = "marechal";
        }
        
        public Marechal(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }
        
        public override string ToString()
        {
            return "Maréchal";
        }
    }
}