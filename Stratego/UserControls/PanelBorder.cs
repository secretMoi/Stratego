using System.Drawing;
using System.Windows.Forms;

namespace Stratego.UserControls
{
	public sealed partial class PanelBorder : Panel
	{
		private Color colorBorder = Theme.CouleurTexte;
		private Pen pinceau;
		private int epaisseur;

		public PanelBorder()
		{
			this.SetStyle(ControlStyles.UserPaint, true);
			epaisseur = 5;
			pinceau = new Pen(new SolidBrush(colorBorder), epaisseur);

			BackColor = Theme.CouleurFondLight;

		}

		protected override void OnPaint(PaintEventArgs e)
		{

			/*e.Graphics.DrawRectangle(
				pinceau,
				0,
				0,
				Width,
				Height);*/
			base.OnPaint(e);
			e.Graphics.DrawRectangle(pinceau, ClientRectangle);
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
			get => colorBorder;
			set
			{
				colorBorder = value;
				SetPinceau();
			}
		}
	}
}
