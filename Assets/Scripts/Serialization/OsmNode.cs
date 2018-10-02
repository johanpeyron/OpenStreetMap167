using System.Xml;

class OsmNode : BaseOsm
{

    public ulong ID { get; private set; }

    public float Latitude { get; private set; }

    public float Longitude { get; private set; }

    public float X { get; private set; }
    
    public float Y { get; private set; }
    

    public OsmNode(XmlNode node)
    {
        ID = GetAttributes<ulong>("id", node.Attributes);
        Latitude = GetAttributes<float>("lat", node.Attributes);
        Longitude = GetAttributes<float>("lon", node.Attributes);

        X = (float)MercatorProjection.lonToX(Longitude);
        Y = (float)MercatorProjection.latToY(Latitude);


    }

}
