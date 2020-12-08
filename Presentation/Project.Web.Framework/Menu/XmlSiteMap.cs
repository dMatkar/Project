using Project.Core;
using System;
using System.IO;
using System.Xml;

namespace Project.Web.Framework.Menu
{
    public class XmlSiteMap
    {
        #region Constructor
       public XmlSiteMap()
        {
            RootNode = new SiteMapNode();
        }
        #endregion

        #region Properties
        public SiteMapNode RootNode { get; set; }

        #endregion

        #region Methods

        public void LoadFrom(string path)
        {
            path = CommonHelper.MapPath(path);
            string content = File.ReadAllText(path);
            if (!string.IsNullOrWhiteSpace(content))
            {
                using (StringReader stringReader = new StringReader(content))
                {
                    using (XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings()
                    {
                        CloseInput = true,
                        IgnoreComments = true,
                        IgnoreProcessingInstructions = true,
                        IgnoreWhitespace = true
                    }))
                    {
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(xmlReader);
                        XmlElement xmlElement = xmlDocument.DocumentElement;
                        if (xmlElement != null && xmlElement.HasChildNodes)
                        {
                            Iterate(RootNode, xmlElement.FirstChild);
                        }
                    }
                }
            }
        }

        public void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);
            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    if (xmlNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SiteMapNode newNode = new SiteMapNode();
                        siteMapNode.ChildNodes.Value.Add(newNode);
                        Iterate(newNode, node);
                    }
                }
            }
        }

        public void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            siteMapNode.Controller = GetAttributeValue(xmlNode.Attributes, "controller");
            siteMapNode.Action = GetAttributeValue(xmlNode.Attributes, "action");
            siteMapNode.IconClass= GetAttributeValue(xmlNode.Attributes, "IconClass");
            siteMapNode.SystemName= GetAttributeValue(xmlNode.Attributes, "systemName");
        }

        public string GetAttributeValue(XmlAttributeCollection xmlAttribute, string attributeName)
        {
            string attributeValue = string.Empty;
            if (xmlAttribute == null && xmlAttribute.Count == 0)
                return attributeValue;

            var tempAttribute = xmlAttribute[attributeName];
            if (tempAttribute != null)
            {
                attributeValue = tempAttribute.Value;
            }
            return attributeValue;
        }

        #endregion
    }
}
