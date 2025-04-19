using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIGameOver : GUIBase
{
    [Header("Buttons")]
    [SerializeField] private Button restartButton;

    #region MonoBehaviour

    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnClickRestart);
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        
    }

    #endregion

    #region Private Methods

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }

    #endregion

    #region Button Events

    private void OnClickRestart()
    {
        Hide();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
