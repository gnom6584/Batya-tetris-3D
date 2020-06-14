using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPallete : MonoBehaviour
{
    [SerializeField]
    private Material[] materials = null;
    public int Length { get => materials.Length; }

    public Material this[int i]
    {
        get
        {
            return materials[i];
        }
    }

}
