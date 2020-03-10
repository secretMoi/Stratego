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
			ForeColor = Color.Chocolate;
			BackColor = Color.FromArgb(218, 184, 133);
			UseVisualStyleBackColor = false;
		}
	}
}
