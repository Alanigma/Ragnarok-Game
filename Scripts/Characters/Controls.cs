using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Controls : MonoBehaviour
{
    private Status status;
    [SerializeField] private Character body;

    private void Start() {
        status = transform.GetChild(0).GetComponent<Status>();
        body = transform.GetChild(0).GetComponent<Character>();
    }

    public void Moviment(InputAction.CallbackContext value){
        status.axisX = (int)Math.Round(value.ReadValue<Vector2>().x);
        status.axisY = (int)Math.Round(value.ReadValue<Vector2>().y);
        if((int)Math.Round(value.ReadValue<Vector2>().x) != 0) status.axisXLast = (int)Math.Round(value.ReadValue<Vector2>().x);
        if((int)Math.Round(value.ReadValue<Vector2>().y) != -1) status.axisYLast = (int)Math.Round(value.ReadValue<Vector2>().y);
    }

    public void AtackButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Canceled") body.Atack();
    }

    public void JumpButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started"){
            body.Jump();
        }
    }

    public void SpecialButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started"){
            status.usingSpecial = true;
            body.Special();
        }
        if(value.phase.ToString() == "Canceled") status.usingSpecial = false;
    }
}
