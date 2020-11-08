using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace cw2
{
    [XmlRoot("uczelnia")]
    public class StudentConteiner
    {
        [XmlElement("studenci")]
        public List<Student> Studenci { get; set; }
        [XmlAttribute("createdAt")]
        public string CreatedAt = DateTime.Now.ToString("dd.MM.yyyy");
        [XmlAttribute("author")]
        public string Author = "Damian Goraj";
        public StudentConteiner () {}
    }
}
