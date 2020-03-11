using System.Drawing;
using System.Windows.Forms;

namespace Stratego.UserControls
{
	public partial class Bouton : Button
	{
		public Bouton()
		{
			InitializeComponent();

			BackColor = SystemColors.ControlLight;
			FlatAppearance.BorderSize = 2;
			FlatStyle = FlatStyle.Flat;
			Font = new Font(
				"Microsoft Sans Serif",
				12F,
				FontStyle.Regular,
				GraphicsUnit.Point,
				((byte)(0))
			);
			ForeColor = Theme.CouleurTexte;
			BackColor = Theme.CouleurFond;
			UseVisualStyleBackColor = false;
		}
	}
}
