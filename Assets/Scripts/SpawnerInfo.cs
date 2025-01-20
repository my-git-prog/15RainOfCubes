using TMPro;
using UnityEngine;

public class SpawnerInfo<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] private TextMeshProUGUI _countAll;
    [SerializeField] private TextMeshProUGUI _countCreated;
    [SerializeField] private TextMeshProUGUI _countActive;
    [SerializeField] private Spawner<T> _spawner;

    private void OnEnable()
    {
        _spawner.CountsUpdated += UpdateCounts;
    }
    private void UpdateCounts()
    {
        _countAll.text = _spawner.CountAll.ToString();
        _countCreated.text = _spawner.CountCreated.ToString();
        _countActive.text = _spawner.CountActive.ToString();
    }
}
