using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>, MMEventListener<EGameOver>, MMEventListener<EDataChanged>,MMEventListener<EActiveBooster>
{
    [Header("GUIs")] 
    [SerializeField] private GUIGameOver guiGameOver;
    [SerializeField] private GUIHUD guiHUD;
    [SerializeField] private GUIShop guiShop;
    [SerializeField] private GUISettings guiSettings;
    
    public GUIGameOver GUIGameOver => guiGameOver;
    public GUIHUD GUIHUD => guiHUD;
    public GUIShop GUIShop => guiShop;
    public GUISettings GUISettings => guiSettings;

    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EGameOver>();
        this.MMEventStartListening<EDataChanged>();
        this.MMEventStartListening<EActiveBooster>();

    }

    private void OnDisable()
    {
        this.MMEventStopListening<EGameOver>();
        this.MMEventStopListening<EDataChanged>();
        this.MMEventStopListening<EActiveBooster>();
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

    
    public void OnMMEvent(EActiveBooster eventType)
    {
        guiHUD.AppyBoosterCooldown(eventType.BoosterType, eventType.Duration);
    }
}
