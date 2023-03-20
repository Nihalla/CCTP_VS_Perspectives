using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    [SerializeField] private int current_level;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (current_level != 3)
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
