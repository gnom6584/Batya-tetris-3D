using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameField : MonoBehaviour
{
    [SerializeField]
    private int width = 10;
    [SerializeField]
    private int height = 24;
    [SerializeField]
    private int lenght = 1;

    private int[,,] blocks;
    
    public int Width { get => width; }
    public int Heigth { get => height; }
    public int Length { get => lenght; }

    public delegate void BlocksPlaced(int id, Vector3Int offset, params Vector3Int[] blocks);
    public delegate void LineCleared(int y, int lineCount);

    public event LineCleared OnLineСleaned;
    public event BlocksPlaced OnBlocksPlaced;
    
    private void Awake()
    {
        blocks = new int[Width, Heigth, Length];
    }

    public void PutBlocks(int id, Vector3Int offset, params Vector3Int[] points)
    {
        bool changed = false;
        foreach (var point in points)
        {
            var pos = new Vector3Int(offset.x + point.x, offset.y - point.y, offset.z + point.z);
            if (id != blocks[pos.x, pos.y, pos.z])
            {
                blocks[pos.x, pos.y, pos.z] = id;
                changed = true;
            }
        }
        if (changed)
        {
            OnBlocksPlaced(id, offset, points);
            ClearLines();
        }
    }

    public bool CheckBlocks(Vector3Int offset, params Vector3Int[] points)
    {
        foreach (var point in points)
        {
            var pos = new Vector3Int(offset.x + point.x, offset.y - point.y, offset.z + point.z);
            if (pos.x < 0 || pos.x >= Width
            || pos.y < 0 || pos.y >= Heigth
            || pos.z < 0 || pos.z >= Length)
            {
                return true;
            }
            if(blocks[pos.x, pos.y, pos.z] != 0)
            {
                return true;
            }
        }
        return false;
    }

    private void ClearLines()
    {
        int count = 0;
        for(int y = 0; y < Heigth; y++)
        {
            bool destroyFlag = true;
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Length; z++)
                {
                    if (blocks[x, y, z] == 0) {
                        destroyFlag = false;
                        break;
                    }
                }
            }
            if (destroyFlag)
            {             
                OnLineСleaned.Invoke(y, count);
                count++;
                for (int y1 = y; y1 < Heigth; y1++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        for (int z = 0; z < Length; z++)
                        {
                            blocks[x, y1, z] = (y1 + 1 < Heigth) ? blocks[x, y1 + 1, z] : 0;
                        }
                    }
                }
                y--;
            }
        }
    }

    [SerializeField]
    Material lineMaterial;
    //DEBUG 
    public void OnRenderObject()
    {
      
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
 

        GL.Begin(GL.LINES);

        const float offset = -0.5f;

        GL.Vertex(new Vector3(offset, offset, offset));
        GL.Vertex(new Vector3(width + offset, offset, offset));
        GL.Vertex(new Vector3(offset, offset, offset));
        GL.Vertex(new Vector3(offset, offset, lenght + offset));
        GL.Vertex(new Vector3(offset, offset, offset));
        GL.Vertex(new Vector3(offset, height + offset, offset));
        GL.Vertex(new Vector3(width + offset, offset, offset));
        GL.Vertex(new Vector3(width + offset, offset, lenght + offset));
        GL.Vertex(new Vector3(width + offset, offset, lenght + offset));
        GL.Vertex(new Vector3(offset, offset, lenght + offset));

        GL.Vertex(new Vector3(offset, offset, lenght + offset));
        GL.Vertex(new Vector3(offset, offset + height, lenght + offset));

        GL.Vertex(new Vector3(width + offset, offset, offset));
        GL.Vertex(new Vector3(width + offset, offset + height, offset));
    
        GL.Vertex(new Vector3(width + offset, offset, lenght + offset));
        GL.Vertex(new Vector3(width + offset, offset + height, lenght + offset));

        GL.Vertex(new Vector3(width + offset, offset + height, offset));
        GL.Vertex(new Vector3(width + offset, offset + height, lenght + offset));
        GL.Vertex(new Vector3(width + offset, offset + height, lenght + offset));
        GL.Vertex(new Vector3(offset, offset + height, lenght + offset));

        GL.Vertex(new Vector3(offset, offset+height, offset));
        GL.Vertex(new Vector3(width + offset, offset + height, offset));
        GL.Vertex(new Vector3(offset, offset+height, offset));
        GL.Vertex(new Vector3(offset, offset+height, lenght + offset));

        GL.End();
        GL.PopMatrix();
    }
}
