using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeSpawnPoint;
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubePool;
    private float _spawnVariation = 8.5f;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => PullCube(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false)
        );
    }

    public Cube GetSpawnedCube()
    {
        return _cubePool.Get();
    }

    public void RemoveCube(Cube cube)
    {
        _cubePool.Release(cube);
    }

    private Cube CreateCube()
    {
        var cube = Instantiate(_cubePrefab);
  
        cube.gameObject.SetActive(false);

        return cube;
    }

    private void PullCube(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = new Vector3(GetRandomSpawnCoordinate(_cubeSpawnPoint.x), _cubeSpawnPoint.y, GetRandomSpawnCoordinate(_cubeSpawnPoint.z));
    }

    private float GetRandomSpawnCoordinate(float coordinate)
    {
        return coordinate + Random.Range(-_spawnVariation, _spawnVariation + 1);
    }
}
