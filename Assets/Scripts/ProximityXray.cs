using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProximityXray : MonoBehaviour
{

    GameObject player;
    Renderer render;

    // Use this for initialization     
    void Start () {

        player = GameObject.Find("glass");
        render = gameObject.GetComponent<Renderer>();


     
    }          // Update is called once per frame     
    void Update () {

        render.sharedMaterial.SetVector("_PlayerPosition", player.transform.position);

        //Vector4 scaleNew = new Vector4(player.transform.localScale.x, player.transform.localScale.x, player.transform.localScale.x, player.transform.localScale.x);
       // render.sharedMaterial.SetVector("_VisibleDistance", scaleNew/2);

    } 
 }