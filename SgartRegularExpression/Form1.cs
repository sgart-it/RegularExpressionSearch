using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Resources;
using System.Reflection;
using System.Linq;

namespace Sgart.RegularExpression
{
    /// <summary>
    /// 22-11-2005 Descrizione di riepilogo per Form1.
    /// </summary>
    public class FormRe : System.Windows.Forms.Form
    {
        #region Private properties

        private string[,] menuRegExp = null;
        string helpAboutMessage;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mmFile;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem mmiAbout;
        private System.Windows.Forms.MenuItem mmRegExp;
        private System.Windows.Forms.MenuItem mmiOpen;
        private System.Windows.Forms.MenuItem mmExit;
        private System.Windows.Forms.MenuItem mmSave;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.MenuItem mmiIgnoreCase;
        private System.Windows.Forms.MenuItem mmResult;
        private System.Windows.Forms.MenuItem mmiResultSave;
        private System.Windows.Forms.MenuItem mmiShowID;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtRegExp;
        private System.Windows.Forms.LinkLabel lblSgart;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuItem mmMultiline;
        private MenuItem mmFavorites;
        private MenuItem mmiFavoritesAdd;
        private MenuItem mmiFavoritesManage;
        private Button btnAddFavorite;
        private MenuItem mmiRestoreLastSession;
        private MenuItem menuItem3;
        private MenuItem menuItem2;
        private MenuItem mmiExplicitCapture;
        private MenuItem mmiECMAScript;
        private MenuItem mmiCultureInvariant;
        private MenuItem menuItem7;
        private MenuItem mmiIgnorePatternWhitespace;
        private MenuItem mmiRightToLeft;
        private MenuItem menuItem1;
        private IContainer components;


        public Favorite Fav { get; set; }

        #endregion

        /// <summary>
        /// Il punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new FormRe());
        }

        public FormRe()
        {
            //
            // Necessario per il supporto di Progettazione Windows Form
            //
            InitializeComponent();

            //
            // TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
            //
            RegExpAddMenuItem();
            string s = ReadResourceString("helpAbout");
            helpAboutMessage = String.Format(s, Application.ProductName, Application.ProductVersion);
        }

        private void FormRe_Load(object sender, System.EventArgs e)
        {
            this.Text = Application.ProductName + " - v. " + Application.ProductVersion;
            //set dimension
            Fav.WindowHeigth = Fav.WindowHeigth < this.MinimumSize.Height ? this.MinimumSize.Height : Fav.WindowHeigth;
            Fav.WindowWidth = Fav.WindowWidth < this.MinimumSize.Width ? this.MinimumSize.Width : Fav.WindowWidth;
            Fav.WindowHeigth = Fav.WindowHeigth < this.MinimumSize.Height ? this.MinimumSize.Height : Fav.WindowHeigth;

            Fav.SplitPosition = Fav.SplitPosition < 50 ? 50 : Fav.SplitPosition;
            Fav.SplitPosition = Fav.SplitPosition > this.Height - 100 ? this.MinimumSize.Height - 100 : Fav.SplitPosition;
            if (Fav.RestoreLastSession && Fav.FileFound)
            {
                txtSource.Text = Fav.TextInput;
                txtRegExp.Text = Fav.RegExInput;
            }
            this.Size = new Size(Fav.WindowWidth, Fav.WindowHeigth);
            this.WindowState = Fav.WindowState;

            this.splitter1.SplitPosition = Fav.SplitPosition;
        }

