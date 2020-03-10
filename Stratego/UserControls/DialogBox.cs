using System;
using System.Drawing;
using System.Windows.Forms;

namespace Stratego.UserControls
{
	public partial class DialogBox : Form
	{
		private Size taille = new Size(150, 50);
		private static DialogResult resultat;
		private static TypeFenetre type;

		private enum TypeFenetre
		{
			Ok, YesNo
		}

		private DialogBox(string texte, string titre = null)
		{
			InitializeComponent();

			if (titre != null)
				Text = titre;
			label1.Text = texte;

			switch (type)
			{
				case TypeFenetre.Ok:
					CreerOkButton();
					break;
				case TypeFenetre.YesNo:
					CreerOuiNonBoutons();
					break;
				default:
					Close();
					break;
			}
		}

		public static void Show(string texte, string titre = null)
		{
			type = TypeFenetre.Ok;

			// using assure que les ressources seront libérées à la fermeture de la fenêtre
			using (DialogBox form = new DialogBox(texte, titre))
			{
				form.ShowDialog();
			}
		}

		public static DialogResult ShowYesNo(string texte, string titre = null)
		{
			type = TypeFenetre.YesNo;

			// using assure que les ressources seront libérées à la fermeture de la fenêtre
			using (DialogBox form = new DialogBox(texte, titre))
			{
				form.ShowDialog();
			}

			return resultat;
		}

		private void CreerOuiNonBoutons()
		{
			Point position = new Point(
				ClientSize.Width / 4 - taille.Width / 2,
				ClientSize.Height - taille.Height - 15
			);
			Button boutonOui = StyleBouton(position);

			boutonOui.Text = @"Oui";
			boutonOui.Click += (sender, args) =>
			{
				resultat = DialogResult.Yes;
				Action_Fermeture(sender, args);
			};

			Controls.Add(boutonOui);

			position = new Point(
				ClientSize.Width * 3 / 4 - taille.Width / 2,
				ClientSize.Height - taille.Height - 15
			);
			Button boutonNon = StyleBouton(position);

			boutonNon.Text = @"Non";
			boutonNon.Click += (sender, args) =>
			{
				resultat = DialogResult.No;
				Action_Fermeture(sender, args);
			};

			Controls.Add(boutonNon);
		}

		private void CreerOkButton()
		{
			Point position = new Point(
				(ClientSize.Width - taille.Width) / 2,
				ClientSize.Height - taille.Height - 15
			);
			Button boutonOk = StyleBouton(position);

			boutonOk.Text = @"OK";
			boutonOk.Click += Action_Fermeture;

			this.Controls.Add(boutonOk);
		}

		private Button StyleBouton(Point position)
		{
			Button bouton = new Button();

			bouton.BackColor = SystemColors.ControlLight;
			bouton.FlatAppearance.BorderSize = 2;
			bouton.FlatStyle = FlatStyle.Flat;
			bouton.Size = taille;
			bouton.Location = position;
			bouton.Font = new Font(
				"Microsoft Sans Serif",
				12F,
				FontStyle.Regular,
				GraphicsUnit.Point,
				((byte)(0))
			);
			bouton.ForeColor = Color.Chocolate;
			bouton.BackColor = Color.FromArgb(218, 184, 133);
			bouton.UseVisualStyleBackColor = false;

			return bouton;
		}

		public sealed override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		private void Action_Fermeture(object sender, EventArgs eventArgs)
		{
			Close();
		}
	}
}
