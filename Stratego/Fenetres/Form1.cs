using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Stratego.Core;
using Stratego.Models;
using Stratego.Personnages;
using Stratego.Reseau.Clients;
using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;
using Stratego.Reseau.Serveurs;
using Stratego.UserControls;

//todo fin de partie si aucune pièce ne peut bouger
namespace Stratego.Fenetres
{
	public partial class Form1 : Form
	{
		private PartieActuelle partieActuelle;
		private Point positionOrigine; // position de départ de la pièce déplacée
		private bool sonActive;
		private MusiqueFond musiqueFond;
		public static Form1 Form;
		private TcpConnection _tcpConnexion;

		public Form1()
		{
			InitializeComponent();
			partieActuelle = new PartieActuelle(pictureBox1);

			if (partieActuelle.Option.GetOption("AfficherHistorique") == false.ToString())
				richTextBox1.Visible = false;

			sonActive = Convert.ToBoolean(partieActuelle.Option.GetOption("EtatSon"));

			MusiqueFond();

			Form = this;
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void evenement_Click(object sender, EventArgs e)
		{
			string nom = ((ToolStripMenuItem) sender).Name; // récupère le nom du controle appelant
			string[] chaine = nom.Split('_'); // scinde le nom pour avoir les 2 parties

			string @namespace = GetType().Namespace;
			string @class = chaine[1];

			// équivalent var typeClasse = Type.GetType(String.Format("{0}.{1}", @namespace, @class));
			var typeClasse = Type.GetType($"{@namespace}.{@class}"); // trouve la classe
			if (typeClasse == null) return; // quitte si la classe est introuvable
			Form fenetre = Activator.CreateInstance(typeClasse) as Form; // instancie un objet

			fenetre?.Show(); // Affiche la fenêtre
		}

		private void FenetreOptions(object sender, EventArgs e)
		{
			using (Options form = new Options())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					richTextBox1.Visible = form.EtatHistortique; // set l'état de la richtextbox

					if (sonActive && !form.EtatSon) // si le son est coupé
						musiqueFond?.Stop(); // on coupe la musique
					if (!sonActive && form.EtatSon)
						MusiqueFond(true);
					if(musiqueFond?.SonEnCours != partieActuelle.Option.GetOption("SonFond"))
						MusiqueFond();
					sonActive = form.EtatSon; // set l'état du son
				}
			}
		}

		private void Partie(object sender, EventArgs e)
		{
			string nom = ((ToolStripMenuItem) sender).Name; // récupère le nom du controle appelant
			string resultat;
			FileStream fichierSauvegarde;

			try
			{
				// Use a BinaryFormatter or SoapFormatter.
				IFormatter formatter = new BinaryFormatter();

				if (nom.Contains("Reprendre"))
				{
					fichierSauvegarde = new FileStream(partieActuelle.Option.GetOption("EmplacementSauvegarde"), FileMode.Open);
					PartieActuelle ancienJeu = (PartieActuelle)formatter.Deserialize(fichierSauvegarde);

					partieActuelle = ancienJeu;

					if (!partieActuelle.MenuContextuel.PlacementPieces)
					{
						buttonRemplir.Enabled = false;
						buttonRemplir.Visible = false;
					}

					resultat = @"Partie restaurée";

					pictureBox1.Invalidate();
				}
				else if(nom.Contains("Sauvegarder"))
				{
					if (partieActuelle.MenuContextuel.PlacementPieces)
					{
						DialogBox.Show(@"Vous devez lancer la partie pour la sauvegarder !");
						return;
					}

					fichierSauvegarde = new FileStream(partieActuelle.Option.GetOption("EmplacementSauvegarde"), FileMode.Create);
					formatter.Serialize(fichierSauvegarde, partieActuelle);

					resultat = @"Partie sauvegardée";
				}
				else
					return;

				fichierSauvegarde.Close();

				DialogBox.Show(resultat);
			}
			catch (ApplicationException caught)
			{
				DialogBox.Show(caught.Source);
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
		{
			if (partieActuelle.MenuContextuel.PlacementPieces) return; // si la pièce n'est pas sélectionnée ce n'est pas la peine de continuer

			partieActuelle.Jeu.LachePiece(e.Location, positionOrigine, richTextBox1);

			Son("drop");

			pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if(partieActuelle.MenuContextuel.PlacementPieces) return;

			partieActuelle.Jeu.BougePiece(e.Location);

			pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
		{
			partieActuelle.Jeu.PrisePiece(ref positionOrigine, e.Location, partieActuelle.MenuContextuel.PlacementPieces);
			partieActuelle.MenuContextuel.PositionOrigine = positionOrigine;

			Son("pick");
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				e.Graphics.DrawImage(partieActuelle.Jeu.Map.Fond, partieActuelle.AireJeu.Rect); // repeint la grille

				partieActuelle.Jeu.DessinePieces(e.Graphics);
			}
			catch (Exception exception)
			{
				Catcher.LogError(exception.Message);
			}
			
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData != Keys.F10) return false;

			partieActuelle.Jeu.CheatMode = !partieActuelle.Jeu.CheatMode;
			pictureBox1.Invalidate();


			return true;
		}

		private void Quitter_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			// todo réactiver
			/*e.Cancel = DialogBox.ShowYesNo(this,
				@"Souhaitez-vous quitter ?" + Environment.NewLine + @"Toute partie non sauvegardée sera perdue...",
				@"Quitter"
			) != DialogResult.Yes;*/
		}

