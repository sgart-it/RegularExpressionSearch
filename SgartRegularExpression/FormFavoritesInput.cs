using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sgart.RegularExpression
{
    public partial class FormFavoritesInput : Form
    {
        public FormFavoritesInput()
        {
            InitializeComponent();
        }

        public Favorite Fav { get; set; }
        public string KeyName { get; set; }
        public string RegexValue { get; set; }

        private void FormFavoritesInput_Load(object sender, EventArgs e)
        {
            txtFavoriteName.Text = KeyName;
            txtFavoriteRe.Text = RegexValue;
        }

        private void btnFavoriteSave_Click(object sender, EventArgs e)
        {
            try
            {
                string k = txtFavoriteName.Text.Trim();
                string v = txtFavoriteRe.Text;
                FavoriteItem item = Fav.Items.FirstOrDefault(q=> q.Name == k);
                if (item != null)
                {
                    DialogResult result = MessageBox.Show("Name already exists. Overvrite?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        item.Value = v;
                        //Favorites.Save(Fav);
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    Fav.Items.Add(new FavoriteItem(k, v));
                    //Favorites.Save(Fav);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFavoriteCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult =  System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


    }
}
