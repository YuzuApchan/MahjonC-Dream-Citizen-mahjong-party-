using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class script_scene_home : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void home_to_game()
    {
        SceneManager.LoadScene(2);
    }
    public void home_to_setting()
    {
        SceneManager.LoadScene(6);
    }
    public void home_to_info()
    {
        SceneManager.LoadScene(7);
    }
    public void home_to_login()
    {
        SceneManager.LoadScene(0);
    }
}
