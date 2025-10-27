using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f);
    [SerializeField] private float smoothSpeed = 0.125f;
    
    void Start()
    {
        // Автоматически находим игрока, если не назначен вручную
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        if (player == null)
        {
            Debug.LogError("Player not found! Assign player manually or tag it as 'Player'");
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Целевая позиция камеры (позади и выше игрока)
            Vector3 desiredPosition = player.position + offset;
            
            // Плавное перемещение камеры
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            
            // Камера всегда смотрит на игрока
            transform.LookAt(player);
        }
    }
}