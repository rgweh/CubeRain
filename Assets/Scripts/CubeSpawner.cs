using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeSpawnPoint;

    private Color _cubeColor = Color.white;
    private float _spawnVariation = 8.5f;

    public GameObject CreateCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Cube>();
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = cube.GetComponent<Cube>().DefaultColor;
        cube.SetActive(false);
        return cube;
    }

    public void SpawnCube(GameObject cube)
    {
        cube.SetActive(true);
        cube.transform.position = new Vector3(GetRandomSpawnCoordinate(_cubeSpawnPoint.x), _cubeSpawnPoint.y, GetRandomSpawnCoordinate(_cubeSpawnPoint.z));
    }

    private float GetRandomSpawnCoordinate(float coordinate)
    {
        return coordinate + Random.Range(-_spawnVariation, _spawnVariation + 1);
    }
}
