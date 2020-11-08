using System;
using System.Xml.Serialization;

namespace cw2
{
    [XmlType("studies")]
    public class Study
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("mode")]
        public string Mode { get; set; }

        public Study()
        {
        }
    }
}
