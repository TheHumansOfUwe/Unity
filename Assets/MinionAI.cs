using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAI : MonoBehaviour
{
    public float fallspeed = 1;
    float yspeed = 0;
    public MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yspeed -= fallspeed * Time.deltaTime;

        //transform.position = transform.position + new Vector3(0, yspeed, 0);


        Ray ray = new Ray();
        ray.origin = transform.position + new Vector3(0, -1, 0);
        ray.direction = new Vector3(0, -1, 0);

        RaycastHit hitResult;

        meshCollider.Raycast(ray, out hitResult, yspeed * -1);

        if(hitResult.distance <= yspeed)
        {
            transform.position = hitResult.point;
            yspeed = 0;
        }
        
    }
}
