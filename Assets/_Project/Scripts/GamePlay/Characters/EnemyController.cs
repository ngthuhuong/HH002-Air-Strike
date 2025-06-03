using System;
using System.Collections;
using DG.Tweening;
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

    private void FixedUpdate()
    {
        CheckIfOutsideScreen();
    }

    private void OnDestroy()
    {
        Debug.Log($"EnemyController.OnDestroy() - {gameObject.name}");
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
        MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.SFX_Explosion_Soft));
        // tắt hình ảnh, collider,
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        moveController.enabled = false;
        // bật hiệu ứng chết
        
        dieVFX.SetActive(true);
        //Instantiate(coinController, transform.position, Quaternion.identity);
        // CoinPoolObject.Instance.GetObject(transform.position, Quaternion.identity);
        // PoolingManager.Instance.ReturnObject(PoolingManager.PoolTag.Coin, gameObject);
        RewardPoolCoin.Instance.GetRandomReward(transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
    
    private void CheckIfOutsideScreen()
    {
        // Check if the projectile is outside the screen
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.y < ConstValue.ViewportMinY)
        {
            // ProjectilePoolObject.Instance.ReturnObject(gameObject);
            // PoolingManager.Instance.ReturnObject(poolTag, gameObject);
            Destroy(gameObject);
        }
    }
    #endregion
}