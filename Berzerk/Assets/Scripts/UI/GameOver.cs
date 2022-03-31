using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    [SerializeField] WaveSystem waveSystem;
    [SerializeField] TextMeshProUGUI waveNumText;
    [SerializeField] Canvas healthPanel;

    [SerializeField] CanvasGroup GameOverCanvasGroup;
    bool fadeIn = false;
    bool fadeOut = false;

    public void Setup(int waveNum) {
        gameObject.SetActive(true);
        healthPanel.enabled = false;
        waveNumText.text = ("You Survived " + waveSystem.waveNum.ToString() + " Rounds");
    }

    void OnEnable() {
        fadeIn = true;
    }

    public void PlayGame() {
        fadeOut = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void Update() {
       if (fadeIn) {
           if (GameOverCanvasGroup.alpha < 1) {
               GameOverCanvasGroup.alpha += Time.deltaTime;
               if (GameOverCanvasGroup.alpha >= 1) {
                   fadeIn = false;
               }
           }
       }
       if (fadeOut) {
           if (GameOverCanvasGroup.alpha >= 0) {
               GameOverCanvasGroup.alpha -= Time.deltaTime;
               if (GameOverCanvasGroup.alpha == 0) {
                   fadeOut = false;
                   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               }
           }
       } 
    }

}
