using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Controller : MonoBehaviour
{
    private Controls controls;
    [Range(1, 6)] [SerializeField] private int current_slice;
    [SerializeField] private GameObject[] slices;
    private float camera_switch;
    [SerializeField] private bool cam_freeze = false;
    private Rigidbody2D player;

    void Awake()
    {
        controls = new Controls();

        controls.Camera.Switch_Cam.performed += ctx => camera_switch = ctx.ReadValue<float>();
        controls.Camera.Switch_Cam.performed += ctx => ChangeCam();
        controls.Player.Freeze.performed += ctx => CharacterFreeze();
        current_slice = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CharacterFreeze()
    {
        cam_freeze = !cam_freeze;
        if (cam_freeze)
        {
            player.isKinematic = true;
            player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            player.isKinematic = false;
            player.constraints = RigidbodyConstraints2D.None;
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }    
    }
    private void ChangeCam()
    {

        if (cam_freeze)
        {
            if (camera_switch > 0 && current_slice < 6)
            {
                slices[current_slice - 1].SetActive(false);
                current_slice++;
                slices[current_slice - 1].SetActive(true);
            }
            else if (camera_switch < 0 && current_slice > 1)
            {
                slices[current_slice - 1].SetActive(false);
                current_slice--;
                slices[current_slice - 1].SetActive(true);
            }

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
