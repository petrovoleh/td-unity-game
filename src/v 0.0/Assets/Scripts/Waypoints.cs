using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : Singleton<Waypoints>
{
    public Transform[] waypoints;

    /* //failed atempt at making waypoints automatic
    private int waypointCount = 0;

    public void AddWaypoints(GameObject newObject)
    {
        Transform waypointPosition = newObject.transform;
        waypoints[waypointCount] = waypointPosition;
        waypointCount++;
    }*/
}
