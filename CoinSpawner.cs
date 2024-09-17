using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private AudioSource _audioSource;

    private ObjectPool<Coin> _pool;
    private int _poolMaxSize = 10;
    private int _poolCapacity = 5;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coin),
            actionOnGet: OnGet,
            actionOnRelease: OnRealise,
            actionOnDestroy: coin => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Update()
    {
        if (_pool.CountActive < _poolMaxSize)
            _pool.Get();
    }

    private void OnGet(Coin coin)
    {
        coin.Init(GetRandomStartPosition());
        coin.gameObject.SetActive(true);
        coin.Changed += ChangePool;
    }

    private void OnRealise(Coin coin)
    {
        coin.gameObject.SetActive(false);
        coin.Changed -= ChangePool;
        _audioSource.Play();
    }

    private void ChangePool(Coin coin)
    {
        _pool.Release(coin);
    }

    private Vector3 GetRandomStartPosition()
    {
        int minRandomValueX = -8;
        int maxRandomValueX = 8;
        int minRandomValueY = -4;
        int maxRandomValueY = 4;
        System.Random random = new System.Random();
        int valueRandomX = random.Next(minRandomValueX, maxRandomValueX + 1);
        int valueRandomY = random.Next(minRandomValueY, maxRandomValueY + 1);

        return new Vector3(
            transform.position.x + valueRandomX,
            transform.position.y + valueRandomY,
            transform.position.z);
    }
}
