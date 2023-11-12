using System;
using UnityEngine;
public class MachineElement : MonoBehaviour
{
    Vector3 mousePosition;
    [SerializeField] Transform target;
    [SerializeField] Transform animTargetPos;
    float lerpValue;
    public bool success;
    Vector3 startPos;
    public event Action OnSet;
    [SerializeField] GameObject visualObject;
    bool isSetted;
    
    private void Start()
    {
        visualObject.SetActive(false);
    }
    Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
    Vector3 GetTargetPos()
    {
        return Camera.main.WorldToScreenPoint(target.position);
    }
    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
        PistonParent.Instance.SetElementParent(transform);
        isSetted = false;
        if(success)
        {
            PistonParent.Instance.RemoveElement();
        }
        success = false;
        
    }
    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        if(GetDistanceToTarget()<20)
        {
            if (!visualObject.activeSelf)
                visualObject.SetActive(true);
        }
        else
        {
            if (visualObject.activeSelf)
                visualObject.SetActive(false);
        }

    }
    private void Update()
    {
        if (isSetted)
        {
            lerpValue += Time.deltaTime/2;
            transform.position = LerpedPosition(lerpValue);
            if(lerpValue>1)
            {
                isSetted= false;
                
                success = true;
                lerpValue=0;
            }
        }
    }
    private void LateUpdate()
    {
        visualObject.transform.position = target.position;
    }
    Vector3 LerpedPosition(float t)
    {
        Vector3 a = Vector3.Lerp(startPos, animTargetPos.position, t);
        Vector3 b = Vector3.Lerp(animTargetPos.position, target.position, t);
        return Vector3.Lerp(a, b, t);
    }
    private void OnMouseUp()
    {
        if (GetDistanceToTarget() < 20f)
        {
            if (animTargetPos == null)
                animTargetPos = target;
            transform.parent = target;
            startPos = transform.position;
            isSetted = true;
            OnSet?.Invoke();
            visualObject.SetActive(false);
            PistonParent.Instance.AddElement();
        }
    }
    float GetDistanceToTarget()
    {
        return Vector3.Distance(GetMousePos(), GetTargetPos());
    }
}