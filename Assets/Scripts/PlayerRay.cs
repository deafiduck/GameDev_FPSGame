using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    //karakterin hedeflere olan uzaklýðýný hesaplar

    public static float distanceFromTarget; //baþka script'lerden de eriþebilmek için static tanýmladýk
    public float toTarget; //tavana,duvarlara yani her þeye olan uzaklýðý hesaplar


    void Update()
    {
        RaycastHit hit;  

        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) 
        {
            toTarget = hit.distance;  
            distanceFromTarget = toTarget;
        }
    }
}
