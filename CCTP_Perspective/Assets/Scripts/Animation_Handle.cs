using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Handle : MonoBehaviour
{
    public Animator anim;

    public int move_velocity;
    private float player_input = 0;
    private Player_Movement player_script;
    private Vector3 default_scale;

    // Start is called before the first frame update
    void Start()
    {
        move_velocity = Animator.StringToHash("Velocity");
        anim = GetComponent<Animator>();
        player_script = FindObjectOfType<Player_Movement>();
        default_scale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        player_input = player_script.movement_input.x;
        if (!player_script.on_main_axis)
        {
            if (player_input < 0)
            {
                gameObject.transform.localScale = new Vector3(Mathf.Abs(default_scale.x), default_scale.y, default_scale.z);
            }
            else if( player_input > 0)
            {
                gameObject.transform.localScale = new Vector3(Mathf.Abs(default_scale.x), default_scale.y, default_scale.z);
            }
        }
        else
        {
            gameObject.transform.localScale = default_scale;
        }
        
        anim?.SetFloat(move_velocity, Mathf.Abs(player_input));
    }
}
