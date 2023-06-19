using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonBasic : Character
{
    [SerializeField] private GameObject tridente;
    [SerializeField] private GameObject specialTrident;

    [SerializeField] private float dashForce;
    private Coroutine atacando;

    override protected void Start() {
        base.Start();
        for (int i = 0; i < 15; i++)
        {
            tridente.GetComponent<PoseidonTrident>().tridentes.Add(Instantiate(specialTrident, new Vector2(14, -6), Quaternion.identity));
            tridente.GetComponent<PoseidonTrident>().tridentes[i].transform.parent = instanceSpace.transform;
            tridente.GetComponent<PoseidonTrident>().tridentes[i].GetComponent<PoseidonTrident>().owner = gameObject;
        }
    }

    override public bool Atack () {
        bool can = base.Atack();
        if(can){
            status.usingAtack = true;

            //Consertar direcao do ataque
            int axisY = GetComponentInParent<Status>().axisY;
            int axisX = GetComponentInParent<Status>().axisX;
            if(axisX == 0 && axisY == 0){
                axisX = GetComponentInParent<Status>().axisXLast;
            }
            if(status.isGrounded && axisY != 1){
                axisX = GetComponentInParent<Status>().axisXLast;
                axisY = GetComponentInParent<Status>().axisYLast;
            }

            //Tridente
            tridente.transform.rotation = Quaternion.Euler(0, 0, angulo[axisX+1, axisY+1]);
            StartCoroutine(tridente.GetComponent<PoseidonTrident>().Atack());

            //Dash
            rb.velocity = new Vector2(dashForce * axisX, dashForce * axisY);
        }
        //So pq o outro tem que ser bool
        return true;
    }

    public override bool Special()
    {
        bool can = base.Special();
        if(can){
            base.Special();
            StartCoroutine(AtackRain());
        }
        //So pq o outro tem que ser bool
        return true;
    }

    private IEnumerator AtackRain(){
        //Consertar direcao do ataque
        int axisY = GetComponentInParent<Status>().axisY;
        int axisX = GetComponentInParent<Status>().axisX;
        if(axisX == 0 && axisY == 0){
            axisX = GetComponentInParent<Status>().axisXLast;
        }
        if(status.isGrounded && axisY != 1){
            axisX = GetComponentInParent<Status>().axisXLast;
            axisY = GetComponentInParent<Status>().axisYLast;
        }
        while(status.usingAtack){
            yield return new WaitForSeconds(0.1f);
        }

        if(status.stamina > status.specialCost){
            status.canMove = false;
            tridente.GetComponent<SpriteRenderer>().enabled = false;
            if(atacando != null) StopCoroutine(atacando);
            atacando = StartCoroutine(tridente.GetComponent<PoseidonTrident>().Special(angulo[axisX+1, axisY+1]));
            while (true)
            {
                status.GastarStamina(status.specialCost);
                if(status.stamina <= 0 || !status.usingSpecial){
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
