using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxCapacity = 10;

    private IObjectPool<GameObject> pool;

    private void Awake()
    {
        //check for existing bulletpool
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        pool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxCapacity
        );
    }

    //create bullet if there isn't one already
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Bullet component = bullet.GetComponent<Bullet>();
        if (component != null)
        {
            component.InitializePool(pool);
        }
        return bullet;
    }

    //set active when retrieved from pool
    private void OnGetFromPool(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    //set inactive when sent back to pool
    private void OnReleaseToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    //destroy when above max size
    private void OnDestroyPoolObject(GameObject bullet)
    {
        Destroy(bullet);
    }

    //shoot method
    public GameObject GetBullet()
    {
        return pool.Get();
    }
}