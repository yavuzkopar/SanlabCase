using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PistonParent : MonoBehaviour
{
    [SerializeField] Transform parentObject;
    public static PistonParent Instance { get; private set; }
    [SerializeField] MachineElement[] machineElements;
    int machineCount = 0;
    bool isFinished;
    [SerializeField] UnityEvent SuccessEvent;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        machineElements = GetComponentsInChildren<MachineElement>();
        machineCount = 0;
    }
    private void Update()
    {
        if(!isFinished)
        {
            if(machineCount == machineElements.Length)
            {
                SuccessEvent?.Invoke();
                isFinished = true;
            }
            
        }
    }
    public void AddElement()
    {
        machineCount++;
    }
    public void RemoveElement()
    {
        machineCount--;
        if(machineCount<0)
        {
            machineCount = 0;
        }
    }
    public void SetElementParent(Transform element)
    {
        element.parent = parentObject;
    }
}
