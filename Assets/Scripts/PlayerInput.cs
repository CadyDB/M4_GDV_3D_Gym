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
    private float moveSpeed = 5f;
    
    private Animator animator;

    private CharacterController cc;

    void Awake()
    {
        map = input.FindActionMap(actionMapName);
        moveAction = map.FindAction("Move");
        jumpAction = map.FindAction("Jump");
        sprintAction = map.FindAction("Sprint");

        animator = GetComponent<Animator>();

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
        cc = GetComponent<CharacterController>();
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
            //rb.AddForce(Vector3.up * 300f, ForceMode.Force);
            animator.SetTrigger("Jump");
        }

       
       if (sprintAction.IsPressed())
         {
            Debug.Log("Sprint Held");
            moveSpeed = 10f;
         }
      else
         {
           moveSpeed = 5f;
         }


        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        float currentMoveSpeed = moveInput.y * moveSpeed * Time.deltaTime;

        cc.Move(transform.forward * currentMoveSpeed);


        //transform.Translate(transform.forward * currentMoveSpeed, Space.World);
        transform.Rotate(Vector3.up, moveInput.x * Time.deltaTime * 100f, Space.World);

        animator.SetFloat("Speed", currentMoveSpeed);
       
    }
}
