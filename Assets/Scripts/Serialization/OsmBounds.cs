using System.Xml;
using UnityEngine;

class OsmBounds : BaseOsm
{
    public float MinLat { get; private set; }

    public float MaxLat { get; private set; }

    public float MinLon { get; private set; }

    public float MaxLon { get; private set; }

    public Vector3 Centre { get; private set; }
    

    public OsmBounds(XmlNode node)
    {
        MinLat = GetAttributes<float>("minlat", node.Attributes);
        MaxLat = GetAttributes<float>("maxlat", node.Attributes);
        MinLon = GetAttributes<float>("minlon", node.Attributes);
        MaxLon = GetAttributes<float>("maxlon", node.Attributes);

        float x = (float)(MercatorProjection.lonToX(MaxLon) + MercatorProjection.lonToX(MinLon)) / 2;
        float y = (float)(MercatorProjection.latToY(MaxLat) + MercatorProjection.latToY(MinLat)) / 2;

        Centre = new Vector3(x, 0, y);
    }
}
