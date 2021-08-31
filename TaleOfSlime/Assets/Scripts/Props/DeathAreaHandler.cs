using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAreaHandler : MonoBehaviour
{
    public LayerMask PlayerLayer;
    
    private GameManager GameManager;
    
    public void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") GameManager.GameOver();
    }
}
