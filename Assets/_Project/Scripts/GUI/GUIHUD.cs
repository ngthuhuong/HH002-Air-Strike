using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class GUIHUD : GUIBase
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI coinText;
    

    #region MonoBehaviour

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    #endregion
    

    #region Public Methods

    public void UpdateUI()
    {
        scoreText.text = DataManager.Instance.CurrentScore.ToString();
        hpText.text = $"HP: {GamePlayManager.Instance.Player.CurrentHealth.ToString()}";
        coinText.text = DataManager.Instance.CurrentCoin.ToString();
    }

    #endregion
}
