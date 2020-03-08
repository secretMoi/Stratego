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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonRemplir = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuProgramme = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProgramme_Sauvegarder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProgramme_Reprendre = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProgramme_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAide = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(822, 912);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1041, 826);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 23);
            this.label1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1041, 868);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 23);
            this.label2.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.Location = new System.Drawing.Point(828, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(503, 629);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(1199, 826);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Client";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1199, 865);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Serveur";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // buttonRemplir
            // 
            this.buttonRemplir.Location = new System.Drawing.Point(828, 662);
            this.buttonRemplir.Name = "buttonRemplir";
            this.buttonRemplir.Size = new System.Drawing.Size(99, 39);
            this.buttonRemplir.TabIndex = 8;
            this.buttonRemplir.Text = "Remplir bleus";
            this.buttonRemplir.UseVisualStyleBackColor = true;
            this.buttonRemplir.Click += new System.EventHandler(this.buttonRemplir_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.menuProgramme, this.menuAide, this.vueToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1336, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuProgramme
            // 
            this.menuProgramme.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuProgramme_Sauvegarder, this.menuProgramme_Reprendre, this.menuProgramme_Options,
                this.quitterToolStripMenuItem
            });
            this.menuProgramme.Name = "menuProgramme";
            this.menuProgramme.Size = new System.Drawing.Size(82, 20);
            this.menuProgramme.Text = "Programme";
            // 
            // menuProgramme_Sauvegarder
            // 
            this.menuProgramme_Sauvegarder.Name = "menuProgramme_Sauvegarder";
            this.menuProgramme_Sauvegarder.Size = new System.Drawing.Size(152, 22);
            this.menuProgramme_Sauvegarder.Text = "Sauvegarder";
            this.menuProgramme_Sauvegarder.Click += new System.EventHandler(this.evenement_Click);
            // 
            // menuProgramme_Reprendre
            // 
            this.menuProgramme_Reprendre.Name = "menuProgramme_Reprendre";
            this.menuProgramme_Reprendre.Size = new System.Drawing.Size(152, 22);
            this.menuProgramme_Reprendre.Text = "Reprendre";
            this.menuProgramme_Reprendre.Click += new System.EventHandler(this.evenement_Click);
            // 
            // menuProgramme_Options
            // 
            this.menuProgramme_Options.Name = "menuProgramme_Options";
            this.menuProgramme_Options.Size = new System.Drawing.Size(152, 22);
            this.menuProgramme_Options.Text = "Options";
            this.menuProgramme_Options.Click += new System.EventHandler(this.evenement_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.Quitter_Click);
            // 
            // menuAide
            // 
            this.menuAide.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.aProposToolStripMenuItem});
            this.menuAide.Name = "menuAide";
            this.menuAide.Size = new System.Drawing.Size(43, 20);
            this.menuAide.Text = "Aide";
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aProposToolStripMenuItem.Text = "A propos";
            this.aProposToolStripMenuItem.Click += new System.EventHandler(this.evenement_Click);
            // 
            // vueToolStripMenuItem
            // 
            this.vueToolStripMenuItem.Name = "vueToolStripMenuItem";
            this.vueToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.vueToolStripMenuItem.Text = "Vue";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 938);
            this.Controls.Add(this.buttonRemplir);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Stratego";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonRemplir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme_Sauvegarder;
        private System.Windows.Forms.ToolStripMenuItem menuAide;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme_Options;
        private System.Windows.Forms.ToolStripMenuItem menuProgramme_Reprendre;
        private System.Windows.Forms.ToolStripMenuItem vueToolStripMenuItem;
    }
}