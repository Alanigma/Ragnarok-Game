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
    public float atackCost;
    public float specialCooldown;
    public float specialDuration;
    public float specialCost;
    public float maxLife;
    public float maxStamina;
    public float life;
    public float stamina;
    public float staminaRegen;

    [Header("Debug")]
    private float staminaRegenCooldown;
    public bool canMove = true;
    public bool isMoving;
    public float actualAtackCooldown;
    public float actualAtackDuration;
    public float actualSpecialCooldown;
    public float actualSpecialDuration;
    public int axisX;
    public int axisY;
    public int axisXLast = 1;
    public int axisYLast = 0;

    private void Start() {
        life = maxLife;
        stamina = maxStamina;
    }

    private void Update() {
        if(actualAtackCooldown > 0){
            actualAtackCooldown -= Time.deltaTime;
        }
        if(actualAtackDuration > 0){
            actualAtackDuration -= Time.deltaTime;
        }
        if(actualSpecialCooldown > 0){
            actualSpecialCooldown -= Time.deltaTime;
        }
        if(actualSpecialDuration > 0){
            actualSpecialDuration -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        if(stamina < maxStamina){
            if(staminaRegenCooldown > 0) staminaRegenCooldown -= Time.deltaTime;
            else stamina += staminaRegen;
        }
    }

    public void GastarStamina(float quant){
        stamina -= quant;
        staminaRegenCooldown = 1;
    }

    public void AtackCooldownCount(){
        actualAtackCooldown = atackCooldown;
    }
    public void AtackDurationCount(){
        actualAtackDuration = atackDuration;
    }
    public void SpecialCooldownCount(){
        actualSpecialCooldown = specialCooldown;
    }
    public void SpecialDurationCount(){
        actualSpecialDuration = specialDuration;
    }
}
