using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehabiour<GameManager>
{
    public bool isGameClear;

    void Awake()
    {
        isGameClear = false;
    }
}
