using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonTrident : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [SerializeField] private float speed;
    private Collider2D col;

    private void Start() {
        col = GetComponent<Collider2D>();
    }

    public IEnumerator Atack(){
        transform.localPosition = new Vector3(0, -0.09f, 0);
        GetComponent<ParticleSystem>().Play();
        col.enabled = true;
        while (Vector2.Distance(transform.position, owner.transform.position) < 0.8f)
        {
            transform.Translate(new Vector3(0, speed, 0));
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        col.enabled = false;
        transform.localPosition = new Vector3(0, -0.09f, 0);
        transform.rotation = Quaternion.Euler(0, 0, 135);
    }
}
