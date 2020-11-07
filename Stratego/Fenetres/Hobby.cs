using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stratego.Reseau.Clients;

namespace Stratego.Fenetres
{
	public partial class Hobby : Form
	{
		public Hobby()
		{
			InitializeComponent();
		}

		private async void Hobby_Load(object sender, EventArgs e)
		{
			List<Task<string>> tasks = new List<Task<string>>();
			ClientController client = new ClientController();

			for (int i = 1; i < 255; i++)
			{
				string ip = "192.168.1." + i;
				tasks.Add( client.PingTask(ip));
			}

			var res = await Task.WhenAll(tasks);

			foreach (var hostname in res)
			{
				if (hostname != null)
					listBoxServersList.Items.Add(hostname);
			}

			MessageBox.Show(@"Scan terminé");
		}
	}
}
