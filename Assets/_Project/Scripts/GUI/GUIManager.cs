using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>, MMEventListener<EventPlayerDie>
{
    [Header("GUIs")] 
    [SerializeField] private GUIGameOver guiGameOver;

    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EventPlayerDie>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EventPlayerDie>();
    }

    #endregion

    public void OnMMEvent(EventPlayerDie eventType)
    {
        //Hiện GUIGameOver
        guiGameOver.Show();
    }
}
