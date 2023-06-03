using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unificator : MonoBehaviour
{
    [SerializeField] public List<GameObject> spawneds;
    [SerializeField] public List<Character> characterControllers;
    [SerializeField] public GameObject prefab;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private LayerMask layerDamage;

    public bool atackButtonPress;
    public bool specialButtonPress;
    public float atackTimePress;
    public float specialTimePress;
    private Collider2D col;
    private Status status;

    void Start()
    {
        status = GetComponent<Status>();

        //Quadruplicar quando Spawnar
        spawneds.Add(Instantiate(prefab, transform.position, Quaternion.identity));
        spawneds[0].transform.parent = gameObject.transform;
        spawneds.Add(Instantiate(prefab, new Vector2(spawneds[0].transform.position.x+17.8f, spawneds[0].transform.position.y), Quaternion.identity));
        spawneds[1].transform.parent = gameObject.transform;
        spawneds.Add(Instantiate(prefab, new Vector2(spawneds[0].transform.position.x, spawneds[0].transform.position.y+10), Quaternion.identity));
        spawneds[2].transform.parent = gameObject.transform;
        spawneds.Add(Instantiate(prefab, new Vector2(spawneds[0].transform.position.x+17.8f, spawneds[0].transform.position.y+10), Quaternion.identity));
        spawneds[3].transform.parent = gameObject.transform;

        col = spawneds[0].GetComponent<Collider2D>();

        foreach (GameObject s in spawneds)
        {
            characterControllers.Add(s.GetComponent<Character>());
        }
    }

    void Update()
    {
        if(atackButtonPress){
            atackTimePress += Time.deltaTime;
        }
    }

    public bool IsGrounded()
    {
        bool isGrounded = false;
        RaycastHit2D ground = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, 0.1f, layerGround);
        if(ground.collider != null) isGrounded = true;
        return isGrounded;
    }

    public void Jump(){
        if(IsGrounded()){
            foreach (Character cc in characterControllers)
            {
                cc.Jump();
            }
        }
    }

    public void Atack(){
        atackButtonPress = false;
        if(atackTimePress < 2){
            if(status.actualAtackCooldown <= 0){
                foreach (Character cc in characterControllers)
                {
                    cc.Atack();
                }
                status.AtackCooldownCount();
            }
        } else{
            foreach (Character cc in characterControllers)
            {
                cc.SuperAtack();
            }
        }
        atackTimePress = 0;
    }
}
