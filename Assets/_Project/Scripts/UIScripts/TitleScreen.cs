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

    public void gotogame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
} 
