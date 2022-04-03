using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameOver gameOverScreen;
    [SerializeField] WaveSystem waveSystem;

    void GameOver() {
        DisablePlayer();
        waveSystem.gameObject.SetActive(false);
        gameOverScreen.Setup(waveSystem.waveNum);
    }

    void DisablePlayer() {
        player.SetActive(false);
    }
    
}
