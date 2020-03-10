using System.Windows.Forms;

namespace Stratego.Fenetres
{
	
    public partial class Options : Form
    {
	    private Stratego.Options options;
        public Options()
        {
            InitializeComponent();
            options = new Stratego.Options();

            textBoxEmplacementSauvegarde.Text = options.GetOption("EmplacementSauvegarde");
        }

        private void buttonAnnuler_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void buttonValider_Click(object sender, System.EventArgs e)
        {

        }
    }
}