using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float destroy_timer = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroy_timer);
    }

}
