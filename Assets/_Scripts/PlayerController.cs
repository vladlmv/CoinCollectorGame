using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private Rigidbody rb;
    private Vector3 movement;
    private Camera mainCamera;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("Animator not found on Player!");
        }
    }

    void Update()
    {
        // Получаем ввод от игрока
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Создаем вектор движения
        movement = new Vector3(horizontal, 0f, vertical);
        
        // Управление анимациями
        HandleAnimations();
    }

    void FixedUpdate()
    {
        MovePlayer();
        
        if (movement.magnitude >= 0.1f)
        {
            RotatePlayer();
        }
    }

    void MovePlayer()
    {
        if (movement.magnitude >= 0.1f)
        {
            Vector3 moveDirection = GetCameraRelativeMovement(movement);
            Vector3 newVelocity = moveDirection * moveSpeed;
            newVelocity.y = rb.linearVelocity.y;
            rb.linearVelocity = newVelocity;
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }

    void RotatePlayer()
    {
        Vector3 moveDirection = GetCameraRelativeMovement(movement);
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    void HandleAnimations()
    {
        if (animator != null)
        {
            // Определяем, движется ли персонаж
            bool isMoving = movement.magnitude > 0.1f;
            
            animator.SetBool("IsMoving", isMoving);
        }
    }

    Vector3 GetCameraRelativeMovement(Vector3 inputMovement)
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        
        Vector3 cameraRight = mainCamera.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();
        
        return (cameraForward * inputMovement.z + cameraRight * inputMovement.x).normalized;
    }
}