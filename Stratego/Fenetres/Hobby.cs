﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Stratego.Reseau.Models;
using Stratego.Reseau.Serveurs;

namespace Stratego.Fenetres
{
	public partial class Hobby : Form
	{
		private readonly ServeurController _serveur = new ServeurController();
		private readonly IList<string> _tokensDiscovered = new List<string>();

		public Hobby()
		{
			InitializeComponent();

			listBoxServersList.DisplayMember = "MachineName";
		}

		private async void Hobby_Load(object sender, EventArgs e)
		{
			await _serveur.ReceiveBroadCastAsync(AddItem);
		}

		public void AddItem(InitModel result)
		{
			if (_tokensDiscovered.Contains(result.Token)) return;

			_tokensDiscovered.Add(result.Token);
			listBoxServersList.Items.Add(result);
		}

		private void Hobby_FormClosing(object sender, FormClosingEventArgs e)
		{
			_serveur.State = false;
		}

		private void listBoxServersList_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitModel joueur2 = listBoxServersList.SelectedItem as InitModel;

			buttonConnect.Text = @"Se connecter à " + joueur2?.MachineName;

		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			InitModel joueur2 = listBoxServersList.SelectedItem as InitModel;

			if (joueur2 == null)
			{
				MessageBox.Show(@"Veuillez sélectionner un serveur.");
				return;
			}

			// ferme les connexions udp
			_serveur.State = false;

			// on est le client vu qu'on va demander au serveur (joueur2) de se connecter

		}
	}
}
