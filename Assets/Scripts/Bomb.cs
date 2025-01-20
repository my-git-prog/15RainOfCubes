using System;
using System.Collections;
using UnityEngine;

public class Bomb : SpawnableObject<Bomb>
{
    [SerializeField] private float _transperencyDeltaTime = 0.3f;
    [SerializeField] private float _maximumTransperency = 1f;
    [SerializeField] private float _minimumTransperency = 0f;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 500f;
    
    public override event Action<Bomb> Destroying;

    private void SetRandomColor()
    {
        SetColor(UnityEngine.Random.ColorHSV());
        Renderer.material.color = new Color(Renderer.material.color.r, Renderer.material.color.g,
            Renderer.material.color.b, UnityEngine.Random.value);
    }

    private void SetColor(Color color)
    {
        Renderer.material.color = color;
    }

    public override void ResetParametres()
    {
        SetColor(DefaultColor);
        transform.rotation = Quaternion.identity;
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        float destroyingTime = UnityEngine.Random.Range(MinDestroyingDelay, MaxDestroyingDelay);
        float elapsedTime = 0f;
        var wait = new WaitForSeconds(_transperencyDeltaTime);

        while (elapsedTime < destroyingTime)
        {
            elapsedTime += _transperencyDeltaTime;
            Renderer.material.color = new Color(DefaultColor.r, DefaultColor.g, DefaultColor.b,
                Mathf.Lerp(_maximumTransperency, _minimumTransperency, elapsedTime/ destroyingTime));

            yield return wait;
        }

        _exploder.Explode(_explosionRadius, _explosionForce);
        Destroying?.Invoke(this);
    }
}
