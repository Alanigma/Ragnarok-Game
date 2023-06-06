using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image lifeBar;
    [SerializeField] Image staminaBar;

    private Status status;

    private void Start() {
        status = GetComponent<Status>();
    }

    private void Update() {
        lifeBar.fillAmount = status.life / status.maxLife;
        staminaBar.fillAmount = status.stamina / status.maxStamina;
    }
}
