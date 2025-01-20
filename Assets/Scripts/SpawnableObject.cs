using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class SpawnableObject <T>: MonoBehaviour
{
    [SerializeField] protected float MinDestroyingDelay = 2f;
    [SerializeField] protected float MaxDestroyingDelay = 5f;
    [SerializeField] protected Color DefaultColor = Color.white;

    protected Rigidbody Rigidbody;
    protected Renderer Renderer;

    public abstract event Action <T> Destroying;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
    }

    public abstract void ResetParametres();
}
