namespace MainApplication
{
    partial class ApplicationSettingForm
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
            this.pictureBox_Setting = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_AmountsSplitCharacter = new System.Windows.Forms.Label();
            this.label_CommentSplitCharacter = new System.Windows.Forms.Label();
            this.textBox_AmountsSplitCharacter = new System.Windows.Forms.TextBox();
            this.textBox_CommentSplitCharacter = new System.Windows.Forms.TextBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label_Setting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Setting)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Setting
            // 
            this.pictureBox_Setting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Setting.Image = global::MainApplication.Properties.Resources.設定画面用のスクリーンショット;
            this.pictureBox_Setting.Location = new System.Drawing.Point(68, 31);
            this.pictureBox_Setting.Name = "pictureBox_Setting";
            this.pictureBox_Setting.Size = new System.Drawing.Size(380, 261);
            this.pictureBox_Setting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Setting.TabIndex = 0;
            this.pictureBox_Setting.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(109, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "↑\r\nコレ";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(161, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 48);
            this.label2.TabIndex = 2;
            this.label2.Text = "↑\r\nコレ";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(205, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 48);
            this.label3.TabIndex = 3;
            this.label3.Text = "↑\r\nコレ";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(250, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 48);
            this.label4.TabIndex = 4;
            this.label4.Text = "↑\r\nコレ";
            this.label4.Visible = false;
            // 
            // label_AmountsSplitCharacter
            // 
            this.label_AmountsSplitCharacter.AutoSize = true;
            this.label_AmountsSplitCharacter.Location = new System.Drawing.Point(55, 316);
            this.label_AmountsSplitCharacter.Name = "label_AmountsSplitCharacter";
            this.label_AmountsSplitCharacter.Size = new System.Drawing.Size(142, 18);
            this.label_AmountsSplitCharacter.TabIndex = 5;
            this.label_AmountsSplitCharacter.Text = "金額の区切り文字";
            // 
            // label_CommentSplitCharacter
            // 
            this.label_CommentSplitCharacter.AutoSize = true;
            this.label_CommentSplitCharacter.Location = new System.Drawing.Point(41, 360);
            this.label_CommentSplitCharacter.Name = "label_CommentSplitCharacter";
            this.label_CommentSplitCharacter.Size = new System.Drawing.Size(156, 18);
            this.label_CommentSplitCharacter.TabIndex = 6;
            this.label_CommentSplitCharacter.Text = "コメントの区切り文字";
            // 
            // textBox_AmountsSplitCharacter
            // 
            this.textBox_AmountsSplitCharacter.Location = new System.Drawing.Point(203, 313);
            this.textBox_AmountsSplitCharacter.MaxLength = 1;
            this.textBox_AmountsSplitCharacter.Name = "textBox_AmountsSplitCharacter";
            this.textBox_AmountsSplitCharacter.Size = new System.Drawing.Size(35, 25);
            this.textBox_AmountsSplitCharacter.TabIndex = 7;
            this.textBox_AmountsSplitCharacter.Enter += new System.EventHandler(this.textBox_AmountsSplitCharacter_Enter);
            this.textBox_AmountsSplitCharacter.Leave += new System.EventHandler(this.textBox_AmountsSplitCharacter_Leave);
            // 
            // textBox_CommentSplitCharacter
            // 
            this.textBox_CommentSplitCharacter.Location = new System.Drawing.Point(203, 357);
            this.textBox_CommentSplitCharacter.MaxLength = 1;
            this.textBox_CommentSplitCharacter.Name = "textBox_CommentSplitCharacter";
            this.textBox_CommentSplitCharacter.Size = new System.Drawing.Size(35, 25);
            this.textBox_CommentSplitCharacter.TabIndex = 8;
            this.textBox_CommentSplitCharacter.Enter += new System.EventHandler(this.textBox_CommentSplitCharacter_Enter);
            this.textBox_CommentSplitCharacter.Leave += new System.EventHandler(this.textBox_CommentSplitCharacter_Leave);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(398, 360);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(112, 46);
            this.button_Cancel.TabIndex = 9;
            this.button_Cancel.Text = "キャンセル";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(280, 360);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(112, 46);
            this.button_OK.TabIndex = 10;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_Setting
            // 
            this.label_Setting.AutoSize = true;
            this.label_Setting.Location = new System.Drawing.Point(129, 9);
            this.label_Setting.Name = "label_Setting";
            this.label_Setting.Size = new System.Drawing.Size(245, 18);
            this.label_Setting.TabIndex = 11;
            this.label_Setting.Text = "金額データ入力時の画面表示例";
            // 
            // ApplicationSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 418);
            this.Controls.Add(this.label_Setting);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.textBox_CommentSplitCharacter);
            this.Controls.Add(this.textBox_AmountsSplitCharacter);
            this.Controls.Add(this.label_CommentSplitCharacter);
            this.Controls.Add(this.label_AmountsSplitCharacter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox_Setting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ApplicationSettingForm";
            this.Text = "設定";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Setting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Setting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_AmountsSplitCharacter;
        private System.Windows.Forms.Label label_CommentSplitCharacter;
        private System.Windows.Forms.TextBox textBox_AmountsSplitCharacter;
        private System.Windows.Forms.TextBox textBox_CommentSplitCharacter;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_Setting;
    }
}