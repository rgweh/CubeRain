using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action<Cube> OnCubeCollisionEnter;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            if (!cube.HasTouchedPlatform)
            {
                OnCubeCollisionEnter?.Invoke(cube);
            }
             
        }
    }
}

