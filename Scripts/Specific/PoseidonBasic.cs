using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonBasic : Character
{
    [SerializeField] private GameObject tridente;
    [SerializeField] private float dashForce;

    override public void Atack () {
        status.AtackDurationCount();

        //Consertar direcao do ataque
        int axisY = GetComponentInParent<Status>().axisY;
        int axisX = GetComponentInParent<Status>().axisX;
        if(axisX == 0 && axisY == 0){
            axisX = GetComponentInParent<Status>().axisXLast;
        }
        if(GetComponentInParent<Unificator>().IsGrounded() && axisY != 1){
            axisX = GetComponentInParent<Status>().axisXLast;
            axisY = GetComponentInParent<Status>().axisYLast;
        }

        //Tridente
        tridente.transform.rotation = Quaternion.Euler(0, 0, angulo[axisX+1, axisY+1]);
        StartCoroutine(tridente.GetComponent<PoseidonTrident>().Atack());

        //Dash
        rb.velocity = new Vector2(dashForce * axisX, dashForce * axisY);
    }

    override public void Special(){
        print("Special Poseidon");
    }
}
