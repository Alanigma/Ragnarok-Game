using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Controls : MonoBehaviour
{
    private Unificator controller;
    private Status status;

    private void Start() {
        controller = GetComponent<Unificator>();
        status = GetComponent<Status>();
    }

    public void Moviment(InputAction.CallbackContext value){
        status.axisX = (int)Math.Round(value.ReadValue<Vector2>().x);
        status.axisY = (int)Math.Round(value.ReadValue<Vector2>().y);
        if((int)Math.Round(value.ReadValue<Vector2>().x) != 0) status.axisXLast = (int)Math.Round(value.ReadValue<Vector2>().x);
        if((int)Math.Round(value.ReadValue<Vector2>().y) != -1) status.axisYLast = (int)Math.Round(value.ReadValue<Vector2>().y);
    }

    public void AtackButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started") controller.atackButtonPress = true;
        if(value.phase.ToString() == "Canceled") controller.Atack();
    }

    public void JumpButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started") controller.Jump();
    }

    public void SpecialButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started"){
            controller.specialButtonPress = true;
            controller.Special();
        }
        if(value.phase.ToString() == "Canceled") controller.specialButtonPress = false;
    }
}
