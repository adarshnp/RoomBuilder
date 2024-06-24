using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float distance;
    public float zoomSpeed = 5f;


    private void LateUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float scroll = Input.GetAxis("Mouse ScrollWheel"); distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, 5f, 10f);

        transform.RotateAround(target.position, Vector3.up, horizontal * Time.deltaTime * rotationSpeed);
        transform.RotateAround(target.position, transform.right, vertical * Time.deltaTime * rotationSpeed);
        float angleX = Mathf.Clamp(transform.eulerAngles.x, 20, 80);
        transform.eulerAngles = new Vector3(angleX, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position = target.position - transform.forward * distance;
    }
}
