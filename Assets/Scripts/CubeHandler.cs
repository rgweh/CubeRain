using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _spawnTime;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            Cube cube = _cubeSpawner.GetSpawnedCube();
            cube.OnPlatformCollisionEnter += CubeActionOnHit;

            yield return wait;
        }
    }

    private void CubeActionOnHit(Cube cube)
    {
        float delay = Random.Range(_minDelay, _maxDelay);
        StartCoroutine(RemoveCubeOvertime(cube, delay));
    }

    private IEnumerator RemoveCubeOvertime(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        cube.OnPlatformCollisionEnter -= CubeActionOnHit;
        _cubeSpawner.RemoveCube(cube);
    }
}
