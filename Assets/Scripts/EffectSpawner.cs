using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private bool spawnOnAwake = true;

    private void Awake()
    {
        if (spawnOnAwake)
            Spawn();
    }

    public void Spawn()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
