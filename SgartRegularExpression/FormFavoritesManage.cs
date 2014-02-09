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
    public partial class FormFavoritesManage : Form
    {
        public FormFavoritesManage()
        {
            InitializeComponent();
        }

        public Favorite Fav { get; set; }

        private DataTable table { get; set; }

        private void FormFavoritesManage_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Value");

            foreach (FavoriteItem item in Fav.Items)
            {
                DataRow row = table.NewRow();
                row["Name"] = item.Name;
                row["Value"] = item.Value;
                table.Rows.Add(row);
            }

            dgFavorites.DataSource = table;

        }

        private void btnFavoriteSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<FavoriteItem> items = new List<FavoriteItem>();
                foreach (DataRow row in table.Rows)
                {
                    string name = (string)row["Name"];
                    string value = (string)row["Value"];
                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentException("Some values are empty.");
                    }
                    items.Add(new FavoriteItem(name,value));
                }
                items.Sort(delegate(FavoriteItem v1, FavoriteItem v2) { return v1.Name.CompareTo(v2.Name); });

                Fav.Items = items;
                //Favorites.Save(Fav);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFavoriteCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = table.NewRow();
            table.Rows.Add(row);
        }

    }
}
