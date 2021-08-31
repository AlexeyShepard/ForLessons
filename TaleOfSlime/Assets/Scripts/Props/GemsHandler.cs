using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsHandler : MonoBehaviour
{

    [Tooltip("������� �����������")]
    public ParticleSystem DestroyParticle;
    [Tooltip("���-�� �����")]
    public int Count;

    private GameManager _GameManager;

    public void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

    public void OnTriggerEnter(Collider other)
    {
        DestroyParticle.transform.parent = null;
        DestroyParticle.Play();
        Destroy(gameObject);
        _GameManager.AddScore(Count);
    }
}
