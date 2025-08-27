using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private bool _hasTouchedPlatform = false;
    [SerializeField] private Color _defaultColor;

    private Renderer _renderer;

    public event Action<Cube> OnPlatformCollisionEnter;

    private void OnEnable()
    {
        _hasTouchedPlatform = false;
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _defaultColor;
    }

    private void RecolorSelf()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                OnPlatformCollisionEnter?.Invoke(this);
                _hasTouchedPlatform = true;
                RecolorSelf();
            }
        }
    }
}


