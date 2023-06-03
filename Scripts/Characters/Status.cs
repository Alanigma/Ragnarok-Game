using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float maxSpeedY = -10;
    public float atackCooldown;
    public float stamina;
    public int axisX;
    public int axisY;
    public int axisXLast = 1;
    public int axisYLast = 0;
}
