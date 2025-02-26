
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject boat;
    public static PlayerMovement instance;
    public bool OnBoat = true;
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float SwimSpeedMulti = 1f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    
    CharacterController characterController;
    void Start()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public float TurnSpeed = 3f;
    public float shakeIntensity = 5;
    public Vector3 oldshake;
    int updatessinceshake;
    void FixedUpdate()
    {
        if (shakefor > 0)
        {

            updatessinceshake++;
            if (updatessinceshake % 2 == 0)
            {


                playerCamera.transform.position -= oldshake;
                oldshake = Random.insideUnitSphere * shakeIntensity;
                shakefor -= Time.fixedDeltaTime;
                playerCamera.transform.position += oldshake;
            }
        }
    }
    void Update()
    {
       
        if (AtWheel)
        {
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        if (InWater)
        {
            HandleMouseLook(false);
            Vector3 movedir = playerCamera.transform.forward * Time.deltaTime * curSpeedX;
            movedir += playerCamera.transform.right * Time.deltaTime * curSpeedY;
            if (Input.GetKey(KeyCode.Space))
            {
                movedir.y += curSpeedY * Time.deltaTime;
            }
            characterController.Move(movedir * SwimSpeedMulti);
            return;
        }
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;

        }
        else
        {

            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {

            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (OnBoat && !AtWheel)
        {
            Boat.Delta.y = 0;
            moveDirection += Boat.Delta / Time.deltaTime;
        }
        if (!AtWheel)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }
        else
        {

            characterController.Move(Boat.Delta);
        }
        if (canMove)
        {
            HandleMouseLook();
        }

    }
    public void HandleMouseLook(bool limited = true)
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = limited ? Mathf.Clamp(rotationX, -lookXLimit, lookXLimit) : rotationX;
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
    public bool InWater = false;

    public bool AtWheel { get; set; }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            InWater = true;
        }
    }
     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            InWater = false;
        }
    }
    public float shakefor = 0;
    public static void TriggerScreenShake(int v, bool boatOnly = true)
    {
        Debug.LogWarning("ran");
        if(!boatOnly || instance.OnBoat)
        {
            Debug.LogWarning("ran2");
            instance.shakefor = v;
        }
    }
}
