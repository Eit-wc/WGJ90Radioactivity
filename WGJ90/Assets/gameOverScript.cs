using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {

    }

    public void restartLevel()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }
    
    public void gotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
   
}
