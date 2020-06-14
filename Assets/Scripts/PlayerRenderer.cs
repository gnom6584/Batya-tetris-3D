using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private MaterialPallete pallete = null;
    [SerializeField]
    private Material background = null;
    [SerializeField]
    private float rotationSpeed = 30.0f;
    [SerializeField]
    private float moveSpeed = 30.0f;

    private Vector3 pivot;
    private SlowVoxelMesh mesh;
    private Transform tempTransform;
    private Vector3 lerpPosition;
    private Shape shape;

    private void Awake()
    {
        player.OnPositionChanged += SetPosition;
        player.OnShapeChanged += SetShape;
        player.OnRotationChanged += RotateZ;
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        player.OnGameOver += () => 
        {
            Destroy(player.gameObject);
            Destroy(gameObject);
        };
        tempTransform = new GameObject("tempfr").transform; //wtf
    }

    private void SetPosition(Vector3 newPosition)
    {      
        lerpPosition = newPosition + new Vector3(pivot.x, -pivot.y);
    }

    private void RotateZ(int axis)
    {
        switch (axis)
        {
            case 2:
                tempTransform.RotateAround(transform.position, Vector3.forward, -90);
                break;
            case 0:
                tempTransform.RotateAround(transform.position, Vector3.up, 90);
                break;
            case 1:
                tempTransform.RotateAround(transform.position, Vector3.right, -90);
                break;
        }
    }

    private void SetShape(Shape newShape)
    {
        mesh.Set(new HashSet<Vector3Int>(newShape.points));
        mesh.mesh.RecalculateTangents();
        pivot = newShape.pivot;
        tempTransform.rotation = Quaternion.identity;
        transform.GetChild(0).localPosition = -pivot;
        GetComponentInChildren<MeshRenderer>().material = pallete[newShape.materialId - 1];
        shape = newShape;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, tempTransform.rotation, Time.deltaTime * rotationSpeed);
        if (Vector3.Distance(transform.localPosition, lerpPosition) < 3)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, lerpPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.localPosition = lerpPosition;
        }
        var vector = transform.localPosition + new Vector3(shape.points[0].x, -shape.points[0].y) - new Vector3(pivot.x, -pivot.y);
        background.SetVector("_Position0", new Vector4((vector.x + 0.5f) / 10.0f, (vector.y+0.5f) / 10.0f, 0.0f, 0.0f));
        vector = transform.localPosition + new Vector3(shape.points[1].x, -shape.points[1].y) - new Vector3(pivot.x, -pivot.y);
        background.SetVector("_Position1", new Vector4((vector.x + 0.5f) / 10.0f, (vector.y + 0.5f) / 10.0f, 0.0f, 0.0f));
        vector = transform.localPosition + new Vector3(shape.points[2].x, -shape.points[2].y) - new Vector3(pivot.x, -pivot.y);
        background.SetVector("_Position2", new Vector4((vector.x + 0.5f) / 10.0f, (vector.y+0.5f) / 10.0f, 0.0f, 0.0f));
        vector = transform.localPosition + new Vector3(shape.points[3].x, -shape.points[3].y) - new Vector3(pivot.x, -pivot.y);
        background.SetVector("_Position3", new Vector4((vector.x + 0.5f) / 10.0f, (vector.y+0.5f) / 10.0f, 0.0f, 0.0f));
    }
}
