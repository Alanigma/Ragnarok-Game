using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChar : MonoBehaviour
{
    private Vector2 beforePosition;
    private Vector2 actualPosition;
    protected Status status;
    private Teleportable tele;

    private void Start() {
        status = GetComponent<Status>();
        tele = GetComponent<Teleportable>();
        beforePosition = transform.position;
        actualPosition = transform.position;
    }

    private void Update() {
        //Flip
        if(status.axisX > 0){
            Flip(1, 1);
        } else if(status.axisX < 0){
            Flip(-1, 1);
        }

        //Animacoes
        if(status.usingAtack || status.usingSpecial){
            SetAnimation("atacking");
        }
        else{
            if(status.isGrounded){
                if(status.axisX != 0){
                    SetAnimation("walking");
                } else{
                    SetAnimation("idle");
                }
            }
            if(!status.isGrounded){
                SetAnimation("falling");
            }
        }
    }

    public void SetAnimation(string animation){
        GetComponent<Animator>().Play(animation);
        foreach (GameObject clone in tele.clones)
        {
            clone.GetComponent<Animator>().Play(animation);
        }
    }

    public void Flip(int x, int y){
        transform.localScale = new Vector2(x, y);
    }
}