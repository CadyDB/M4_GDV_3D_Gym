using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset input;
    [SerializeField] private string actionMapName = "Player1";

    private InputActionMap map;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private float speed = 5f;

    private Rigidbody rb;

    void Awake()
    {
        map = input.FindActionMap(actionMapName);
        moveAction = map.FindAction("Move");
        jumpAction = map.FindAction("Jump");
        sprintAction = map.FindAction("Sprint");
    }

    void OnEnable()
    {
        map.Enable();
    }

    void OnDisable()
    {
        map.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            Debug.Log("Jump pressed");
        }
        else if (jumpAction.IsPressed())
        {
            Debug.Log("Jump Held");
        }
        else if (jumpAction.WasReleasedThisFrame())
        {
            Debug.Log("Jump Released");
            rb.AddForce(Vector3.up * 300f, ForceMode.Force);
        }

       

        if (sprintAction.IsPressed())
        {
            Debug.Log("Sprint Held");
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }


        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(moveInput.y * transform.forward * Time.deltaTime * speed, Space.World);
        transform.Rotate(Vector3.up, moveInput.x * Time.deltaTime * 100f, Space.World);
    }
}
