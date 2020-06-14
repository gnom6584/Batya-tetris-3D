using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineDestroyAnimation : MonoBehaviour
{
    [SerializeField]
    private int width = 10;

    public Material material;

    private GameObject[] cubesPool = null;

    private GameObject CreateCube(GameObject cube, int i)
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = transform;
        cube.transform.localPosition = new Vector3(i - width / 2.0f + 1, 0);
        var rigidbody = cube.AddComponent<Rigidbody>();
        //   rigidbody.AddForce((-transform.forward + Random.insideUnitSphere) * 200);
        var mat = new Material(material);
        cube.GetComponent<Renderer>().material = mat;
        StartCoroutine(AlphaToZero(mat));
        cube.layer = 8;
        return cube;
    }

    IEnumerator AlphaToZero(Material material)
    {
        Color color = material.color;
        while(color.a > 0)
        {
            color.a -= Time.deltaTime / 2.0f;
            material.color = color;
            yield return null;
        };
        foreach(var cube in cubesPool)
        {
            Destroy(cube);
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        cubesPool = new GameObject[width];
        cubesPool = cubesPool.
            Select((cube, i) => CreateCube(cube, i))        
            .ToArray();
        var explode = new GameObject().AddComponent<CapsuleCollider>();
        explode.transform.position = transform.position;
        explode.gameObject.AddComponent<Rigidbody>();
        explode.direction = 0;
        explode.height = width;
        explode.radius = 0.5f;
    }

    private void Update()
    {
        
    }
}
