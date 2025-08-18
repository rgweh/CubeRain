using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private bool _hasTouchedPlatform = false;
    [SerializeField] private Color _defaultColor = Color.white;

    public Color DefaultColor => _defaultColor;

    public bool HasTouchedPlatform => _hasTouchedPlatform;

    public void ModifyCubeOnHit()
    {
        _hasTouchedPlatform = true;
        this.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public void ModifyOnSpawn()
    {
        _hasTouchedPlatform = false;
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}


