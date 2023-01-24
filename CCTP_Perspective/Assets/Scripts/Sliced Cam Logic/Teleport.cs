using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private bool entrance = false;
    [SerializeField] private GameObject link;
    private GameObject player;
    private Rotation_Controller manager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Rotation_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<BoxCollider2D>() && entrance)
        {
            Debug.Log("collided");
            manager.ManualSliceSet(link.transform.parent.gameObject);
            player.transform.position = new Vector3(link.transform.position.x, link.transform.position.y, player.transform.position.z);
            manager.after_teleport = true;
        }
    }
}
