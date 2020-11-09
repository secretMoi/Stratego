namespace Stratego.Fenetres
{
    partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuProgramme = new System.Windows.Forms.ToolStripMenuItem();
			this.menuProgramme_Sauvegarder = new System.Windows.Forms.ToolStripMenuItem();
			this.menuProgramme_Reprendre = new System.Windows.Forms.ToolStripMenuItem();
			this.menuProgramme_Hobby = new System.Windows.Forms.ToolStripMenuItem();
			this.menuProgramme_Options = new System.Windows.Forms.ToolStripMenuItem();
			this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonRemplir = new Stratego.UserControls.Bouton();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(0, 23);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(705, 790);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(892, 716);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 20);
			this.label1.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(892, 752);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(249, 20);
			this.label2.TabIndex = 3;
			// 
			// richTextBox1
			// 
			this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox1.ForeColor = System.Drawing.Color.Chocolate;
			this.richTextBox1.Location = new System.Drawing.Point(710, 23);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(432, 546);
			this.richTextBox1.TabIndex = 5;
			this.richTextBox1.Text = "";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProgramme});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1145, 24);
			this.menuStrip1.TabIndex = 9;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuProgramme
			// 
			this.menuProgramme.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProgramme_Sauvegarder,
            this.menuProgramme_Reprendre,
            this.menuProgramme_Hobby,
            this.menuProgramme_Options,
            this.quitterToolStripMenuItem});
			this.menuProgramme.Name = "menuProgramme";
			this.menuProgramme.Size = new System.Drawing.Size(82, 20);
			this.menuProgramme.Text = "Programme";
			// 
			// menuProgramme_Sauvegarder
			// 
			this.menuProgramme_Sauvegarder.Name = "menuProgramme_Sauvegarder";
			this.menuProgramme_Sauvegarder.Size = new System.Drawing.Size(172, 22);
			this.menuProgramme_Sauvegarder.Text = "Sauvegarder partie";
			this.menuProgramme_Sauvegarder.Click += new System.EventHandler(this.Partie);
			// 
			// menuProgramme_Reprendre
			// 
			this.menuProgramme_Reprendre.Name = "menuProgramme_Reprendre";
			this.menuProgramme_Reprendre.Size = new System.Drawing.Size(172, 22);
			this.menuProgramme_Reprendre.Text = "Reprendre partie";
			this.menuProgramme_Reprendre.Click += new System.EventHandler(this.Partie);
			// 
			// menuProgramme_Hobby
			// 
			this.menuProgramme_Hobby.Name = "menuProgramme_Hobby";
			this.menuProgramme_Hobby.Size = new System.Drawing.Size(172, 22);
			this.menuProgramme_Hobby.Text = "Hobby";
			this.menuProgramme_Hobby.Click += new System.EventHandler(this.evenement_Click);
			// 
			// menuProgramme_Options
			// 
			this.menuProgramme_Options.Name = "menuProgramme_Options";
			this.menuProgramme_Options.Size = new System.Drawing.Size(172, 22);
			this.menuProgramme_Options.Text = "Options";
			this.menuProgramme_Options.Click += new System.EventHandler(this.FenetreOptions);
			// 
			// quitterToolStripMenuItem
			// 
			this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
			this.quitterToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.quitterToolStripMenuItem.Text = "Quitter";
			this.quitterToolStripMenuItem.Click += new System.EventHandler(this.Quitter_Click);
			// 
			// buttonRemplir
			// 
			this.buttonRemplir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.buttonRemplir.FlatAppearance.BorderSize = 2;
			this.buttonRemplir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonRemplir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonRemplir.ForeColor = System.Drawing.Color.Chocolate;
			this.buttonRemplir.Location = new System.Drawing.Point(710, 575);
			this.buttonRemplir.Name = "buttonRemplir";
			this.buttonRemplir.Size = new System.Drawing.Size(152, 48);
			this.buttonRemplir.TabIndex = 10;
			this.buttonRemplir.Text = "Remplir bleus";
			this.buttonRemplir.UseVisualStyleBackColor = false;
			this.buttonRemplir.Click += new System.EventHandler(this.buttonRemplir_Click_1);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.ClientSize = new System.Drawing.Size(1145, 813);
			this.Controls.Add(this.buttonRemplir);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Stratego";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme_Sauvegarder;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme_Options;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme_Reprendre;
		private UserControls.Bouton buttonRemplir;
		private System.Windows.Forms.ToolStripMenuItem menuProgramme_Hobby;
	}
}