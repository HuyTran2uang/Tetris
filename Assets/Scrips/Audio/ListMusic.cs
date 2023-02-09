using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMusic : MonoBehaviour
{
    public static ListMusic Instance { get; private set; }

    public AudioClip Music1;

    private void Awake()
    {
        Instance = this;
    }
}
