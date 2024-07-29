using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> Clicked;

    public float SplitChance { get; private set; } = 1f;
    public float MultiplierValue { get; private set; } = 1f;

    public void Init(Vector3 position, Vector3 scale, float splitChance, float multiplierValue)
    {
        transform.position = position;
        transform.localScale = scale;
        SplitChance = splitChance;
        MultiplierValue = multiplierValue;
        GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }
}