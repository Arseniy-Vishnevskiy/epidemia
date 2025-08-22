using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;               // ����� ��� ������, �� ������� ������
    public Transform LevCollider;
    public float smoothSpeed = 5f;         // �������� �����������
    public Vector2 minBounds;          // ����� ������ ������� ������
    public Vector2 maxBounds;              // ������ ������� ������� ������

    private float camHalfWidth;
    private float camHalfHeight;
    private Camera cam;

    void Start()
    {
        //Bounds lvBounds = LevCollider.GetComponent<BoxCollider>().bounds;
        //minBounds = new Vector2(lvBounds.min.x, lvBounds.min.y);
        //maxBounds = new Vector2(lvBounds.max.x, lvBounds.max.y);
        cam = Camera.main;
        camHalfHeight = cam.orthographicSize/2;
        camHalfWidth = cam.aspect * camHalfHeight/2;
    }

    void LateUpdate()
    {
        if (!target) return;

        // ������� ������� ������
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // �����������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // ����������� � �������� ������
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x  + camHalfWidth, maxBounds.x  - camHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
}
