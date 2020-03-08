using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public class Eclaireur : Personnage
    {
        public Eclaireur()
        {
            puissance = 2;
                
            type = "eclaireur";
        }
        
        // deserialise
        public Eclaireur(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }

        public override void Hydrate(int id,  int deplacement, Point point, bool equipe)
        {
            base.Hydrate(id, deplacement, point, equipe);
            this.deplacement = deplacement - 1;
        }

        public override string ToString()
        {
            return "Eclaireur";
        }
    }
}