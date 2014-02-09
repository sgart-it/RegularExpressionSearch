namespace Sgart.RegularExpression
{
    partial class FormFavoritesInput
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFavoriteName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFavoriteRe = new System.Windows.Forms.TextBox();
            this.btnFavoriteCancel = new System.Windows.Forms.Button();
            this.btnFavoriteSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Regular Expression";
            // 
            // txtFavoriteName
            // 
            this.txtFavoriteName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFavoriteName.Location = new System.Drawing.Point(27, 25);
            this.txtFavoriteName.Name = "txtFavoriteName";
            this.txtFavoriteName.Size = new System.Drawing.Size(337, 20);
            this.txtFavoriteName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // txtFavoriteRe
            // 
            this.txtFavoriteRe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFavoriteRe.Location = new System.Drawing.Point(27, 64);
            this.txtFavoriteRe.Name = "txtFavoriteRe";
            this.txtFavoriteRe.Size = new System.Drawing.Size(337, 20);
            this.txtFavoriteRe.TabIndex = 2;
            // 
            // btnFavoriteCancel
            // 
            this.btnFavoriteCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFavoriteCancel.Location = new System.Drawing.Point(289, 92);
            this.btnFavoriteCancel.Name = "btnFavoriteCancel";
            this.btnFavoriteCancel.Size = new System.Drawing.Size(75, 23);
            this.btnFavoriteCancel.TabIndex = 4;
            this.btnFavoriteCancel.Text = "Cancel";
            this.btnFavoriteCancel.UseVisualStyleBackColor = true;
            this.btnFavoriteCancel.Click += new System.EventHandler(this.btnFavoriteCancel_Click);
            // 
            // btnFavoriteSave
            // 
            this.btnFavoriteSave.Location = new System.Drawing.Point(208, 91);
            this.btnFavoriteSave.Name = "btnFavoriteSave";
            this.btnFavoriteSave.Size = new System.Drawing.Size(75, 24);
            this.btnFavoriteSave.TabIndex = 3;
            this.btnFavoriteSave.Text = "Save";
            this.btnFavoriteSave.UseVisualStyleBackColor = true;
            this.btnFavoriteSave.Click += new System.EventHandler(this.btnFavoriteSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // FormFavoritesInput
            // 
            this.AcceptButton = this.btnFavoriteSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFavoriteCancel;
            this.ClientSize = new System.Drawing.Size(389, 130);
            this.Controls.Add(this.btnFavoriteSave);
            this.Controls.Add(this.btnFavoriteCancel);
            this.Controls.Add(this.txtFavoriteRe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFavoriteName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormFavoritesInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add favorite";
            this.Load += new System.EventHandler(this.FormFavoritesInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFavoriteName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFavoriteRe;
        private System.Windows.Forms.Button btnFavoriteCancel;
        private System.Windows.Forms.Button btnFavoriteSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}