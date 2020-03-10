using System.Drawing;
using System.Windows.Forms;

namespace Stratego.UserControls
{
	public partial class PanelBorder : Panel
	{
		private Color colorBorder = Color.Chocolate;
		private Pen pinceau;
		private int epaisseur;

		public PanelBorder() : base()
		{
			this.SetStyle(ControlStyles.UserPaint, true);
			epaisseur = 5;
			pinceau = new Pen(new SolidBrush(colorBorder), epaisseur);

			BackColor = Color.FromArgb(247, 211, 165);

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawRectangle(pinceau, e.ClipRectangle);
		}

		private void SetPinceau()
		{
			pinceau = new Pen(new SolidBrush(colorBorder), epaisseur);
		}

		public int Epaisseur
		{
			get => epaisseur;
			set
			{
				epaisseur = value;
				SetPinceau();
			}
		}

		public Color BorderColor
		{
			get { return colorBorder; }
			set
			{
				colorBorder = value;
				SetPinceau();
			}
		}
	}
}
