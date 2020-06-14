using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisMeshRendererInit : MonoBehaviour
{
    [SerializeField]
    private GameField gameField = null;
    [SerializeField]
    private MaterialPallete materialPallete = null;
    [SerializeField]
    private Material material;

    private TetrisMeshRenderer[] subRenderers;



    private void Start()
    {
        subRenderers = new TetrisMeshRenderer[materialPallete.Length];
        for(int i = 0; i  < materialPallete.Length; i++)
        {
            var newRenderer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newRenderer.layer = 8;
            subRenderers[i] = newRenderer.AddComponent<TetrisMeshRenderer>();
            newRenderer.GetComponent<Renderer>().material = materialPallete[i];
        }
        gameField.OnBlocksPlaced += SetBlocks;
        gameField.OnLineСleaned += ClearLines;
    }

    private void SetBlocks(int id, Vector3Int offset, params Vector3Int[] points)
    {
        var passPoint = new Vector3Int[points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            passPoint[i] = new Vector3Int(offset.x + points[i].x, offset.y - points[i].y, offset.z + points[i].z);
        }
        subRenderers[id - 1].PassBlocks(passPoint);
    }

    private void ClearLines(int y, int lineCount)
    {
        foreach(var r in subRenderers)
        {
            r.RemoveLine(y);
        }
        var animation = new GameObject();
        var b = animation.AddComponent<LineDestroyAnimation>();
        b.material = material;
        animation.transform.position = new Vector3(4, y + lineCount, 0);
    }
}
