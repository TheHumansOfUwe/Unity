using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionAI : MonoBehaviour
{
    
    public MeshCollider meshCollider;
    public GameObject house;
    private NavMeshAgent agent;
    private bool hasWondered = false;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //move_towards(5,0,5);
    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        wander();
        
    }

    void move_towards(float x, float y, float z)
    {
        
        agent.destination = new Vector3(x,y,z);
    }

    void settle()
    {
        

        bool canSettle = true;

        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(6, 0.25f, 6));

        foreach(Collider collider in colliders)
        {
            if(collider == meshCollider)
            {
                canSettle = false;
            }
                
            GameObject collidedObject = meshCollider.gameObject;

            if(collidedObject.name.Contains("House"))
            {
                canSettle = false;
            }
        }

        if(canSettle)
        {
            become_house();
        }
        else
        {
            hasWondered = false;
        }
    }

    Vector3 wander_point(float range)
    {
        return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
    }

    void wander()
    {
        Vector3 goPos;

        if(Vector3.Distance(transform.position,agent.destination) < 2)
        {
            
            
            
            goPos = wander_point(30);
            print("wandering");

            RaycastHit hit;
            Ray ray = new Ray();
            goPos.y = 500;
            ray.origin = goPos;
            ray.direction = new Vector3(0,-1,0);

            meshCollider.Raycast(ray, out hit, 1000);

            while(hit.distance >= 1000)
            {
                goPos = wander_point(30);
                goPos.y = 500;
                ray.origin = goPos;
                meshCollider.Raycast(ray, out hit, 1000);
                print("Recalculating destination");
            }

            move_towards(hit.point.x,hit.point.y,hit.point.z);
            

            if (hasWondered)
            {
                settle();
            }

            hasWondered = true;
        }
        
    }

    void become_house()
    {
        //Turn into haus.
        Instantiate(house, transform.position, Quaternion.identity);
        Destroy(gameObject);
        this.enabled = false;
    }
}
