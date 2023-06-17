using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected LayerMask layerGround;
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
    public float actualAtackCooldown;
    public float actualSpecialCooldown;
    public int axisX;
    public int axisY;
    public int axisXLast = 1;
    public int axisYLast = 0;
    public bool canMove = true;
    public bool canControl = true;
    public bool isMoving;
    public bool usingAtack;
    public bool usingSpecial;
    public bool isGrounded;

    private RaycastHit2D ground;
    private Collider2D col;

    private void Start() {
        col = GetComponent<Collider2D>();
        life = maxLife;
        stamina = maxStamina;
    }

    private void Update() {
        //GroundCheck
        ground = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, 0.1f, layerGround);
        isGrounded = ground.collider != null;

        //Atacando
        if(actualAtackCooldown > 0){
            actualAtackCooldown -= Time.deltaTime;
        }
        if(actualSpecialCooldown > 0){
            actualSpecialCooldown -= Time.deltaTime;
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
    public void SpecialCooldownCount(){
        actualSpecialCooldown = specialCooldown;
    }
}
