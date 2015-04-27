using ModelEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace XMLConvertation
{
    public class XmlConverter : ITestConvert
    {
        public NewTestEntityModel getFromBytes(byte[] bytes)
        {
            string str = System.Text.Encoding.UTF8.GetString(bytes);
            XDocument doc = XDocument.Parse(str);
            NewTestEntityModel model = new NewTestEntityModel();
            model.Regex = (string)doc.Element("AttachmentContent").Element("Regex");
            model.Description = (string)doc.Element("AttachmentContent").Element("Description");
            model.States = (int)doc.Element("AttachmentContent").Element("StateCount");
            model.Values = (int)doc.Element("AttachmentContent").Element("ValueCount");
            model.FinalStates = new int?[model.States];
            model.ValuesArray = new string[model.Values];
            model.GraphArray = new string[model.States * model.Values];
            int i = 0;
            foreach (XElement element in doc.Element("AttachmentContent").Element("ValueArray").Elements("Value"))
            {
                model.ValuesArray[i++] = (string)element;
            }
            i = 0;
            foreach (XElement element in doc.Element("AttachmentContent").Element("StateArray").Elements("State"))
            {
                int num;
                if (Int32.TryParse((string)element, out num))
                    model.FinalStates[i++] = num;
            }
            i = 0;
            foreach (XElement element in doc.Element("AttachmentContent").Element("GraphArray").Elements("Row"))
            {
                foreach (XElement element1 in element.Elements("El"))
                {
                    model.GraphArray[i++] = (string)element1;
                }
            }
            return model;
        }

        public byte[] getFromNewTest(NewTestEntityModel model)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            XElement head = new XElement("AttachmentContent");
            head.Add(new XElement("Regex", model.Regex));
            head.Add(new XElement("Description", model.Description));
            head.Add(new XElement("StateCount", model.States));
            head.Add(new XElement("ValueCount", model.Values));
            XElement valArray = new XElement("ValueArray");
            foreach (var val in model.ValuesArray)
            {
                valArray.Add(new XElement("Value", val));
            }
            XElement stateArray = new XElement("StateArray");
            foreach (var state in model.FinalStates)
            {
                stateArray.Add(new XElement("State", state));
            }
            XElement graphArray = new XElement("GraphArray");
            for (int i = 0; i!= model.States; i++)
            {
                XElement row = new XElement("Row");
                for (int j = 0; j!=model.Values; j++)
                {
                    row.Add(new XElement("El", model.GraphArray[i * model.Values + j]));
                }
                graphArray.Add(row);
            }
            head.Add(valArray);
            head.Add(stateArray);
            head.Add(graphArray);
            doc.Add(head);
            return System.Text.Encoding.UTF8.GetBytes(doc.ToString());
        }
    }
}