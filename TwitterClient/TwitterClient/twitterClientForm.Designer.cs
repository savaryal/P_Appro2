namespace TwitterClient
{
    partial class twitterClientForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(twitterClientForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.tweeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.likeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tweeterSplitContainer = new System.Windows.Forms.SplitContainer();
            this.lastTweetsLabel = new System.Windows.Forms.Label();
            this.tweeterButton = new System.Windows.Forms.Button();
            this.tweeterRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tweeterLabel = new System.Windows.Forms.Label();
            this.connectedLabel = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.followingFollowersSplitContainer = new System.Windows.Forms.SplitContainer();
            this.followersLabel = new System.Windows.Forms.Label();
            this.followingLabel = new System.Windows.Forms.Label();
            this.likePictureBox = new System.Windows.Forms.PictureBox();
            this.retweetPictureBox = new System.Windows.Forms.PictureBox();
            this.timelineLikeSplitContainer = new System.Windows.Forms.SplitContainer();
            this.likeLabel = new System.Windows.Forms.Label();
            this.timelineLabel = new System.Windows.Forms.Label();
            this.saveTweetsButton = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tweeterSplitContainer)).BeginInit();
            this.tweeterSplitContainer.Panel1.SuspendLayout();
            this.tweeterSplitContainer.Panel2.SuspendLayout();
            this.tweeterSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.followingFollowersSplitContainer)).BeginInit();
            this.followingFollowersSplitContainer.Panel1.SuspendLayout();
            this.followingFollowersSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.likePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retweetPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timelineLikeSplitContainer)).BeginInit();
            this.timelineLikeSplitContainer.Panel1.SuspendLayout();
            this.timelineLikeSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mainMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tweeterToolStripMenuItem,
            this.followingToolStripMenuItem,
            this.followersToolStripMenuItem,
            this.timelineToolStripMenuItem,
            this.likeToolStripMenuItem});
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 53);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(468, 27);
            this.mainMenuStrip.TabIndex = 1;
            // 
            // tweeterToolStripMenuItem
            // 
            this.tweeterToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.tweeterToolStripMenuItem.Name = "tweeterToolStripMenuItem";
            this.tweeterToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
            this.tweeterToolStripMenuItem.Text = "Tweeter";
            this.tweeterToolStripMenuItem.Click += new System.EventHandler(this.tweeterToolStripMenuItem_Click);
            // 
            // followingToolStripMenuItem
            // 
            this.followingToolStripMenuItem.Name = "followingToolStripMenuItem";
            this.followingToolStripMenuItem.Size = new System.Drawing.Size(130, 23);
            this.followingToolStripMenuItem.Text = "Abonnements";
            this.followingToolStripMenuItem.Click += new System.EventHandler(this.followingToolStripMenuItem_Click);
            // 
            // followersToolStripMenuItem
            // 
            this.followersToolStripMenuItem.Name = "followersToolStripMenuItem";
            this.followersToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.followersToolStripMenuItem.Text = "Abonnés";
            this.followersToolStripMenuItem.Click += new System.EventHandler(this.followersToolStripMenuItem_Click);
            // 
            // timelineToolStripMenuItem
            // 
            this.timelineToolStripMenuItem.Name = "timelineToolStripMenuItem";
            this.timelineToolStripMenuItem.Size = new System.Drawing.Size(86, 23);
            this.timelineToolStripMenuItem.Text = "Timeline";
            this.timelineToolStripMenuItem.Click += new System.EventHandler(this.timelineToolStripMenuItem_Click);
            // 
            // likeToolStripMenuItem
            // 
            this.likeToolStripMenuItem.Name = "likeToolStripMenuItem";
            this.likeToolStripMenuItem.Size = new System.Drawing.Size(74, 23);
            this.likeToolStripMenuItem.Text = "J\'aime";
            this.likeToolStripMenuItem.Click += new System.EventHandler(this.likeToolStripMenuItem_Click);
            // 
            // tweeterSplitContainer
            // 
            this.tweeterSplitContainer.IsSplitterFixed = true;
            this.tweeterSplitContainer.Location = new System.Drawing.Point(12, 96);
            this.tweeterSplitContainer.Name = "tweeterSplitContainer";
            // 
            // tweeterSplitContainer.Panel1
            // 
            this.tweeterSplitContainer.Panel1.AutoScroll = true;
            this.tweeterSplitContainer.Panel1.Controls.Add(this.lastTweetsLabel);
            // 
            // tweeterSplitContainer.Panel2
            // 
            this.tweeterSplitContainer.Panel2.Controls.Add(this.tweeterButton);
            this.tweeterSplitContainer.Panel2.Controls.Add(this.tweeterRichTextBox);
            this.tweeterSplitContainer.Panel2.Controls.Add(this.tweeterLabel);
            this.tweeterSplitContainer.Size = new System.Drawing.Size(496, 390);
            this.tweeterSplitContainer.SplitterDistance = 246;
            this.tweeterSplitContainer.TabIndex = 2;
            // 
            // lastTweetsLabel
            // 
            this.lastTweetsLabel.AutoSize = true;
            this.lastTweetsLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastTweetsLabel.Location = new System.Drawing.Point(3, 5);
            this.lastTweetsLabel.Name = "lastTweetsLabel";
            this.lastTweetsLabel.Size = new System.Drawing.Size(149, 17);
            this.lastTweetsLabel.TabIndex = 3;
            this.lastTweetsLabel.Text = "Vos 20 derniers tweets";
            // 
            // tweeterButton
            // 
            this.tweeterButton.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweeterButton.Location = new System.Drawing.Point(4, 160);
            this.tweeterButton.Name = "tweeterButton";
            this.tweeterButton.Size = new System.Drawing.Size(81, 27);
            this.tweeterButton.TabIndex = 1;
            this.tweeterButton.Text = "Tweeter";
            this.tweeterButton.UseVisualStyleBackColor = true;
            this.tweeterButton.Click += new System.EventHandler(this.tweeterButton_Click);
            // 
            // tweeterRichTextBox
            // 
            this.tweeterRichTextBox.Location = new System.Drawing.Point(3, 28);
            this.tweeterRichTextBox.MaxLength = 140;
            this.tweeterRichTextBox.Name = "tweeterRichTextBox";
            this.tweeterRichTextBox.Size = new System.Drawing.Size(239, 126);
            this.tweeterRichTextBox.TabIndex = 0;
            this.tweeterRichTextBox.Text = "";
            // 
            // tweeterLabel
            // 
            this.tweeterLabel.AutoSize = true;
            this.tweeterLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweeterLabel.Location = new System.Drawing.Point(3, 5);
            this.tweeterLabel.Name = "tweeterLabel";
            this.tweeterLabel.Size = new System.Drawing.Size(58, 17);
            this.tweeterLabel.TabIndex = 4;
            this.tweeterLabel.Text = "Tweeter";
            // 
            // connectedLabel
            // 
            this.connectedLabel.AutoSize = true;
            this.connectedLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectedLabel.Location = new System.Drawing.Point(17, 17);
            this.connectedLabel.Name = "connectedLabel";
            this.connectedLabel.Size = new System.Drawing.Size(137, 17);
            this.connectedLabel.TabIndex = 5;
            this.connectedLabel.Text = "Compte connecté :";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginLabel.Location = new System.Drawing.Point(160, 17);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(109, 17);
            this.loginLabel.TabIndex = 6;
            this.loginLabel.Text = "@projetTCSharp";
            // 
            // followingFollowersSplitContainer
            // 
            this.followingFollowersSplitContainer.IsSplitterFixed = true;
            this.followingFollowersSplitContainer.Location = new System.Drawing.Point(12, 96);
            this.followingFollowersSplitContainer.Name = "followingFollowersSplitContainer";
            this.followingFollowersSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // followingFollowersSplitContainer.Panel1
            // 
            this.followingFollowersSplitContainer.Panel1.Controls.Add(this.followersLabel);
            this.followingFollowersSplitContainer.Panel1.Controls.Add(this.followingLabel);
            // 
            // followingFollowersSplitContainer.Panel2
            // 
            this.followingFollowersSplitContainer.Panel2.AutoScroll = true;
            this.followingFollowersSplitContainer.Size = new System.Drawing.Size(496, 390);
            this.followingFollowersSplitContainer.SplitterDistance = 30;
            this.followingFollowersSplitContainer.TabIndex = 7;
            this.followingFollowersSplitContainer.Visible = false;
            // 
            // followersLabel
            // 
            this.followersLabel.AutoSize = true;
            this.followersLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.followersLabel.Location = new System.Drawing.Point(6, 5);
            this.followersLabel.Name = "followersLabel";
            this.followersLabel.Size = new System.Drawing.Size(162, 17);
            this.followersLabel.TabIndex = 1;
            this.followersLabel.Text = "Vos 20 derniers abonnés";
            this.followersLabel.Visible = false;
            // 
            // followingLabel
            // 
            this.followingLabel.AutoSize = true;
            this.followingLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.followingLabel.Location = new System.Drawing.Point(6, 5);
            this.followingLabel.Name = "followingLabel";
            this.followingLabel.Size = new System.Drawing.Size(196, 17);
            this.followingLabel.TabIndex = 0;
            this.followingLabel.Text = "Vos 20 derniers abonnements";
            this.followingLabel.Visible = false;
            // 
            // likePictureBox
            // 
            this.likePictureBox.Location = new System.Drawing.Point(448, 157);
            this.likePictureBox.Name = "likePictureBox";
            this.likePictureBox.Size = new System.Drawing.Size(30, 30);
            this.likePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.likePictureBox.TabIndex = 9;
            this.likePictureBox.TabStop = false;
            this.likePictureBox.Visible = false;
            this.likePictureBox.Click += new System.EventHandler(this.likePictureBox_Click);
            // 
            // retweetPictureBox
            // 
            this.retweetPictureBox.Location = new System.Drawing.Point(394, 157);
            this.retweetPictureBox.Name = "retweetPictureBox";
            this.retweetPictureBox.Size = new System.Drawing.Size(30, 30);
            this.retweetPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.retweetPictureBox.TabIndex = 9;
            this.retweetPictureBox.TabStop = false;
            this.retweetPictureBox.Visible = false;
            this.retweetPictureBox.Click += new System.EventHandler(this.retweetPictureBox_Click);
            // 
            // timelineLikeSplitContainer
            // 
            this.timelineLikeSplitContainer.IsSplitterFixed = true;
            this.timelineLikeSplitContainer.Location = new System.Drawing.Point(12, 96);
            this.timelineLikeSplitContainer.Name = "timelineLikeSplitContainer";
            this.timelineLikeSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // timelineLikeSplitContainer.Panel1
            // 
            this.timelineLikeSplitContainer.Panel1.Controls.Add(this.likeLabel);
            this.timelineLikeSplitContainer.Panel1.Controls.Add(this.timelineLabel);
            // 
            // timelineLikeSplitContainer.Panel2
            // 
            this.timelineLikeSplitContainer.Panel2.AutoScroll = true;
            this.timelineLikeSplitContainer.Size = new System.Drawing.Size(369, 390);
            this.timelineLikeSplitContainer.SplitterDistance = 30;
            this.timelineLikeSplitContainer.TabIndex = 8;
            this.timelineLikeSplitContainer.Visible = false;
            // 
            // likeLabel
            // 
            this.likeLabel.AutoSize = true;
            this.likeLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.likeLabel.Location = new System.Drawing.Point(4, 5);
            this.likeLabel.Name = "likeLabel";
            this.likeLabel.Size = new System.Drawing.Size(164, 17);
            this.likeLabel.TabIndex = 1;
            this.likeLabel.Text = "20 derniers tweets aimés";
            this.likeLabel.Visible = false;
            // 
            // timelineLabel
            // 
            this.timelineLabel.AutoSize = true;
            this.timelineLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timelineLabel.Location = new System.Drawing.Point(4, 5);
            this.timelineLabel.Name = "timelineLabel";
            this.timelineLabel.Size = new System.Drawing.Size(236, 17);
            this.timelineLabel.TabIndex = 0;
            this.timelineLabel.Text = "20 derniers tweets de votre timeline";
            this.timelineLabel.Visible = false;
            // 
            // saveTweetsButton
            // 
            this.saveTweetsButton.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.saveTweetsButton.Location = new System.Drawing.Point(385, 457);
            this.saveTweetsButton.Name = "saveTweetsButton";
            this.saveTweetsButton.Size = new System.Drawing.Size(123, 26);
            this.saveTweetsButton.TabIndex = 10;
            this.saveTweetsButton.Text = "Sauvegarder";
            this.saveTweetsButton.UseVisualStyleBackColor = true;
            this.saveTweetsButton.Click += new System.EventHandler(this.saveTweetsButton_Click);
            // 
            // twitterClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(520, 498);
            this.Controls.Add(this.saveTweetsButton);
            this.Controls.Add(this.timelineLikeSplitContainer);
            this.Controls.Add(this.followingFollowersSplitContainer);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.connectedLabel);
            this.Controls.Add(this.tweeterSplitContainer);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.likePictureBox);
            this.Controls.Add(this.retweetPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "twitterClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client Twitter C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.twitterClientForm_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tweeterSplitContainer.Panel1.ResumeLayout(false);
            this.tweeterSplitContainer.Panel1.PerformLayout();
            this.tweeterSplitContainer.Panel2.ResumeLayout(false);
            this.tweeterSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tweeterSplitContainer)).EndInit();
            this.tweeterSplitContainer.ResumeLayout(false);
            this.followingFollowersSplitContainer.Panel1.ResumeLayout(false);
            this.followingFollowersSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.followingFollowersSplitContainer)).EndInit();
            this.followingFollowersSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.likePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retweetPictureBox)).EndInit();
            this.timelineLikeSplitContainer.Panel1.ResumeLayout(false);
            this.timelineLikeSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timelineLikeSplitContainer)).EndInit();
            this.timelineLikeSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tweeterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timelineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem likeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer tweeterSplitContainer;
        private System.Windows.Forms.Label lastTweetsLabel;
        private System.Windows.Forms.Label tweeterLabel;
        private System.Windows.Forms.Button tweeterButton;
        private System.Windows.Forms.RichTextBox tweeterRichTextBox;
        private System.Windows.Forms.Label connectedLabel;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.SplitContainer followingFollowersSplitContainer;
        private System.Windows.Forms.Label followingLabel;
        private System.Windows.Forms.Label followersLabel;
        private System.Windows.Forms.SplitContainer timelineLikeSplitContainer;
        private System.Windows.Forms.Label timelineLabel;
        private System.Windows.Forms.Label likeLabel;
        private System.Windows.Forms.PictureBox retweetPictureBox;
        private System.Windows.Forms.PictureBox likePictureBox;
        private System.Windows.Forms.Button saveTweetsButton;
    }
}

