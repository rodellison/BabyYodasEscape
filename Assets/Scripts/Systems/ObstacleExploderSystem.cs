using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExploderSystem : MonoBehaviour
{
    public GameObject explosionChild;
 
    private void OnTriggerEnter(Collider other)
    {
   //     Debug.Log("Something Trigger hit Destructable Object");
        if (other.tag.Equals("LaserShot"))
        {
           explosionChild.SetActive(true);
            //We can set off the explosion, and then hide this object (since it's pooled..)
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Invoke("ResetAfterExplosion", 3f);
        }
    }

    void ResetAfterExplosion()
    {
        explosionChild.SetActive(false);
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
    }
}
