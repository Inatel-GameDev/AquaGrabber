using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMP_Text money;
    public TMP_Text lives;
    public Image victoryImage;
    // Start is called before the first frame update
    void Start()
    {
        money.text = "X 00";
        lives.text = "X 3";
        victoryImage.gameObject.SetActive(false);
    }

    public void AddMoney(int points){
        money.text = $"X {points}";
    }

    public void ChangeLives(int life){
        lives.text = $"X {life}";
    }

    public void Victory(){
        victoryImage.gameObject.SetActive(true);
        StartCoroutine(VictoryAnimation());
    }

    IEnumerator VictoryAnimation(){
       RectTransform rectTransform = victoryImage.rectTransform;

        // Time variables
        float duration = 0.8f;
        float halfDuration = duration / 2f;

        // Scale from Y = 0 to Y = 1.5
        float elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float newYScale = Mathf.Lerp(0f, 1.5f, elapsedTime / halfDuration);
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, newYScale, rectTransform.localScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the Y scale is exactly 1.5 at the end of this part
        rectTransform.localScale = new Vector3(rectTransform.localScale.x, 1.5f, rectTransform.localScale.z);

        // Scale from Y = 1.5 to Y = 1
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float newYScale = Mathf.Lerp(1.5f, 1f, elapsedTime / halfDuration);
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, newYScale, rectTransform.localScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the Y scale is exactly 1 at the end of this part
        rectTransform.localScale = new Vector3(rectTransform.localScale.x, 1f, rectTransform.localScale.z);
        yield return new WaitForSeconds(halfDuration);
        victoryImage.gameObject.SetActive(false);
        GameController.Instance.GameOver();
    }
}
