using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeGun : MonoBehaviour
{
    public float theDistance;
    public GameObject BubbleGun; 
    public GameObject BubbleGunfps;

    private void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.E) && theDistance <= 10)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            BubbleGunfps.SetActive(true);
            BubbleGun.SetActive(false);
        }
    }

 
}
