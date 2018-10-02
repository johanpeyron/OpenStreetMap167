using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

class BaseXmlElementParser
{
    // For each class we need a list of property names and how to access them
    // from the node.
    // Some nodes are inner text, some are attributes

    class PropertyDetails() 
    {
        public string Name { get; private set; }
    }

    public BaseXmlElementParser(XmlNode node)
    {

    }
}
