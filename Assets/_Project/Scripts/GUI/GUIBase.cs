using UnityEngine;
using DG.Tweening;

public class GUIBase : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, .5f).SetUpdate(true); // Fade in over 0.5 seconds
    }

    public virtual void Hide()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0f, .5f).SetUpdate(true).OnComplete(() => gameObject.SetActive(false)); 
        
        // Fade out over 0.5 seconds
    }
}