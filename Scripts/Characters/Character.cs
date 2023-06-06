using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject shotSpace;
    protected Rigidbody2D rb;
    protected Status status;
    protected Unificator unificator;
    protected float[,] angulo = {
        {135, 90, 45}, {180, 0, 0}, {-135, -90, -45}
    };

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponentInParent<Status>();
        unificator = GetComponentInParent<Unificator>();
        shotSpace = GameObject.FindGameObjectWithTag("shot-space");
    }

    virtual protected void Update() {
        //Andar para os lados
        if(status.actualAtackDuration <= 0 && status.canMove) rb.velocity = new Vector2(status.speed * status.axisX, rb.velocity.y < status.maxSpeedY ? status.maxSpeedY : rb.velocity.y);
        if(!status.canMove){
            rb.simulated = false;
            rb.velocity = new Vector2(0, 0);
        } else{
            rb.simulated = true;
        }
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

    virtual public IEnumerator Special(){
        print("Special");
        yield return new WaitForSeconds(1);
    }
    
    virtual public IEnumerator SuperSpecial(){
        print("Super Special");
        yield return new WaitForSeconds(1);
    }
}
