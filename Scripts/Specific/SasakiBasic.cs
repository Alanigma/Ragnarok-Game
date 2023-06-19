using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasakiBasic : Character
{
    [SerializeField] private float atackSpeed;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject characterSpace;
    [SerializeField] private float specialDelay;

    override protected void Start() {
        base.Start();
        characterSpace = GameObject.FindGameObjectWithTag("character-space");
    }

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
        weapon.GetComponent<Collider2D>().enabled = true;
        weapon.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.001f);
        weapon.transform.localScale = new Vector3(1, 1, 1);
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
            weapon.transform.RotateAround(weapon.transform.parent.position, new Vector3(0, 0, 1 * transform.localScale.x), atackSpeed);
            yield return new WaitForSeconds(0.05f);
        }
        weapon.GetComponent<ParticleSystem>().Stop();
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.transform.localPosition = new Vector2(0.231f, 0.605f);
        status.usingAtack = false;
        status.canControl = true;
    }

    public override bool Special()
    {
        bool can = base.Special();
        if(can){
            StartCoroutine(Previsao());
        }
        //So pq o outro tem que ser bool
        return true;
    }

    private IEnumerator Previsao(){
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(7, 9);
        List<Status> targets = new List<Status>();
        for (int i = 0; i < characterSpace.transform.childCount; i++)
        {
            if(characterSpace.transform.GetChild(i) != transform.parent){
                targets.Add(characterSpace.transform.GetChild(i).GetComponentInChildren<Status>());
            }
        }

        //Criar sombras
        foreach (Status target in targets)
        {
            target.shadow.transform.position = target.transform.position;
            target.shadow.transform.rotation = target.transform.rotation;
            target.shadow.transform.localScale = target.transform.localScale;
            target.shadow.GetComponent<Teleportable>().teleporteOn = true;
            target.shadow.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            target.shadowOn = true;
            target.transform.parent.GetComponent<Controls>().delay = specialDelay;
        }

        status.canControl = false;
        yield return new WaitForSeconds(specialDelay);
        status.canControl = true;
        while (status.stamina >= status.specialCost)
        {
            status.GastarStamina(status.specialCost);
            yield return new WaitForSeconds(0.1f);   
        }

        //Apagar sombras
        foreach (Status target in targets)
        {
            target.Remove(target.shadow);
            target.shadowOn = false;
        }
    }
}
