using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;
using System.IO;

public class editablemesh : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh mesh;
    private Vector3[] vertices;

    private NavMeshData navmesh;
    public NavMeshBuildSettings settings;
    private string path = "";

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        
        mesh.MarkDynamic();
        vertices = mesh.vertices;
        path = Application.persistentDataPath+"/map.flox";

        var rotation = GetComponent<Transform>().rotation;
        Vector3 transf = GetComponent<Transform>().position;
        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource> { };
        var buildsrc = new NavMeshBuildSource { };
        buildsrc.sourceObject = mesh;
        sources.Add(buildsrc);
        NavMeshBuilder.BuildNavMeshData(settings, sources, mesh.bounds, transf, rotation);
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }

    public void serialize(){
        string s = "";
        foreach (var item in mesh.vertices)
        {
            s += item.ToString() + "~";
        }
        s = s.Remove(s.Length - 1);
        print(s);

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(s);
        writer.Close();
        print(path);
        }


    public void loadmap(){
        StreamReader reader = new StreamReader(path); 
        string f = "";
        List<Vector3> mapData = new List<Vector3>();
        foreach (var c in reader.ReadToEnd())
        {
            if(c.ToString() == "~"){
                string x = "";
                string y = "";
                string z = "";
                int count = 1;
                foreach (var g in f)
                {
                    if(g.ToString() == "(") {}else{
                    if(g.ToString() == ")"){
                        mapData.Add(new Vector3(float.Parse(x),float.Parse(y),float.Parse(z)));
                        print("X: "+ x + " Y:" + y + " Z: " +z);

                         x = "";
                            y = "";
                         z = "";
                    }else{

                        if(g.ToString()==","){
                            count++;
                        }else{
                        if(count == 1){
                            x += g.ToString();
                        }else if(count == 2){
                            y += g.ToString();
                        }else if(count == 3){
                            z += g.ToString();
                        }
                    }
                    }
                    }
                }
                f = "";
            }else{
                f += c.ToString();
            }
        }
        mapData.Add(new Vector3(0.0f,0.0f,0.0f));
        reader.Close();
        print(mesh.vertices.Length);
        print(mapData.ToArray().Length);
        mesh.SetVertices(mapData.ToArray());
        vertices = mapData.ToArray();


        var rotation = GetComponent<Transform>().rotation;
        Vector3 transf = GetComponent<Transform>().position;
        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource> { };
        var buildsrc = new NavMeshBuildSource { };
        buildsrc.sourceObject = mesh;
        sources.Add(buildsrc);
        NavMeshBuilder.BuildNavMeshData(settings, sources, mesh.bounds, transf, rotation);
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();

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