		private void buttonRemplir_Click_1(object sender, EventArgs e)
		{
			Point caseCourante = new Point(0, Map.CasesY - 1); // position de la pièce à placer
			List<Point> listeCases = new List<Point>(40); // liste les coordonnées des cases disponibles
			Random positionAleatoire = new Random();
			int positionChoisie; // id du point de la liste sélectionné aléatoirement
			if (buttonRemplir.Text.Contains("rouges"))
				caseCourante.Y = 3;

			// création de la liste
			for (int i = 0; i < 40; i++)
			{
				if (partieActuelle.Jeu.Map.GetPiece(caseCourante) == null)
					listeCases.Add(caseCourante);

				caseCourante.X++;

				if (caseCourante.X == 10)
				{
					caseCourante.X = 0;
					caseCourante.Y--;
				}
			}

			// génère une pièce pour chaque Point de la liste
			foreach (KeyValuePair<string, int> piece in partieActuelle.MenuContextuel.PiecesRestantes())
			{
				for (int repetitionPiece = 0; repetitionPiece < piece.Value; repetitionPiece++)
				{
					positionChoisie = positionAleatoire.Next(listeCases.Count);

					partieActuelle.Jeu.GenereUnePiece(piece.Key, listeCases[positionChoisie]);

					listeCases.RemoveAt(positionChoisie);
				}
			}

			partieActuelle.MenuContextuel.GenereMenu();

			// si le bouton s'applique aux rouges
			if (buttonRemplir.Text.Contains("rouges"))
			{
				buttonRemplir.Enabled = false;
				buttonRemplir.Visible = false;
				partieActuelle.MenuContextuel.DesactiveMenuContextuel();
			}
			else // sinon aux bleus
				buttonRemplir.Text = buttonRemplir.Text.Replace("bleus", "rouges");

			pictureBox1.Invalidate();
		}

		private void Son(string nom)
		{
			if (sonActive)
			{
				Son son = new Son(nom);
				son.Joue();
			}
		}

		private void MusiqueFond(bool force = false)
		{
			if (sonActive || force)
			{
				musiqueFond?.Stop();

				musiqueFond = new MusiqueFond(partieActuelle.Option.GetOption("SonFond"));
				musiqueFond.Joue();
			}
		}

		public async void SetConnection<T>(T connection) where T : Tcp
		{
			var test = HobbyToFormModel<T>.TcpConnection;
			if (HobbyToFormModel<T>.TcpConnection is ServerTcpController)
				_tcpConnexion = new TcpConnection
				{
					Server = HobbyToFormModel<T>.TcpConnection as ServerTcpController,
					Type = TcpConnection.TcpType.Server
				};
			else if(HobbyToFormModel<T>.TcpConnection is ClientTcpController)
			{
				_tcpConnexion = new TcpConnection
				{
					Client = HobbyToFormModel<T>.TcpConnection as ClientTcpController,
					Type = TcpConnection.TcpType.Client
				};
			}
			else
			{
				DialogBox.Show("Erreur lors de la communication entre les joueurs.");
				return;
			}
			var test2 = connection;

			DialogBox.Show("Vous êtes maintenant connecté avec l'autre joueur !");

			if (_tcpConnexion.Server != null) // si on est le serveur
			{
				await _tcpConnexion.Server.ReceiveCallbackAsync<PartieActuelle>(ReceiveTurn);
			}

			partieActuelle.Jeu.ChangeTurnCallback = ChangeTurn;
		}

		/**
		 * <summary>Méthode à appeler à chaque changement de tour</summary>
		 */
		public async void ChangeTurn()
		{
			TurnModel model = new TurnModel
			{
				Map = partieActuelle.Jeu.Map,
				ListePieces = partieActuelle.Jeu.ListePieces,
				PositionPieces = partieActuelle.Jeu.PositionPieces
			};

			if (_tcpConnexion.Server != null) // si on est le serveur
			{
				await _tcpConnexion.Server.SendAsync(partieActuelle);
				await _tcpConnexion.Server.ReceiveCallbackAsync<PartieActuelle>(ReceiveTurn);
			}
			else
			{
				await _tcpConnexion.Client.SendAsync(partieActuelle);
				await _tcpConnexion.Client.ReceiveCallbackAsync<PartieActuelle>(ReceiveTurn);
			}

			pictureBox1.Invalidate();
		}

		private void ReceiveTurn(PartieActuelle model)
		{
			/*partieActuelle.Jeu.Map = model.Map;
			partieActuelle.Jeu.ListePieces = model.ListePieces;
			partieActuelle.Jeu.PositionPieces = model.PositionPieces;*/
			if (model == null)
			{
				DialogBox.Show("Erreur lors de la communication");
				return;
			}
			partieActuelle = model;
		}
	}
}
