using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonTrident : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [SerializeField] private float speed;
    private bool arremessado;
    [SerializeField] private float arremessadoTempo;
    private Collider2D col;
    private GameObject shot;

    private void Start() {
        col = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(col, owner.GetComponent<Collider2D>());
    }

    private void FixedUpdate() {
        if(arremessado){
            transform.Translate(new Vector3(0, speed, 0));
            arremessadoTempo -= Time.deltaTime;
            if(arremessadoTempo <= 0) Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //
    }

    public IEnumerator Atack(){
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
    }

    public IEnumerator Special(float angulo){
        while (true)
        {
            for (float i = angulo - 20; i <= angulo + 20; i += 10)
            {
                shot = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, i));
                shot.transform.parent = owner.GetComponent<Character>().shotSpace.transform;
                shot.GetComponent<PoseidonTrident>().arremessado = true;
                shot.GetComponent<Collider2D>().enabled = true;
                shot.GetComponent<Damageble>().damage /= 4;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
