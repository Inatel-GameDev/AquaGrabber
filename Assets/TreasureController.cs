using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    public event Action OnCatch;
    private Transform currentParent; // To keep track of the current parent
    private HookController hook;
    public Rigidbody2D rb2d {get; private set;}
    public float yOffset;
    public int points;
    public bool big;
    public bool canCatch;

    void Start(){
        hook = FindObjectOfType<HookController>();
        rb2d = GetComponent<Rigidbody2D>();
        canCatch = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(enabled){
            if (collision.gameObject.CompareTag("Grab") && !hook.holding && hook.downing && canCatch)
            {
                rb2d.isKinematic = true;
                OnCatch?.Invoke();
                transform.SetParent(collision.transform);
                currentParent = collision.transform;
            transform.localPosition = new(0, yOffset);
            }
            if(collision.gameObject.CompareTag("Net") && !rb2d.isKinematic){
                Catch();
            }
        }
    }

    void Update()
    {
        // Check if the current parent exists and has the specified bool variable
        if (currentParent != null)
        {   
            if(big){
                hook.downing = true;
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
                    Release();
            }else{
                if(hook.retracted){
                    Catch();
                }
            }
        }
    }

    public void Release(){
        transform.SetParent(null);
        rb2d.isKinematic = false;
        currentParent = null;
        canCatch = false;
        StartCoroutine(Cooldown());
    }

    void Catch(){
        Destroy(gameObject);
        GameController.Instance.AddMoney(points);
    }

    IEnumerator Cooldown(){
        yield return new WaitForSeconds(.3f);
        canCatch = true;
    }

    public IEnumerator SlideToCenter(Vector2 pos){
        Vector3 startPos = transform.position;
        Vector3 targetPos = new(pos.x, transform.position.y, transform.position.z);

        float tempo = 0f;
        while (tempo < 0.5f)
        {
            tempo += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, tempo / 0.5f);
            yield return null;
        }
        transform.position = targetPos;
    }
}
