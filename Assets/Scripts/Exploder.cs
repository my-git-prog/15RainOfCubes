using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(float explosionRadius, float explosionForce)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(explosionRadius))
        {
            explodableObject.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
