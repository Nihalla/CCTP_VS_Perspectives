using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColission : MonoBehaviour
{
    private GameObject camera_view;
    private Material base_mat_1;
    private Material base_mat_2;
    [SerializeField] private Material inactive_mat;
    private MeshRenderer mesh_renderer;
    private Material[] materials_inactive;
    private Material[] materials_active;
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
        mesh_renderer = GetComponent<MeshRenderer>();
        if (mesh_renderer.enabled)
        {
            materials_active = mesh_renderer.materials;

            materials_inactive = mesh_renderer.materials;
            materials_inactive[0] = inactive_mat;
            materials_inactive[1] = inactive_mat;
        }
        
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
                    mesh_renderer.materials = materials_inactive;

                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mesh_renderer.materials = materials_active;
                }

                break;

            case Perspectives.EAST:
                if (camera_view.gameObject.name == "Camera E")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    mesh_renderer.materials = materials_inactive;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mesh_renderer.materials = materials_active;
                }

                break;

            case Perspectives.WEST:
                if (camera_view.gameObject.name == "Camera W")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    mesh_renderer.materials = materials_inactive;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mesh_renderer.materials = materials_active;
                }

                break;

            case Perspectives.SOUTH:
                if (camera_view.gameObject.name == "Camera S")
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    mesh_renderer.materials = materials_inactive;

                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mesh_renderer.materials = materials_active;
                }

                break;

            case Perspectives.NONE:
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
        }
    }
}
