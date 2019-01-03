namespace AG_TSP
{
    partial class Ajuda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ajuda));
            this.como_usar = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnEN = new System.Windows.Forms.Button();
            this.BtnBR = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // como_usar
            // 
            this.como_usar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.como_usar.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.como_usar.Location = new System.Drawing.Point(282, 9);
            this.como_usar.Name = "como_usar";
            this.como_usar.Size = new System.Drawing.Size(157, 32);
            this.como_usar.TabIndex = 0;
            this.como_usar.Text = "Como Usar";
            this.como_usar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(51, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(648, 420);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.BtnEN);
            this.panel1.Controls.Add(this.BtnBR);
            this.panel1.Location = new System.Drawing.Point(-3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 480);
            this.panel1.TabIndex = 2;
            // 
            // BtnEN
            // 
            this.BtnEN.Image = ((System.Drawing.Image)(resources.GetObject("BtnEN.Image")));
            this.BtnEN.Location = new System.Drawing.Point(665, 9);
            this.BtnEN.Name = "BtnEN";
            this.BtnEN.Size = new System.Drawing.Size(37, 32);
            this.BtnEN.TabIndex = 1;
            this.BtnEN.UseVisualStyleBackColor = true;
            this.BtnEN.Click += new System.EventHandler(this.BtnEN_Click);
            // 
            // BtnBR
            // 
            this.BtnBR.Image = ((System.Drawing.Image)(resources.GetObject("BtnBR.Image")));
            this.BtnBR.Location = new System.Drawing.Point(622, 9);
            this.BtnBR.Name = "BtnBR";
            this.BtnBR.Size = new System.Drawing.Size(37, 32);
            this.BtnBR.TabIndex = 0;
            this.BtnBR.UseVisualStyleBackColor = true;
            this.BtnBR.Click += new System.EventHandler(this.BtnBR_Click);
            // 
            // Ajuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 476);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.como_usar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Ajuda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajuda";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label como_usar;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnEN;
        private System.Windows.Forms.Button BtnBR;
    }
}