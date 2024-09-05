using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform SpawnPoint;
    public SpriteRenderer spriteRenderer;
    public GameObject Cockpit;
    private Rigidbody2D rb2d;
    public WaterLevelController water;
    private Vector2 velocity;
    private Vector2 position;
    private float horizontalInput;
    private float verticalInput;
    public float moveSpeed = 8f;
    public float gravity = 0.1f;
    public float bumbForce = 0f;
    public float waterIncrease = 0f;
    public bool canMove = false;
    public bool onAir = false;
    private bool drowned = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        canMove = true;
        drowned = false;
        transform.position = SpawnPoint.position;
        position = SpawnPoint.position;
        rb2d.isKinematic = false;
        Cockpit.transform.SetParent(transform);
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.P)){
            Drown();
        }
    }

    private void FixedUpdate()
    {
       
        position.y += velocity.y * Time.fixedDeltaTime - gravity;
        position.x += velocity.x * Time.fixedDeltaTime;
        Debug.Log(rb2d.velocity);

        rb2d.MovePosition(position);
    }

    private void Movement()
    {
        if(!canMove){
            return;
        }
        // Accelerate / decelerate
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        velocity.x = Mathf.MoveTowards(velocity.x, horizontalInput * moveSpeed, moveSpeed * Time.deltaTime);
        velocity.y = Mathf.MoveTowards(velocity.y, verticalInput * moveSpeed, moveSpeed * Time.deltaTime);
        if(onAir){
            verticalInput = Mathf.Clamp(verticalInput, -1, 0);
        }

    }

    private void Bump()
    {
        if (!drowned)
        {
            water.level += waterIncrease;
            DropTreasure();
            StopCoroutine(DisableMovement(0));
            StartCoroutine(DisableMovement(.15f));
            velocity = -2 * velocity / 3;
            if (velocity.y >= 0)
            {
                velocity.y = Mathf.Max(velocity.y, 5);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Bump")){
            Bump();
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Air")){
            water.level = 0;
            onAir = true;
        }else{
            onAir = false;
        }
    }

    private void DropTreasure(){
        TreasureController treasure = GetComponentInChildren<TreasureController>();
        if(treasure != null){
            treasure.Release();
        }
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public void Drown(){
        if(!drowned){
            StartCoroutine(Drowning());
            drowned = true;
        }    
    }

    private IEnumerator Drowning(){
        Cockpit.transform.SetParent(null);
        canMove = false;
        yield return new WaitForSeconds(1f);
         if(GameController.Instance.GetLives() > 1){
            Debug.Log(GameController.Instance.GetLives());
            GameController.Instance.ChangeLives();
            Revive();
        }else{
            GameController.Instance.GameOver();
        }
    }

    private void Revive(){
        transform.position = SpawnPoint.position;
        position = transform.position;
        water.level = 0;
        Cockpit.transform.SetParent(transform);
        Cockpit.transform.rotation = Quaternion.Euler(0, 0, 0);
        Cockpit.transform.localPosition = new(0,0.5f,0);
        drowned = false;
        canMove = true;

    }
    
    public IEnumerator Dragged(GameObject obj, float time, int from, int to){
        DropTreasure();
        StartCoroutine(DisableMovement(time));
        Quaternion initialRotation = Quaternion.Euler(0, 0, from);
        Quaternion targetRotation = Quaternion.Euler(0, 0, to); // Rotate 360 degrees
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, obj.transform.position, elapsedTime / time), Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / time));
            elapsedTime += Time.deltaTime; // Increment the elapsed time
            yield return null; // Wait until the next frame
        }
        transform.SetPositionAndRotation(obj.transform.position, targetRotation);
        position = transform.position;
    }
}
