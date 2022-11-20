using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private GameObject particleEffect;


    public void ParticleInstance(Vector3 pos)
    {
        float randomIndex = Random.Range(0, 360);
        GameObject g = Instantiate(particleEffect, pos, Quaternion.Euler(0, 0, randomIndex));
        Destroy(g, 0.33f);
    }
}
