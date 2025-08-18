using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _spawnTime;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private ObjectPool<GameObject> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<GameObject>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => SpawnCube(cube),
        actionOnRelease: (cube) => cube.SetActive(false)
        );

        Platform[] platforms = FindObjectsOfType<Platform>();

        foreach (Platform platform in platforms)
        {
            platform.OnCubeCollisionEnter += CubeActionOnHit;
        }

        StartCoroutine(SpawnCubes());
    }

    private GameObject CreateCube()
    {
        return _cubeSpawner.CreateCube();
    }

    private void SpawnCube(GameObject cube)
    {
        _cubeSpawner.SpawnCube(cube);
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            GameObject cube = _cubePool.Get();
            cube.GetComponent<Cube>().ModifyOnSpawn();

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

        _cubePool.Release(cube.gameObject);
    }
}
