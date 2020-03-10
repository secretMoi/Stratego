using System;
using System.Windows.Forms;
using Stratego.Core;

namespace Stratego.Fenetres
{
	
    public partial class Options : Form
    {
	    private Stratego.Options options;
        public Options()
        {
            InitializeComponent();

            options = Stratego.Options.Instance;

            textBoxEmplacementSauvegarde.Text = options.GetOption("EmplacementSauvegarde");
            textBoxEmplacementPiece.Text = options.GetOption("EmplacementPiece");
            checkBoxHistorique.Checked = Convert.ToBoolean(options.GetOption("AfficherHistorique"));
        }

        private void boutonOpenSauvegarde_Click(object sender, EventArgs e)
        {
            FileGUI ouvrir = new FileGUI();
            ouvrir.AddFilter("Fichier de sauvegarde", "sav");

	        if (ouvrir.ShowDialog() == DialogResult.OK)
		        textBoxEmplacementSauvegarde.Text = ouvrir.FileName;
        }

        private void boutonEmplacementPiece_Click(object sender, EventArgs e)
        {
	        FileGUI ouvrir = new FileGUI();
	        ouvrir.AddFilter("Fichier de pièces", "xml");

	        if (ouvrir.ShowDialog() == DialogResult.OK)
		        textBoxEmplacementPiece.Text = ouvrir.FileName;
        }

        private void boutonAnnuler_Click(object sender, EventArgs e)
        {
	        Close();
        }

        private void boutonConfirmer_Click(object sender, EventArgs e)
        {
	        options.SetOption("EmplacementSauvegarde", textBoxEmplacementSauvegarde.Text);
	        options.SetOption("EmplacementPiece", textBoxEmplacementPiece.Text);
	        options.SetOption("AfficherHistorique", checkBoxHistorique.Checked.ToString());

            options.Enregistre();
            Close();
        }
    }
}