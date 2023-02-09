using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        float volume = mesh.bounds.size.x * mesh.bounds.size.y * mesh.bounds.size.z;
        Debug.Log(volume);
        Debug.Log(VolumeOfMesh(mesh));
    }
    [ContextMenu("CalculateVolume")]
    public void CalculateVolume()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        float volume = mesh.bounds.size.x * mesh.bounds.size.y * mesh.bounds.size.z;
        Debug.Log((4/3)*3.14f*.5f*.5f*.5f);
        Debug.Log(VolumeOfMesh(mesh));

    }
    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        float scale  = transform.localScale.y;
        Transform currentTransform = this.transform;
        while(true)
        {
            if(currentTransform.parent != null)
            {
                scale *= currentTransform.parent.localScale.y;
                currentTransform = currentTransform.parent;
            }
            else
                break;
        }
        
        return Mathf.Abs(volume * scale);
    }
    
}
