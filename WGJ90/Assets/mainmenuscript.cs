using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenuscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startintro()
    {
        SceneManager.LoadScene("IntroScene",LoadSceneMode.Single);
    }
}
