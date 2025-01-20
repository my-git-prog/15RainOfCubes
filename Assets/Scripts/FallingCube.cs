using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingCube : SpawnableObject<FallingCube>
{
    private List<GameObject>_collidedPlanes = new();
    private bool _isDestroying = false;

    public override event Action<FallingCube> Destroying;

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

        StartCoroutine(DelayedDestroy());
        _isDestroying = true;
    }

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
        _collidedPlanes.Clear();
        _isDestroying = false;
    }

    private IEnumerator DelayedDestroy()
    {
        var wait = new WaitForSeconds(UnityEngine.Random.Range(MinDestroyingDelay, MaxDestroyingDelay));

        yield return wait;

        Destroying?.Invoke(this);

        yield break;
    }
}
