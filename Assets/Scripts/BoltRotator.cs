using UnityEngine;
public class BoltRotator : MonoBehaviour
{
    MachineElement machineElement;
    bool rotate;
    [SerializeField] float rotateSpeed;
    float lerpValue;

    void Start()
    {
        machineElement = GetComponent<MachineElement>();
        machineElement.OnSet += MachineElement_OnSet;
    }
    private void MachineElement_OnSet()
    {
        rotate = true;
    }
    void Update()
    {
        if(rotate)
        {
            lerpValue += Time.deltaTime/2;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y, 
                transform.localEulerAngles.z - Time.deltaTime * rotateSpeed); 
            if(lerpValue>1)
            {
                rotate = false;
                lerpValue= 0;
            }
        }
    }
}