namespace MainMenu
{
    partial class Mainmenu
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
            this.btnplay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnplay
            // 
            this.btnplay.BackColor = System.Drawing.Color.Aqua;
            this.btnplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnplay.Location = new System.Drawing.Point(291, 211);
            this.btnplay.Name = "btnplay";
            this.btnplay.Size = new System.Drawing.Size(192, 86);
            this.btnplay.TabIndex = 0;
            this.btnplay.Text = "Play";
            this.btnplay.UseVisualStyleBackColor = false;
            this.btnplay.Click += new System.EventHandler(this.btnplay_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(250, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 89);
            this.label1.TabIndex = 1;
            this.label1.Text = "TicDiceToe";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MainMenu.Properties.Resources.wall_phoenix;
            this.ClientSize = new System.Drawing.Size(783, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnplay);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnplay;
        private System.Windows.Forms.Label label1;
    }
}

