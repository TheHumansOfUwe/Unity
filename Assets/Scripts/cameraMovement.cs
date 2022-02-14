using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 moveVector = (transform.forward * v) + (transform.right * h);
         moveVector *= 50 * Time.deltaTime;

        transform.localPosition += moveVector;
    }
}
