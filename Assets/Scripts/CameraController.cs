using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimitX = new Vector2(-50, 50);
    public Vector2 panLimitZ = new Vector2(-50, 50);

    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;

    public float rotationSpeed = 100f;

    void Update()
    {
        Vector3 pos = transform.position;

        // Paniranje mišem po rubovima ekrana
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos += transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos -= transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos += transform.right * panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos -= transform.right * panSpeed * Time.deltaTime;
        }

        // Zoom s kotačićem miša
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100 * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Rotacija kamere lijevim i desnim tipkama miša
        if (Input.GetMouseButton(1))  // Desni klik miša držanjem rotiraj kameru
        {
            float rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation, Space.World);
        }

        // Ograniči poziciju kamere unutar područja
        pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
        pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);

        transform.position = pos;
    }
}
