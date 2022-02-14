using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor.UI;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class editablemesh : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh mesh;
    private Vector3[] vertices;
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.MarkDynamic();
        vertices = mesh.vertices;
        

       
    }

    // Update is called once per frame
    void Update()
    {
        
       


    }

    public void RaiseVertice(Vector3 transform)
    {
        int nearest = -1;
        float nearestSqDist = 10000000000.0f;
        for (int i = 0; i < vertices.Length; i++) {
            float sqDist = (vertices[i] - transform).sqrMagnitude;
			
            if (sqDist < nearestSqDist) {
                nearest = i;
                nearestSqDist = sqDist;
            }
        }

        if (nearest > -1)
        {
            vertices[nearest] += Vector3.up;
            mesh.vertices = vertices;
            mesh.RecalculateBounds(); 
            GetComponent<MeshCollider>().sharedMesh = null;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
        /*
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += Vector3.up;
            
        }*/

    }

    public void LowerVertice(Vector3 transform)
    {
        int nearest = -1;
        float nearestSqDist = 10000000000.0f;
        for (int i = 0; i < vertices.Length; i++) {
            float sqDist = (vertices[i] - transform).sqrMagnitude;
			
            if (sqDist < nearestSqDist) {
                nearest = i;
                nearestSqDist = sqDist;
            }
        }

        if (nearest > -1)
        {
            vertices[nearest] -= Vector3.up;
            mesh.vertices = vertices;
            mesh.RecalculateBounds(); 
            GetComponent<MeshCollider>().sharedMesh = null;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}
