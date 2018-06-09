using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject FinishPanel;
    public GameObject Player;
    private PlayerController _player;
    public GameObject LevelIndicator;
    public GameObject LostLifePanel;
    public GameObject GameOver;
    public Text LifesText;

    // Already lost life?
    private bool alreadyDone;

    IEnumerator LoadNextLevel(){
        yield return new WaitForSeconds(7f);
        GameManager.Instance.NextLevel();
        yield return null;
    }

    IEnumerator ResetLevel(){
        yield return new WaitForSeconds(6.5f);
        GameManager.Instance.RestartLevel();
        yield return null;
    }

    IEnumerator MainMenu(){
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    public void FinishedLevel(){
        if (alreadyDone != true)
        {
            alreadyDone = true;
            FinishPanel.SetActive(true);
            LevelIndicator.SetActive(false);
        }
    }

    public void LostLife(){
        if (alreadyDone != true)
        {
            Debug.Log("LostLife");
            GameManager.Instance.LostLife();
            alreadyDone = true;
            if (GameManager.Instance.IsGameOver() == false)
            {
                LevelIndicator.SetActive(false);
                LostLifePanel.SetActive(true);
                LifesText.text = "You have " + GameManager.Instance.lifes.ToString() + " life(s) left";
                StartCoroutine(ResetLevel());
            } else {
                alreadyDone = true;
                GameOver.SetActive(true);
                StartCoroutine(MainMenu());
            }
        }
    }


    private void Start()
    {
        Player = GameObject.Find("Player");
        _player = Player.GetComponent<PlayerController>();
        alreadyDone = false;
    }

    private void Update()
    {
        if (_player.finishedLevel == true){
            FinishedLevel();
            StartCoroutine(LoadNextLevel());
        }
        if (_player.lostLife == true)
            LostLife();
    }
}
