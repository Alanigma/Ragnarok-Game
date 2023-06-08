using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonBasic : Character
{
    [SerializeField] private GameObject tridente;
    [SerializeField] private float dashForce;
    private Coroutine atacando;

    override public void Atack () {
        status.AtackDurationCount();

        //Consertar direcao do ataque
        int axisY = GetComponentInParent<Status>().axisY;
        int axisX = GetComponentInParent<Status>().axisX;
        if(axisX == 0 && axisY == 0){
            axisX = GetComponentInParent<Status>().axisXLast;
        }
        if(GetComponentInParent<Unificator>().isGrounded && axisY != 1){
            axisX = GetComponentInParent<Status>().axisXLast;
            axisY = GetComponentInParent<Status>().axisYLast;
        }

        //Tridente
        tridente.transform.rotation = Quaternion.Euler(0, 0, angulo[axisX+1, axisY+1]);
        StartCoroutine(tridente.GetComponent<PoseidonTrident>().Atack());

        //Dash
        rb.velocity = new Vector2(dashForce * axisX, dashForce * axisY);
    }

    public override void SuperAtack()
    {
        Atack();
    }

    override public IEnumerator Special(){
        //Consertar direcao do ataque
        int axisY = GetComponentInParent<Status>().axisY;
        int axisX = GetComponentInParent<Status>().axisX;
        if(axisX == 0 && axisY == 0){
            axisX = GetComponentInParent<Status>().axisXLast;
        }
        if(GetComponentInParent<Unificator>().isGrounded && axisY != 1){
            axisX = GetComponentInParent<Status>().axisXLast;
            axisY = GetComponentInParent<Status>().axisYLast;
        }

        if(status.stamina > status.specialCost){
            status.canMove = false;
            tridente.GetComponent<SpriteRenderer>().enabled = false;
            if(atacando != null) StopCoroutine(atacando);
            atacando = StartCoroutine(tridente.GetComponent<PoseidonTrident>().Special(angulo[axisX+1, axisY+1]));
            while (true)
            {
                status.GastarStamina(status.specialCost);
                if(status.stamina <= 0 || !unificator.specialButtonPress){
                    StopCoroutine(atacando);
                    tridente.transform.localPosition = new Vector3(0, 0, 0);
                    tridente.transform.rotation = Quaternion.Euler(0, 0, 135);
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            tridente.GetComponent<SpriteRenderer>().enabled = true;
            status.canMove = true;
        }
    }
}
