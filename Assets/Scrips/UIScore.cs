using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public static UIScore Instance { get; private set; }

    [SerializeField] private Text _scoreText;

    public void SetScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void Awake()
    {
        Instance = this;
    }
}
