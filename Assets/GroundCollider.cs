using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public TreasureController treasure;

    void Start(){
        treasure = GetComponentInParent<TreasureController>();
    }

    private void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("Bump") && !treasure.rb2d.isKinematic){
            treasure.transform.position += Vector3.up;
        }
    }

}
