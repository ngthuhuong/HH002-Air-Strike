using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>, MMEventListener<EGameOver>, MMEventListener<EDataChanged>
{
    [Header("GUIs")] 
    [SerializeField] private GUIGameOver guiGameOver;
    [SerializeField] private GUIHUD guiHUD;

    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EGameOver>();
        this.MMEventStartListening<EDataChanged>();

    }

    private void OnDisable()
    {
        this.MMEventStopListening<EGameOver>();
        this.MMEventStopListening<EDataChanged>();

    }

    #endregion

    public void OnMMEvent(EGameOver eventType)
    {
        //Hiện GUIGameOver
        guiGameOver.Show();
    }

    public void OnMMEvent(EDataChanged eventType)
    {
        guiHUD.UpdateUI();
    }
}
