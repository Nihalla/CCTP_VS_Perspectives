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
    private bool in_wall = false;
    [System.NonSerialized] public bool after_teleport = false;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Material on;
    [SerializeField] private Material off;

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
        if (cam_freeze)
        {

            RaycastHit hit;
            
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3 (player.transform.position.x, player.transform.position.y, slices[current_slice-1].transform.position.z));
            Debug.DrawRay(Camera.main.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, slices[current_slice - 1].transform.position.z) - Camera.main.transform.position, Color.red);
            if (Physics.Raycast(Camera.main.transform.position, (new Vector3(player.transform.position.x, player.transform.position.y, slices[current_slice - 1].transform.position.z) - Camera.main.transform.position), out hit))
            {
                Transform objectHit = hit.transform;
                //Debug.Log(objectHit.gameObject.name);
                //Debug.Log(objectHit.gameObject.layer);
                if (objectHit.gameObject.layer == 7)
                {
                    indicator.GetComponent<Renderer>().material = off;
                    in_wall = true;
                }   
            }
            else
            {
                indicator.GetComponent<Renderer>().material = on;
                in_wall = false;
            }


        }
    }

    private void CharacterFreeze()
    {
        if (!in_wall)
        {
            cam_freeze = !cam_freeze;
            if (cam_freeze)
            {
                player.isKinematic = true;
                player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                indicator.SetActive(true);
            }
            else
            {
                player.isKinematic = false;
                player.constraints = RigidbodyConstraints2D.None;
                player.constraints = RigidbodyConstraints2D.FreezeRotation;
                indicator.SetActive(false);
            }
        }
    }
    private void ChangeCam()
    {

        if (cam_freeze)
        {
            if (!after_teleport)
            {
                if (camera_switch > 0 && current_slice < 3)
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
            else
            {
                if (camera_switch > 0 && current_slice < 6)
                {
                    slices[current_slice - 1].SetActive(false);
                    current_slice++;
                    slices[current_slice - 1].SetActive(true);
                }
                else if (camera_switch < 0 && current_slice > 4)
                {
                    slices[current_slice - 1].SetActive(false);
                    current_slice--;
                    slices[current_slice - 1].SetActive(true);
                }
            }

        }
    }
    
    public void ManualSliceSet(GameObject slice_to_set)
    {
        slices[current_slice - 1].SetActive(false);
        int i = 0;
        foreach(GameObject slice in slices)
        {
            i++;
            if(slice.name == slice_to_set.name)
            {
                break;
            }
        }
        current_slice = i;
        slices[current_slice - 1].SetActive(true);
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
