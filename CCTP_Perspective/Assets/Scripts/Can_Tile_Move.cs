using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_Tile_Move : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {

        GetComponentInParent<Movement_Options>().can_be_moved = false;
        Debug.Log(GetComponentInParent<Movement_Options>().can_be_moved);

    }

    private void OnTriggerExit(Collider other)
    {

        GetComponentInParent<Movement_Options>().can_be_moved = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GetComponentInParent<Movement_Options>().can_be_moved = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        GetComponentInParent<Movement_Options>().can_be_moved = true;

    }
}
