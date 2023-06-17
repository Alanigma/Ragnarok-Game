using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasakiBasic : Character
{
    [SerializeField] private float atackSpeed;
    [SerializeField] private GameObject weapon;

    override protected void Update() {
        base.Update();
        if(!status.usingAtack) weapon.transform.localScale = new Vector3(1, 1, 1);
    }

    public override bool Atack()
    {
        bool can = base.Atack();
        if(can){
            StartCoroutine(RasanteDaAndorinha(weapon.transform.localEulerAngles.z - 270, status.axisXLast));
        }
        //So pq o outro tem que ser bool
        return true;
    }

    private IEnumerator RasanteDaAndorinha(float grauInicial, int axis){
        weapon.GetComponent<ParticleSystem>().Play();
        status.usingAtack = true;
        status.canControl = false;
        if(status.isGrounded) status.canMove = false;
        while (weapon.transform.localEulerAngles.z - 270 > 0 - grauInicial / 2)
        {
            weapon.transform.RotateAround(weapon.transform.parent.position, new Vector3(0, 0, -1 * transform.localScale.x), atackSpeed);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.2f);
        status.canMove = true;
        rb.velocity = new Vector2(status.speed * 4 * axis, rb.velocity.y);
        weapon.transform.localScale = new Vector3(-1, 1, 1);
        while (weapon.transform.localEulerAngles.z - 270 < grauInicial)
        {
            print(weapon.transform.localEulerAngles.z - 270);
            weapon.transform.RotateAround(weapon.transform.parent.position, new Vector3(0, 0, 1 * transform.localScale.x), atackSpeed);
            yield return new WaitForSeconds(0.05f);
        }
        weapon.GetComponent<ParticleSystem>().Stop();
        weapon.transform.localPosition = new Vector2(0.4f, 0.4f);
        status.usingAtack = false;
        status.canControl = true;
    }
}
