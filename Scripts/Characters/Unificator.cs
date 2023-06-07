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

    public bool jumpButtonPress;
    public bool atackButtonPress;
    public bool specialButtonPress;
    public float atackTimePress;
    public float specialTimePress;
    public bool isGrounded;
    private RaycastHit2D ground;
    private Collider2D col;
    private Rigidbody2D rb;
    private Status status;
    private Vector2 beforePosition;
    private Vector2 actualPosition;

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
        rb = spawneds[0].GetComponent<Rigidbody2D>();

        foreach (GameObject s in spawneds)
        {
            characterControllers.Add(s.GetComponent<Character>());
        }

        beforePosition = spawneds[0].transform.position;
        actualPosition = spawneds[0].transform.position;
        StartCoroutine(checkMoviment());
    }

    void Update()
    {
        //GroundCheck
        ground = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, 0.1f, layerGround);
        isGrounded = ground.collider != null;

        if(atackButtonPress){
            atackTimePress += Time.deltaTime;
        }

        //Flip
        if(status.isMoving){
            if(rb.velocity.x > 0){
                Flip(1, 1);
            } else if(rb.velocity.x < 0){
                Flip(-1, 1);
            }
        }
        //Animacoes
        if(isGrounded){
            if(status.isMoving){
                SetAnimation("walking");
            } else{
                SetAnimation("idle");
            }
        }
        if(!isGrounded){
            SetAnimation("falling");
        }
    }

    private IEnumerator checkMoviment(){
        while (true)
        {
            actualPosition = spawneds[0].transform.position;
            if(Vector2.Distance(actualPosition, beforePosition) < 0.1f){
                status.isMoving = false;
            } else{
                status.isMoving = true;
                beforePosition = actualPosition;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void SetAnimation(string animation){
        foreach (GameObject s in spawneds)
        {
            s.GetComponent<Animator>().Play(animation);
        }
    }

    public void Flip(int x, int y){
        foreach (GameObject s in spawneds)
        {
            s.transform.localScale = new Vector2(x, y);
        }
    }

    public void Jump(){
        if(isGrounded){
            foreach (Character cc in characterControllers)
            {
                cc.Jump();
            }
        }
    }

    public void Atack(){
        if(status.stamina > status.atackCost && status.actualAtackCooldown <= 0 && status.actualAtackDuration <= 0){
            status.GastarStamina(status.atackCost);
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

    public void Special(){
        foreach (Character cc in characterControllers)
        {
            StartCoroutine(cc.Special());
        }
    }
}
