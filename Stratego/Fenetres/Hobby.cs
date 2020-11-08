using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Stratego.Reseau;
using Stratego.Reseau.Clients;
using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;
using Stratego.Reseau.Serveurs;

namespace Stratego.Fenetres
{
	public partial class Hobby : Form
	{
		private ServeurBroadcastController serveurBroadcast;
		Udp udp = new Udp();
		private readonly ClientBroadcastController _clientBroadcast = new ClientBroadcastController();
		//private readonly ServeurTcpController _serveurTcp = new ServeurTcpController();
		private readonly IList<string> _tokensDiscovered = new List<string>();

		public Hobby()
		{
			InitializeComponent();

			listBoxServersList.DisplayMember = "MachineName";
		}

		private void Hobby_Load(object sender, EventArgs e)
		{
			/*_clientBroadcast.LaunchBroadcast();
			await serveurBroadcast.ReceiveBroadCastAsync(AddItem);

			/*bool res = await _serveurTcp.ListenAsync(new IPEndPoint(Reseau.Reseau.GetLocalIpAddress(), 35000));
			if (!res)
			{
				MessageBox.Show(@"Impossible de démarrer le serveur TCP");
			}
			else
			{
				var t = await _serveurTcp.ReceiveAsync<InitModel>();
			}*/
		}

		public void AddItem(InitModel result)
		{
			if (_tokensDiscovered.Contains(result.Token)) return;

			_tokensDiscovered.Add(result.Token);
			listBoxServersList.Items.Add(result);
		}

		private void Hobby_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(serveurBroadcast != null)
				serveurBroadcast.State = false;
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
			//serveurBroadcast.State = false;

			// ferme le serveur
			//_serveurTcp.Close();

			// on est le client vu qu'on va demander au serveur (joueur2) de se connecter
			/*ClientTcpController client = new ClientTcpController();
			await client.ConnectAsync(joueur2);
			await client.SendAsync(joueur2);*/
		}

		private async void buttonServer_Click(object sender, EventArgs e)
		{
			serveurBroadcast = new ServeurBroadcastController(32430);
			await serveurBroadcast.ReceiveBroadCastAsync(RespondToClient);

			/*bool res = await _serveurTcp.ListenAsync(new IPEndPoint(Reseau.Reseau.GetLocalIpAddress(), 35000));
			if (!res)
			{
				MessageBox.Show(@"Impossible de démarrer le serveur TCP");
			}
			else
			{
				var t = await _serveurTcp.ReceiveAsync<InitModel>();
			}*/
		}

		private async void RespondToClient(InitModel initModel)
		{
			await udp.SendAsync(initModel, initModel.Address);
		}

		private async void buttonClient_Click(object sender, EventArgs e)
		{
			serveurBroadcast = new ServeurBroadcastController(32530);

			_clientBroadcast.LaunchBroadcast();

			await serveurBroadcast.ReceiveBroadCastAsync(AddItem);

			// stop le broadcasting lorsque le serveur répond
			serveurBroadcast.State = false;
			_clientBroadcast.EndBroadcast();

			//udp.SendAsync()
		}
	}
}
