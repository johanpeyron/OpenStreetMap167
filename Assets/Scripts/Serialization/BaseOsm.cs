using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

class BaseOsm
{
    protected T GetAttributes<T>(string attrName, XmlAttributeCollection attributes)
    {
        // TODO: We are going to assume 'attrName' exists in the collection
        string strValue = attributes[attrName].Value;
        return (T)Convert.ChangeType(strValue, typeof(T));
    }

}
