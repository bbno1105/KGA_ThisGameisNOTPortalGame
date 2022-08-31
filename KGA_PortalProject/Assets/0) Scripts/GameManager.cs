using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehabiour<GameManager>
{
    public string playerName;

    public bool isGameClear;

    void Awake()
    {
        isGameClear = false;
    }
}
