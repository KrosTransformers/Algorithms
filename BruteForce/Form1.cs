using System;
using System.Windows.Forms;

namespace BruteForce
{
    public partial class frmMain : Form
    {

        #region Constants

        private const string LOWER = "abcdefghijklmnopqrstuvwxyz";
        private const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DIGITS = "0123456789";

        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCrack_Click(object sender, EventArgs e)
        {
            int minLength = (string.IsNullOrEmpty(txtMinLength.Text) ? 1 : int.Parse(txtMinLength.Text));
            int maxLength = (string.IsNullOrEmpty(txtMaxLength.Text) ? 100 : int.Parse(txtMaxLength.Text));
            string alphabet = $"{(chkLower.Checked ? LOWER : "")}{(chkUpper.Checked ? UPPER : "")}{(chkDigits.Checked ? DIGITS : "")}";

            if (!string.IsNullOrEmpty(alphabet))
            {
                txtCrackedPassword.Text = "### NOT FOUND ###";
                lblTime.Text = " - ";
                Enabled = false;
                Cursor = Cursors.WaitCursor;

                DateTime startTime = DateTime.Now;
                for (int l = minLength; l <= maxLength; l++)
                {
                    char[] word = new char[l];
                    for (int i = 0; i < l; i++)
                    {
                        word[i] = alphabet[0];
                    }

                    if (CheckPassword(word, 0, alphabet, txtPassword.Text))
                    {
                        txtCrackedPassword.Text = string.Join("", word);                        
                        break;
                    }
                }

                Cursor = Cursors.Default;
                Enabled = true;
                lblTime.Text = (DateTime.Now - startTime).TotalMilliseconds.ToString();
            }
        }

        /// <summary>
        /// Tries to recursively find a password.
        /// </summary>
        /// <param name="word">Password suggestion.</param>
        /// <param name="index">Current letter.</param>
        /// <param name="alphabet">Allowed characters.</param>
        /// <param name="password">Password.</param>
        /// <returns>True, if provided word is password; otherwise false.</returns>
        private bool CheckPassword(char[] word, int index, string alphabet, string password)
        {
            if (string.Join("", word) == password)
            {
                return true;
            }
            else if (index == word.Length)
            {
                return false;
            }
            else
            {
                foreach (char letter in alphabet)
                {
                    word[index] = letter;
                    if (CheckPassword(word, index + 1, alphabet, password))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
