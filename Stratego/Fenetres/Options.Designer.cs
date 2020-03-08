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
            this.panelSauvegarde = new System.Windows.Forms.Panel();
            this.labelSauvegarde = new System.Windows.Forms.Label();
            this.panelGraphique = new System.Windows.Forms.Panel();
            this.labelGraphique = new System.Windows.Forms.Label();
            this.panelDivers = new System.Windows.Forms.Panel();
            this.labelDivers = new System.Windows.Forms.Label();
            this.panelSon = new System.Windows.Forms.Panel();
            this.labelSon = new System.Windows.Forms.Label();
            this.buttonValider = new System.Windows.Forms.Button();
            this.buttonAnnuler = new System.Windows.Forms.Button();
            this.panelSauvegarde.SuspendLayout();
            this.panelGraphique.SuspendLayout();
            this.panelDivers.SuspendLayout();
            this.panelSon.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSauvegarde
            // 
            this.panelSauvegarde.BackColor = System.Drawing.Color.SlateBlue;
            this.panelSauvegarde.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSauvegarde.Controls.Add(this.labelSauvegarde);
            this.panelSauvegarde.ForeColor = System.Drawing.Color.White;
            this.panelSauvegarde.Location = new System.Drawing.Point(12, 12);
            this.panelSauvegarde.Name = "panelSauvegarde";
            this.panelSauvegarde.Size = new System.Drawing.Size(350, 150);
            this.panelSauvegarde.TabIndex = 0;
            // 
            // labelSauvegarde
            // 
            this.labelSauvegarde.Location = new System.Drawing.Point(0, 0);
            this.labelSauvegarde.Name = "labelSauvegarde";
            this.labelSauvegarde.Size = new System.Drawing.Size(100, 23);
            this.labelSauvegarde.TabIndex = 0;
            this.labelSauvegarde.Text = "Sauvegarde";
            // 
            // panelGraphique
            // 
            this.panelGraphique.BackColor = System.Drawing.Color.SlateBlue;
            this.panelGraphique.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGraphique.Controls.Add(this.labelGraphique);
            this.panelGraphique.Location = new System.Drawing.Point(12, 198);
            this.panelGraphique.Name = "panelGraphique";
            this.panelGraphique.Size = new System.Drawing.Size(350, 150);
            this.panelGraphique.TabIndex = 1;
            // 
            // labelGraphique
            // 
            this.labelGraphique.ForeColor = System.Drawing.Color.White;
            this.labelGraphique.Location = new System.Drawing.Point(0, 0);
            this.labelGraphique.Name = "labelGraphique";
            this.labelGraphique.Size = new System.Drawing.Size(127, 23);
            this.labelGraphique.TabIndex = 0;
            this.labelGraphique.Text = "Options graphiques";
            // 
            // panelDivers
            // 
            this.panelDivers.BackColor = System.Drawing.Color.SlateBlue;
            this.panelDivers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDivers.Controls.Add(this.labelDivers);
            this.panelDivers.Location = new System.Drawing.Point(407, 12);
            this.panelDivers.Name = "panelDivers";
            this.panelDivers.Size = new System.Drawing.Size(350, 150);
            this.panelDivers.TabIndex = 2;
            // 
            // labelDivers
            // 
            this.labelDivers.ForeColor = System.Drawing.Color.White;
            this.labelDivers.Location = new System.Drawing.Point(0, 0);
            this.labelDivers.Name = "labelDivers";
            this.labelDivers.Size = new System.Drawing.Size(127, 23);
            this.labelDivers.TabIndex = 0;
            this.labelDivers.Text = "Divers";
            // 
            // panelSon
            // 
            this.panelSon.BackColor = System.Drawing.Color.SlateBlue;
            this.panelSon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSon.Controls.Add(this.labelSon);
            this.panelSon.Location = new System.Drawing.Point(407, 198);
            this.panelSon.Name = "panelSon";
            this.panelSon.Size = new System.Drawing.Size(350, 150);
            this.panelSon.TabIndex = 3;
            // 
            // labelSon
            // 
            this.labelSon.ForeColor = System.Drawing.Color.White;
            this.labelSon.Location = new System.Drawing.Point(0, 0);
            this.labelSon.Name = "labelSon";
            this.labelSon.Size = new System.Drawing.Size(127, 23);
            this.labelSon.TabIndex = 0;
            this.labelSon.Text = "Options sonores";
            // 
            // buttonValider
            // 
            this.buttonValider.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonValider.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.Location = new System.Drawing.Point(125, 397);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.Size = new System.Drawing.Size(110, 63);
            this.buttonValider.TabIndex = 4;
            this.buttonValider.Text = "Valider";
            this.buttonValider.UseVisualStyleBackColor = true;
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnnuler.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(527, 397);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(110, 63);
            this.buttonAnnuler.TabIndex = 5;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(775, 484);
            this.Controls.Add(this.buttonAnnuler);
            this.Controls.Add(this.buttonValider);
            this.Controls.Add(this.panelSon);
            this.Controls.Add(this.panelDivers);
            this.Controls.Add(this.panelGraphique);
            this.Controls.Add(this.panelSauvegarde);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "Options";
            this.panelSauvegarde.ResumeLayout(false);
            this.panelGraphique.ResumeLayout(false);
            this.panelDivers.ResumeLayout(false);
            this.panelSon.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label labelGraphique;
        private System.Windows.Forms.Panel panelGraphique;
        private System.Windows.Forms.Label labelSauvegarde;
        private System.Windows.Forms.Panel panelSauvegarde;
        private System.Windows.Forms.Label labelDivers;
        private System.Windows.Forms.Panel panelDivers;
        private System.Windows.Forms.Label labelSon;
        private System.Windows.Forms.Panel panelSon;
        private System.Windows.Forms.Button buttonValider;
        private System.Windows.Forms.Button buttonAnnuler;
    }
}