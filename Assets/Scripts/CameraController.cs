using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] BoxCollider2D cameraBounds;

    private PlayerController player;
    private float halfHeight, halfWidth;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp( player.transform.position.x, cameraBounds.bounds.min.x + halfWidth, cameraBounds.bounds.max.x - halfWidth),
            Mathf.Clamp(player.transform.position.y, cameraBounds.bounds.min.y + halfHeight, cameraBounds.bounds.max.y - halfHeight), 
            transform.position.z);
    }
}
