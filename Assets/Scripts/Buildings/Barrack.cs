using UnityEngine;
using System.Collections;

public class Barrack : Building{
    public Unite unitToSpawn;
    public uint nbUnitToSpawnPerRound;
    public uint spawnInterval;

    private PointPassage path;

    public PointPassage Path{
        get {return path;}
    }

    public override void Start()
    {
        base.Start();
        path = choosePath();
    }

    PointPassage choosePath()
    {
        PointPassage nextPath = null;
        PointPassage[] points = FindObjectsOfType<PointPassage>();
        float distance;
        float minDistance = -1;
        foreach (PointPassage point in points)
        {
            distance = Vector2.Distance(transform.position, point.transform.position);
            if (minDistance == -1) minDistance = distance;
            if(distance <= minDistance)
            {
                minDistance = distance;
                nextPath = point;
            }
        }
        return nextPath;
    }
}
