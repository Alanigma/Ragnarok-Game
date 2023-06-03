using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Status status;
    protected float[,] angulo = {
        {135, 90, 45}, {180, 0, 0}, {-135, -90, -45}
    };

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponentInParent<Status>();
    }

    virtual protected void Update() {
        //Andar para os lados
        if(status.actualAtackDuration <= 0) rb.velocity = new Vector2(status.speed * status.axisX, rb.velocity.y < status.maxSpeedY ? status.maxSpeedY : rb.velocity.y);
    }

    virtual public void TakeDamage(){
        print(gameObject.name+" foi atingido");
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * status.jumpForce);
    }

    virtual public void Atack () {
        print("Atack");
    }
    
    virtual public void SuperAtack () {
        print("Super Atack");
    }

    virtual public void Special(){
        print("Special");
    }
    
    virtual public void SuperSpecial(){
        print("Super Special");
    }
}
