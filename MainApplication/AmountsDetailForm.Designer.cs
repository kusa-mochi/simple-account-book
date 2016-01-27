namespace MainApplication
{
    partial class AmountsDetailForm
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
            this.listBox_AmountsDetail = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_AmountsDetail
            // 
            this.listBox_AmountsDetail.FormattingEnabled = true;
            this.listBox_AmountsDetail.ItemHeight = 18;
            this.listBox_AmountsDetail.Location = new System.Drawing.Point(0, 0);
            this.listBox_AmountsDetail.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_AmountsDetail.Name = "listBox_AmountsDetail";
            this.listBox_AmountsDetail.Size = new System.Drawing.Size(270, 238);
            this.listBox_AmountsDetail.TabIndex = 0;
            // 
            // AmountsDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 244);
            this.Controls.Add(this.listBox_AmountsDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AmountsDetailForm";
            this.Text = "AmountsDetailForm";
            this.Load += new System.EventHandler(this.AmountsDetailForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_AmountsDetail;
    }
}