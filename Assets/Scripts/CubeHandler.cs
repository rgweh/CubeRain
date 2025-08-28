using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _minRemoveDelay;
    [SerializeField] private float _maxRemoveDelay;

    private void OnEnable()
    {
        _cubeSpawner.OnCubeSpawned += SubscribeToCube;
    }

    private void OnDisable()
    {
        _cubeSpawner.OnCubeSpawned -= SubscribeToCube;
    }

    private void SubscribeToCube(Cube cube)
    {
        cube.OnPlatformCollisionEnter += RemoveCubeOvertime;
    }

    private void RemoveCubeOvertime(Cube cube)
    {
        cube.OnPlatformCollisionEnter -= RemoveCubeOvertime;
        cube.Renderer.material.color = Random.ColorHSV();

        float delay = Random.Range(_minRemoveDelay, _maxRemoveDelay);
        StartCoroutine(_cubeSpawner.RemoveCubeOvertime(cube, delay));
    }
}
