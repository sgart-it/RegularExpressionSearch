using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Sgart.RegularExpression
{
    [XmlRoot("Item")]
    public class FavoriteItem
    {

        public FavoriteItem()
        {
            Name = "";
            Value = "";
        }
        
        public FavoriteItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public FavoriteItem(FavoriteItem item)
        {
            this.Name = item.Name;
            this.Value = item.Value;
        }

        [XmlAttribute]
        public string Name { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
