using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapReader))]
class BuildingMaker : MonoBehaviour
{
    MapReader map;

    public Material building;

    IEnumerator Start()
    {
        map = GetComponent<MapReader>();
        while (!map.IsReady)
        {
            yield return null;
        }

        foreach (var way in map.ways.FindAll((w) => { return w.IsBuilding && w.NodeIds.Count > 1; }))
        {
            GameObject go = new GameObject();
            // Centerpoint of the building in local space
            Vector3 localOrigin = GetCentre(way);
            // Centerpoint of the building translated to world space
            go.transform.position = localOrigin;

            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();

            mr.material = building;

            List<Vector3> vectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indicies = new List<int>();

            for (int i = 1; i < way.NodeIds.Count; i++)
            { 
                OsmNode p1 = map.nodes[way.NodeIds[i - 1]];
                OsmNode p2 = map.nodes[way.NodeIds[i]];

                Vector3 v1 = p1 - localOrigin;
                Vector3 v2 = p2 - localOrigin;
                Vector3 v3 = v1 + new Vector3(0, way.Height, 0);
                Vector3 v4 = v2 + new Vector3(0, way.Height, 0);

                vectors.Add(v1);
                vectors.Add(v2);
                vectors.Add(v3);
                vectors.Add(v4);

                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);

                int idx1, idx2, idx3, idx4;
                idx4 = vectors.Count - 1;
                idx3 = vectors.Count - 2;
                idx2 = vectors.Count - 3;
                idx1 = vectors.Count - 4;

                // first triangle v1, v3, v2
                indicies.Add(idx1);
                indicies.Add(idx3);
                indicies.Add(idx2);

                // second         v3, v4, v2
                indicies.Add(idx3);
                indicies.Add(idx4);
                indicies.Add(idx2);

                // third          v2, v3, v1
                indicies.Add(idx2);
                indicies.Add(idx3);
                indicies.Add(idx1);

                // fourth         v2, v4, v3
                indicies.Add(idx2);
                indicies.Add(idx4);
                indicies.Add(idx3);

                mf.mesh.vertices = vectors.ToArray();
                mf.mesh.normals = vectors.ToArray();
                mf.mesh.triangles = indicies.ToArray();

                yield return null;
            }





        }

        Vector3 GetCentre(OsmWay way)
        {
            Vector3 total = Vector3.zero;

            foreach(var id in way.NodeIds)
            {
                total += map.nodes[id];
            }

            return total / way.NodeIds.Count;

        }
    }

}
