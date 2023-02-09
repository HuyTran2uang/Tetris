using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    public static UIGamePlay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
