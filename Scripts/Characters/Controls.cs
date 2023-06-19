using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Controls : MonoBehaviour
{
    public float delay;
    [SerializeField] private Character body;
    private Status status;

    private void Start() {
        status = transform.GetChild(0).GetComponent<Status>();
        body = transform.GetChild(0).GetComponent<Character>();
    }

    public void MovimentGet(InputAction.CallbackContext value){
        StartCoroutine(MovimentSet((int)Math.Round(value.ReadValue<Vector2>().x), (int)Math.Round(value.ReadValue<Vector2>().y)));
    }

    private IEnumerator MovimentSet(int x, int y){
        //Previsao do Sasaki
        if(status.shadowOn){
            status.shadow.GetComponent<Status>().axisX = x;
            status.shadow.GetComponent<Status>().axisY = y;
            if(x != 0) status.shadow.GetComponent<Status>().axisXLast = x;
            if(y != -1) status.shadow.GetComponent<Status>().axisYLast = y;
            yield return new WaitForSeconds(delay);
        }

        status.axisX = x;
        status.axisY = y;
        if(x != 0) status.axisXLast = x;
        if(y != -1) status.axisYLast = y;
    }

    public void AtackButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Canceled") StartCoroutine(AtackButtonCanceled());
    }

    public IEnumerator AtackButtonCanceled(){
        if(status.shadowOn){
            status.shadow.GetComponent<Character>().Atack();
            yield return new WaitForSeconds(delay);
        }
        
        body.Atack();
    }

    public void JumpButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started") StartCoroutine(JumpButtonStarted());
    }

    public IEnumerator JumpButtonStarted(){
        if(status.shadowOn){
            status.shadow.GetComponent<Character>().Jump();
            yield return new WaitForSeconds(delay);
        }
        body.Jump();
    }

    public void SpecialButton(InputAction.CallbackContext value){
        if(value.phase.ToString() == "Started") StartCoroutine(SpecialButtonStarted());
        if(value.phase.ToString() == "Canceled") StartCoroutine(SpecialButtonCanceled());
    }

    public IEnumerator SpecialButtonStarted(){
        if(status.shadowOn){
            status.shadow.GetComponent<Status>().usingSpecial = true;
            status.shadow.GetComponent<Character>().Special();
            yield return new WaitForSeconds(delay);
        }
        status.usingSpecial = true;
        body.Special();
    }

    public IEnumerator SpecialButtonCanceled(){
        if(status.shadowOn){
            status.shadow.GetComponent<Status>().usingSpecial = false;
            yield return new WaitForSeconds(delay);
        }
        status.usingSpecial = false;
    }
}
