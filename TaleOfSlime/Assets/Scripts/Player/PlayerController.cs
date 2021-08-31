using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Минимальная дистанция нахождения игрока на земле")]
    public float DistationToGround = 0.1f;
    [Tooltip("Слой для перемещения игрока")]
    public LayerMask PlayerMovementLayer;
    [Tooltip("Скорость перемещения персонажа")]
    public float MovementBodySpeed = 10f;
    [Tooltip("Скорость поворота персонажа")]
    public float RotationBodySpeed = 180f;
    [Tooltip("Сглаживание поворота персонажа")]
    public float RotationBodySmooth = 0.2f;
    [Tooltip("Сила прыжка")]
    public float JumpForce = 2.5f;
    [Tooltip("Сила атаки")]
    public int Strength = 10;
    [Tooltip("Точка атаки")]
    public GameObject AttackPoint;
    [Tooltip("Радиус атаки")]
    public float AttackRange = 0.7f;
    [Tooltip("Слой с атакуемыми игровыми объектами")]
    public LayerMask AttackableLayer;
    [Tooltip("Кулдайн атак игрока в секунду")]
    public int AttackCountdownSeconds;

    public GameObject Body;
    
    private InputHandler _InputHandler;
    private AnimationManager _AnimationManager;
    private Rigidbody _RigidBody;
    private Vector3 _CurrentMovementDirection;

    private bool IsGrounded = true;
    private bool EnableAttack = true;
    
    private void Start()
    {
        _InputHandler = GetComponent<InputHandler>();
        _RigidBody = GetComponent<Rigidbody>();
        _AnimationManager = GetComponent<AnimationManager>();
    }

    private void FixedUpdate()
    {
        GroundCheck();

        if (_InputHandler.JumpButtonWasClicked() && IsGrounded) Jump();

        if (_InputHandler.AttackButtonWasClicked() && EnableAttack) Attack();

        SetDirection();

        MoveBody();

        RotateBody();

        SetPlayerAnimation();
    }

    private void SetDirection()
    {
        Vector2 JoystickDirection = _InputHandler.GetDirectionMovement();
        _CurrentMovementDirection = Vector3.forward * JoystickDirection.y + Vector3.right * JoystickDirection.x;
    }
    
    private void MoveBody()
    {
        if (IsMoving()) _RigidBody.MovePosition(CalculateMovement());
    }
   
    private bool IsMoving()
    {
        return _CurrentMovementDirection != Vector3.zero;
    }

    private Vector3 CalculateMovement()
    {
        return _RigidBody.transform.position + _CurrentMovementDirection * Time.fixedDeltaTime * MovementBodySpeed;
    }

    private void RotateBody()
    {
        if (IsMoving()) Body.transform.rotation = Quaternion.Lerp(Body.transform.rotation, CalculateBodyRotation(), RotationBodySmooth);
    }

    private Quaternion CalculateBodyRotation()
    {
        return Quaternion.LookRotation(_CurrentMovementDirection * Time.fixedDeltaTime);
    }

    private void SetPlayerAnimation()
    {

        if (IsGrounded)
        {
            if (_InputHandler.AttackButtonWasClicked()) _AnimationManager.SetAnimationAttack();
            else
            {
                if (IsMoving()) _AnimationManager.SetAnimationRun();
                else _AnimationManager.SetAnimationIdle();
            }           
        }
        else _AnimationManager.SetAnimationJump();
    }

    private void GroundCheck()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, DistationToGround, PlayerMovementLayer);
    }

    private void Jump()
    {
        _RigidBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private void Attack()
    {
        
        Collider[] HitedColliders = Physics.OverlapSphere(AttackPoint.transform.position, AttackRange, AttackableLayer);
        
        foreach(Collider HitedCollider in HitedColliders)
        {
            IAttackable Attackable = HitedCollider.gameObject.GetComponent<IAttackable>();
            Attackable.DealDamage(Strength);
            Debug.Log(HitedCollider.name + " нанесено " + Strength + " урона");
        }

        EnableAttack = false;

        StartCoroutine(AttackCountdown());
    }

    private IEnumerator AttackCountdown()
    {
        int Counter = AttackCountdownSeconds;
        while(Counter > 0)
        {
            yield return new WaitForSeconds(1);
            Counter--;
        }

        EnableAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, AttackRange);
    }

}
