using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnQuit;
    //
    [SerializeField] private Button _btnMusic;
    [SerializeField] private Button _btnSound;

    private void PlayGame()
    {
        AudioSystem.Instance.PlaySoundOnce(ListSound.Instance.Click);
        SceneManager.LoadScene("GamePlay");
    }

    private void QuitGame()
    {
        AudioSystem.Instance.PlaySoundOnce(ListSound.Instance.Click);
        Application.Quit();
    }

    private void ActionBtnMusic()
    {
        _btnMusic.onClick.AddListener(() => AudioSystem.Instance.OnMusicClicked());
    }

    private void ActionBtnSound()
    {
        _btnMusic.onClick.AddListener(() => AudioSystem.Instance.OnSoundClicked());
    }

    public void PlayOnceSoundClick()
    {
        AudioSystem.Instance.PlaySoundOnce(ListSound.Instance.Click);
    }

    private void Start()
    {
        _btnPlay.onClick.AddListener(() => this.PlayGame());
        _btnQuit.onClick.AddListener(() => this.QuitGame());
        this.ActionBtnMusic();
        this.ActionBtnSound();
        AudioSystem.Instance.PlayMusicWithClip(ListMusic.Instance.Music1);
    }
}
