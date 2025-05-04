using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class GUIHUD : GUIBase, MMEventListener<EDataChanged>
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hpText;

    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EDataChanged>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EDataChanged>();
    }

    #endregion
    

    #region Private Methods

    private void UpdateUI()
    {
        scoreText.text = DataManager.Instance.CurrentScore.ToString();
        hpText.text = $"HP: {GamePlayManager.Instance.Player.CurrentHealth.ToString()}";
    }

    #endregion

    #region Events Listen

    public void OnMMEvent(EDataChanged eventType)
    {
        UpdateUI();
    }

    #endregion
}
