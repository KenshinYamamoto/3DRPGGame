using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObserver : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;

    public static GameObserver gameObserver;
    private void Awake()
    {
        gameObserver = this;
    }

    public void ShowGameOverText()
    {
        gameOverText.SetActive(true);
        Invoke("ToTitleScene", 2f);
    }

    public void ShowGameClearText()
    {
        gameClearText.SetActive(true);
        Invoke("ToTitleScene", 2f);
    }

    void ToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
