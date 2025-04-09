using UnityEngine;
using UnityEngine.SceneManagement;

public class endscreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {

    }


    void Update()
    {

    }

    public void GoToGame()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("started");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }


}
