using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class editablemesh : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh mesh;
    private Vector3[] vertices;
    private NavMeshData navmesh;
    public NavMeshBuildSettings settings;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        
        mesh.MarkDynamic();
        vertices = mesh.vertices;
        settings.agentSlope = 360;
        settings.agentClimb = 1000;

    }

    // Update is called once per frame
    void Update()
    {
        
       


    }

    public void RaiseVertice(Vector3 transform)
    {
        int nearest = -1;
        float nearestSqDist = 10000000000.0f;
        for (int i = 0; i < vertices.Length; i++)
        {
            float sqDist = (vertices[i] - transform).sqrMagnitude;

            if (sqDist < nearestSqDist)
            {
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
        var rotation = GetComponent<Transform>().rotation;
        Vector3 transf = GetComponent<Transform>().position;
        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource> { };
        var buildsrc = new NavMeshBuildSource { };
        buildsrc.sourceObject = mesh;
        sources.Add(buildsrc);
        NavMeshBuilder.BuildNavMeshData(settings, sources, mesh.bounds, transf, rotation);
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();




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
        var rotation = GetComponent<Transform>().rotation;
        Vector3 transf = GetComponent<Transform>().position;
        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource> { };
        var buildsrc = new NavMeshBuildSource { };
        buildsrc.sourceObject = mesh;
        sources.Add(buildsrc);
        mesh.RecalculateBounds();
        NavMeshBuilder.BuildNavMeshData(settings, sources, mesh.bounds, transf, rotation);
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();

    }
}
