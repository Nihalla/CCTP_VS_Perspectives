using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Scaling : MonoBehaviour
{
    private Player_Movement player_script;
    private float distance_from_cam;
    private Vector3 default_scale;
    //private int multiplier_value;
    private int mid_point = 10;
    [SerializeField] private int tiles_per_increment = 5;
    private GameObject active_cam;
    // Start is called before the first frame update
    void Start()
    {
        player_script = FindObjectOfType<Player_Movement>();
        default_scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        distance_from_cam = player_script.Difference_Calc(gameObject);
        Resize();
    }

    private void Resize()
    {
        int multiplier_value = 0;
        //multiplier_value = 1;
        active_cam = player_script.GetCam();
        if(distance_from_cam != mid_point)
        {
            multiplier_value = (int) ((distance_from_cam - mid_point) / tiles_per_increment * -1); 

        }
        //Debug.Log(multiplier_value);
        if (multiplier_value >= 0)
        { transform.localScale = default_scale * (multiplier_value + 1); }
        else
        { transform.localScale = default_scale * (0.5f / Mathf.Abs(multiplier_value)); }
    }
}
