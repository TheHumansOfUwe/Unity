using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour
{
    public float maxTime;
    public GameObject minion;
    public MeshCollider collisionMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject miniMe = Instantiate(minion, transform);
            miniMe.GetComponent<MinionAI>().meshCollider = collisionMesh;
            miniMe.GetComponent<MinionAI>().house = UnityEditor.PrefabUtility.FindPrefabRoot(gameObject);
            miniMe.transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
