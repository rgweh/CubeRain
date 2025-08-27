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

    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => PullCube(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false)
        );

        StartCoroutine(SpawnCubes());
    }

    private Cube CreateCube()
    {
        Cube cube = _cubeSpawner.CreateCube();
        cube.OnPlatformCollisionEnter += CubeActionOnHit;

        return cube;
    }

    private void PullCube(Cube cube)
    {
        _cubeSpawner.PullCube(cube.gameObject);
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            Cube cube = _cubePool.Get();
            cube.GetComponent<Cube>().ModifySelfOnSpawn();

            yield return wait;
        }
    }

    private void CubeActionOnHit(Cube cube)
    {
        cube.ModifyCubeOnHit();

        float delay = Random.Range(_minDelay, _maxDelay);
        StartCoroutine(RemoveCubeOvertime(cube, delay));
    }

    private IEnumerator RemoveCubeOvertime(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        cube.OnPlatformCollisionEnter -= CubeActionOnHit;
        _cubePool.Release(cube);
    }
}
