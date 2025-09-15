using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeSpawnPoint;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _spawnTime;
    [SerializeField] private Color _defaultCubeColor;

    private ObjectPool<Cube> _cubePool;
    private float _spawnVariation = 8.5f;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => TakeCube(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false)
        );
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnCubes());
    }

    public void RemoveCube(Cube cube)
    {
        cube.OnRemoveDelayEnd -= RemoveCube;
        _cubePool.Release(cube);
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (enabled)
        {
            Cube cube = _cubePool.Get();
            cube.OnRemoveDelayEnd += RemoveCube;

            yield return wait;
        }
    }

    private Cube CreateCube()
    {
        var cube = Instantiate(_cubePrefab);
        cube.gameObject.SetActive(false);

        return cube;
    }

    private void TakeCube(Cube cube)
    {
        cube.SetToDefault(_defaultCubeColor, new Vector3(GetRandomSpawnCoordinate(_cubeSpawnPoint.x), _cubeSpawnPoint.y, GetRandomSpawnCoordinate(_cubeSpawnPoint.z)));
    }

    private float GetRandomSpawnCoordinate(float coordinate)
    {
        return coordinate + UnityEngine.Random.Range(-_spawnVariation, _spawnVariation + 1);
    }
}
