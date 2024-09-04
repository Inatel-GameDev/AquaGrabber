using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OysterController : MonoBehaviour
{
    public Transform axis;
    public GameObject Top;
    public GameObject Bottom;
    private TreasureController Pearl;
    public float angulo = 80f;
    public float velocidade = 0.5f;
    public float espera = 0.5f;
    public bool free;

    void Start()
    {
        StartCoroutine(RotateBackAndForth());
        Pearl = GetComponentInChildren<TreasureController>();
        Pearl.OnCatch += () => free = true;
        free = false;
    }

    private IEnumerator RotateBackAndForth()
    {
        while (true)
        {
            yield return StartCoroutine(Move(angulo));

            yield return new WaitForSeconds(espera);

            yield return StartCoroutine(Move(-angulo));
            if(Pearl != null && !free)
                Pearl.gameObject.SetActive(false);

            yield return new WaitForSeconds(espera);
            if(Pearl != null && !free)
                Pearl.gameObject.SetActive(true);
        }
    }

    private IEnumerator Move(float targetAngle)
    {
        // Get the current local rotation angle of the Top object on the Z-axis
        float startAngle = Top.transform.localEulerAngles.z;
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
            Top.transform.RotateAround(axis.position, Vector3.forward, angle - Top.transform.localEulerAngles.z);

            yield return null; // Wait until the next frame
        }

        // Ensure the rotation is exactly at the target angle
        Top.transform.localEulerAngles = new Vector3(Top.transform.localEulerAngles.x, Top.transform.localEulerAngles.y, endAngle);
    }
}
