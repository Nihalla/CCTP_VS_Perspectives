using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Manager : MonoBehaviour
{
    //private GameObject player;
    private GameObject[] blocks;
    private List<GameObject> solid_blocks = new();
    //[SerializeField] private LayerMask layer;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        blocks = FindObjectsOfType<GameObject>();
        for(int i = 0; i < blocks.Length; i++)
        {
            
            if(blocks[i].layer == 6)
            {
                //Debug.Log(blocks[i].layer);
                solid_blocks.Add(blocks[i]);
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
