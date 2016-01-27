using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApplication
{
    public partial class ApplicationSettingForm : Form
    {
        private DataManager _dataManager;

        public ApplicationSettingForm(DataManager data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            InitializeComponent();

            _dataManager = data;

            this.textBox_AmountsSplitCharacter.Text = _dataManager.GetAmountsSplitCharacter().ToString();
            this.textBox_CommentSplitCharacter.Text = _dataManager.GetCommentSplitCharacter().ToString();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            string amountSplitCharacter = this.textBox_AmountsSplitCharacter.Text;
            string commentSplitCharacter = this.textBox_CommentSplitCharacter.Text;

            if (
                (string.IsNullOrEmpty(amountSplitCharacter) == true) ||
                (string.IsNullOrEmpty(commentSplitCharacter) == true)
                )
            {
                MessageBox.Show("区切り文字を入力してください。");
                return;
            }

            switch (amountSplitCharacter)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case " ":
                    MessageBox.Show("数値以外，空白以外の文字を指定してください。");
                    return;
                default:
                    break;
            }

            switch (commentSplitCharacter)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case " ":
                    MessageBox.Show("数値以外，空白以外の文字を指定してください。");
                    return;
                default:
                    break;
            }

            if (amountSplitCharacter == commentSplitCharacter)
            {
                MessageBox.Show("金額の区切り文字と，コメントの区切り文字にはそれぞれ異なる文字を設定してください。");
                return;
            }

            _dataManager.SetAmountsSplitCharacter(amountSplitCharacter[0]);
            _dataManager.SetCommentSplitCharacter(commentSplitCharacter[0]);

            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_AmountsSplitCharacter_Enter(object sender, EventArgs e)
        {
            this.label1.Visible = true;
            this.label3.Visible = true;
            this.label4.Visible = true;
        }

        private void textBox_CommentSplitCharacter_Enter(object sender, EventArgs e)
        {
            this.label2.Visible = true;
        }

        private void textBox_AmountsSplitCharacter_Leave(object sender, EventArgs e)
        {
            this.label1.Visible = false;
            this.label3.Visible = false;
            this.label4.Visible = false;
        }

        private void textBox_CommentSplitCharacter_Leave(object sender, EventArgs e)
        {
            this.label2.Visible = false;
        }
    }
}
