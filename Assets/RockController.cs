using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockController : MonoBehaviour
{
    TreasureController treasureController;
    Rigidbody2D rb2d;
    public GameObject center;
    
    private void Start(){
        treasureController = GetComponent<TreasureController>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<BigOysterController>() != null){
           treasureController.enabled = false;
           rb2d.simulated = false;
           StartCoroutine(treasureController.SlideToCenter(center.transform.position));
        }
    }
}
