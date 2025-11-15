using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
  public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
