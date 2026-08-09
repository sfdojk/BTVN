using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PLayerMove : MonoBehaviour
{
    [SerializeField] private Vector3[] trackDodge;
    [SerializeField] private Vector3 jumpVector;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallPoint;
    private int index;
    private Rigidbody rb;
    private Transform tf;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        index = 1;
        tf = this.GetComponent<Transform>();
        
}

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame && index > 0)
        {
            index--;
            transform.position = trackDodge[index];
        }
        if (Keyboard.current.dKey.wasPressedThisFrame && index < trackDodge.Length - 1)
        {
            index++;
            transform.position = trackDodge[index];
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
        
        }
        if (tf.position.y <= fallPoint && rb.linearVelocity.y < 0)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = trackDodge[index];

        }

    }
}
