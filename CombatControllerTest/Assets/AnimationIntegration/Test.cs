using System;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private float speed;

    private Animator animator;

    private float horizontal;
    private float vertical;

    private float velocityZ;
    private float velocityX;

    private Vector3 movement;
    private Ray ray;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MovementController();

        AimTowards();

    }

    private void AimTowards()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            Vector3 direction = hitInfo.point - transform.position;
            direction.y = 0;

            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;
            direction.Normalize();

            Debug.DrawRay(Camera.main.transform.position, direction, Color.green);

            transform.forward = direction;

        }
    }
    private void MovementController()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0f, vertical);

        if(movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            velocityZ = Vector3.Dot(movement.normalized, transform.forward);
            velocityX = Vector3.Dot(movement.normalized, transform.right);

            animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        }

        else
        {
            animator.SetFloat("VelocityZ", horizontal, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityX", vertical, 0.1f, Time.deltaTime);
        }
    }
}
