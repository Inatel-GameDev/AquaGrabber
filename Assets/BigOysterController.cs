using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOysterController : MonoBehaviour
{
    public bool safe = false;
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    public Transform spit;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Rock")){
            safe = true;
        }
        if(col.gameObject.CompareTag("Player")){
            StartCoroutine(Kill());
        }
    }

    private void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Treasure") && !safe){
            StartCoroutine(Kill());
        }
    }

    private IEnumerator Kill(){
        PlayerController p = FindObjectOfType<PlayerController>();
        top.sortingLayerName = "Terrain";
        bottom.sortingLayerName = "Terrain";
        StartCoroutine(p.Dragged(top.gameObject, 0.5f, 0, -30));
        yield return new WaitForSeconds(1);
        StartCoroutine(p.Dragged(spit.gameObject, 1.5f, 180, 0));
        top.sortingLayerName = "Default";
        bottom.sortingLayerName = "Default";
    }
}
