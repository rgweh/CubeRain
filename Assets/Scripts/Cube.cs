using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private bool _hasTouchedPlatform = false;
    [SerializeField] private float _minRemoveDelay = 2f;
    [SerializeField] private float _maxRemoveDelay = 5f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public event Action<Cube> OnRemoveDelayEnd;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                _hasTouchedPlatform = true;
                _renderer.material.color = UnityEngine.Random.ColorHSV();
                float delay = UnityEngine.Random.Range(_minRemoveDelay, _maxRemoveDelay);
                StartCoroutine(RemoveAfterDelay(delay));
            }
        }
    }

    private IEnumerator RemoveAfterDelay(float delay)
    {
        var wait = new WaitForSeconds(delay);

        yield return wait;

        OnRemoveDelayEnd?.Invoke(this);
    }

    public void SetToDefault(Color color, Vector3 coordinates)
    {
        gameObject.SetActive(true);
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _renderer.material.color = color;
        transform.position = coordinates;
        _hasTouchedPlatform = false;
    }
}


