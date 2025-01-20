using System.Collections;
using UnityEngine;

public class SpawnerFallingCubes : Spawner<FallingCube>
{
    [SerializeField] private float _minSpawnX = -10;
    [SerializeField] private float _maxSpawnX = 10;
    [SerializeField] private float _minSpawnY = 5;
    [SerializeField] private float _maxSpawnY = 15;
    [SerializeField] private float _minSpawnZ = -10;
    [SerializeField] private float _maxSpawnZ = 10;
    [SerializeField] private float _repeatRate = 0.05f;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (IsSpawning)
        {
            FallingCube cube = GetObjectInstance();
            cube.transform.position = GetRandomSpawnPoint();

            yield return wait;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(_minSpawnX, _maxSpawnX),
            Random.Range(_minSpawnY, _maxSpawnY), 
            Random.Range(_minSpawnZ, _maxSpawnZ));
    }

}
