using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Stratego.Reseau;
using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;

namespace Stratego.Fenetres
{
	public partial class Hobby : Form
	{
		private Broadcast serveurBroadcast;
		private Broadcast _clientBroadcastServer;

		//private readonly ServeurTcpController _serveurTcp = new ServeurTcpController();
		private readonly IList<string> _tokensDiscovered = new List<string>();

		public Hobby()
		{
			InitializeComponent();

			listBoxServersList.DisplayMember = "MachineName";
		}

		private void Hobby_Load(object sender, EventArgs e)
		{
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

			// stop le broadcasting lorsque le serveur répond
			_clientBroadcastServer.ServerState = false;
		}

		private void Hobby_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(serveurBroadcast != null)
				serveurBroadcast.ServerState = false;

			if(_clientBroadcastServer != null)
				_clientBroadcastServer.ServerState = false;
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
			serveurBroadcast = new Broadcast(new Udp(32430));
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
			await serveurBroadcast.Udp.SendAsync(initModel, initModel.Address);
		}

		private async void buttonClient_Click(object sender, EventArgs e)
		{
			_clientBroadcastServer = new Broadcast(new Udp(32530));

			Udp udp = new Udp(32430);
			Broadcast broadcast = new Broadcast(udp);
			InitModel model = new InitModel // données du client
			{
				Address = new IPEndPoint(Reseau.Reseau.GetLocalIpAddress(), 32530),
				MachineName = Environment.MachineName,
				Token = udp.Token
			};
			broadcast.LaunchBroadcast(model, 32430); // lance le broadcast sur les serveurs

			await _clientBroadcastServer.ReceiveBroadCastAsync(AddItem); // écoute les réponses

			broadcast.EndBroadcast(); // ferme les req broadcast
		}
	}
}
