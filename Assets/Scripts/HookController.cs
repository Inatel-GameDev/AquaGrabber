using System;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public GameObject hook;
    private PlayerController player;
    public bool downing;
    public bool holding;
    public bool retracted;
    public float range;
    public float speed;
    private float pos;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Vector2.zero;
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y >= 0){
            retracted = true;
        }else{
            retracted = false;
        }

        holding = CheckItem();
        //Swing();

        pos = Mathf.Clamp(pos, -1.5f, 0);

        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) && player.canMove){
            downing = true;
        }else{
            downing = false;
        }
    }

    private void FixedUpdate(){
        if(downing){
            pos -= speed;
        }else{
            pos += speed;
        }
        transform.localPosition = new(0,pos);
    }

    // private void Swing(){
    //     Vector3 rotationPoint = transform.TransformPoint(new Vector3(0, 2.5f, 0));
    //     transform.RotateAround(rotationPoint, Vector3.forward, 2* Time.deltaTime);
    // }

    private bool CheckItem(){
        // Check if the GameObject has any children
        if (hook.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }
}
