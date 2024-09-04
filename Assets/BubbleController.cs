using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
        if(col.gameObject.CompareTag("Air")){
            StartCoroutine(Burst());
        }
    }

    private IEnumerator Burst(){
        //animação
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
