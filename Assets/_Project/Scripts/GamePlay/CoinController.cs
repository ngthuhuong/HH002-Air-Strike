using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Player))
        {
            CoinPoolObject.Instance.ReturnObject(gameObject);
            // PoolingManager.Instance.ReturnObject(PoolingManager.PoolTag.Coin, gameObject);
        }
    }

    #endregion
}
