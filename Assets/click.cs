using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    // Start is called before the first frame update
    private Ray ray;
    private RaycastHit hit;
    void Start()
    {
        print("Functioning");
     
    }

    // Update is called once per frame
    void Update()
    {
           ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.CompareTag("Map"))
                        {
                            hit.collider.GetComponent<editablemesh>().RaiseVertice(hit.point);

                        }
                    }
                    if(Input.GetMouseButtonDown(1))
                    {
                        if (hit.collider.CompareTag("Map"))
                        {
                            hit.collider.GetComponent<editablemesh>().LowerVertice(hit.point);
                        }
                    }
                }
    }
}
