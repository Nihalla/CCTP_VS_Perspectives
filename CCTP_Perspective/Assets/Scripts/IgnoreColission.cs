using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColission : MonoBehaviour
{
    private GameObject camera_view;
    private enum Perspectives
    {
        NORTH,
        EAST,
        WEST,
        SOUTH,
        NONE   
    };
    [SerializeField] private Perspectives ignore;

    private void Start()
    {
        camera_view = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().GetCamLoc();
    }
    public void UpdateCollissions(GameObject new_view)
    {
        //Debug.Log(new_view);
        camera_view = new_view;
        switch (ignore)
        {
            case Perspectives.NORTH:
                if (camera_view.gameObject.name == "Camera N")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }

                break;

            case Perspectives.EAST:
                if (camera_view.gameObject.name == "Camera E")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }

                break;

            case Perspectives.WEST:
                if (camera_view.gameObject.name == "Camera W")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }

                break;

            case Perspectives.SOUTH:
                if (camera_view.gameObject.name == "Camera S")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }

                break;

            case Perspectives.NONE:
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
        }
    }
}
