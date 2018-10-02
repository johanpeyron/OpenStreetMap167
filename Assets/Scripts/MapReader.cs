using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapReader : MonoBehaviour {

    [Tooltip("The resource file that contains the OSM map data")]
    public string resourceFile;

    // Use this for initialization
	void Start () {
        var txtAsset = Resources.Load<TextAsset>(resourceFile);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(txtAsset.text);

        SetBounds(doc.SelectSingleNode("/osm/bounds"));
        GetNodes(doc.SelectNodes("/osm/node"));
        GetWays(doc.SelectNodes("/osm/way"));

	}

    void GetWays(XmlNodeList xmlNodeList)
    {
    
    }

    void GetNodes(XmlNodeList xmlNodeList)
    {
    
    }

    void SetBounds(XmlNode xmlNode)
    {
    
    }

    // Update is called once per frame
    void Update () {
		
	}
}
