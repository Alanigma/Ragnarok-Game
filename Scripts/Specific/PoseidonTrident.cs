using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonTrident : MonoBehaviour
{
    public GameObject owner;
    public List<GameObject> tridentes;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float arremessadoTempo;

    [SerializeField] private bool arremessado;
    private int actualTridente;
    private Collider2D col;
    private Status status;

    private void Start() {
        col = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(col, owner.GetComponent<Collider2D>());
        status = owner.GetComponent<Status>();
    }

    private void FixedUpdate() {
        if(arremessado){
            transform.Translate(new Vector3(0, speed, 0));
            arremessadoTempo -= Time.deltaTime;
            if(arremessadoTempo <= 0){
                Remove();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 8){
            other.gameObject.GetComponent<Character>().TakeDamage(damage);
        }
        if(arremessado){
            Remove();
        }
    }

    public void Remove(){
        GetComponent<Teleportable>().teleporteOn = false;
        arremessadoTempo = 0.2f;
        arremessado = false;
        transform.position = new Vector2(14, -6);
    }

    public IEnumerator Atack(){
        status.canControl = false;
        transform.localPosition = new Vector3(0, 0, 0);
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        col.enabled = true;
        while (Vector2.Distance(transform.position, owner.transform.position) < 0.8f)
        {
            transform.Translate(new Vector3(0, speed, 0));
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        col.enabled = false;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 135);
        status.usingAtack = false;
        status.canControl = true;
    }

    public IEnumerator Special(float angulo){
        while (true)
        {
            for (float i = angulo - 20; i <= angulo + 20; i += 10)
            {
                if(actualTridente == tridentes.Count-1) actualTridente = 0;
                else actualTridente++;
                tridentes[actualTridente].transform.position = transform.position;
                tridentes[actualTridente].transform.rotation = Quaternion.Euler(0, 0, i);
                tridentes[actualTridente].GetComponent<Teleportable>().teleporteOn = true;
                tridentes[actualTridente].GetComponent<PoseidonTrident>().arremessado = true;
                // tridentes[actualTridente].GetComponent<SpriteRenderer>().enabled = true;
                // tridentes[actualTridente].GetComponent<Collider2D>().enabled = true;
                // tridentes[actualTridente].GetComponent<Damageble>().damage /= 4;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
