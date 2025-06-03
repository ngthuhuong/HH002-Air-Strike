using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>, MMEventListener<EGameOver>, MMEventListener<EDataChanged>,MMEventListener<EActiveBooster>, MMEventListener<EGameRestart>, IResetable
{
    [Header("GUIs")] 
    [SerializeField] private GUIGameOver guiGameOver;
    [SerializeField] private GUIHUD guiHUD;
    [SerializeField] private GUIShop guiShop;
    [SerializeField] private GUISettings guiSettings;
    [SerializeField] private GUIProfile guiProfile;
    
    public GUIGameOver GUIGameOver => guiGameOver;
    public GUIHUD GUIHUD => guiHUD;
    public GUIShop GUIShop => guiShop;
    public GUISettings GUISettings => guiSettings;
    public GUIProfile GUIProfile => guiProfile;

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

    #region Events Listen

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

    public void OnMMEvent(EGameRestart eventType)
    {
        ResetState();
    }

    #endregion

    #region IResetable

    public bool isActivated { get; set; }
    public void ResetState()
    {
        throw new NotImplementedException();
    }

    public void StartState()
    {
        throw new NotImplementedException();
    }

    public void EndState()
    {
        throw new NotImplementedException();
    }

    #endregion
}
