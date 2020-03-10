using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace Stratego.Fenetres
{
	
    public partial class Options : Form
    {
	    private Color couleurFond = Color.FromArgb(218, 184, 133);
	    private Color couleurFondLight = Color.FromArgb(247, 211, 165);


        public Options()
        {
            InitializeComponent();

            BackColor = couleurFond;

            /*panelBorderSauvegarde.BackColor = couleurFondLight;
            panelBorderGraphique.BackColor = couleurFondLight;
            panelBorderSauvegarde.BackColor = couleurFondLight;
            panelBorderSon.BackColor = couleurFondLight;

            panelBorderSauvegarde.BorderColor = Color.Chocolate;*/
        }

        
    }
}