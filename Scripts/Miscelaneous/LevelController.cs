using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> players;
    public GameObject shots;

    public void Restart()
    {
        foreach (GameObject player in players)
        {
            for (int i = 0; i < shots.transform.childCount; i++)
            {
                Destroy(shots.transform.GetChild(i).gameObject);
            }
        }
    }
}
