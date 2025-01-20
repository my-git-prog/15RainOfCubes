using UnityEngine;

public class SpawnerBombs : Spawner<Bomb>
{
    [SerializeField] private SpawnerFallingCubes _spawnerFallingCubes;

    private void OnEnable()
    {
        _spawnerFallingCubes.ObjectReleased += SpawnBomb;
    }

    private void OnDisable()
    {
        _spawnerFallingCubes.ObjectReleased -= SpawnBomb;
    }

    private void SpawnBomb(Vector3 position)
    {
        if (IsSpawning == false) 
            return;

        Bomb bomb = GetObjectInstance();
        bomb.transform.position = position;
    }
}
