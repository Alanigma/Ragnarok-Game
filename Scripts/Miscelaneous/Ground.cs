using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // public float life = 1000;

    private void OnCollisionEnter2D(Collision2D other) {
        // if(other.gameObject.layer == 7){
        //     life -= other.gameObject.GetComponent<Damageble>().damage;
        //     if(life <= 0){
        //         try
        //         {
        //             GetComponentInParent<FixedJoint2D>().breakForce = 0;
        //         }
        //         catch (System.Exception)
        //         {
        //             return;
        //         }
        //         GetComponent<Rigidbody2D>().freezeRotation = false;
        //     }
        // }
        if(other.transform.parent.CompareTag("shot-space")){
            Destroy(other.gameObject);
        }
    }
}
