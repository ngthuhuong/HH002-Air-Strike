using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    public Animator Animator => animator;
    
    Coroutine currentCoroutine;
    void OnEnable()
    {
        currentCoroutine = StartCoroutine(ITurnOff());
    }

    private void OnDisable()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    private IEnumerator ITurnOff()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
