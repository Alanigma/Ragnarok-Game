using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Settings")]
    public float speed;
    public float jumpForce;
    public float maxSpeedY = -10;
    public float atackCooldown;
    public float atackDuration;
    public float stamina;

    [Header("Debug")]
    public bool canMove = true;
    public float actualAtackCooldown;
    public float actualAtackDuration;
    public float actualStamina;
    public int axisX;
    public int axisY;
    public int axisXLast = 1;
    public int axisYLast = 0;

    private void Update() {
        if(actualAtackCooldown > 0){
            actualAtackCooldown -= Time.deltaTime;
        }
        if(actualAtackDuration > 0){
            actualAtackDuration -= Time.deltaTime;
        }
    }

    public void AtackCooldownCount(){
        actualAtackCooldown = atackCooldown;
    }
    public void AtackDurationCount(){
        actualAtackDuration = atackDuration;
    }
}
