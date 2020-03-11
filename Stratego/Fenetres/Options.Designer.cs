using System.ComponentModel;

namespace Stratego.Fenetres
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
			this.boutonAnnuler = new Stratego.UserControls.Bouton();
			this.boutonConfirmer = new Stratego.UserControls.Bouton();
			this.panelBorderSon = new Stratego.UserControls.PanelBorder();
			this.labelSon = new System.Windows.Forms.Label();
			this.panelBorderGraphique = new Stratego.UserControls.PanelBorder();
			this.checkBoxHistorique = new System.Windows.Forms.CheckBox();
			this.labelHistorique = new System.Windows.Forms.Label();
			this.labelGraphique = new System.Windows.Forms.Label();
			this.panelBorderDivers = new Stratego.UserControls.PanelBorder();
			this.labelDivers = new System.Windows.Forms.Label();
			this.panelBorderSauvegarde = new Stratego.UserControls.PanelBorder();
			this.boutonEmplacementPiece = new Stratego.UserControls.Bouton();
			this.textBoxEmplacementPiece = new System.Windows.Forms.TextBox();
			this.labelEmplaementPiece = new System.Windows.Forms.Label();
			this.boutonOpenSauvegarde = new Stratego.UserControls.Bouton();
			this.textBoxEmplacementSauvegarde = new System.Windows.Forms.TextBox();
			this.labelEmplacementSauvegarde = new System.Windows.Forms.Label();
			this.labelSauvegarde = new System.Windows.Forms.Label();
			this.panelBorderSon.SuspendLayout();
			this.panelBorderGraphique.SuspendLayout();
			this.panelBorderDivers.SuspendLayout();
			this.panelBorderSauvegarde.SuspendLayout();
			this.SuspendLayout();
			// 
			// boutonAnnuler
			// 
			this.boutonAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.boutonAnnuler.FlatAppearance.BorderSize = 2;
			this.boutonAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.boutonAnnuler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.boutonAnnuler.ForeColor = System.Drawing.Color.Chocolate;
			this.boutonAnnuler.Location = new System.Drawing.Point(448, 428);
			this.boutonAnnuler.Name = "boutonAnnuler";
			this.boutonAnnuler.Size = new System.Drawing.Size(106, 60);
			this.boutonAnnuler.TabIndex = 11;
			this.boutonAnnuler.Text = "Annuler";
			this.boutonAnnuler.UseVisualStyleBackColor = false;
			this.boutonAnnuler.Click += new System.EventHandler(this.boutonAnnuler_Click);
			// 
			// boutonConfirmer
			// 
			this.boutonConfirmer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.boutonConfirmer.FlatAppearance.BorderSize = 2;
			this.boutonConfirmer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.boutonConfirmer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.boutonConfirmer.ForeColor = System.Drawing.Color.Chocolate;
			this.boutonConfirmer.Location = new System.Drawing.Point(96, 428);
			this.boutonConfirmer.Name = "boutonConfirmer";
			this.boutonConfirmer.Size = new System.Drawing.Size(106, 60);
			this.boutonConfirmer.TabIndex = 10;
			this.boutonConfirmer.Text = "Confirmer";
			this.boutonConfirmer.UseVisualStyleBackColor = false;
			this.boutonConfirmer.Click += new System.EventHandler(this.boutonConfirmer_Click);
			// 
			// panelBorderSon
			// 
			this.panelBorderSon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(211)))), ((int)(((byte)(165)))));
			this.panelBorderSon.BorderColor = System.Drawing.Color.Chocolate;
			this.panelBorderSon.Controls.Add(this.labelSon);
			this.panelBorderSon.Epaisseur = 5;
			this.panelBorderSon.Location = new System.Drawing.Point(349, 237);
			this.panelBorderSon.Name = "panelBorderSon";
			this.panelBorderSon.Size = new System.Drawing.Size(299, 129);
			this.panelBorderSon.TabIndex = 9;
			// 
			// labelSon
			// 
			this.labelSon.AutoSize = true;
			this.labelSon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelSon.ForeColor = System.Drawing.Color.Chocolate;
			this.labelSon.Location = new System.Drawing.Point(14, 10);
			this.labelSon.Name = "labelSon";
			this.labelSon.Size = new System.Drawing.Size(125, 20);
			this.labelSon.TabIndex = 1;
			this.labelSon.Text = "Options sonores";
			// 
			// panelBorderGraphique
			// 
			this.panelBorderGraphique.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(211)))), ((int)(((byte)(165)))));
			this.panelBorderGraphique.BorderColor = System.Drawing.Color.Chocolate;
			this.panelBorderGraphique.Controls.Add(this.checkBoxHistorique);
			this.panelBorderGraphique.Controls.Add(this.labelHistorique);
			this.panelBorderGraphique.Controls.Add(this.labelGraphique);
			this.panelBorderGraphique.Epaisseur = 5;
			this.panelBorderGraphique.Location = new System.Drawing.Point(12, 237);
			this.panelBorderGraphique.Name = "panelBorderGraphique";
			this.panelBorderGraphique.Size = new System.Drawing.Size(299, 129);
			this.panelBorderGraphique.TabIndex = 8;
			// 
			// checkBoxHistorique
			// 
			this.checkBoxHistorique.AutoSize = true;
			this.checkBoxHistorique.Location = new System.Drawing.Point(216, 43);
			this.checkBoxHistorique.Name = "checkBoxHistorique";
			this.checkBoxHistorique.Size = new System.Drawing.Size(80, 17);
			this.checkBoxHistorique.TabIndex = 6;
			this.checkBoxHistorique.Text = "checkBox1";
			this.checkBoxHistorique.UseVisualStyleBackColor = true;
			this.checkBoxHistorique.CheckedChanged += new System.EventHandler(this.checkBoxHistorique_CheckedChanged);
			// 
			// labelHistorique
			// 
			this.labelHistorique.AutoSize = true;
			this.labelHistorique.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHistorique.ForeColor = System.Drawing.Color.Chocolate;
			this.labelHistorique.Location = new System.Drawing.Point(13, 42);
			this.labelHistorique.Name = "labelHistorique";
			this.labelHistorique.Size = new System.Drawing.Size(123, 16);
			this.labelHistorique.TabIndex = 5;
			this.labelHistorique.Text = "Afficher historique ?";
			// 
			// labelGraphique
			// 
			this.labelGraphique.AutoSize = true;
			this.labelGraphique.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelGraphique.ForeColor = System.Drawing.Color.Chocolate;
			this.labelGraphique.Location = new System.Drawing.Point(13, 10);
			this.labelGraphique.Name = "labelGraphique";
			this.labelGraphique.Size = new System.Drawing.Size(147, 20);
			this.labelGraphique.TabIndex = 1;
			this.labelGraphique.Text = "Options graphiques";
			// 
			// panelBorderDivers
			// 
			this.panelBorderDivers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(211)))), ((int)(((byte)(165)))));
			this.panelBorderDivers.BorderColor = System.Drawing.Color.Chocolate;
			this.panelBorderDivers.Controls.Add(this.labelDivers);
			this.panelBorderDivers.Epaisseur = 5;
			this.panelBorderDivers.Location = new System.Drawing.Point(349, 12);
			this.panelBorderDivers.Name = "panelBorderDivers";
			this.panelBorderDivers.Size = new System.Drawing.Size(299, 177);
			this.panelBorderDivers.TabIndex = 7;
			// 
			// labelDivers
			// 
			this.labelDivers.AutoSize = true;
			this.labelDivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDivers.ForeColor = System.Drawing.Color.Chocolate;
			this.labelDivers.Location = new System.Drawing.Point(14, 10);
			this.labelDivers.Name = "labelDivers";
			this.labelDivers.Size = new System.Drawing.Size(53, 20);
			this.labelDivers.TabIndex = 1;
			this.labelDivers.Text = "Divers";
			// 
			// panelBorderSauvegarde
			// 
			this.panelBorderSauvegarde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(211)))), ((int)(((byte)(165)))));
			this.panelBorderSauvegarde.BorderColor = System.Drawing.Color.Chocolate;
			this.panelBorderSauvegarde.Controls.Add(this.boutonEmplacementPiece);
			this.panelBorderSauvegarde.Controls.Add(this.textBoxEmplacementPiece);
			this.panelBorderSauvegarde.Controls.Add(this.labelEmplaementPiece);
			this.panelBorderSauvegarde.Controls.Add(this.boutonOpenSauvegarde);
			this.panelBorderSauvegarde.Controls.Add(this.textBoxEmplacementSauvegarde);
			this.panelBorderSauvegarde.Controls.Add(this.labelEmplacementSauvegarde);
			this.panelBorderSauvegarde.Controls.Add(this.labelSauvegarde);
			this.panelBorderSauvegarde.Epaisseur = 5;
			this.panelBorderSauvegarde.Location = new System.Drawing.Point(12, 12);
			this.panelBorderSauvegarde.Name = "panelBorderSauvegarde";
			this.panelBorderSauvegarde.Size = new System.Drawing.Size(299, 177);
			this.panelBorderSauvegarde.TabIndex = 6;
			// 
			// boutonEmplacementPiece
			// 
			this.boutonEmplacementPiece.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.boutonEmplacementPiece.FlatAppearance.BorderSize = 2;
			this.boutonEmplacementPiece.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.boutonEmplacementPiece.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.boutonEmplacementPiece.ForeColor = System.Drawing.Color.Chocolate;
			this.boutonEmplacementPiece.Location = new System.Drawing.Point(234, 117);
			this.boutonEmplacementPiece.Name = "boutonEmplacementPiece";
			this.boutonEmplacementPiece.Size = new System.Drawing.Size(62, 30);
			this.boutonEmplacementPiece.TabIndex = 6;
			this.boutonEmplacementPiece.Text = "Ouvrir";
			this.boutonEmplacementPiece.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.boutonEmplacementPiece.UseVisualStyleBackColor = false;
			this.boutonEmplacementPiece.Click += new System.EventHandler(this.boutonEmplacementPiece_Click);
			// 
			// textBoxEmplacementPiece
			// 
			this.textBoxEmplacementPiece.Location = new System.Drawing.Point(17, 127);
			this.textBoxEmplacementPiece.Name = "textBoxEmplacementPiece";
			this.textBoxEmplacementPiece.Size = new System.Drawing.Size(211, 20);
			this.textBoxEmplacementPiece.TabIndex = 5;
			// 
			// labelEmplaementPiece
			// 
			this.labelEmplaementPiece.AutoSize = true;
			this.labelEmplaementPiece.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelEmplaementPiece.ForeColor = System.Drawing.Color.Chocolate;
			this.labelEmplaementPiece.Location = new System.Drawing.Point(13, 98);
			this.labelEmplaementPiece.Name = "labelEmplaementPiece";
			this.labelEmplaementPiece.Size = new System.Drawing.Size(205, 16);
			this.labelEmplaementPiece.TabIndex = 4;
			this.labelEmplaementPiece.Text = "Emplacement fichier des pièces :";
			// 
			// boutonOpenSauvegarde
			// 
			this.boutonOpenSauvegarde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.boutonOpenSauvegarde.FlatAppearance.BorderSize = 2;
			this.boutonOpenSauvegarde.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.boutonOpenSauvegarde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.boutonOpenSauvegarde.ForeColor = System.Drawing.Color.Chocolate;
			this.boutonOpenSauvegarde.Location = new System.Drawing.Point(234, 61);
			this.boutonOpenSauvegarde.Name = "boutonOpenSauvegarde";
			this.boutonOpenSauvegarde.Size = new System.Drawing.Size(62, 30);
			this.boutonOpenSauvegarde.TabIndex = 3;
			this.boutonOpenSauvegarde.Text = "Ouvrir";
			this.boutonOpenSauvegarde.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.boutonOpenSauvegarde.UseVisualStyleBackColor = false;
			this.boutonOpenSauvegarde.Click += new System.EventHandler(this.boutonOpenSauvegarde_Click);
			// 
			// textBoxEmplacementSauvegarde
			// 
			this.textBoxEmplacementSauvegarde.Location = new System.Drawing.Point(17, 71);
			this.textBoxEmplacementSauvegarde.Name = "textBoxEmplacementSauvegarde";
			this.textBoxEmplacementSauvegarde.Size = new System.Drawing.Size(211, 20);
			this.textBoxEmplacementSauvegarde.TabIndex = 2;
			// 
			// labelEmplacementSauvegarde
			// 
			this.labelEmplacementSauvegarde.AutoSize = true;
			this.labelEmplacementSauvegarde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelEmplacementSauvegarde.ForeColor = System.Drawing.Color.Chocolate;
			this.labelEmplacementSauvegarde.Location = new System.Drawing.Point(13, 42);
			this.labelEmplacementSauvegarde.Name = "labelEmplacementSauvegarde";
			this.labelEmplacementSauvegarde.Size = new System.Drawing.Size(230, 16);
			this.labelEmplacementSauvegarde.TabIndex = 1;
			this.labelEmplacementSauvegarde.Text = "Emplacement fichier de sauvegarde :";
			// 
			// labelSauvegarde
			// 
			this.labelSauvegarde.AutoSize = true;
			this.labelSauvegarde.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelSauvegarde.ForeColor = System.Drawing.Color.Chocolate;
			this.labelSauvegarde.Location = new System.Drawing.Point(13, 10);
			this.labelSauvegarde.Name = "labelSauvegarde";
			this.labelSauvegarde.Size = new System.Drawing.Size(95, 20);
			this.labelSauvegarde.TabIndex = 0;
			this.labelSauvegarde.Text = "Sauvegarde";
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(184)))), ((int)(((byte)(133)))));
			this.ClientSize = new System.Drawing.Size(664, 500);
			this.Controls.Add(this.boutonAnnuler);
			this.Controls.Add(this.boutonConfirmer);
			this.Controls.Add(this.panelBorderSon);
			this.Controls.Add(this.panelBorderGraphique);
			this.Controls.Add(this.panelBorderDivers);
			this.Controls.Add(this.panelBorderSauvegarde);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.Text = "Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
			this.panelBorderSon.ResumeLayout(false);
			this.panelBorderSon.PerformLayout();
			this.panelBorderGraphique.ResumeLayout(false);
			this.panelBorderGraphique.PerformLayout();
			this.panelBorderDivers.ResumeLayout(false);
			this.panelBorderDivers.PerformLayout();
			this.panelBorderSauvegarde.ResumeLayout(false);
			this.panelBorderSauvegarde.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
		private UserControls.PanelBorder panelBorderSauvegarde;
		private UserControls.PanelBorder panelBorderDivers;
		private UserControls.PanelBorder panelBorderGraphique;
		private UserControls.PanelBorder panelBorderSon;
		private System.Windows.Forms.Label labelSauvegarde;
		private System.Windows.Forms.Label labelSon;
		private System.Windows.Forms.Label labelGraphique;
		private System.Windows.Forms.Label labelDivers;
		private System.Windows.Forms.TextBox textBoxEmplacementSauvegarde;
		private System.Windows.Forms.Label labelEmplacementSauvegarde;
		private UserControls.Bouton boutonOpenSauvegarde;
		private UserControls.Bouton boutonConfirmer;
		private UserControls.Bouton boutonAnnuler;
		private UserControls.Bouton boutonEmplacementPiece;
		private System.Windows.Forms.TextBox textBoxEmplacementPiece;
		private System.Windows.Forms.Label labelEmplaementPiece;
		private System.Windows.Forms.CheckBox checkBoxHistorique;
		private System.Windows.Forms.Label labelHistorique;
	}
}