        private void FormRe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fav.WindowState = this.WindowState;
            Fav.WindowWidth = this.Width;
            Fav.WindowHeigth = this.Height;
            Fav.SplitPosition = this.splitter1.SplitPosition;
            if (Fav.RestoreLastSession)
            {
                Fav.TextInput = txtSource.Text;
                Fav.RegExInput = txtRegExp.Text;
            }
            else
            {
                Fav.TextInput = "";
                Fav.RegExInput = "";
            }
            Favorite.Save(Fav);
        }

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Favoritex and standard

        private void UpdateFavorites(bool load)
        {
            if (load)
            {
                try
                {
                    Fav = Favorite.Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                for (int i = mmFavorites.MenuItems.Count - 1; i >= 0; i--)
                {
                    if (mmFavorites.MenuItems[i].Name.StartsWith("mmiFavoritesAuto"))
                    {
                        mmFavorites.MenuItems.RemoveAt(i);
                    }
                }
            }

            int c = 0;
            foreach (FavoriteItem item in Fav.Items)
            {
                MenuItem mi = new MenuItem(item.Name, new System.EventHandler(this.RegExp2_Click));
                mi.Name = string.Format("mmiFavoritesAuto{0}", c++);
                mi.Tag = item.Value;
                mmFavorites.MenuItems.Add(mi);
            }
        }

        private void RegExp2_Click(object sender, System.EventArgs e)
        {
            MenuItem element = (MenuItem)sender;
            txtRegExp.Text = element.Tag.ToString();
        }

        private void RegExpAddMenuItem()
        {
            menuRegExp = new string[,]{
					{"Mail", @"([A-Za-z0-9._]+@[A-Za-z0-9.-_]+)"},
					{"Mail 2", @"([\w-\.]+@(?:[\w-]+\.)+(?:[a-zA-Z]{2,4}|[0-9]{1,3}))"},
					{"URL", @"(\w+://(?:[\w-]+\.)+[\w-]+(?:/[\w-./?%&~=]*)?)[^.]"},
					{"Smtp:Mail", @"smtp:(?<mail>\S+);"},
					{"IP nnn.nnn.nnn.nnn", @"([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})"},
					{"Number", @"([\+-]*[0-9]+(?:\.[0-9])*)"},
					{"Word", @"(\w+)"},
					{"Quoted", @"""([\S ]+)"""},
					{"Tag HTML", @"(<[^>]*>)"},
					{"HKEY (last)", @"\[HKEY_[a-z0-9_\{\}\\]+\\([a-z0-9_ -]+)\]"},
					{"Dir > file", @"^[0-9]{1}[\s\S]{34} (.+?)$"},
                    {"Line", @"^(.+?)$"}
			};
            for (int i = 0; i <= menuRegExp.GetUpperBound(0); i++)
            {
                mmRegExp.MenuItems.Add(new MenuItem(menuRegExp[i, 0], new System.EventHandler(this.RegExp_Click)));
            }
            Fav = new Favorite();
            UpdateFavorites(true);
        }

        // evento per menu RegExp
        private void RegExp_Click(object sender, System.EventArgs e)
        {
            string element = ((MenuItem)sender).Text;

            for (int i = 0; i <= menuRegExp.GetUpperBound(0); i++)
            {
                if (menuRegExp[i, 0] == element)
                {
                    txtRegExp.Text = menuRegExp[i, 1];
                    break;
                }
            }
        }

        #endregion

        #region Codice generato da Progettazione Windows Form
        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRe));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mmFile = new System.Windows.Forms.MenuItem();
            this.mmiOpen = new System.Windows.Forms.MenuItem();
            this.mmSave = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mmiRestoreLastSession = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mmExit = new System.Windows.Forms.MenuItem();
            this.mmRegExp = new System.Windows.Forms.MenuItem();
            this.mmFavorites = new System.Windows.Forms.MenuItem();
            this.mmiFavoritesAdd = new System.Windows.Forms.MenuItem();
            this.mmiFavoritesManage = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mmResult = new System.Windows.Forms.MenuItem();
            this.mmiIgnoreCase = new System.Windows.Forms.MenuItem();
            this.mmMultiline = new System.Windows.Forms.MenuItem();
            this.mmiExplicitCapture = new System.Windows.Forms.MenuItem();
            this.mmiECMAScript = new System.Windows.Forms.MenuItem();
            this.mmiCultureInvariant = new System.Windows.Forms.MenuItem();
            this.mmiIgnorePatternWhitespace = new System.Windows.Forms.MenuItem();
            this.mmiRightToLeft = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mmiShowID = new System.Windows.Forms.MenuItem();
            this.mmiResultSave = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.mmiAbout = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtRegExp = new System.Windows.Forms.TextBox();
            this.lblSgart = new System.Windows.Forms.LinkLabel();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddFavorite = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mmFile,
            this.mmRegExp,
            this.mmFavorites,
            this.mmResult,
            this.menuItem11});
            // 
            // mmFile
            // 
            this.mmFile.Index = 0;
            this.mmFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mmiOpen,
            this.mmSave,
            this.menuItem1,
            this.mmiResultSave,
            this.menuItem2,
            this.mmiRestoreLastSession,
            this.menuItem4,
            this.mmExit});
            this.mmFile.Text = "&File";
            this.mmFile.Popup += new System.EventHandler(this.mmFile_Popup);
            // 
            // mmiOpen
            // 
            this.mmiOpen.Index = 0;
            this.mmiOpen.Text = "&Open";
            this.mmiOpen.Click += new System.EventHandler(this.mmiOpen_Click);
            // 
            // mmSave
            // 
            this.mmSave.Index = 1;
            this.mmSave.Text = "&Save";
            this.mmSave.Click += new System.EventHandler(this.mmSave_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.Text = "-";
            // 
            // mmiRestoreLastSession
            // 
            this.mmiRestoreLastSession.Checked = true;
            this.mmiRestoreLastSession.Index = 5;
            this.mmiRestoreLastSession.Text = "Restore &last session";
            this.mmiRestoreLastSession.Click += new System.EventHandler(this.mmiSaveLastSession_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 6;
            this.menuItem4.Text = "-";
            // 
            // mmExit
            // 
            this.mmExit.Index = 7;
            this.mmExit.Text = "E&xit";
            this.mmExit.Click += new System.EventHandler(this.mmExit_Click);
            // 
            // mmRegExp
            // 
            this.mmRegExp.Index = 1;
            this.mmRegExp.Text = "&RegExp";
            // 
            // mmFavorites
            // 
            this.mmFavorites.Index = 2;
            this.mmFavorites.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mmiFavoritesAdd,
            this.mmiFavoritesManage,
            this.menuItem3});
            this.mmFavorites.Text = "&Favorites";
            this.mmFavorites.Popup += new System.EventHandler(this.mmFavorites_Popup);
            // 
            // mmiFavoritesAdd
            // 
            this.mmiFavoritesAdd.Index = 0;
            this.mmiFavoritesAdd.Text = "&Add ...";
            this.mmiFavoritesAdd.Click += new System.EventHandler(this.mmiFavoritesAdd_Click);
            // 
            // mmiFavoritesManage
            // 
            this.mmiFavoritesManage.Index = 1;
            this.mmiFavoritesManage.Text = "&Manage ...";
            this.mmiFavoritesManage.Click += new System.EventHandler(this.mmiFavoritesManage_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // mmResult
            // 
            this.mmResult.Index = 3;
            this.mmResult.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mmiIgnoreCase,
            this.mmMultiline,
            this.mmiExplicitCapture,
            this.mmiECMAScript,
            this.mmiCultureInvariant,
            this.mmiIgnorePatternWhitespace,
            this.mmiRightToLeft,
            this.menuItem7,
            this.mmiShowID});
            this.mmResult.Text = "&Options";
            this.mmResult.Popup += new System.EventHandler(this.mmResult_Popup);
            // 
            // mmiIgnoreCase
            // 
            this.mmiIgnoreCase.Index = 0;
            this.mmiIgnoreCase.Text = "&Ignore case";
            this.mmiIgnoreCase.Click += new System.EventHandler(this.mmiIgnoreCase_Click);
            // 
            // mmMultiline
            // 
            this.mmMultiline.Index = 1;
            this.mmMultiline.Text = "&Multiline";
            this.mmMultiline.Click += new System.EventHandler(this.mmMultiline_Click);
            // 
            // mmiExplicitCapture
            // 
            this.mmiExplicitCapture.Index = 2;
            this.mmiExplicitCapture.Text = "&Explicit capture";
            this.mmiExplicitCapture.Click += new System.EventHandler(this.mmiExplicitCapture_Click);
            // 
            // mmiECMAScript
            // 
            this.mmiECMAScript.Index = 3;
            this.mmiECMAScript.Text = "ECMA &script";
            this.mmiECMAScript.Click += new System.EventHandler(this.mmiECMAScript_Click);
            // 
            // mmiCultureInvariant
            // 
            this.mmiCultureInvariant.Index = 4;
            this.mmiCultureInvariant.Text = "&Culture invariant";
            this.mmiCultureInvariant.Click += new System.EventHandler(this.mmiCultureInvariant_Click);
            // 
            // mmiIgnorePatternWhitespace
            // 
            this.mmiIgnorePatternWhitespace.Index = 5;
            this.mmiIgnorePatternWhitespace.Text = "Ignore pattern &whitespace";
            this.mmiIgnorePatternWhitespace.Click += new System.EventHandler(this.mmiIgnorePatternWhitespace_Click);
            // 
            // mmiRightToLeft
            // 
            this.mmiRightToLeft.Index = 6;
            this.mmiRightToLeft.Text = "&Right to left";
            this.mmiRightToLeft.Click += new System.EventHandler(this.mmiRightToLeft_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 7;
            this.menuItem7.Text = "-";
            // 
            // mmiShowID
            // 
            this.mmiShowID.Index = 8;
            this.mmiShowID.Text = "Show ID / &Group name";
            this.mmiShowID.Click += new System.EventHandler(this.mmiGroupID_Click);
            // 
            // mmiResultSave
            // 
            this.mmiResultSave.Index = 3;
            this.mmiResultSave.Text = "Save &result";
            this.mmiResultSave.Click += new System.EventHandler(this.mmiResultSave_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 4;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mmiAbout});
            this.menuItem11.Text = "&Help";
            // 
            // mmiAbout
            // 
            this.mmiAbout.Index = 0;
            this.mmiAbout.Text = "&About";
            this.mmiAbout.Click += new System.EventHandler(this.mmiAbout_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 313);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(440, 16);
            this.statusBar.TabIndex = 8;
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(0, 128);
            this.txtResult.MaxLength = 0;
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(440, 185);
            this.txtResult.TabIndex = 13;
            this.txtResult.WordWrap = false;
            this.txtResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResult_KeyPress);
            // 
            // txtRegExp
            // 
            this.txtRegExp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegExp.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegExp.Location = new System.Drawing.Point(0, 104);
            this.txtRegExp.Name = "txtRegExp";
            this.txtRegExp.Size = new System.Drawing.Size(415, 20);
            this.txtRegExp.TabIndex = 14;
            this.txtRegExp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegExp_KeyPress);
            // 
            // lblSgart
            // 
            this.lblSgart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSgart.Location = new System.Drawing.Point(376, 80);
            this.lblSgart.Name = "lblSgart";
            this.lblSgart.Size = new System.Drawing.Size(56, 16);
            this.lblSgart.TabIndex = 13;
            this.lblSgart.TabStop = true;
            this.lblSgart.Text = "by Sgart.it";
            this.lblSgart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSgart_LinkClicked);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(376, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 24);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "&Search";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(0, 0);
            this.txtSource.MaxLength = 0;
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(368, 104);
            this.txtSource.TabIndex = 11;
            this.txtSource.Text = resources.GetString("txtSource.Text");
            this.txtSource.WordWrap = false;
            this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddFavorite);
            this.panel1.Controls.Add(this.txtRegExp);
            this.panel1.Controls.Add(this.lblSgart);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.txtSource);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 128);
            this.panel1.TabIndex = 15;
            // 
            // btnAddFavorite
            // 
            this.btnAddFavorite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFavorite.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFavorite.FlatAppearance.BorderSize = 0;
            this.btnAddFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFavorite.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFavorite.Image")));
            this.btnAddFavorite.Location = new System.Drawing.Point(411, 100);
            this.btnAddFavorite.Name = "btnAddFavorite";
            this.btnAddFavorite.Size = new System.Drawing.Size(26, 23);
            this.btnAddFavorite.TabIndex = 15;
            this.btnAddFavorite.UseVisualStyleBackColor = false;
            this.btnAddFavorite.Click += new System.EventHandler(this.btnAddFavorite_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 128);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(440, 3);
            this.splitter1.TabIndex = 16;
            this.splitter1.TabStop = false;
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // FormRe
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 329);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "FormRe";
            this.Text = "Regular Expression Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRe_FormClosing);
            this.Load += new System.EventHandler(this.FormRe_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Resource and file

        private static string ReadResourceString(string rn)
        {
            ResourceManager rm = new System.Resources.ResourceManager(
                "Sgart.RegularExpression.Resource",
                Assembly.GetExecutingAssembly()
                );

            return rm.GetString(rn);
        }

        private static string ReadFile(string fn, bool noDialog)
        {
            string s;
            try
            {
                using (StreamReader re = File.OpenText(fn))
                {
                    s = re.ReadToEnd();
                }
            }
            catch
            {
                if (noDialog != true)
                    MessageBox.Show("Can't read file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                s = "";
            }
            return s;
        }

        private static void WriteFile(string fn, string s)
        {
            try
            {
                using (StreamWriter wr = File.CreateText(fn))
                {
                    wr.Write(s);
                }
            }
            catch
            {
                MessageBox.Show("Can't write file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        #endregion

        #region Button events

        private void txtRegExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnStart_Click(sender, e);
            }
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (txtRegExp.Text != "")
            {
                int mCount = 0;
                StringBuilder sb = new StringBuilder();
                btnStart.Enabled = false;
                //Cursor = Cursors.WaitCursor;
                statusBar.Text = "";
                txtResult.Text = "";
                RegexOptions reo = RegexOptions.None;
                if (Fav.IgnoreCase == true) reo = RegexOptions.IgnoreCase;
                if (Fav.Multiline == true) reo |= RegexOptions.Multiline;
                if (Fav.ExplicitCapture == true) reo |= RegexOptions.ExplicitCapture;
                if (Fav.ECMAScript == true) reo |= RegexOptions.ECMAScript;
                if (Fav.CultureInvariant == true) reo |= RegexOptions.CultureInvariant;
                if (Fav.IgnorePatternWhitespace == true) reo |= RegexOptions.IgnorePatternWhitespace;
                if (Fav.RightToLeft == true) reo |= RegexOptions.RightToLeft;

                try
                {
                    Regex re = new Regex(txtRegExp.Text, reo);
                    MatchCollection mc = re.Matches(txtSource.Text);
                    foreach (Match m in mc)
                    {
                        if (m.Groups.Count == 1)
                        {
                            sb.Append(m.Groups[0].Value);
                        }
                        else
                        {
                            for (int i = 1; i < m.Groups.Count; i++)
                            {
                                if (Fav.ShowID == true)
                                    sb.AppendFormat("[{0}]", Fav.ExplicitCapture ? re.GroupNameFromNumber(i) : i.ToString());
                                if (i == 1 || Fav.ShowID == true)
                                    sb.Append(m.Groups[i].Value);
                                else
                                    sb.AppendFormat(" {0} ", m.Groups[i].Value);
                            }
                        }
                        sb.AppendLine();
                        mCount++;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Cursor = Cursors.Default ;
                if (mCount == 0)
                {
                    statusBar.Text = "No match";
                }
                else
                {
                    statusBar.Text = "Match: " + mCount.ToString();
                }
                txtResult.Text = sb.ToString();
                btnStart.Enabled = true;
            }
            else
            {
                statusBar.Text = "No expression";
            }
        }

        private void lblSgart_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sgart.it/?prg=RegularExpressionSearch");
        }

        private void btnAddFavorite_Click(object sender, EventArgs e)
        {
            mmiFavoritesAdd_Click(sender, e);
        }

        #endregion

        #region Events menu popup

        private void mmFile_Popup(object sender, EventArgs e)
        {
            mmiRestoreLastSession.Checked = Fav.RestoreLastSession;
        }

        private void mmResult_Popup(object sender, System.EventArgs e)
        {
            mmiIgnoreCase.Checked = Fav.IgnoreCase;
            mmiShowID.Checked = Fav.ShowID;
            mmMultiline.Checked = Fav.Multiline;
            mmiExplicitCapture.Checked = Fav.ExplicitCapture;
            mmiECMAScript.Checked = Fav.ECMAScript;
            mmiCultureInvariant.Checked = Fav.CultureInvariant;
            mmiIgnorePatternWhitespace.Checked = Fav.IgnorePatternWhitespace;
            mmiRightToLeft.Checked = Fav.RightToLeft;
        }

        private void mmFavorites_Popup(object sender, EventArgs e)
        {
            mmiFavoritesAdd.Enabled = !string.IsNullOrEmpty(txtRegExp.Text);
        }
        #endregion

        #region Events menu click

        private void mmiOpen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            txtSource.Text = ReadFile(openDialog.FileName, false);
        }

        private void mmExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void mmSave_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            WriteFile(saveDialog.FileName, txtSource.Text);
        }

        private void mmiSaveLastSession_Click(object sender, EventArgs e)
        {
            Fav.RestoreLastSession = !Fav.RestoreLastSession;
        }

        private void mmiIgnoreCase_Click(object sender, System.EventArgs e)
        {
            Fav.IgnoreCase = !Fav.IgnoreCase;
        }

        private void mmiResultSave_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            WriteFile(saveDialog.FileName, txtResult.Text);

        }

        private void mmiGroupID_Click(object sender, System.EventArgs e)
        {
            Fav.ShowID = !Fav.ShowID;
        }

        private void mmMultiline_Click(object sender, System.EventArgs e)
        {
            Fav.Multiline = !Fav.Multiline;
        }

        private void mmiExplicitCapture_Click(object sender, EventArgs e)
        {
            Fav.ExplicitCapture = !Fav.ExplicitCapture;
        }

        private void mmiECMAScript_Click(object sender, EventArgs e)
        {
            Fav.ECMAScript = !Fav.ECMAScript;
        }

        private void mmiCultureInvariant_Click(object sender, EventArgs e)
        {
            Fav.CultureInvariant = !Fav.CultureInvariant;
        }

        private void mmiIgnorePatternWhitespace_Click(object sender, EventArgs e)
        {
            Fav.IgnorePatternWhitespace = !Fav.IgnorePatternWhitespace;
        }

        private void mmiRightToLeft_Click(object sender, EventArgs e)
        {
            Fav.RightToLeft = !Fav.RightToLeft;
        }

        private void mmiFavoritesAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegExp.Text) == false)
            {
                using (FormFavoritesInput frm = new FormFavoritesInput())
                {
                    frm.Fav = Fav;
                    frm.KeyName = string.Format("Regex {0:yyyy-mm-dd HH:mm:ss}", DateTime.Now);
                    frm.RegexValue = txtRegExp.Text;
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        UpdateFavorites(false);
                    }
                }
            }
        }

        private void mmiFavoritesManage_Click(object sender, EventArgs e)
        {
            using (FormFavoritesManage frm = new FormFavoritesManage())
            {
                frm.Fav = Fav;
                DialogResult result = frm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    UpdateFavorites(false);
                }
            }
        }

        private void mmiAbout_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(helpAboutMessage, "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Key events

        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 1)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 1)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        #endregion

    }
}
