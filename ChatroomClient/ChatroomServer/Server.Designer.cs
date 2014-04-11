namespace ChatroomServer
{
    partial class frm_server
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
            this.list_Clients = new System.Windows.Forms.ListBox();
            this.btn_kick = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_number = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // list_Clients
            // 
            this.list_Clients.FormattingEnabled = true;
            this.list_Clients.Location = new System.Drawing.Point(12, 52);
            this.list_Clients.Name = "list_Clients";
            this.list_Clients.Size = new System.Drawing.Size(212, 251);
            this.list_Clients.TabIndex = 0;
            this.list_Clients.SelectedValueChanged += new System.EventHandler(this.selectedItemChanged);
            // 
            // btn_kick
            // 
            this.btn_kick.Enabled = false;
            this.btn_kick.Location = new System.Drawing.Point(12, 309);
            this.btn_kick.Name = "btn_kick";
            this.btn_kick.Size = new System.Drawing.Size(75, 23);
            this.btn_kick.TabIndex = 1;
            this.btn_kick.Text = "Kick";
            this.btn_kick.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connected Clients:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of clients:";
            // 
            // lbl_number
            // 
            this.lbl_number.AutoSize = true;
            this.lbl_number.Location = new System.Drawing.Point(337, 36);
            this.lbl_number.Name = "lbl_number";
            this.lbl_number.Size = new System.Drawing.Size(0, 13);
            this.lbl_number.TabIndex = 4;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // frm_server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.lbl_number);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_kick);
            this.Controls.Add(this.list_Clients);
            this.Name = "frm_server";
            this.Text = "Chatroom Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_Clients;
        private System.Windows.Forms.Button btn_kick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_number;
        private System.Windows.Forms.Timer UpdateTimer;

    }
}

