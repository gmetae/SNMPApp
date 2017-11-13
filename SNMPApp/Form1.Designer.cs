namespace SNMPApp
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
            this.askButton = new System.Windows.Forms.Button();
            this.responseTextBox = new System.Windows.Forms.TextBox();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.communityTextBox = new System.Windows.Forms.TextBox();
            this.communityLabel = new System.Windows.Forms.Label();
            this.adapterComboBox = new System.Windows.Forms.ComboBox();
            this.addressesListBox = new System.Windows.Forms.ListBox();
            this.adapterLabel = new System.Windows.Forms.Label();
            this.addressesLabel = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // askButton
            // 
            this.askButton.Location = new System.Drawing.Point(536, 25);
            this.askButton.Name = "askButton";
            this.askButton.Size = new System.Drawing.Size(75, 43);
            this.askButton.TabIndex = 0;
            this.askButton.Text = "Ask";
            this.askButton.UseVisualStyleBackColor = true;
            this.askButton.Click += new System.EventHandler(this.AskButtonClick);
            // 
            // responseTextBox
            // 
            this.responseTextBox.Location = new System.Drawing.Point(310, 89);
            this.responseTextBox.Multiline = true;
            this.responseTextBox.Name = "responseTextBox";
            this.responseTextBox.Size = new System.Drawing.Size(301, 303);
            this.responseTextBox.TabIndex = 1;
            // 
            // hostTextBox
            // 
            this.hostTextBox.Enabled = false;
            this.hostTextBox.Location = new System.Drawing.Point(310, 25);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(220, 20);
            this.hostTextBox.TabIndex = 2;
            this.hostTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HostTextBoxKeyPress);
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Enabled = false;
            this.hostLabel.Location = new System.Drawing.Point(307, 9);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(80, 13);
            this.hostLabel.TabIndex = 3;
            this.hostLabel.Text = "Hostname or IP";
            // 
            // communityTextBox
            // 
            this.communityTextBox.Location = new System.Drawing.Point(400, 48);
            this.communityTextBox.Name = "communityTextBox";
            this.communityTextBox.Size = new System.Drawing.Size(130, 20);
            this.communityTextBox.TabIndex = 4;
            // 
            // communityLabel
            // 
            this.communityLabel.AutoSize = true;
            this.communityLabel.Location = new System.Drawing.Point(307, 51);
            this.communityLabel.Name = "communityLabel";
            this.communityLabel.Size = new System.Drawing.Size(87, 13);
            this.communityLabel.TabIndex = 5;
            this.communityLabel.Text = "Community name";
            // 
            // adapterComboBox
            // 
            this.adapterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adapterComboBox.FormattingEnabled = true;
            this.adapterComboBox.Location = new System.Drawing.Point(12, 47);
            this.adapterComboBox.Name = "adapterComboBox";
            this.adapterComboBox.Size = new System.Drawing.Size(242, 21);
            this.adapterComboBox.TabIndex = 7;
            this.adapterComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectedIndexChanged);
            // 
            // addressesListBox
            // 
            this.addressesListBox.FormattingEnabled = true;
            this.addressesListBox.Location = new System.Drawing.Point(12, 115);
            this.addressesListBox.Name = "addressesListBox";
            this.addressesListBox.Size = new System.Drawing.Size(242, 277);
            this.addressesListBox.TabIndex = 8;
            this.addressesListBox.SelectedIndexChanged += new System.EventHandler(this.addressesListBox_SelectedIndexChanged);
            // 
            // adapterLabel
            // 
            this.adapterLabel.AutoSize = true;
            this.adapterLabel.Location = new System.Drawing.Point(12, 32);
            this.adapterLabel.Name = "adapterLabel";
            this.adapterLabel.Size = new System.Drawing.Size(123, 13);
            this.adapterLabel.TabIndex = 9;
            this.adapterLabel.Text = "Choose network adapter";
            // 
            // addressesLabel
            // 
            this.addressesLabel.AutoSize = true;
            this.addressesLabel.Location = new System.Drawing.Point(11, 99);
            this.addressesLabel.Name = "addressesLabel";
            this.addressesLabel.Size = new System.Drawing.Size(124, 13);
            this.addressesLabel.TabIndex = 10;
            this.addressesLabel.Text = "addresses in subnetwork";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(12, 12);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(135, 17);
            this.checkBox.TabIndex = 11;
            this.checkBox.Text = "Enter address manually";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 79);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(161, 11);
            this.progressBar.TabIndex = 12;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(179, 74);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 13;
            this.stopButton.Text = "Stop scan";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 402);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.addressesLabel);
            this.Controls.Add(this.adapterLabel);
            this.Controls.Add(this.addressesListBox);
            this.Controls.Add(this.adapterComboBox);
            this.Controls.Add(this.communityLabel);
            this.Controls.Add(this.communityTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.responseTextBox);
            this.Controls.Add(this.askButton);
            this.Name = "Form1";
            this.Text = "SNMP App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button askButton;
        private System.Windows.Forms.TextBox responseTextBox;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox communityTextBox;
        private System.Windows.Forms.Label communityLabel;
        private System.Windows.Forms.ComboBox adapterComboBox;
        private System.Windows.Forms.ListBox addressesListBox;
        private System.Windows.Forms.Label adapterLabel;
        private System.Windows.Forms.Label addressesLabel;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button stopButton;
    }
}