using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static Manager Instance { get; private set; }
    // Start is called before the first frame update
    public void LoadGame(string Scene){
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
