using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Movement : MonoBehaviour
{
    private Vector2 mouse_pos;
    private Vector3 selection_XY;
    public GameObject thing;
    public GameObject ground;
    private GameObject selected_object;
    [SerializeField] private Camera cam;
    Plane plane;
    [SerializeField] private LayerMask ignore;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(ground.transform.up, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cam.transform.position, new Vector3(selection_XY.x, selection_XY.y, selection_XY.z) - cam.transform.position, Color.red);
    }

    public GameObject GetSelection()
    {
        mouse_pos = FindObjectOfType<Player_Movement>().mouse_pos;
        var ray = cam.ScreenPointToRay(mouse_pos);
        float ent;
        if (plane.Raycast(ray, out ent))
        {

            selection_XY = ray.GetPoint(ent);
            //thing.transform.position = selection_XY;
            
            RaycastHit hit;
            //Debug.DrawRay(cam.transform.position, new Vector3(selection_XY.x, selection_XY.y, selection_XY.z) - cam.transform.position, Color.red);
            if (Physics.Raycast(cam.transform.position, new Vector3(selection_XY.x, selection_XY.y, selection_XY.z) - cam.transform.position, out hit, 1000f, ~ignore))
            {

                Transform objectHit = hit.transform;
                Debug.Log(objectHit.gameObject.name);
                if (objectHit.parent != null)
                {
                    if (objectHit.parent.tag == "Moveable")
                    {
                        return objectHit.parent.gameObject;
                    }
                }
            }

        }
        return null;
    }
}
