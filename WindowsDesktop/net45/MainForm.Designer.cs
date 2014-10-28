namespace U2UConsult.WindowsDemoApp45
{
    partial class MainForm
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
            this.signInButton = new System.Windows.Forms.Button();
            this.requireTwoFactorButton = new System.Windows.Forms.Button();
            this.profilePictureBox = new System.Windows.Forms.PictureBox();
            this.friendsListBox = new System.Windows.Forms.ListBox();
            this.profileNameLabel = new System.Windows.Forms.Label();
            this.friendsLabel = new System.Windows.Forms.Label();
            this.friendPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // signInButton
            // 
            this.signInButton.Location = new System.Drawing.Point(13, 13);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(75, 23);
            this.signInButton.TabIndex = 0;
            this.signInButton.Text = "Sign In";
            this.signInButton.UseVisualStyleBackColor = true;
            this.signInButton.Click += new System.EventHandler(this.signInButton_Click);
            // 
            // requireTwoFactorButton
            // 
            this.requireTwoFactorButton.Location = new System.Drawing.Point(13, 42);
            this.requireTwoFactorButton.Name = "requireTwoFactorButton";
            this.requireTwoFactorButton.Size = new System.Drawing.Size(198, 23);
            this.requireTwoFactorButton.TabIndex = 1;
            this.requireTwoFactorButton.Text = "Require Two Factor Authentication";
            this.requireTwoFactorButton.UseVisualStyleBackColor = true;
            this.requireTwoFactorButton.Visible = false;
            this.requireTwoFactorButton.Click += new System.EventHandler(this.requireTwoFactorButton_Click);
            // 
            // profilePictureBox
            // 
            this.profilePictureBox.Location = new System.Drawing.Point(13, 72);
            this.profilePictureBox.Name = "profilePictureBox";
            this.profilePictureBox.Size = new System.Drawing.Size(175, 239);
            this.profilePictureBox.TabIndex = 2;
            this.profilePictureBox.TabStop = false;
            this.profilePictureBox.Visible = false;
            // 
            // friendsListBox
            // 
            this.friendsListBox.FormattingEnabled = true;
            this.friendsListBox.Location = new System.Drawing.Point(369, 98);
            this.friendsListBox.Name = "friendsListBox";
            this.friendsListBox.Size = new System.Drawing.Size(119, 212);
            this.friendsListBox.TabIndex = 3;
            this.friendsListBox.Visible = false;
            // 
            // profileNameLabel
            // 
            this.profileNameLabel.AutoSize = true;
            this.profileNameLabel.Location = new System.Drawing.Point(195, 72);
            this.profileNameLabel.MinimumSize = new System.Drawing.Size(150, 0);
            this.profileNameLabel.Name = "profileNameLabel";
            this.profileNameLabel.Size = new System.Drawing.Size(150, 13);
            this.profileNameLabel.TabIndex = 4;
            this.profileNameLabel.Visible = false;
            // 
            // friendsLabel
            // 
            this.friendsLabel.AutoSize = true;
            this.friendsLabel.Location = new System.Drawing.Point(369, 72);
            this.friendsLabel.Name = "friendsLabel";
            this.friendsLabel.Size = new System.Drawing.Size(41, 13);
            this.friendsLabel.TabIndex = 5;
            this.friendsLabel.Text = "Friends";
            this.friendsLabel.Visible = false;
            // 
            // friendPictureBox
            // 
            this.friendPictureBox.Location = new System.Drawing.Point(514, 98);
            this.friendPictureBox.Name = "friendPictureBox";
            this.friendPictureBox.Size = new System.Drawing.Size(141, 212);
            this.friendPictureBox.TabIndex = 6;
            this.friendPictureBox.TabStop = false;
            this.friendPictureBox.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 333);
            this.Controls.Add(this.friendPictureBox);
            this.Controls.Add(this.friendsLabel);
            this.Controls.Add(this.profileNameLabel);
            this.Controls.Add(this.friendsListBox);
            this.Controls.Add(this.profilePictureBox);
            this.Controls.Add(this.requireTwoFactorButton);
            this.Controls.Add(this.signInButton);
            this.Name = "MainForm";
            this.Text = "Demo App";
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button signInButton;
        private System.Windows.Forms.Button requireTwoFactorButton;
        private System.Windows.Forms.PictureBox profilePictureBox;
        private System.Windows.Forms.ListBox friendsListBox;
        private System.Windows.Forms.Label profileNameLabel;
        private System.Windows.Forms.Label friendsLabel;
        private System.Windows.Forms.PictureBox friendPictureBox;
    }
}

