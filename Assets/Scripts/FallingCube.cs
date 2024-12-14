using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class FallingCube : MonoBehaviour
{
    [SerializeField] private float _minDestroyingDelay = 2f;
    [SerializeField] private float _maxDestroyingDelay = 5f;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private List<GameObject>_collidedPlanes = new();
    private bool _isDestroying = false;

    public event UnityAction<FallingCube> Destroying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CollidedPlane>() == false)
            return;

        if (_collidedPlanes.Contains(collision.gameObject))
            return;

        _collidedPlanes.Add(collision.gameObject);
        SetRandomColor();

        if (_isDestroying)
            return;

        //Invoke(nameof(CallDestroyEvent), Random.Range(_minDestroyingDelay, _maxDestroyingDelay));
        StartCoroutine(DelayedDestroy());
        _isDestroying = true;
    }

    /*private void CallDestroyEvent()
    {
        Destroying?.Invoke(this);
    }*/

    private void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void ResetParametres()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _collidedPlanes.Clear();
        _isDestroying = false;
    }

    private IEnumerator DelayedDestroy()
    {
        var wait = new WaitForSeconds(Random.Range(_minDestroyingDelay, _maxDestroyingDelay));

        yield return wait;

        Destroying?.Invoke(this);

        yield break;
    }
}
