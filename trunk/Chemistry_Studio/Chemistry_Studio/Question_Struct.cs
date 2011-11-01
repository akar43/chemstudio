using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace Chemistry_Studio
{
    class Question_Struct
    {
        string id;
        string question;
        List<string> options;

        public Question_Struct()
        {
            options = new List<string>();
        }

        public Question_Struct(string fileName)
        {
            options = new List<string>();
            XmlDocument xmlDoc = null;
            // Setting the XmlReaderSettings so as to ignore Comments from the Input XML file.
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreWhitespace = true;
            readerSettings.IgnoreComments = true;

            using (XmlReader reader = XmlReader.Create(fileName, readerSettings))
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
            }
            XmlNode questionTag = xmlDoc.FirstChild; // Skipping the root node.
            this.id = questionTag.ChildNodes[0].InnerText;
            this.question = questionTag.ChildNodes[1].InnerText;

            for (int i = 2; i < questionTag.ChildNodes.Count; i++)
            {
                options.Add(questionTag.ChildNodes[i].InnerText);
            }
        }

        public override string ToString()
        {
            string representation = this.id + ") ";
            representation += this.question + "\n\n";
            for (int i = 0; i < options.Count; i++)
            {
                representation += (i + 1).ToString() + ". " + options[i] + "\n";
            }
            return representation;
        }

    }
}
