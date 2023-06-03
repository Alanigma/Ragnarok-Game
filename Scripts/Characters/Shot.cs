// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class Shot : MonoBehaviour
// {
//     private enum gun {
//         none,
//         revolver,
//         bow,
//         sword
//     };
//     [SerializeField] private gun myGun;
//     [SerializeField] private GameObject revolverAmmo;
//     [SerializeField] private GameObject hookAmmo;

//     public GameObject hook;
//     private Rigidbody2D rb;
//     private LineRenderer line;
//     private float ammoSpeed = 0;
//     private float delay = 0;
//     public bool isHooking = false;

//     private void Start() {
//         rb = GetComponent<Rigidbody2D>();
//         line = GetComponent<LineRenderer>();
//     }

//     private void Update() {
//         if(hook != null){
//             line.startWidth = 0.1f;
//             line.endWidth = 0.1f;
//             line.SetPosition(0, transform.position);
//             line.SetPosition(1, hook.transform.position);
//         } else{
//             line.startWidth = 0;
//             line.endWidth = 0;
//         }
//     }

//     public IEnumerator Atack()
//     {
//         if (delay == 0){
//             //Consertar direcao do tiro
//             int axisY = GetComponentInParent<Controls>().AxisY;
//             int axisX = GetComponentInParent<Controls>().AxisX;
//             if(axisX == 0 && axisY == 0){
//                 axisX = GetComponentInParent<Controls>().AxisXLast;
//             }
//             if(GetComponentInParent<Character>().IsGrounded() && axisY != 1){
//                 axisX = GetComponentInParent<Controls>().AxisXLast;
//                 axisY = GetComponentInParent<Controls>().AxisYLast;
//             }

//             switch (myGun)
//             {
//                 case gun.revolver:
//                     GameObject ammo = Instantiate(revolverAmmo, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
//                     ammo.GetComponent<Ammo>().owner = gameObject;
//                     ammo.transform.parent = transform.parent.GetComponent<Character>().Shots.transform;
//                     ammoSpeed = 11;
//                     if(axisX != 0 && axisY != 0) ammoSpeed /= 2;
//                     delay = 0.5f;
//                     ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(axisX, axisY) * ammoSpeed;
//                     break;
//                 default:
//                     print("pow!");
//                     break;
//             }
//             yield return new WaitForSeconds(delay);
//             delay = 0;
//         }
//     }

//     public void Hook(){
//         isHooking = !isHooking;

//         if(isHooking){
//             //Consertar direcao do tiro
//             int axisY = GetComponentInParent<Controls>().AxisY;
//             int axisX = GetComponentInParent<Controls>().AxisX;
//             if(axisX == 0 && axisY == 0){
//                 axisX = GetComponentInParent<Controls>().AxisXLast;
//             }
//             if(GetComponentInParent<Character>().IsGrounded() && axisY != 1){
//                 axisX = GetComponentInParent<Controls>().AxisXLast;
//                 axisY = GetComponentInParent<Controls>().AxisYLast;
//             }

//             hook = Instantiate(hookAmmo, transform.position, Quaternion.identity);
//             ammoSpeed = 20;
//             if(axisX != 0 && axisY != 0) ammoSpeed /= 2;
//             hook.GetComponent<Rigidbody2D>().velocity = new Vector2(axisX, axisY) * ammoSpeed;
//             hook.GetComponent<Hook>().body = rb;
//             GetComponent<Teleportable>().dependent = hook;
//         } else{
//             hook.GetComponent<Hook>().Pull();
//         }
//     }
// }
