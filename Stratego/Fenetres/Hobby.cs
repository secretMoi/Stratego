using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Stratego.Models;
using Stratego.Reseau;
using Stratego.Reseau.Clients;
using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;
using Stratego.Reseau.Serveurs;

namespace Stratego.Fenetres
{
	public partial class Hobby : Form
	{
		private Broadcast serveurBroadcast;
		private Broadcast _clientBroadcastServer;

		private readonly ServerTcpController serverTcp = new ServerTcpController();
		private readonly IList<string> _tokensDiscovered = new List<string>();

		public Hobby()
		{
			InitializeComponent();

			listBoxServersList.DisplayMember = "MachineName";
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
				serveurBroadcast.ServerState = false;

			if(_clientBroadcastServer != null)
				_clientBroadcastServer.ServerState = false;
		}

		private void listBoxServersList_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitModel joueur2 = listBoxServersList.SelectedItem as InitModel;

			buttonConnect.Text = @"Se connecter à " + joueur2?.MachineName;

		}

		private async void buttonConnect_Click(object sender, EventArgs e)
		{
			InitModel joueur2 = listBoxServersList.SelectedItem as InitModel;

			if (joueur2 == null)
			{
				MessageBox.Show(@"Veuillez sélectionner un serveur.");
				return;
			}

			_clientBroadcastServer.EndBroadcast(); // ferme les req broadcast

			// démarre le client tcp
			ClientTcpController client = new ClientTcpController();
			await client.ConnectAsync(new IPEndPoint(Reseau.Reseau.GetLocalIpAddress(), 32430));
			//await client.SendAsync(GetInitModel(_clientBroadcastServer.Udp.Token));

			PassConnectionToForm(client, GetInitModel(_clientBroadcastServer.Udp.Token));
		}

		private async void buttonServer_Click(object sender, EventArgs e)
		{
			serveurBroadcast = new Broadcast(new Udp(32430));
			await serveurBroadcast.ReceiveBroadCastAsync(RespondToClient);

			bool res = await serverTcp.ListenAsync(new IPEndPoint(Reseau.Reseau.GetLocalIpAddress(), 32430));
			if (!res)
			{
				MessageBox.Show(@"Impossible de démarrer le serveur TCP");
			}
			else
			{
				//var t = await serverTcp.ReceiveAsync<InitModel>();

				PassConnectionToForm(serverTcp, null);
			}
		}

		private async void RespondToClient(InitModel initModel)
		{
			await serveurBroadcast.Udp.SendAsync(initModel, initModel.Address);

			serveurBroadcast.ServerState = false;
		}

		private async void buttonClient_Click(object sender, EventArgs e)
		{
			_clientBroadcastServer = new Broadcast(new Udp(32530));

			Udp udp = new Udp(32430);
			var model = GetInitModel(udp.Token);

			_clientBroadcastServer.LaunchBroadcast(model, 32430); // lance le broadcast sur les serveurs

			await _clientBroadcastServer.ReceiveBroadCastAsync(AddItem); // écoute les réponses
		}

		private InitModel GetInitModel(string token)
		{
			return new InitModel // données du client
			{
				Address = new IPEndPoint(Reseau.Reseau.GetLocalIpAddress(), 32530),
				MachineName = Environment.MachineName,
				Token = token
			};
		}

		private void PassConnectionToForm<T>(T connection, InitModel model) where T : Tcp
		{
			HobbyToFormModel<T>.TcpConnection = connection;
			HobbyToFormModel<T>.InitModel = model;

			Form1.Form.SetConnection(connection);
			Close();
		}
	}
}
