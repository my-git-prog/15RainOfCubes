using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner <T> : MonoBehaviour where T : SpawnableObject <T>
{
    [SerializeField] private T _objectPrefab;
    [SerializeField] private int _poolCapacity = 100;
    [SerializeField] private int _poolMaxSize = 150;

    [SerializeField] protected bool IsSpawning = true;

    private int _countAll = 0;
    private ObjectPool<T> _pool;
    
    public event Action<Vector3> ObjectReleased;
    public event Action CountsUpdated;

    public int CountAll => _countAll;
    public int CountCreated => _pool.CountAll;
    public int CountActive => _pool.CountActive;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => CreateNewObjectInstance(),
            actionOnGet: (obj) => SpawnObjectInstance(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => DestroyObjectInstance(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private T CreateNewObjectInstance()
    {
        T newObj = Instantiate(_objectPrefab);
        newObj.Destroying += ReleaseObjectInstance;

        return newObj;
    }

    private void ReleaseObjectInstance(T obj)
    {
        ObjectReleased?.Invoke(obj.transform.position);
        _pool.Release(obj);
        CountsUpdated?.Invoke();
    }

    private void SpawnObjectInstance(T obj)
    {
        obj.gameObject.SetActive(true);
        obj.ResetParametres();
        CountsUpdated?.Invoke();
    }

    protected T GetObjectInstance()
    {
        _countAll++;
        return _pool.Get();
    }

    private void DestroyObjectInstance(T obj)
    {
        obj.Destroying -= ReleaseObjectInstance;
        Destroy(obj);
    }
}
