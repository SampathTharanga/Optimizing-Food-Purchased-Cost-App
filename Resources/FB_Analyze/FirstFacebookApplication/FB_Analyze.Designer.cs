namespace FB_Analyze
{
    partial class FB_Analyze
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
            this.btnFacebookLogin = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFacebookLogin
            // 
            this.btnFacebookLogin.Location = new System.Drawing.Point(154, 73);
            this.btnFacebookLogin.Name = "btnFacebookLogin";
            this.btnFacebookLogin.Size = new System.Drawing.Size(139, 23);
            this.btnFacebookLogin.TabIndex = 1;
            this.btnFacebookLogin.Text = "Login to Facebook";
            this.btnFacebookLogin.UseVisualStyleBackColor = true;
            this.btnFacebookLogin.Click += new System.EventHandler(this.btnFacebookLogin_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(154, 102);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(139, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Visible = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FB_Analyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 145);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnFacebookLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FB_Analyze";
            this.Text = "Facebook C# SDK - FB_Analyze";
            this.Load += new System.EventHandler(this.FB_Analyze_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFacebookLogin;
        private System.Windows.Forms.Button btnLogout;
    }
}

