using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Character_Controller_2D controller;
    public IsGrounded Z_jump;
    private Controls controls;
    public GameObject background;
    public GameObject z_axis;
    private bool is_grounded;
    public float char_speed = 40f;
    public Vector2 movement_input;
    public Camera active_camera;
    private int camera_label;
    //private int active_cam_track;
    public Camera[] cameras;
    private float camera_switch;
    private Vector3 m_Velocity = Vector3.zero;
    private bool jump = false;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        controls.Player.Move.performed += ctx => movement_input = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement_input = Vector2.zero;
        controls.Player.Jump.performed += ctx => Jump();
        controls.Camera.Switch_Cam.performed += ctx => camera_switch = ctx.ReadValue<float>();
        controls.Camera.Switch_Cam.performed += ctx => ChangeCam();
    }

    private void Start()
    {
        foreach (Camera cam in cameras)
        {
            if (cam.isActiveAndEnabled)
            {
                active_camera = cam; 
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(camera_switch);
    }

    private void FixedUpdate()
    {
        switch (active_camera.gameObject.name)
        {
            case "Camera N":
                
                controller.Move(movement_input.x * char_speed * Time.deltaTime, false, jump, false);
                z_axis.transform.position = new Vector3(transform.position.x, transform.position.y, z_axis.transform.position.z);
                jump = false;
                break;

            case "Camera E":
                
                Vector3 target_vel = new Vector3(0, z_axis.GetComponent<Rigidbody>().velocity.y, -movement_input.x * 5f);
                z_axis.GetComponent<Rigidbody>().velocity = Vector3.SmoothDamp(z_axis.GetComponent<Rigidbody>().velocity, target_vel, ref m_Velocity, m_MovementSmoothing);
                transform.position = new Vector3(transform.position.x, z_axis.transform.position.y, z_axis.transform.position.z);

                if (Z_jump.GetGround() && jump)
                {
                    Z_jump.SetGround(false);
                    z_axis.GetComponent<Rigidbody>().AddForce(new Vector2(0f, 250f));
                }
                jump = false;
                break;

            case "Camera W":
                
                Vector3 target_velo = new Vector3(0, z_axis.GetComponent<Rigidbody>().velocity.y, movement_input.x * 5f);
                z_axis.GetComponent<Rigidbody>().velocity = Vector3.SmoothDamp(z_axis.GetComponent<Rigidbody>().velocity, target_velo, ref m_Velocity, m_MovementSmoothing);
                transform.position = new Vector3(transform.position.x, z_axis.transform.position.y, z_axis.transform.position.z);
                if (Z_jump.GetGround() && jump)
                {
                    Z_jump.SetGround(false);
                    z_axis.GetComponent<Rigidbody>().AddForce(new Vector2(0f, 250f));
                }
                jump = false;
                break;

            case "Camera S":
                
                controller.Move(-movement_input.x * char_speed * Time.deltaTime, false, jump, false);
                z_axis.transform.position = new Vector3(transform.position.x, transform.position.y, z_axis.transform.position.z);
                jump = false;
                break;
        }
        
    }

    private void Z_Axis()
    {

        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        z_axis.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    private void X_Axis()
    {

        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        z_axis.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
    }
    private void Jump()
    {
        jump = true;
    }

    private void ChangeCam()
    {
        
        if (camera_switch > 0)
        {
            switch (active_camera.gameObject.name)
            {
                case "Camera N":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[2];
                    active_camera.gameObject.SetActive(true);
                    break;

                case "Camera E":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[0];
                    active_camera.gameObject.SetActive(true);
                    break;

                case "Camera W":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[3];
                    active_camera.gameObject.SetActive(true);
                    break;

                case "Camera S":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[1];
                    active_camera.gameObject.SetActive(true);
                    break;
            }
        }
        else if (camera_switch < 0)
        {
            switch (active_camera.gameObject.name)
            {
                case "Camera N":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[1];
                    active_camera.gameObject.SetActive(true);
                    X_Axis();
                    break;

                case "Camera E":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[3];
                    active_camera.gameObject.SetActive(true);
                    Z_Axis();
                    break;

                case "Camera W":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[0];
                    active_camera.gameObject.SetActive(true);
                    Z_Axis();
                    break;

                case "Camera S":
                    active_camera.gameObject.SetActive(false);
                    active_camera = cameras[2];
                    active_camera.gameObject.SetActive(true);
                    X_Axis();
                    break;
            }
        }

        switch (active_camera.gameObject.name)
        {
            case "Camera N":
                background.transform.GetChild(0).gameObject.SetActive(true);
                background.transform.GetChild(1).gameObject.SetActive(false);
                background.transform.GetChild(2).gameObject.SetActive(false);
                background.transform.GetChild(3).gameObject.SetActive(false);
                X_Axis();
                break;

            case "Camera E":
                background.transform.GetChild(0).gameObject.SetActive(false);
                background.transform.GetChild(1).gameObject.SetActive(true);
                background.transform.GetChild(2).gameObject.SetActive(false);
                background.transform.GetChild(3).gameObject.SetActive(false);
                Z_Axis();
                break;

            case "Camera W":
                background.transform.GetChild(0).gameObject.SetActive(false);
                background.transform.GetChild(1).gameObject.SetActive(false);
                background.transform.GetChild(2).gameObject.SetActive(true);
                background.transform.GetChild(3).gameObject.SetActive(false);
                Z_Axis();
                break;

            case "Camera S":
                background.transform.GetChild(0).gameObject.SetActive(false);
                background.transform.GetChild(1).gameObject.SetActive(false);
                background.transform.GetChild(2).gameObject.SetActive(false);
                background.transform.GetChild(3).gameObject.SetActive(true);
                X_Axis();
                break;
        }
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Camera.Enable();

    }
    public void EnableInput()
    {
        controls.Player.Enable();
        controls.Camera.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Camera.Disable();
    }
    public void DisableInput()
    {
        controls.Player.Disable();
        controls.Camera.Disable();
    }
}
