using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    public float speed;
    public float level;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector2.zero;
    }

    private void FixedUpdate(){
        level += speed/100;
        level=  Mathf.Min(level, 1);
        transform.localPosition = new(0,level);
        if(level == 1){
            PlayerController player = FindObjectOfType<PlayerController>();
            player.Drown();
        }
    }
}
