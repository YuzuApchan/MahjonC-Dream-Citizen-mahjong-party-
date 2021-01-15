using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_scene_login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void login_to_home()
    {
        SceneManager.LoadScene(3);
    }

    public void login_to_register()
    {
        SceneManager.LoadScene(1);
    }
}
