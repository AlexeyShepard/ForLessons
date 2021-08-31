using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHandler : MonoBehaviour, IAttackable
{
    [Tooltip("Кол-во здоровья")]
    public int Health = 30;
    [Tooltip("Частицы нанесения урона")]
    public ParticleSystem DamageParticle;
    [Tooltip("Спаунпонт гемов")]
    public GameObject[] GemSpawnPoints;
    [Tooltip("Награда при уничтожении")]
    public GameObject Gem;

    void FixedUpdate()
    {
        if (Health <= 0) DestroyCrystal();     
    }

    public void DealDamage(int Count)
    {
        Health -= Count;
        DamageParticle.Play();
    }

    public void DestroyCrystal()
    {
        DamageParticle.transform.parent = null;
        DamageParticle.Play();

        foreach(GameObject GemSpawnPoint in GemSpawnPoints)
        {
            GemSpawnPoint.transform.parent = null;
            Instantiate(Gem, GemSpawnPoint.transform);
        }

        Destroy(gameObject);
    }
}
