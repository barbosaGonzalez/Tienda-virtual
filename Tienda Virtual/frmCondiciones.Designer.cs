namespace Tienda_Virtual
{
    partial class frmCondiciones
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCondiciones));
            this.lblQueHacemos = new System.Windows.Forms.Label();
            this.lblDerechos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAcepto = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQueHacemos
            // 
            this.lblQueHacemos.AutoSize = true;
            this.lblQueHacemos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueHacemos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblQueHacemos.Location = new System.Drawing.Point(12, 9);
            this.lblQueHacemos.Name = "lblQueHacemos";
            this.lblQueHacemos.Size = new System.Drawing.Size(575, 240);
            this.lblQueHacemos.TabIndex = 15;
            this.lblQueHacemos.Text = resources.GetString("lblQueHacemos.Text");
            this.lblQueHacemos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDerechos
            // 
            this.lblDerechos.AutoSize = true;
            this.lblDerechos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDerechos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDerechos.Location = new System.Drawing.Point(150, 266);
            this.lblDerechos.Name = "lblDerechos";
            this.lblDerechos.Size = new System.Drawing.Size(257, 24);
            this.lblDerechos.TabIndex = 14;
            this.lblDerechos.Text = "Pegasus Technology 2014";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Green;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 361);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(602, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(587, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Image = global::Tienda_Virtual.Properties.Resources._64x64;
            this.pictureBox1.Location = new System.Drawing.Point(523, 295);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // btnAcepto
            // 
            this.btnAcepto.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcepto.Image = global::Tienda_Virtual.Properties.Resources.Tips;
            this.btnAcepto.Location = new System.Drawing.Point(227, 320);
            this.btnAcepto.Name = "btnAcepto";
            this.btnAcepto.Size = new System.Drawing.Size(114, 39);
            this.btnAcepto.TabIndex = 1;
            this.btnAcepto.Text = "&Acepto";
            this.btnAcepto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAcepto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAcepto.UseVisualStyleBackColor = true;
            this.btnAcepto.Click += new System.EventHandler(this.btnAcepto_Click);
            // 
            // frmCondiciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(602, 383);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblQueHacemos);
            this.Controls.Add(this.lblDerechos);
            this.Controls.Add(this.btnAcepto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCondiciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminos y condiciones";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAcepto;
        private System.Windows.Forms.Label lblQueHacemos;
        private System.Windows.Forms.Label lblDerechos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}