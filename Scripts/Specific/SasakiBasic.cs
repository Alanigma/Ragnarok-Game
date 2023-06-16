using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasakiBasic : Character
{
    [SerializeField] private float atackSpeed;
    [SerializeField] private GameObject weapon;

    public override bool Atack()
    {
        bool can = base.Atack();
        if(can){
            StartCoroutine(RasanteDaAndorinha(weapon.transform.localEulerAngles.z - 270));
        }
        //So pq o outro tem que ser bool
        return true;
    }

    private IEnumerator RasanteDaAndorinha(float grauInicial){
        print(weapon.transform.localEulerAngles.z - 270);
        while (weapon.transform.localEulerAngles.z - 270 > 0)
        {
            weapon.transform.RotateAround(weapon.transform.parent.position, new Vector3(0, 0, -1 * transform.localScale.x), atackSpeed);
            yield return new WaitForSeconds(0.1f);
        }
        weapon.transform.localScale = new Vector3(-1, 1, 1);
        while (weapon.transform.localEulerAngles.z - 270 < grauInicial)
        {
            weapon.transform.RotateAround(weapon.transform.parent.position, new Vector3(0, 0, 1 * transform.localScale.x), atackSpeed);
            yield return new WaitForSeconds(0.1f);
        }
        weapon.transform.localScale = new Vector3(1, 1, 1);
    }
}
