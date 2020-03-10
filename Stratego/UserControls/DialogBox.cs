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

			Bouton bouton = CreerBouton(position, @"Oui");

			bouton.Click += (sender, args) =>
			{
				resultat = DialogResult.Yes;
				Action_Fermeture(sender, args);
			};

			position = new Point(
				ClientSize.Width * 3 / 4 - taille.Width / 2,
				ClientSize.Height - taille.Height - 15
			);
			bouton = CreerBouton(position, @"Non");

			bouton.Click += (sender, args) =>
			{
				resultat = DialogResult.No;
				Action_Fermeture(sender, args);
			};
		}

		private void CreerOkButton()
		{
			Point position = new Point(
				(ClientSize.Width - taille.Width) / 2,
				ClientSize.Height - taille.Height - 15
			);

			Bouton boutonOk = CreerBouton(position, @"OK");

			boutonOk.Click += Action_Fermeture;
		}

		private Bouton CreerBouton(Point position, string texte)
		{
			Bouton bouton = new Bouton
			{
				Location = position,
				Size = taille, Text = texte
			};

			Controls.Add(bouton);

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
