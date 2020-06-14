using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//шок wtf контент - полный пиздец
public class TetrisMeshRenderer : MonoBehaviour
{
    private const float destroyDelay = 0.0f;

    private static Vector3[] back = new Vector3[] 
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f)
    };

    private static Vector3[] front = new Vector3[]
    {
        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f)
    };

    private static Vector3[] left = new Vector3[]
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f)
    };

    private static Vector3[] right = new Vector3[]
    {
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, 0.5f)
    };

    private static Vector3[] top = new Vector3[]
    {
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f)
    };

    private static Vector3[] bot = new Vector3[]
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, 0.5f)
    };


    private Mesh mesh;
    public List<Vector3> cubes = new List<Vector3>();
    public List<int> cubesY = new List<int>();

    public void SetVertices(bool clear = false)
    {
        if (clear)
        {
            mesh.triangles = null;
            mesh.uv = null;
            mesh.normals = null;
        }
        var verticesArray = new Vector3[cubes.Count * 24];
        for(int i = 0; i < cubes.Count; i++)
        {
            int index = i * 24;
            for (int j = 0; j < 4; j++)
            {
                verticesArray[index + j] = back[j] + cubes[i];
            }
            for (int j = 0; j < 4; j++)
            {
                verticesArray[index + j + 4] = front[j] + cubes[i];
            }
            for (int j = 0; j < 4; j++)
            {
                verticesArray[index + j + 8] = left[j] + cubes[i];
            }
            for (int j = 0; j < 4; j++)
            {
                verticesArray[index + j + 12] = right[j] + cubes[i];
            }
            for (int j = 0; j < 4; j++)
            {
                verticesArray[index + j + 16] = top[j] + cubes[i];
            }
            for (int j = 0; j < 4; j++)
            {
                verticesArray[index + j + 20] = bot[j] + cubes[i];
            }
        }
        mesh.vertices = verticesArray;
    }

    public void SetTriangles()
    {
        int[] triangles = new int[mesh.vertices.Length * 3 / 2];
        for (int i = 0, v = 0; i < triangles.Length; i += 6, v += 4)
        {
            triangles[i + 0] = v;
            triangles[i + 1] = 2 + v;
            triangles[i + 2] = 1 + v;
            triangles[i + 3] = 1 + v;
            triangles[i + 4] = 2 + v;
            triangles[i + 5] = 3 + v;
        }
        mesh.triangles = triangles;
        Vector2[] uv = new Vector2[mesh.vertices.Length];
        for (int i = 0; i < uv.Length; i += 4)
        {
            uv[i] = new Vector2(0.0f, 0.0f);
            uv[i + 1] = new Vector2(1.0f, 0.0f);
            uv[i + 2] = new Vector2(0.0f, 1.0f);
            uv[i + 3] = new Vector2(1.0f, 1.0f);
        }
        mesh.uv = uv;
        Vector3[] normals = new Vector3[mesh.vertices.Length];
        for(int i = 0; i < normals.Length; i += 24)
        {
            var back = new Vector3(0.0f, 0.0f, -1.0f);
            for(int j = 0; j < 4; j++)
            {
                normals[i + j + 0] = back; 
            }
            var front = new Vector3(0.0f, 0.0f, 1.0f);
            for (int j = 0; j < 4; j++)
            {
                normals[i + j + 4] = front;
            }
            var left = new Vector3(-1.0f, 0.0f, 0.0f);
            for (int j = 0; j < 4; j++)
            {
                normals[i + j + 8] = left;
            }
            var right = new Vector3(1.0f, 0.0f, 0.0f);
            for (int j = 0; j < 4; j++)
            {
                normals[i + j + 12] = right;
            }
            var top = new Vector3(0.0f, 1.0f, 0.0f);
            for (int j = 0; j < 4; j++)
            {
                normals[i + j + 16] = top;
            }
            var bot = new Vector3(0.0f, -1.0f, 0.0f);
            for (int j = 0; j < 4; j++)
            {
                normals[i + j + 20] = bot;
            }
        }
        mesh.normals = normals;
        mesh.RecalculateTangents();
    }

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
    }

    public void PassBlocks(Vector3Int[] points)
    {
        foreach(var point in points)
        {
            cubes.Add(point);
            cubesY.Add(point.y);
        }
        SetVertices();
        SetTriangles();
    }

    private int linesCount = 0;
    private int lastLineCount = 0;
    private Coroutine update; 

    public void RemoveLine(int y)
    {
        for (int i = cubes.Count - 1; i >= 0; i--)
        {
            Vector3 cube = cubes[i];
            if(Mathf.Approximately(cube.y - linesCount, y))
            {
                cubes.Remove(cube);
                cubesY.RemoveAt(i);               
            }
            else if(cubesY[i] > y)
            {
                cubesY[i]--;
            }
        }
        if (linesCount == 0)
        {
            if (update != null)
            {
                StopCoroutine(update);
            }
            update = StartCoroutine(UpdateCall());
        }
        linesCount++;
        lastLineCount = linesCount;
    }
    
    private IEnumerator UpdateCall()
    {
        yield return new WaitForSeconds(destroyDelay);
        SetVertices(true);
        SetTriangles();
        bool stop = false;
        while (!stop)
        {
            stop = true;
            for (int i = 0; i < cubes.Count; i++)
            {
                var old = cubes[i];
                cubes[i] = new Vector3(cubes[i].x, Mathf.MoveTowards(cubes[i].y, cubesY[i], Time.deltaTime * (3.0f + lastLineCount)), cubes[i].z);
                if(cubes[i] != old)
                {
                    stop = false;
                }
            }
            SetVertices();
            yield return null;
        }
    }

    private void Update()
    {
        linesCount = 0;   
    }
}
