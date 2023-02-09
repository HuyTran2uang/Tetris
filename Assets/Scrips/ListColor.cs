using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListColor : MonoBehaviour
{
    public static ListColor Instance { get; private set; }

    public Sprite Null;
    public Sprite BestPos;
    public Sprite Red;
    public Sprite Green;
    public Sprite Blue;
    public Sprite Navy;
    public Sprite Orange;
    public Sprite Pink;
    public Sprite Yellow;

    private void Awake()
    {
        Instance = this;
    }
}
