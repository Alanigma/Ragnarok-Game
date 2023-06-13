using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject shotSpace;
    protected GameObject instanceSpace;
    protected Rigidbody2D rb;
    protected Status status;
    protected float[,] angulo = {
        {135, 90, 45}, {180, 0, 0}, {-135, -90, -45}
    };

    virtual protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<Status>();
        shotSpace = GameObject.FindGameObjectWithTag("shot-space");
        instanceSpace = GameObject.FindGameObjectWithTag("instance-space");
    }

    virtual protected void Update() {
        //Andar para os lados
        if(status.actualAtackDuration <= 0 && status.canMove){
            rb.velocity = new Vector2(status.speed * status.axisX, rb.velocity.y < status.maxSpeedY ? status.maxSpeedY : rb.velocity.y);
        }

        //Travar o personagem
        if(!status.canMove){
            rb.simulated = false;
            rb.velocity = new Vector2(0, 0);
        } else{
            rb.simulated = true;
        }
    }

    virtual public void TakeDamage(float damage){
        status.life -= damage;
        if(status.life <= 0){
            Dead();
        }
    }

    virtual protected void Dead(){
        print(gameObject.name + " morreu");
    }

    public void Jump()
    {
        if(status.isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * status.jumpForce);
        }
    }

    virtual public bool Atack () {
        if(status.stamina > status.atackCost && status.actualAtackCooldown <= 0 && status.actualAtackDuration <= 0){
            status.GastarStamina(status.atackCost);
            if(status.actualAtackCooldown <= 0){
                status.AtackCooldownCount();
                status.AtackDurationCount();
                return true;
            }
        }
        return false;
    }

    virtual public void Special(){
        if(status.stamina > status.specialCost && status.actualSpecialCooldown <= 0 && status.actualSpecialDuration <= 0){
            status.GastarStamina(status.specialCost);
            if(status.actualSpecialCooldown <= 0){
                status.SpecialCooldownCount();
            }
        }
    }
}
