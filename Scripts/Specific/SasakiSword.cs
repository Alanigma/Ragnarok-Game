using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasakiSword : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 8){
            other.gameObject.GetComponent<Character>().TakeDamage(damage);
        }
    }
}
