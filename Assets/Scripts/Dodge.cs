using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Dodge : MonoBehaviour
{
    [SerializeField] private Transform[] trackDodge;
    private int Index = 1;

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            if(Index > 0)
            {
                Index--;
                transform.position = trackDodge[Index].position;
            }
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            if(Index < trackDodge.Length - 1)
            {
                Index++;
                transform.position = trackDodge[Index].position;
            }
        }
        
    }
}
