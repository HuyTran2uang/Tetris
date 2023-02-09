using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Image image;
    public Button button;
    public bool hasValue;

    public void SetColor(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetListAction(List<Action> calls)
    {
        foreach (var call in calls)
            button.onClick.AddListener(() => call());
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }
}
