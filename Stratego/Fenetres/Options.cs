using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Stratego.Core;

namespace Stratego.Fenetres
{
	
    public partial class Options : Form
    {
	    private readonly Stratego.Options options;
	    private readonly Dictionary<string, string> optionsTemporaires;

        public Options()
        {
            InitializeComponent();

            options = Stratego.Options.Instance;

            optionsTemporaires = new Dictionary<string, string>();

            AjouteOptionTemporaire("EmplacementSauvegarde");
            AjouteOptionTemporaire("EmplacementPiece");
            AjouteOptionTemporaire("AfficherHistorique");
            AjouteOptionTemporaire("EtatSon");
            AjouteOptionTemporaire("SonFond");


            textBoxEmplacementSauvegarde.Text = optionsTemporaires["EmplacementSauvegarde"];
            textBoxEmplacementPiece.Text = optionsTemporaires["EmplacementPiece"];
            checkBoxHistorique.Checked = Convert.ToBoolean(optionsTemporaires["AfficherHistorique"]);
            checkBoxSon.Checked = Convert.ToBoolean(optionsTemporaires["EtatSon"]);

            TexteCheckBox();
            TexteCheckBoxSon();
            RemplitListSonsFond();
        }

        private void RemplitListSonsFond()
        {
	        foreach (string son in MusiqueFond.ListeSons())
		        listViewSonsFond.Items.Add(son);
        }

        private void AjouteOptionTemporaire(string nom)
        {
	        optionsTemporaires.Add(nom, options.GetOption(nom));
        }

        private void boutonOpenSauvegarde_Click(object sender, EventArgs e)
        {
            FileGUI ouvrir = new FileGUI();
            ouvrir.AddFilter("Fichier de sauvegarde", "sav");

	        if (ouvrir.ShowDialog() == DialogResult.OK)
		        textBoxEmplacementSauvegarde.Text = CutPath(ouvrir.FileName);
        }

        private void boutonEmplacementPiece_Click(object sender, EventArgs e)
        {
	        FileGUI ouvrir = new FileGUI();
	        ouvrir.AddFilter("Fichier de pièces", "xml");

	        if (ouvrir.ShowDialog() == DialogResult.OK)
		        textBoxEmplacementPiece.Text = CutPath(ouvrir.FileName);
        }

        private string CutPath(string path)
        {
            string[] paths = path.Split('\\');
            return paths[paths.Length - 1];
        }

        private void boutonAnnuler_Click(object sender, EventArgs e)
        {
	        DialogResult = DialogResult.Cancel;

            Close();
        }

        private void boutonConfirmer_Click(object sender, EventArgs e)
        {
	        DialogResult = DialogResult.OK;

	        EtatHistortique = checkBoxHistorique.Checked;
	        EtatSon = checkBoxSon.Checked;
	        EtatMusiqueFond = optionsTemporaires["SonFond"];

	        foreach (KeyValuePair<string, string> option in optionsTemporaires)
		        options.SetOption(option.Key, option.Value);

            options.Enregistre();
            Close();
        }

        private void TexteCheckBox()
        {
	        if (bool.Parse(optionsTemporaires["AfficherHistorique"]))
		        checkBoxHistorique.Text = @"Affiché";
            else
		        checkBoxHistorique.Text = @"Caché";
        }

        private void TexteCheckBoxSon()
        {
	        if (bool.Parse(optionsTemporaires["EtatSon"]))
		        checkBoxSon.Text = @"Oui";
	        else
		        checkBoxSon.Text = @"Non";
        }

        private void checkBoxHistorique_CheckedChanged(object sender, EventArgs e)
        {
	        optionsTemporaires["AfficherHistorique"] = checkBoxHistorique.Checked.ToString();
            TexteCheckBox();
        }

        private void checkBoxSon_CheckedChanged(object sender, EventArgs e)
        {
	        optionsTemporaires["EtatSon"] = checkBoxSon.Checked.ToString();
	        TexteCheckBoxSon();
        }

        private void listViewSonsFond_SelectedIndexChanged(object sender, EventArgs e)
        {
	        if (listViewSonsFond.SelectedItems.Count > 0)
		        optionsTemporaires["SonFond"] = listViewSonsFond.SelectedItems[0].Text;
        }

        public bool EtatHistortique { get; private set; }
        public bool EtatSon { get; private set; }
        public string EtatMusiqueFond { get; private set; }
    }
}