using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    
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
