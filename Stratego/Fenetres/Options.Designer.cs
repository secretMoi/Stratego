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
			this.buttonValider = new System.Windows.Forms.Button();
			this.buttonAnnuler = new System.Windows.Forms.Button();
			this.panelBorderSon = new Stratego.UserControls.PanelBorder();
			this.labelSon = new System.Windows.Forms.Label();
			this.panelBorderGraphique = new Stratego.UserControls.PanelBorder();
			this.labelGraphique = new System.Windows.Forms.Label();
			this.panelBorderDivers = new Stratego.UserControls.PanelBorder();
			this.labelDivers = new System.Windows.Forms.Label();
			this.panelBorderSauvegarde = new Stratego.UserControls.PanelBorder();
			this.textBoxEmplacementSauvegarde = new System.Windows.Forms.TextBox();
			this.labelEmplacementSauvegarde = new System.Windows.Forms.Label();
			this.labelSauvegarde = new System.Windows.Forms.Label();
			this.panelBorderSon.SuspendLayout();
			this.panelBorderGraphique.SuspendLayout();
			this.panelBorderDivers.SuspendLayout();
			this.panelBorderSauvegarde.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonValider
			// 
			this.buttonValider.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate;
			this.buttonValider.FlatAppearance.BorderSize = 2;
			this.buttonValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonValider.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.buttonValider.ForeColor = System.Drawing.Color.Chocolate;
			this.buttonValider.Location = new System.Drawing.Point(101, 433);
			this.buttonValider.Name = "buttonValider";
			this.buttonValider.Size = new System.Drawing.Size(94, 55);
			this.buttonValider.TabIndex = 4;
			this.buttonValider.Text = "Valider";
			this.buttonValider.UseVisualStyleBackColor = true;
			this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
			// 
			// buttonAnnuler
			// 
			this.buttonAnnuler.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate;
			this.buttonAnnuler.FlatAppearance.BorderSize = 2;
			this.buttonAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAnnuler.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.buttonAnnuler.ForeColor = System.Drawing.Color.Chocolate;
			this.buttonAnnuler.Location = new System.Drawing.Point(459, 433);
			this.buttonAnnuler.Name = "buttonAnnuler";
			this.buttonAnnuler.Size = new System.Drawing.Size(94, 55);
			this.buttonAnnuler.TabIndex = 5;
			this.buttonAnnuler.Text = "Annuler";
			this.buttonAnnuler.UseVisualStyleBackColor = true;
			this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
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
			this.panelBorderGraphique.Controls.Add(this.labelGraphique);
			this.panelBorderGraphique.Epaisseur = 5;
			this.panelBorderGraphique.Location = new System.Drawing.Point(12, 237);
			this.panelBorderGraphique.Name = "panelBorderGraphique";
			this.panelBorderGraphique.Size = new System.Drawing.Size(299, 129);
			this.panelBorderGraphique.TabIndex = 8;
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
			this.panelBorderSauvegarde.Controls.Add(this.textBoxEmplacementSauvegarde);
			this.panelBorderSauvegarde.Controls.Add(this.labelEmplacementSauvegarde);
			this.panelBorderSauvegarde.Controls.Add(this.labelSauvegarde);
			this.panelBorderSauvegarde.Epaisseur = 5;
			this.panelBorderSauvegarde.Location = new System.Drawing.Point(12, 12);
			this.panelBorderSauvegarde.Name = "panelBorderSauvegarde";
			this.panelBorderSauvegarde.Size = new System.Drawing.Size(299, 177);
			this.panelBorderSauvegarde.TabIndex = 6;
			// 
			// textBoxEmplacementSauvegarde
			// 
			this.textBoxEmplacementSauvegarde.Location = new System.Drawing.Point(17, 61);
			this.textBoxEmplacementSauvegarde.Name = "textBoxEmplacementSauvegarde";
			this.textBoxEmplacementSauvegarde.Size = new System.Drawing.Size(238, 20);
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
			this.Controls.Add(this.panelBorderSon);
			this.Controls.Add(this.panelBorderGraphique);
			this.Controls.Add(this.panelBorderDivers);
			this.Controls.Add(this.panelBorderSauvegarde);
			this.Controls.Add(this.buttonAnnuler);
			this.Controls.Add(this.buttonValider);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.Text = "Options";
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
        private System.Windows.Forms.Button buttonValider;
        private System.Windows.Forms.Button buttonAnnuler;
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
	}
}