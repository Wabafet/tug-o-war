using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;

    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuickPlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void Info()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
    }
}
