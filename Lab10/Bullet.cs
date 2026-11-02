using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<GameObject> myPool;
    [SerializeField] private float lifeTime = 5f;
    public void InitializePool(IObjectPool<GameObject> pool)
    {
        myPool = pool;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * 12f * Time.deltaTime);
    }

    private void OnEnable()
    {
        //return bullet to pool after time
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void OnDisable()
    {
        //return bullet to pool if hit
        CancelInvoke();
    }

    private void ReturnToPool()
    {
        if (myPool != null)
        {
            myPool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //return bullet if it hits something
    private void OnTriggerEnter2D(Collider2D collided)
    {
        ReturnToPool();
    }
}