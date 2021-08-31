using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public LayerMask PlayerLayer;

    private GameManager GameManager;

    public void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") SceneManager.LoadScene(0);
    }
}
