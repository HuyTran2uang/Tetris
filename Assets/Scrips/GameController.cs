using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private void StartGame()
    {
        Map.Instance.StartGame();
        SpawnBlock.Instance.StartGame();
        FindBestPosition.Instance.StartGame();
    }

    public void Replay()
    {
        MenuManager.Instance.Open("GamePlay");
        Score.Instance.ResetScore();
        this.StartGame();
    }

    public void Leave()
    {
        SceneManager.LoadScene("Launcher");
    }

    public void GameOver()
    {
        MenuManager.Instance.Open("GameOver");
    }

    public void OnMusicClicked()
    {
        AudioSystem.Instance.OnMusicClicked();
    }

    public void OnSoundClicked()
    {
        AudioSystem.Instance.OnSoundClicked();
    }

    private void Start()
    {
        this.StartGame();
    }

    private void Awake()
    {
        Instance = this;
    }
}
