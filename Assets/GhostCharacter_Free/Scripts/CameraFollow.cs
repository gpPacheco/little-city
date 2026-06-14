using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 6f;
    public float height = 3f;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Posição desejada atrás e acima do personagem
        Vector3 desiredPosition = target.position 
            - target.forward * distance 
            + Vector3.up * height;

        // Suaviza o movimento
        transform.position = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed * Time.deltaTime
        );

        // Sempre olha para o personagem
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}