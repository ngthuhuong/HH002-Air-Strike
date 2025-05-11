using DG.Tweening;
using UnityEngine;

public class MoveBetweenPositions : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Vector3 positionA;
    [SerializeField] private Vector3 positionB;

    [Header("Settings")]
    [SerializeField] private float duration = 2f;
    [SerializeField] private Ease easeType = Ease.Linear;

    private Tween currentTween;

    private void Start()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        // Start moving between positionA and positionB
        currentTween = transform.DOMove(positionB, duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void UpdateEase()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.SetEase(easeType);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateEase();
    }
#endif
}