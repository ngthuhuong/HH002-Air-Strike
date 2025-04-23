using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>, MMEventListener<EGameOver>
{
    [Header("GUIs")] 
    [SerializeField] private GUIGameOver guiGameOver;
    [SerializeField] private GUIHUD guiHUD;

    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EGameOver>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EGameOver>();
    }

    #endregion

    public void OnMMEvent(EGameOver eventType)
    {
        //Hiện GUIGameOver
        guiGameOver.Show();
    }
}
