using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _minExplosionRadius;
    [SerializeField] private float _minExplosionForce;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();    
    }

    private void OnEnable()
    {
        _spawner.NotSpawned += Explode;    
    }

    private void Explode(Cube cube)
    {
        float explosionForce = _minExplosionForce * cube.MultiplierValue;
        float explosionRadius = _minExplosionRadius * cube.MultiplierValue;

        foreach (Rigidbody explodableObject in GetExplodableObjects(cube))
        {
            explodableObject.AddExplosionForce(explosionForce, cube.transform.position, explosionRadius);
        }        
    }

    private List<Rigidbody> GetExplodableObjects(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, _minExplosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}