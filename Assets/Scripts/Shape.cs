using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Shape", menuName = "Shape", order = 51)]
public class Shape : ScriptableObject
{
    public int materialId = 1;
    public Vector3Int pivot;
    public Vector3Int[] points;
    public void Rotate(float pitch, float roll, float yaw)
    {
        var cosa = Mathf.Cos(yaw);
        var sina = Mathf.Sin(yaw);
                       
        var cosb = Mathf.Cos(pitch);
        var sinb = Mathf.Sin(pitch);
                       
        var cosc = Mathf.Cos(roll);
        var sinc = Mathf.Sin(roll);

        var Axx = cosa * cosb;
        var Axy = cosa * sinb * sinc - sina * cosc;
        var Axz = cosa * sinb * cosc + sina * sinc;

        var Ayx = sina * cosb;
        var Ayy = sina * sinb * sinc + cosa * cosc;
        var Ayz = sina * sinb * cosc - cosa * sinc;

        var Azx = -sinb;
        var Azy = cosb * sinc;
        var Azz = cosb * cosc;

        for (var i = 0; i < points.Length; i++)
        {
            float px = points[i].x - pivot.x;
            float py = points[i].y - pivot.y;
            float pz = points[i].z - pivot.z;

            points[i].x = Mathf.RoundToInt(Axx * px + Axy * py + Axz * pz) + pivot.x;
            points[i].y = Mathf.RoundToInt(Ayx * px + Ayy * py + Ayz * pz) + pivot.y;
            points[i].z = Mathf.RoundToInt(Azx * px + Azy * py + Azz * pz) + pivot.z;
        }
    }
}
