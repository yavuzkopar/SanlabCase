using UnityEngine;
public class PistonRotator : MonoBehaviour
{
    float cameraRotationX;
    float cameraRotationY;
    void Start()
    {
        cameraRotationX= transform.localEulerAngles.x;
        cameraRotationY= transform.localEulerAngles.y;
    }
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            cameraRotationX += Input.GetAxis("Mouse Y") * Time.deltaTime * 50;
            cameraRotationY += Input.GetAxis("Mouse X") * -1 * Time.deltaTime * 50;
            transform.rotation = Quaternion.Euler(cameraRotationX,cameraRotationY, 0);
        }
    }
}