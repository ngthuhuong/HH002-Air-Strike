using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private MoveController moveController;


    #region MonoBehaviour

    private void OnDestroy()
    {
        MMEventManager.TriggerEvent(new EEnemyDie());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Player))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Private Methods

    

    #endregion
}