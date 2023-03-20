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
    private GameObject active_camera;
    public GameObject[] cam_locations;
    private int camera_label;
    //private int active_cam_track;
    public Camera main_camera;
    private float camera_switch;
    private Vector3 m_Velocity = Vector3.zero;
    private bool jump = false;
    public bool switched = false;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool use_perspective_camera = true;
    public Camera perpesctive_cam;
    public bool in_orthodox = true;
    public Vector2 mouse_pos;
    private GameObject selected_tile;
    [SerializeField] private Canvas canvas;
    private GameObject cosmetics;
    private GameObject visual;
    public bool on_main_axis = true;

    [SerializeField] private bool perspective_change_enabled = true;
    [SerializeField] private bool block_movement_enabled = true;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        controls.Player.Move.performed += ctx => movement_input = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement_input = Vector2.zero;
        controls.Player.Jump.performed += ctx => Jump();
        controls.Camera.Switch_Cam.performed += ctx => camera_switch = ctx.ReadValue<float>();
        controls.Camera.Switch_Cam.performed += ctx => ChangeCam();
        controls.Camera.Perspective_Mode.performed += ctx => ChangePerspective();
        controls.Player.Look.performed += ctx => mouse_pos = ctx.ReadValue<Vector2>();
        controls.Camera.Select.performed += ctx => Select();
        active_camera = cam_locations[0];
        canvas.enabled = false;
        cosmetics = GameObject.FindGameObjectWithTag("Bounds");
    }

    private void Start()
    {
        visual = FindObjectOfType<Animator>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(camera_switch);
        
    }

    private void FixedUpdate()
    {
        if (in_orthodox)
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
        
    }
    
    private void Select()
    {
        if (!in_orthodox)
        {
            GameObject select = FindObjectOfType<Obj_Movement>().GetSelection();
            if (select != null)
            {
                if (select.GetComponent<Movement_Options>().can_be_moved)
                {
                    selected_tile = select;
                    selected_tile.GetComponent<BoxCollider2D>().enabled = false;
                    selected_tile.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
    private void ChangePerspective()
    {
        if (block_movement_enabled)
        {
            in_orthodox = !in_orthodox;
            if (in_orthodox)
            {
                cosmetics.SetActive(true);
                if (selected_tile != null)
                {
                    selected_tile.GetComponent<BoxCollider2D>().enabled = true;
                    selected_tile.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
                    selected_tile = null;
                }
                canvas.enabled = false;
                perpesctive_cam.enabled = false;
                main_camera.enabled = true;
            }
            else
            {
                cosmetics.SetActive(false);
                canvas.enabled = true;
                perpesctive_cam.enabled = true;
                main_camera.enabled = false;
            }
        }
    }
    public GameObject GetCam()
    {
        return active_camera;
    }
    public float Difference_Calc(GameObject object_to_scale)
    {
        float distance_difference = 0;
        if (active_camera.gameObject.name == "Camera N" || active_camera.gameObject.name == "Camera S")
        { 
            distance_difference = Mathf.Abs(object_to_scale.transform.position.z - active_camera.transform.position.z); 
        }
        else
        {
            distance_difference = Mathf.Abs(object_to_scale.transform.position.x - active_camera.transform.position.x);
        }

        return distance_difference;
    }

    private void Z_Axis()
    {
        on_main_axis = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        z_axis.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    private void X_Axis()
    {
        on_main_axis = true;
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
        if (perspective_change_enabled)
        {
            if (use_perspective_camera && in_orthodox && movement_input == Vector2.zero)
            {
                if (camera_switch > 0)
                {
                    switch (active_camera.gameObject.name)
                    {
                        case "Camera N":

                            active_camera = cam_locations[2];
                            

                            break;

                        case "Camera E":

                            active_camera = cam_locations[0];

                            break;

                        case "Camera W":

                            active_camera = cam_locations[3];

                            break;

                        case "Camera S":

                            active_camera = cam_locations[1];

                            break;
                    }
                    main_camera.transform.Rotate(0.0f, -90.0f, 0.0f);
                }
                else if (camera_switch < 0)
                {
                    switch (active_camera.gameObject.name)
                    {
                        case "Camera N":

                            active_camera = cam_locations[1];
                            X_Axis();
                            break;

                        case "Camera E":

                            active_camera = cam_locations[3];

                            Z_Axis();
                            break;

                        case "Camera W":

                            active_camera = cam_locations[0];

                            Z_Axis();
                            break;

                        case "Camera S":

                            active_camera = cam_locations[2];
                            

                            X_Axis();
                            break;
                    }
                    main_camera.transform.Rotate(0.0f, 90.0f, 0.0f);
                }

                main_camera.gameObject.transform.position = active_camera.transform.position;
                visual.transform.localScale = visual.GetComponent<Animation_Handle>().default_scale;
                switch (active_camera.gameObject.name)
                {
                    
                    case "Camera N":
                        visual.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        visual.GetComponent<SpriteRenderer>().flipX = true;
                        
                        X_Axis();
                        break;

                    case "Camera E":
                        visual.transform.rotation = Quaternion.Euler(0f, Mathf.Abs(90f), 0f);
                        Z_Axis();
                        break;

                    case "Camera W":
                        visual.transform.rotation = Quaternion.Euler(0f, Mathf.Abs(90f), 0f);
                        Z_Axis();
                        break;

                    case "Camera S":
                        visual.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        visual.GetComponent<SpriteRenderer>().flipX = false;
                        X_Axis();
                        break;
                }
            }
            
            //Debug.Log(active_camera.name);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<Collider_Manager>().UpdateSolids(active_camera);
        }
    }
    public GameObject GetCamLoc()
    {
        
        return active_camera;
    }

    public void MoveUp()
    {
        if (selected_tile != null)
        {
            selected_tile.GetComponent<Movement_Options>().MoveUp();
        }
    }
    public void MoveDown()
    {
        if (selected_tile != null)
        {
            selected_tile.GetComponent<Movement_Options>().MoveDown();
        }
    }
    public void MoveLeft()
    {
        if (selected_tile != null)
        {
            selected_tile.GetComponent<Movement_Options>().MoveLeft();
        }
    }
    public void MoveRight()
    {
        if (selected_tile != null)
        {
            selected_tile.GetComponent<Movement_Options>().MoveRight();
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
