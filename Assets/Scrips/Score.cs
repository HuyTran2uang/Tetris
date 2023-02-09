using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    [SerializeField] private GameObject _animScore50Prefab;
    [SerializeField] private Transform _gamePlay;
    private int _score;
    public int GetScore => _score;

    public void IncreaseScore(int score)
    {
        _score += score;
        UIScore.Instance.SetScoreText(_score);
        Instantiate(_animScore50Prefab, _gamePlay);
    }

    public void ResetScore()
    {
        _score = 0;
    }

    private void Start()
    {
        UIScore.Instance.SetScoreText(_score);
    }

    private void Awake()
    {
        Instance = this;
    }
}
