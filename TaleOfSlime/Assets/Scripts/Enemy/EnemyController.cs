using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IAttackable
{

    [Tooltip("��������� ������")]
    public float ViewingDistance = 10f;
    [Tooltip("��������� �����")]
    public float AttackDistance = 2f;
    [Tooltip("����� �����")]
    public GameObject AttackPoint;
    [Tooltip("������ �����")]
    public float AttackRange = 0.7f;
    [Tooltip("���� � �������")]
    public LayerMask PlayerLayer;
    [Tooltip("���-�� ������ ����� �������")]
    public int AttackCountdownSeconds = 1;
    [Tooltip("���-�� ��������")]
    public int Health = 30;
    [Tooltip("������� ��� ������")]
    public ParticleSystem DamageParticle;

    private bool EnableAttack = true;
    private Transform Target;
    private NavMeshAgent Agent;
    private Animator Animator;
    private GameManager GameManager;

    private float DistanceToPlayer;

    void Start()
    {
        Target = GameManager.ManagerInstance.Player.transform;
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    void FixedUpdate()
    {
        SetAnimation();

        DistanceToPlayer = Vector3.Distance(Target.position, transform.position);       

        if (DistanceToPlayer <= ViewingDistance)
        {
            Agent.SetDestination(Target.position);
            transform.LookAt(Target.position);

            if (DistanceToPlayer <= AttackDistance && EnableAttack) StartCoroutine(AttackCountdown());           
        }

        if (Health <= 0) Death();
    }

    public void DealDamage(int Count)
    {
        Health -= Count;
        DamageParticle.Play();
    }

    private void SetAnimation()
    {
        if (DistanceToPlayer <= AttackDistance && EnableAttack) Animator.SetInteger("Animation", 2);

        else
        {
            if (DistanceToPlayer <= ViewingDistance) Animator.SetInteger("Animation", 1);
            else Animator.SetInteger("Animation", 0);
        }
    }

    private void Attack()
    {       
        Collider[] HitedColliders = Physics.OverlapSphere(AttackPoint.transform.position, AttackRange, PlayerLayer);
        EnableAttack = true;

        foreach (Collider HitedCollider in HitedColliders)
        {           
            GameManager.DamagePlayer(10);
            Debug.Log(GameManager.Health);
        }
    }

    private IEnumerator AttackCountdown()
    {
        EnableAttack = false;

        int Counter = AttackCountdownSeconds;
        while (Counter > 0)
        {
            yield return new WaitForSeconds(1);
            Counter--;
        }

        Attack();

    }

    private void Death()
    {
        DamageParticle.transform.parent = null;
        DamageParticle.Play();

        GameObject.Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ViewingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
