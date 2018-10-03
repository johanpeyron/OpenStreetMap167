﻿using System.Collections.Generic;
using System.Xml;
using UnityEngine;

class MapReader : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<ulong, OsmNode> nodes;
    
    [HideInInspector]
    public List<OsmWay> ways;
    
    [HideInInspector]
    public OsmBounds bounds;

    [Tooltip("The resource file that contains the OSM map data")]
    public string resourceFile;

    public bool IsReady { get; private set; }

    // Use this for initialization
	void Start () {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new List<OsmWay>();
    
        var txtAsset = Resources.Load<TextAsset>(resourceFile);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(txtAsset.text);

        SetBounds(doc.SelectSingleNode("/osm/bounds"));
        GetNodes(doc.SelectNodes("/osm/node"));
        GetWays(doc.SelectNodes("/osm/way"));

        IsReady = true;
	}

    void Update()
    {
        foreach(OsmWay w in ways)
        {
            if (w.Visible)
            {
                Color c = Color.cyan;  // cyan for buildings
                if (!w.IsBoundary)
                {
                    c = Color.red;     // red for roads
                }

                for (int i = 1; i < w.NodeIds.Count; i++)
                {
                    OsmNode p1 = nodes[w.NodeIds[i - 1]];
                    OsmNode p2 = nodes[w.NodeIds[i]];

                    Vector3 v1 = p1 - bounds.Centre;
                    Vector3 v2 = p2 - bounds.Centre;

                    Debug.DrawLine(v1, v2, c);

                }
                
            }
        }
    }

    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach(XmlNode node in xmlNodeList)
        {
            OsmWay way = new OsmWay(node);
            ways.Add(way);
        }
    }

    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    
    }

    void SetBounds(XmlNode xmlnode)
    {
        bounds = new OsmBounds(xmlnode);
    
    }

}
