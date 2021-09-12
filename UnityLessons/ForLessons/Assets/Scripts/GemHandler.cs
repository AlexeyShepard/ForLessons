using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHandler : MonoBehaviour
{

    public ParticleSystem DestroyParticles;

    public int ScoreCount = 10;

    private GameManager _GameManager;

    private void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyParticles.transform.parent = null;
        DestroyParticles.Play();
        Destroy(gameObject);
        _GameManager.AddScore(ScoreCount);
    }
}

