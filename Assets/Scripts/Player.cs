using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public abstract class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private protected Player player;
    }

    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private Shape[] shapes = null;
    [SerializeField]
    private GameField tetrisField = null;

    private Shape currentShape;
    private Vector3 position;

    public float Speed { get => speed; set => speed = value; }
    public bool Boost { get; set; }

    public delegate void ShapeChanged(Shape shape);
    public delegate void PositionChanged(Vector3 newPosition);
    public delegate void RotationChanged(int axis);
    public delegate void NoArguments();

    public event NoArguments OnGameOver;
    public event ShapeChanged OnShapeChanged;
    public event PositionChanged OnPositionChanged;
    public event RotationChanged OnRotationChanged;

    private System.Random random = new System.Random();

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        var oldy = position.y;
        MoveY(-Time.deltaTime * (Boost? 30.0f : speed));
        if(oldy == position.y)
        {
            OnGameOver.Invoke();
        }
    }

    private void ResetState()
    {
        currentShape = Instantiate(shapes[random.Next(0, shapes.Length)]);     
        position.y = tetrisField.Heigth - 1 + currentShape.pivot.y;
        position.x = tetrisField.Width / 2 - currentShape.pivot.x;
        position.z = (tetrisField.Length / 2) - currentShape.pivot.z;
        OnShapeChanged.Invoke(currentShape);
        OnPositionChanged.Invoke(position);
    }

    public void MoveX(int x)
    {
        position.x += x;
        if (tetrisField.CheckBlocks(new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points))
        {
            position.x -= x;
            return;
        }
        OnPositionChanged.Invoke(position);
    }

    public void MoveY(float y)
    {
        position.y += y;
        if(tetrisField.CheckBlocks(new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points))
        {
            position.y -= y;
            Put();
            ResetState();
            return;
        }
        OnPositionChanged.Invoke(position);
    }

    public void MoveZ(int z)
    {
        position.z += z;
        if (tetrisField.CheckBlocks(new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points))
        {
            position.z -= z;
            return;

        }
        OnPositionChanged.Invoke(position);

    }

    public void RotateZ()
    {
        currentShape.Rotate(0.0f, 0.0f, Mathf.PI / 2);
        if (tetrisField.CheckBlocks(new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points))
        {
            currentShape.Rotate(0.0f, 0.0f, -Mathf.PI / 2);
            return;
        }
        OnRotationChanged.Invoke(2);   
    }

    public void RotateY()
    {
        currentShape.Rotate(0.0f, Mathf.PI / 2.0f, 0.0f);
        if (tetrisField.CheckBlocks(new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points))
        {
            currentShape.Rotate(0.0f, -Mathf.PI / 2.0f, 0.0f);
            return;
        }
        OnRotationChanged.Invoke(1);
    }

    public void RotateX()
    {
        currentShape.Rotate(Mathf.PI / 2.0f, 0.0f, 0.0f);
        if (tetrisField.CheckBlocks(new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points))
        {
            currentShape.Rotate(-Mathf.PI / 2.0f, 0.0f, 0.0f);

            return;
        }
        OnRotationChanged.Invoke(0);   
    }

    private void Put()
    {
        position = Vector3Int.FloorToInt(position);
        OnPositionChanged(position);
        tetrisField.PutBlocks(currentShape.materialId, new Vector3Int((int)position.x, (int)position.y, (int)position.z), currentShape.points);      
    }
}
