using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private MoveController moveController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private CoinController coinController;

    [Header("VFX")] 
    [SerializeField] private GameObject dieVFX;

    #region MonoBehaviour

    private void OnDestroy()
    {
        MMEventManager.TriggerEvent(new EEnemyDie());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Player))
        {
            Die();
        }
    }

    #endregion

    #region Public Methods

    public void Die()
    {
        StartCoroutine(IDie());
    }

    #endregion
    
    #region Private Methods

    private IEnumerator IDie()
    {
        // tắt hình ảnh, collider,
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        moveController.enabled = false;
        // bật hiệu ứng chết
        
        dieVFX.SetActive(true);
        Instantiate(coinController, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        //spawn coin
    }
    
    #endregion
}