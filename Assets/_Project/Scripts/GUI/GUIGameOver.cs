using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIGameOver : GUIBase
{
    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

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
        scoreText.text = $"Your Score: {DataManager.Instance.CurrentScore}";
        highScoreText.text = $"High Score: {DataManager.Instance.HighScore}";
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
        Debug.Log($"Click restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        MMEventManager.TriggerEvent(new EGameRestart());
    }

    

    #endregion
}
