using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLFieldDebugger : MonoBehaviour
{
    [SerializeField]
    private GameField gameField = null;

    private Material material;

    private void Start()
    {
        material = new Material(Shader.Find("Sprites/Default"));
    }

    private void OnRenderObject()
    {
        GL.PushMatrix();
        material.SetPass(0);
   

        GL.Begin(GL.LINES);
        GL.Color(Color.black);

        var offset = new Vector3(-0.5f, -0.5f, -0.5f);

        GL.Vertex(new Vector3(0.0f, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(gameField.Width, 0.0f, 0.0f) + offset);

        GL.Vertex(new Vector3(0.0f, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(0.0f, 0.0f, gameField.Length) + offset);

        GL.Vertex(new Vector3(gameField.Width, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(gameField.Width, 0.0f, gameField.Length) + offset);

        GL.Vertex(new Vector3(0.0f, 0.0f, gameField.Length) + offset);
        GL.Vertex(new Vector3(gameField.Width, 0.0f, gameField.Length) + offset);


        GL.Vertex(new Vector3(0.0f, gameField.Heigth, 0.0f) + offset);
        GL.Vertex(new Vector3(gameField.Width, gameField.Heigth, 0.0f) + offset);

        GL.Vertex(new Vector3(0.0f, gameField.Heigth, 0.0f) + offset);
        GL.Vertex(new Vector3(0.0f, gameField.Heigth, gameField.Length) + offset);

        GL.Vertex(new Vector3(gameField.Width, gameField.Heigth, 0.0f) + offset);
        GL.Vertex(new Vector3(gameField.Width, gameField.Heigth, gameField.Length) + offset);

        GL.Vertex(new Vector3(0.0f, gameField.Heigth, gameField.Length) + offset);
        GL.Vertex(new Vector3(gameField.Width, gameField.Heigth, gameField.Length) + offset);

        GL.Vertex(new Vector3(0.0f, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(0.0f, gameField.Heigth, 0.0f) + offset);

        GL.Vertex(new Vector3(gameField.Width, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(gameField.Width, gameField.Heigth, 0.0f) + offset);

        GL.Vertex(new Vector3(0.0f, 0.0f, gameField.Length) + offset);
        GL.Vertex(new Vector3(0.0f, gameField.Heigth, gameField.Length) + offset);

        GL.Vertex(new Vector3(gameField.Width, 0.0f, gameField.Length) + offset);
        GL.Vertex(new Vector3(gameField.Width, gameField.Heigth, gameField.Length) + offset);

        GL.Color(Color.blue);
        GL.Vertex(new Vector3(0.0f, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(0.0f, 0.0f, 1000.0f) + offset);

        GL.Color(Color.red);
        GL.Vertex(new Vector3(0.0f, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(1000.0f, 0.0f, 0.0f) + offset);

        GL.Color(Color.green);
        GL.Vertex(new Vector3(0.0f, 0.0f, 0.0f) + offset);
        GL.Vertex(new Vector3(0.0f, 1000.0f, 0.0f) + offset);



        GL.End();

        GL.PopMatrix();
    }

}
