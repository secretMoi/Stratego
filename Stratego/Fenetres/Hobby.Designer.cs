namespace Stratego.Fenetres
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
			this.SuspendLayout();
			// 
			// listBoxServersList
			// 
			this.listBoxServersList.FormattingEnabled = true;
			this.listBoxServersList.Location = new System.Drawing.Point(12, 12);
			this.listBoxServersList.Name = "listBoxServersList";
			this.listBoxServersList.Size = new System.Drawing.Size(776, 225);
			this.listBoxServersList.TabIndex = 0;
			// 
			// Hobby
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.listBoxServersList);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Hobby";
			this.Text = "Hobby";
			this.Load += new System.EventHandler(this.Hobby_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxServersList;
	}
}