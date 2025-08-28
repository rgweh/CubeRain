using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private bool _hasTouchedPlatform = false;

    public Renderer Renderer;
    public Rigidbody Rigidbody;

    public event Action<Cube> OnPlatformCollisionEnter;

    private void OnEnable()
    {
        _hasTouchedPlatform = false;
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                OnPlatformCollisionEnter?.Invoke(this);
                _hasTouchedPlatform = true;
            }
        }
    }
}


