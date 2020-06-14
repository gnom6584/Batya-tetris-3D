using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SlowVoxelMesh
{
    public Mesh mesh;
    private List<Vector3> vertices = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    private bool index32 = false;

    private SlowVoxelMesh(Mesh mesh)
    {
        this.mesh = mesh;
        this.mesh.MarkDynamic();
    }

    private void CreatCube(Vector3Int position, HashSet<Vector3Int> map)
    {
        if (!map.Contains(new Vector3Int(0, 0, -1) + position))
        {
            vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f) + position);
            vertices.Add(new Vector3(0.5f, -0.5f, -0.5f) + position);
            vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f) + position);
            vertices.Add(new Vector3(0.5f, 0.5f, -0.5f) + position);
            Vector3 normal = new Vector3(0, 0, -1);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
        if (!map.Contains(new Vector3Int(0, 0, 1) + position))
        {
            vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f) + position);
            vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f) + position);
            vertices.Add(new Vector3(0.5f, -0.5f, 0.5f) + position);
            vertices.Add(new Vector3(0.5f, 0.5f, 0.5f) + position);
            Vector3 normal = new Vector3(0, 0, 1);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
        if (!map.Contains(new Vector3Int(-1, 0, 0) + position))
        {
            vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f) + position);
            vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f) + position);
            vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f) + position);
            vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f) + position);
            Vector3 normal = new Vector3(-1, 0, 0);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
        if (!map.Contains(new Vector3Int(1, 0, 0) + position))
        {
            vertices.Add(new Vector3(0.5f, -0.5f, -0.5f) + position);
            vertices.Add(new Vector3(0.5f, -0.5f, 0.5f) + position);
            vertices.Add(new Vector3(0.5f, 0.5f, -0.5f) + position);
            vertices.Add(new Vector3(0.5f, 0.5f, 0.5f) + position);
            Vector3 normal = new Vector3(1, 0, 0);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
        if (!map.Contains(new Vector3Int(0, 1, 0) + position))
        {
            vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f) + position);
            vertices.Add(new Vector3(0.5f, 0.5f, -0.5f) + position);
            vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f) + position);
            vertices.Add(new Vector3(0.5f, 0.5f, 0.5f) + position);
            Vector3 normal = new Vector3(0, 1, 0);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
        if (!map.Contains(new Vector3Int(0, -1, 0) + position))
        {
            vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f) + position);
            vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f) + position);
            vertices.Add(new Vector3(0.5f, -0.5f, -0.5f) + position);
            vertices.Add(new Vector3(0.5f, -0.5f, 0.5f) + position);
            Vector3 normal = new Vector3(0, -1, 0);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
    }

    public void VerticesLoop(System.Func<Vector3, Vector3> func)
    {
        for(int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = func(vertices[i]); 
        }
        mesh.SetVertices(vertices);
    }

    public void Set(HashSet<Vector3Int> map)
    {
        if (map == null)
        {
            return;
        }
        vertices.Clear();
        mesh.uv = null;
        mesh.triangles = null;
        foreach(var position in map)
        {
            CreatCube(position, map);
        }
        int[] triangles = new int[vertices.Count * 3 / 2];
        for(int i = 0, v = 0; i < triangles.Length; i += 6, v += 4)
        {
            triangles[i] = v; 
            triangles[i + 1] = 2 + v; 
            triangles[i + 2] = 1 + v;
            triangles[i + 3] = 1 + v;
            triangles[i + 4] = 2 + v;
            triangles[i + 5] = 3 + v;
        }
        Vector2[] uv = new Vector2[vertices.Count];
        for(int i = 0; i < uv.Length; i += 4)
        {
            uv[i] = new Vector2(0.0f, 0.0f);
            uv[i + 1] = new Vector2(1.0f, 0.0f);
            uv[i + 2] = new Vector2(0.0f, 1.0f);
            uv[i + 3] = new Vector2(1.0f, 1.0f);
        }
        if(!index32 && vertices.Count > 65535)
        {
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            index32 = true;
        }
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.triangles = triangles;
        mesh.uv = uv;
        normals.Clear();
    }

    public static implicit operator SlowVoxelMesh(Mesh mesh) => new SlowVoxelMesh(mesh);
}