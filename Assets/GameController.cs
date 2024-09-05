using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private int points;
    private int lives;
    private UIController UI;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        points = 0;
        lives = 3;
        UI = FindObjectOfType<UIController>();
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void LoadGame(string Scene){
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }

    public void GameOver(){
        SceneManager.LoadScene("MainMenuScene");
    }

    public void AddMoney(int point, bool end){
        points += point;
        UI.AddMoney(points);
        if(end)
            StartCoroutine(CheckForCompletion());
    }

    public IEnumerator CheckForCompletion(){
        yield return new WaitForSeconds(.1f);
        UI.Victory();
    }

    public void ChangeLives(){
        lives--;
        UI.ChangeLives(lives);
    }

    public int GetLives(){
        return lives;
    }
}