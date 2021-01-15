using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class script_scene_game_start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void game_start_to_rank()
    {
        SceneManager.LoadScene(4);
    }
    public void game_start_to_normal()
    {
        SceneManager.LoadScene(4);
    }
    public void game_start_to_home()
    {
        SceneManager.LoadScene(3);
    }
}
