using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
