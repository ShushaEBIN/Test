using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes = new List<Cube>();
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;

    public event Action<Cube> NotSpawned;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
        {
            cube.Clicked += SpawnCubes;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            cube.Clicked -= SpawnCubes;
        }
    }

    private void SpawnCubes(Cube cube)
    {
        cube.Clicked -= SpawnCubes;
        _cubes.Remove(cube);

        if (Random.value <= cube.SplitChance)
        {
            int scaleReduce = 2;
            int splitReduce = 2;
            int multiplierIncrease = 1;

            Vector3 position = cube.transform.position;
            Vector3 scale = cube.transform.localScale / scaleReduce;
            float newSplitChance = cube.SplitChance / splitReduce;
            float newMultiplierValue = cube.MultiplierValue + multiplierIncrease;

            int count = Random.Range(_minCubes, _maxCubes + 1);

            for (int i = 0; i < count; i++)
            {
                Cube newCube = Instantiate(cube, position, Quaternion.identity);
                newCube.Init(position, scale, newSplitChance, newMultiplierValue);
                newCube.Clicked += SpawnCubes;
                _cubes.Add(newCube);
            }
        }
        else
        {
            NotSpawned?.Invoke(cube);
        }
    }
}