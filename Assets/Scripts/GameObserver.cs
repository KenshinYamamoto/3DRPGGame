using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void ShowGameClearText()
    {
        gameClearText.SetActive(true);
    }
}
