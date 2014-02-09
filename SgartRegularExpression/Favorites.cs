using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Sgart.RegularExpression
{
    [Serializable]
    [XmlRoot("Favorite")]
    public class Favorite
    {
        public Favorite()
        {
            Items = new List<FavoriteItem>();

            WindowState = System.Windows.Forms.FormWindowState.Normal;
            WindowWidth = 456;
            WindowHeigth = 388;

            SplitPosition = 138;
            RestoreLastSession = true;
            IgnoreCase = true;
            Multiline = true;
            ExplicitCapture = false;
            ECMAScript = false;
            CultureInvariant = false;
            IgnorePatternWhitespace = false;
            RightToLeft = false;
            ShowID = false;
            TextInput = "";
            RegExInput = "";
            FileFound = false;
        }

        public string GetItemValue(string key)
        {
            FavoriteItem item = Items.FirstOrDefault(q => q.Value == key);
            if (item == null)
                return "";
            else
                return item.Value;
        }

        public void SetItemValue(string key, string value)
        {
            FavoriteItem item = Items.FirstOrDefault(q => q.Value == key);
            if (item == null)
                throw new ArgumentException();
            else
                item.Value = value;
        }

        [XmlArray("Items"), XmlArrayItem("Item")]
        public List<FavoriteItem> Items { get; set; }

        [XmlAttribute("WindowState")]
        public System.Windows.Forms.FormWindowState WindowState { get; set; }

        [XmlAttribute("Width")]
        public int WindowWidth { get; set; }

        [XmlAttribute("Height")]
        public int WindowHeigth { get; set; }

        [XmlAttribute("SplitPosition")]
        public int SplitPosition { get; set; }

        [XmlAttribute("RestoreLast")]
        public bool RestoreLastSession { get; set; }

        [XmlAttribute("IgnoreCase")]
        public bool IgnoreCase { get; set; }

        [XmlAttribute("Multiline")]
        public bool Multiline { get; set; }

        [XmlAttribute("ExplicitCapture")]
        public bool ExplicitCapture { get; set; }

        [XmlAttribute("ECMAScript")]
        public bool ECMAScript { get; set; }

        [XmlAttribute("CultureInvariant")]
        public bool CultureInvariant { get; set; }

        [XmlAttribute("IgnorePatternWhitespace")]
        public bool IgnorePatternWhitespace { get; set; }

        [XmlAttribute("RightToLeft")]
        public bool RightToLeft { get; set; }
        
        [XmlAttribute("ShowID")]
        public bool ShowID { get; set; }

        [XmlElement("RegEx")]
        public string RegExInput { get; set; }

        [XmlElement("Text")]
        //[XmlIgnore]
        public string TextInput { get; set; }

        [XmlIgnore]
        public bool FileFound { get; set; }

        #region Load and Save

        private static string FileName
        {
            get
            {
                System.Reflection.Module mod = (System.Reflection.Assembly.GetExecutingAssembly().GetModules())[0];
                return mod.FullyQualifiedName + ".Favorites.xml";
            }
        }

        public static Favorite Load()
        {
            if (File.Exists(FileName) == true)
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Favorite));
                using (TextReader strm = new StreamReader(FileName, Encoding.UTF8))
                {
                    try
                    {
                        //valorizzo l'oggetto con le proprietà lette dal file
                        Favorite f = (Favorite)xs.Deserialize(strm);
                        f.FileFound = true;
                        f.TextInput = f.TextInput.Replace("\n", "\r\n");
                        return f;
                    }
                    catch { }
                }
            }
            return new Favorite();
        }

        public static void Save(Favorite obj)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Favorite));
            using (TextWriter strm = new StreamWriter(FileName, false, Encoding.UTF8))
            {
                strm.NewLine = "\r\n";
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xs.Serialize(strm, obj, ns);
            }
        }

        #endregion
    }
}
