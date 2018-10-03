using System.Collections.Generic;
using System.Xml;

class OsmWay : BaseOsm
{
    public ulong ID { get; private set; }

    public bool Visible { get; private set; }

    public List<ulong> NodeIds { get; private set; }

    public bool IsBoundary { get; private set; }

    public bool IsBuilding { get; private set; }

    public float Height { get; private set; }


    public OsmWay(XmlNode node)
    {
        NodeIds = new List<ulong>();
        Height = 3.0f;
        
        ID = GetAttribute<ulong>("id", node.Attributes);
        Visible = GetAttribute<bool>("visible", node.Attributes);

        XmlNodeList nds = node.SelectNodes("nd");

        foreach(XmlNode n in nds)
        {
            ulong refNo = GetAttribute<ulong>("ref", n.Attributes);
            NodeIds.Add(refNo);
        }

        //  TODO: Determine what type of way this is; is it a road, boundary etc.

        if (NodeIds.Count > 1)
        {
            IsBoundary = NodeIds[0] == NodeIds[NodeIds.Count - 1];
        }

        XmlNodeList tags = node.SelectNodes("tag");
        foreach( XmlNode t in tags)
        {
            string key = GetAttribute<string>("k", t.Attributes);
            
            // <tag k="building:levels" v="2"/>
            if (key == "building:levels")
            {
                Height = 3.0f * GetAttribute<float>("v", t.Attributes);
            }
            else if (key == "height")
            {
                Height = 0.3048f * GetAttribute<float>("v", t.Attributes);
            }
            else if (key == "building")
            {
                IsBuilding = GetAttribute<string>("v", t.Attributes) == "yes";
            }
        }

    }

}
