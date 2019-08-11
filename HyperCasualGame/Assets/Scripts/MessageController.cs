using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageController : MonoBehaviour
{
    static MessageController instance;
    static public MessageController Instance
    {
        get { return instance; }
    }

    [SerializeField] private int nextLevel;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text gameWinText;

    private void Awake()
    {
        instance = this;
    }

    public void GameOverEvent()
    {
        gameOverText.enabled = true;
        StartCoroutine(WaitBeforeReload());
    }

    public void GameWinEvent()
    {
        gameWinText.enabled = true;
        StartCoroutine(WaitBeforeReload());
    }

    IEnumerator WaitBeforeReload()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextLevel);
    }
}
