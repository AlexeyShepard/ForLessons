using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Скорость персонажа")]
    public float MovementSpeed = 10f;
    [Tooltip("Скорость персонажа")]
    public float JumpForce = 2f;
    [Tooltip("Минимальная дистанция нахождения игрока на земле")]
    public float DistationToGround = 0.1f;

    private Rigidbody _RigidBody;
    private bool IsGrounded;

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();   
    }

    void FixedUpdate()
    {
        GroundCheck();

        if (Input.GetKey(KeyCode.Space) && IsGrounded) Jump();

        _RigidBody.MovePosition(CalculateMovement());

        SetRotation();
    }

    private Vector3 CalculateMovement()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");
        float VerticalDirection = Input.GetAxis("Vertical");

        return _RigidBody.transform.position + new Vector3(HorizontalDirection, 0, VerticalDirection) * Time.fixedDeltaTime * MovementSpeed;
    }

    void SetRotation()
    {      		
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, MovementSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        _RigidBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, DistationToGround);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * DistationToGround));
    }

    
}
