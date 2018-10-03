using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MapReader))]
class RoadMaker : MonoBehaviour
{
    MapReader map;

    IEnumerator Start()
    {
        map = GetComponent<MapReader>();
        while (!map.IsReady)
        {
            yield return null;
        }

        // TODO: Proces map data to create buildings

    }

}
