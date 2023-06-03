using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    public GameObject dependent;

    void Update()
    {
        if(transform.position.x + transform.localScale.x/2 < -17.8f){
            transform.position = new Vector2(transform.position.x + 35.6f, transform.position.y);
            if(dependent != null) dependent.transform.position = new Vector2(dependent.transform.position.x + 35.6f, dependent.transform.position.y);
        } else if(transform.position.x + transform.localScale.x/2 > 17.8f){
            transform.position = new Vector2(transform.position.x - 35.6f, transform.position.y);
            if(dependent != null) dependent.transform.position = new Vector2(dependent.transform.position.x - 35.6f, dependent.transform.position.y);
        }
        if(transform.position.y + transform.localScale.y/2 < -10){
            transform.position = new Vector2(transform.position.x, transform.position.y + 20);
            if(dependent != null) dependent.transform.position = new Vector2(dependent.transform.position.x, dependent.transform.position.y + 20);
        } else if(transform.position.y + transform.localScale.y/2 > 10){
            transform.position = new Vector2(transform.position.x, transform.position.y - 20);
            if(dependent != null) dependent.transform.position = new Vector2(dependent.transform.position.x, dependent.transform.position.y - 20);
        }
    }
}
