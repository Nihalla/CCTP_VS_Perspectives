using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Options : MonoBehaviour
{
    // Up Down Left Right
    [SerializeField] List<int> movement_restrictions = new();
    private int up = 0;
    private int down = 0;
    private int left = 0;
    private int right = 0;

    public bool can_be_moved = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUp()
    {
        if (up < movement_restrictions[0])
        {
            up++;
            down--;
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
    }

    public void MoveDown()
    {
        if (down < movement_restrictions[1])
        {
            up--;
            down++;
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
    }

    public void MoveLeft()
    {
        if (left < movement_restrictions[2])
        {
            left++;
            right--;
            gameObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
    }

    public void MoveRight()
    {
        if (right < movement_restrictions[3])
        {
            right++;
            left--;
            gameObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
    }


}
