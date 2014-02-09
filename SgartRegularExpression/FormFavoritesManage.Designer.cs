namespace Sgart.RegularExpression
{
    partial class FormFavoritesManage
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
            this.dgFavorites = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFavoriteSave = new System.Windows.Forms.Button();
            this.btnFavoriteCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgFavorites)).BeginInit();
            this.SuspendLayout();
            // 
            // dgFavorites
            // 
            this.dgFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFavorites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFavorites.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colValue});
            this.dgFavorites.Location = new System.Drawing.Point(12, 12);
            this.dgFavorites.Name = "dgFavorites";
            this.dgFavorites.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFavorites.Size = new System.Drawing.Size(455, 208);
            this.dgFavorites.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 200;
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "Regular Expression";
            this.colValue.Name = "colValue";
            this.colValue.Width = 300;
            // 
            // btnFavoriteSave
            // 
            this.btnFavoriteSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavoriteSave.Location = new System.Drawing.Point(311, 226);
            this.btnFavoriteSave.Name = "btnFavoriteSave";
            this.btnFavoriteSave.Size = new System.Drawing.Size(75, 24);
            this.btnFavoriteSave.TabIndex = 6;
            this.btnFavoriteSave.Text = "Save";
            this.btnFavoriteSave.UseVisualStyleBackColor = true;
            this.btnFavoriteSave.Click += new System.EventHandler(this.btnFavoriteSave_Click);
            // 
            // btnFavoriteCancel
            // 
            this.btnFavoriteCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavoriteCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFavoriteCancel.Location = new System.Drawing.Point(392, 227);
            this.btnFavoriteCancel.Name = "btnFavoriteCancel";
            this.btnFavoriteCancel.Size = new System.Drawing.Size(75, 23);
            this.btnFavoriteCancel.TabIndex = 7;
            this.btnFavoriteCancel.Text = "Cancel";
            this.btnFavoriteCancel.UseVisualStyleBackColor = true;
            this.btnFavoriteCancel.Click += new System.EventHandler(this.btnFavoriteCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(12, 227);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 24);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormFavoritesManage
            // 
            this.AcceptButton = this.btnFavoriteSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFavoriteCancel;
            this.ClientSize = new System.Drawing.Size(479, 262);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnFavoriteSave);
            this.Controls.Add(this.btnFavoriteCancel);
            this.Controls.Add(this.dgFavorites);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "FormFavoritesManage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage favorites";
            this.Load += new System.EventHandler(this.FormFavoritesManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFavorites)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgFavorites;
        private System.Windows.Forms.Button btnFavoriteSave;
        private System.Windows.Forms.Button btnFavoriteCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
}