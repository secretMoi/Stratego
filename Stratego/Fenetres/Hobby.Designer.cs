﻿namespace Stratego.Fenetres
{
	partial class Hobby
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hobby));
			this.listBoxServersList = new System.Windows.Forms.ListBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonServer = new System.Windows.Forms.Button();
			this.buttonClient = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBoxServersList
			// 
			this.listBoxServersList.FormattingEnabled = true;
			this.listBoxServersList.Location = new System.Drawing.Point(12, 12);
			this.listBoxServersList.Name = "listBoxServersList";
			this.listBoxServersList.Size = new System.Drawing.Size(776, 225);
			this.listBoxServersList.TabIndex = 0;
			this.listBoxServersList.SelectedIndexChanged += new System.EventHandler(this.listBoxServersList_SelectedIndexChanged);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(630, 243);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(158, 49);
			this.buttonConnect.TabIndex = 1;
			this.buttonConnect.Text = "Se connecter";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonServer
			// 
			this.buttonServer.Location = new System.Drawing.Point(12, 243);
			this.buttonServer.Name = "buttonServer";
			this.buttonServer.Size = new System.Drawing.Size(158, 49);
			this.buttonServer.TabIndex = 2;
			this.buttonServer.Text = "Héberger";
			this.buttonServer.UseVisualStyleBackColor = true;
			this.buttonServer.Click += new System.EventHandler(this.buttonServer_Click);
			// 
			// buttonClient
			// 
			this.buttonClient.Location = new System.Drawing.Point(176, 243);
			this.buttonClient.Name = "buttonClient";
			this.buttonClient.Size = new System.Drawing.Size(158, 49);
			this.buttonClient.TabIndex = 3;
			this.buttonClient.Text = "Chercher";
			this.buttonClient.UseVisualStyleBackColor = true;
			this.buttonClient.Click += new System.EventHandler(this.buttonClient_Click);
			// 
			// Hobby
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.buttonClient);
			this.Controls.Add(this.buttonServer);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.listBoxServersList);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Hobby";
			this.Text = "Hobby";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Hobby_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxServersList;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonServer;
		private System.Windows.Forms.Button buttonClient;
	}
}