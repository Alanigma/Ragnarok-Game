using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    public List<GameObject> clones;
    [SerializeField] private List<GameObject> remove;
    public bool teleporteOn = true;
    private GameObject cloneSpace;

    private void Start() {
        cloneSpace = GameObject.FindGameObjectWithTag("clone-space");

        //Quadruplicar quando Spawnar
        clones.Add(Instantiate(gameObject, new Vector2(transform.position.x + 17.8f, transform.position.y), Quaternion.identity));
        clones[0].transform.parent = cloneSpace.transform;
        clones.Add(Instantiate(gameObject, new Vector2(transform.position.x, transform.position.y + 10), Quaternion.identity));
        clones[1].transform.parent = cloneSpace.transform;
        clones.Add(Instantiate(gameObject, new Vector2(transform.position.x + 17.8f, transform.position.y + 10), Quaternion.identity));
        clones[2].transform.parent = cloneSpace.transform;

        //Remover lixo
        foreach (GameObject clone in clones)
        {
            for (int i = clone.GetComponent<Teleportable>().remove.Count -1; i >= 0; i--)
            {
                Destroy(clone.GetComponent<Teleportable>().remove[i]);
            }
            if(clone.GetComponent<Collider2D>() != null) Destroy(clone.GetComponent<Collider2D>());
            if(clone.GetComponent<Rigidbody2D>() != null) Destroy(clone.GetComponent<Rigidbody2D>());
            if(clone.GetComponent<AudioSource>() != null) Destroy(clone.GetComponent<AudioSource>());
            if(clone.GetComponents<MonoBehaviour>() != null)
                foreach (MonoBehaviour script in clone.GetComponents<MonoBehaviour>())
                {
                    Destroy(script);
                }
        }
    }

    void Update()
    {
        //Manter os clones nos seus lugares copiando o original
        foreach (GameObject clone in clones)
        {
            clone.transform.rotation = transform.rotation;
            clone.transform.localScale = transform.localScale;
        }

        clones[0].transform.position = new Vector2(transform.position.x + 17.8f, transform.position.y);
        clones[1].transform.position = new Vector2(transform.position.x, transform.position.y + 10);
        clones[2].transform.position = new Vector2(transform.position.x + 17.8f, transform.position.y + 10);

        //Teleporte
        if(teleporteOn){
            if(transform.position.x > 0){
                transform.position = new Vector2(transform.position.x - 17.8f, transform.position.y);
            } else if(transform.position.x < -17.8f){
                transform.position = new Vector2(transform.position.x + 17.8f, transform.position.y);
            }
            if(transform.position.y > 0){
                transform.position = new Vector2(transform.position.x, transform.position.y - 10);
            } else if(transform.position.y < -10){
                transform.position = new Vector2(transform.position.x, transform.position.y + 10);
            }
        }
    }
}
