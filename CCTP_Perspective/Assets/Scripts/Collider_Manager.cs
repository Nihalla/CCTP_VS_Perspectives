using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Manager : MonoBehaviour
{
    //private GameObject player;
    private GameObject[] blocks;
    private List<GameObject> solid_blocks = new();
    private List<GameObject> fake_2D_blocks = new();
    private GameObject camera_view;
    private GameObject player;
    private bool enable_ZY_2D = false;
    [SerializeField] private Camera main_cam;
    //[SerializeField] private LayerMask layer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        blocks = FindObjectsOfType<GameObject>();
        for(int i = 0; i < blocks.Length; i++)
        {
            if(blocks[i].tag == "ZY_Collider_Extend")
            {
                fake_2D_blocks.Add(blocks[i]);
            }
            
            if(blocks[i].layer == 6)
            {
                //Debug.Log(blocks[i].layer);
                solid_blocks.Add(blocks[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Player_Movement>().in_orthodox)
        {
            camera_view = player.GetComponent<Player_Movement>().GetCamLoc();
            if (camera_view.name == "Camera E" || camera_view.name == "Camera W")
            {
                RaycastHit hit;
                if (Physics.Raycast(main_cam.transform.position, (new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - main_cam.transform.position), out hit))
                {
                    Transform objectHit = hit.transform;
                    //Debug.Log(objectHit.gameObject.name);
                    //Debug.Log(objectHit.gameObject.layer);
                    if (objectHit.gameObject.tag == "Player")
                    {
                        enable_ZY_2D = false;
                    }
                    else
                    {
                        enable_ZY_2D = true;
                    }

                    foreach (GameObject block in fake_2D_blocks)
                    {
                        block.GetComponent<BoxCollider>().enabled = enable_ZY_2D;
                    }
                }
            }
            else
            {
                foreach (GameObject block in fake_2D_blocks)
                {
                    block.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    public void UpdateSolids(GameObject view)
    {

        //Debug.Log(blocks.Co);
        foreach (GameObject block in solid_blocks)
        {
            //Debug.Log(block.name);
            block.GetComponent<IgnoreColission>().UpdateCollissions(view);
        }
    }
    
}
