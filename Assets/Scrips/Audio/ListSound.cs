using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSound : MonoBehaviour
{
    public static ListSound Instance { get; private set; }
    public AudioClip Click;
    public AudioClip Fast;
    public AudioClip EatItem;

    private void Awake()
    {
        Instance = this;
    }
}
