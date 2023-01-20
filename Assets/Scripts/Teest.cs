using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teest : MonoBehaviour
{
    [SerializeField] private Material shader;
    private void Start()
    {
        shader.SetFloat("_Fill", .5f);
    }

}
