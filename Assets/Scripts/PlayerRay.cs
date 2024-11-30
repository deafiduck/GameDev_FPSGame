using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    //karakterin hedeflere olan uzakl���n� hesaplar

    public static float distanceFromTarget; //ba�ka script'lerden de eri�ebilmek i�in static tan�mlad�k
    public float toTarget; //tavana,duvarlara yani her �eye olan uzakl��� hesaplar


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
