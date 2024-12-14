using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private float _minSpawnX = -10;
    [SerializeField] private float _maxSpawnX = 10;
    [SerializeField] private float _minSpawnY = 5;
    [SerializeField] private float _maxSpawnY= 15;
    [SerializeField] private float _minSpawnZ = -10;
    [SerializeField] private float _maxSpawnZ = 10;
    [SerializeField] private float _repeatRate = 0.05f;
    [SerializeField] private int _poolCapacity = 100;
    [SerializeField] private int _poolMaxSize = 150;

    private ObjectPool<FallingCube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<FallingCube>(
            createFunc: () => CreateFunc(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private FallingCube CreateFunc()
    {
        FallingCube newCube = Instantiate(_cubePrefab);
        newCube.Destroying += DestroyingCube;

        return newCube;
    }

    private void DestroyingCube(FallingCube cube)
    {
        _pool.Release(cube);
    }

    private void ActionOnGet(FallingCube cube)
    {
        cube.transform.position = new Vector3(Random.Range(_minSpawnX, _maxSpawnX), 
            Random.Range(_minSpawnY, _maxSpawnY), Random.Range(_minSpawnZ, _maxSpawnZ));
        cube.ResetParametres();
        cube.SetColor(_defaultColor);
        cube.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ActionOnDestroy(FallingCube cube)
    {
        cube.Destroying -= DestroyingCube;
        Destroy(cube);
    }
}
