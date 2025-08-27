using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private bool _hasTouchedPlatform = false;
    [SerializeField] private Color _defaultColor;

    public event Action<Cube> OnPlatformCollisionEnter;

    public void Awake()
    {
        _hasTouchedPlatform = false;
        GetComponent<Renderer>().material.color = _defaultColor;
    }

    public void ModifyCubeOnHit()
    {
        _hasTouchedPlatform = true;
        GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                OnPlatformCollisionEnter?.Invoke(this);
            }
        }
    }
}


