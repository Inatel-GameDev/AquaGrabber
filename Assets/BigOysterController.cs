using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOysterController : MonoBehaviour
{
    public bool safe = false;
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    public Transform spit;
    public Transform axis;
    public Transform center;
    public float velocidade = 0.5f;
    [SerializeField] TreasureController Pearl;
    private bool canRun = true;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Rock")){
            safe = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Treasure") && !safe && canRun){
            StartCoroutine(Kill());
        }
    }

    private IEnumerator Kill(){
        canRun = false;
        PlayerController p = FindObjectOfType<PlayerController>();
        p.canMove = false;
        top.sortingLayerName = "Terrain";
        bottom.sortingLayerName = "Terrain";
        StartCoroutine(p.Dragged(center.gameObject, 0.7f, 0, -30));
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(Move(-65f));

        yield return new WaitForSeconds(1);
        if(Pearl.transform.parent == null){
            Pearl.transform.position = Pearl.initialPos;
        }

        StartCoroutine(Move(65f));
        StartCoroutine(p.Dragged(spit.gameObject, 1.5f, 180, 0));
        top.sortingLayerName = "Default";
        bottom.sortingLayerName = "Default";
        yield return new WaitForSeconds(0.25f);
        p.canMove = true;
        canRun = true;
    }

    private IEnumerator Move(float targetAngle)
    {
        // Get the current local rotation angle of the Top object on the Z-axis
        float startAngle = top.transform.localEulerAngles.z;
        if (startAngle > 180) startAngle -= 360; // Normalize the angle to the range of [-180, 180]

        // Calculate the destination angle relative to the start
        float endAngle = startAngle + targetAngle;

        float elapsedTime = 0f;
        while (elapsedTime < velocidade)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate the angle over time
            float angle = Mathf.Lerp(startAngle, endAngle, elapsedTime / velocidade);

            // Apply the rotation around the Z-axis of the axis object
            top.transform.RotateAround(axis.position, Vector3.forward, angle - top.transform.localEulerAngles.z);

            yield return null; // Wait until the next frame
        }

        // Ensure the rotation is exactly at the target angle
        top.transform.localEulerAngles = new Vector3(top.transform.localEulerAngles.x, top.transform.localEulerAngles.y, endAngle);
    }
}
