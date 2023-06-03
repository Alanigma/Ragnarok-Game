using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneClones : MonoBehaviour
{
    public GameObject scene;
    void Start()
    {
        //Coluna 0
        GameObject newScene = Instantiate(scene, new Vector2(transform.position.x-17.8f, transform.position.y-10), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x-17.8f, transform.position.y), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x-17.8f, transform.position.y+10), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x-17.8f, transform.position.y+20), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        
        //Coluna 1
        newScene = Instantiate(scene, new Vector2(transform.position.x, transform.position.y-10), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x, transform.position.y+10), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x, transform.position.y+20), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        
        //Coluna 2
        newScene = Instantiate(scene, new Vector2(transform.position.x+17.8f, transform.position.y-10), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x+17.8f, transform.position.y), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x+17.8f, transform.position.y+10), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
        newScene = Instantiate(scene, new Vector2(transform.position.x+17.8f, transform.position.y+20), Quaternion.identity);
        newScene.transform.parent = scene.transform.parent.transform;
    }
}
