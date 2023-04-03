using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{

    PlayerManager PM;
    [SerializeField] float gravity;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundCheck;

    [SerializeField] Transform orientation;
    [SerializeField] Transform cam;
    public float sensitivity = 15f;

    Rigidbody rb;

    [SerializeField] float airAcceleration = 0.75f;
    public static float accelerationMultiplier = 1f;
    [SerializeField] float groundAcceleration = 8f;
    [SerializeField] float maxAcceleration = 10f;

    [SerializeField] float stopSpeed = 0.5f;
    [SerializeField] float friction = 15f;

    bool isGrounded;
    bool jumping;
    float jumpBuffer;
    [SerializeField] float jumpBufferTime = 0.5f;
    [SerializeField] float jumpForce = 8f;

    public bool isInputDisabled = false;
    
    void Awake() {
        PM = transform.parent.GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        maxAcceleration *= groundAcceleration*accelerationMultiplier;
    }

    void OnGUI() {
        // debug
        if (!PM.DEBUG_MODE) return;

        Vector3 velocity = rb.velocity; 
        Vector2 speedHorizontal = new Vector2(velocity.x, velocity.z);
        List<string> labels = new List<string> {
            "<color=silver>fps: </color>" + Mathf.Round(1.0f / Time.unscaledDeltaTime),
            "<color=green>position: </color>" + transform.position.ToString(),
            "<color=green>velocity: </color>" + velocity.ToString(),
            "<color=yellow>speed: </color>" + velocity.magnitude,
            "<color=yellow>spdHori: </color>" + speedHorizontal.magnitude,
            "<color=yellow>spdVert: </color>" + Mathf.Abs(velocity.y),
            "<color=lime>friction: </color>" + friction,
            "<color=orange>jumping: </color>" + jumping,
            "<color=orange>jump buffer: </color>" + jumpBuffer,
            "<color=red>isGrounded: </color>" + isGrounded,
        };

        string debug = "";
        foreach (string l in labels) {
            debug += l + "\n";
        }

        GUI.Label(new Rect(0,0,200,500), debug);

    }


    void Update() {
        if (isInputDisabled) return;
        Look();

        // ❤️
        // https://github.com/ValveSoftware/source-sdk-2013/blob/master/sp/src/game/shared/gamemovement.cpp
        // https://github.com/id-Software/Quake/blob/master/QW/client/pmove.c
        Vector3 _moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        Vector3 wishDir = orientation.TransformDirection(_moveInput);
        wishDir = Vector3.ProjectOnPlane(wishDir, Vector3.down).normalized;

        Vector3 velocity = rb.velocity;
        isGrounded = IsGrounded();

        // Jumping && jump buffering
        jumpBuffer -= Time.deltaTime;
        if (jumpBuffer < 0) { jumpBuffer = 0; }

        // Auto bhop
        jumping = PM.AUTO_BHOP ? Input.GetKey(KeyCode.Space) : Input.GetKeyDown(KeyCode.Space);
        if (jumping) {
            jumpBuffer = jumpBufferTime;
        }
        velocity = Jump(velocity);

        // Change acceleration method
        velocity = isGrounded ?
            UpdateVelocityGround(wishDir, velocity, Time.deltaTime) :
            UpdateVelocityAir(wishDir, velocity, Time.deltaTime);

        // Gravity
        velocity = ApplyGravity(velocity, Time.deltaTime);
        
        // Apply every modifier
        rb.velocity = velocity;
    }

    Vector3 UpdateVelocityGround(Vector3 wishDir, Vector3 velocity, float dt) {
        velocity = ApplyFriction(velocity, dt);

        float currentSpeed = Vector3.Dot(velocity, wishDir);
        float addSpeed = Mathf.Clamp((groundAcceleration*accelerationMultiplier) - currentSpeed, 0, maxAcceleration * dt);

        return velocity + addSpeed * wishDir;
    }

    Vector3 UpdateVelocityAir(Vector3 wishDir, Vector3 velocity, float dt) {
        float currentSpeed = Vector3.Dot(velocity, wishDir);
        float addSpeed = Mathf.Clamp((airAcceleration*accelerationMultiplier) - currentSpeed, 0, maxAcceleration * dt);

        return velocity + addSpeed * wishDir;
    }

    Vector3 ApplyFriction(Vector3 velocity, float dt) {
        // https://github.com/ValveSoftware/source-sdk-2013/blob/master/sp/src/game/shared/gamemovement.cpp#L1612
        float speed = velocity.magnitude;
        if (speed <= 0.01f) {
            return Vector3.zero;
        }

        // Slow down player if velocity is small enough
        float control = Mathf.Max(speed, stopSpeed);
        float drop = speed-(control * friction * dt);
        if (drop < 0) drop = 0;

        velocity *= drop / speed; 
        return velocity;
    }

    Vector3 Jump(Vector3 velocity) {
        if (isGrounded && (jumpBuffer > 0)) {
            jumpBuffer = 0;
            velocity.y = jumpForce;
        }

        return velocity;
    }

    Vector3 ApplyGravity(Vector3 velocity, float dt) {
        if (!isGrounded) {
            velocity += Vector3.down * gravity * dt;
        }
        return velocity;
    }

    void Look() {
        if (isInputDisabled) return;
        // Gather mouse input
        Vector2 mouse = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        // Camera stutter when not multiplying by fixedDeltaTime for some reason 
        mouse *= sensitivity * Time.fixedDeltaTime;

        // Calculate rotation
        Vector3 rot = cam.transform.localRotation.eulerAngles;

        Vector2 desired = new Vector2(            
            rot.x - mouse.y,
            rot.y + mouse.x
        );

        // Clamp rotation
        if (desired.x < 270f && desired.x > 180f) desired.x = 270f;
        if (desired.x > 90f && desired.x < 180f) desired.x = 90f;

        // Rotate
        cam.transform.localRotation = Quaternion.Euler(desired.x, desired.y, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desired.y, 0);
    }

    bool IsGrounded() {
        bool _gc = Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, 0.25f, groundLayers);
        // might have to also out hit.normal if sliding is added
        return _gc;
    }
}
