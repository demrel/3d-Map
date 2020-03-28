﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerSetting : MonoBehaviour
{
    public static MultiPlayerSetting multiPlayerSetting;

    public bool delayStart;
    public int maxPlayers;

    public int menuScene;
    public int multiPlayerScene;

    private void Awake()
    {
        if (MultiPlayerSetting.multiPlayerSetting==null)
        {
            MultiPlayerSetting.multiPlayerSetting = this;
        }
        else
        {
            if (MultiPlayerSetting.multiPlayerSetting!=this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
