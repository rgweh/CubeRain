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

    public event Action<Cube> OnCubeSpawned;

    private void OnEnable()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => TakeCube(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false)
        );

        StartCoroutine(SpawnCubes());
    }

    public IEnumerator RemoveCubeOvertime(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        _cubePool.Release(cube);
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (enabled)
        {
            Cube cube = _cubePool.Get(); ;
            OnCubeSpawned?.Invoke(cube);

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
        cube.gameObject.SetActive(true);
        cube.Rigidbody.angularVelocity = Vector3.zero;
        cube.Rigidbody.linearVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.Renderer.material.color = _defaultCubeColor;
        cube.transform.position = new Vector3(GetRandomSpawnCoordinate(_cubeSpawnPoint.x), _cubeSpawnPoint.y, GetRandomSpawnCoordinate(_cubeSpawnPoint.z));
    }

    private float GetRandomSpawnCoordinate(float coordinate)
    {
        return coordinate + UnityEngine.Random.Range(-_spawnVariation, _spawnVariation + 1);
    }
}
