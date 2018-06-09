using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void PlayGame(){
        StartCoroutine(PlayGameCoroutine());
    }

    public void GoOptions(){
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GoCredits(){
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame(){
        Application.Quit();
    }

    IEnumerator PlayGameCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.InitGame();
        GameManager.Instance.StartGame();
        yield return null;
    }

}
