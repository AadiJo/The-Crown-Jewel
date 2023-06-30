using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public GameObject bg;
    [Range(1, 10)]
    public float smoothFactor;
    private Camera camera;
    private void Start()
    {

        camera = gameObject.GetComponent<Camera>();
        camera.backgroundColor = Color.black;
        bg.SetActive(true);

    }
    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;

    }

}