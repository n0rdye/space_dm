using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Button quit_button;
    private inventory inv;
    private enemy_manager enemy_manager;
    public Text version;
    public Scene curr_scene;

    void Start()
    {
        curr_scene = SceneManager.GetActiveScene();
    }
    // Update is called once per frame
    void Update()
    {
        enemy_manager = GameObject.Find("enemies").GetComponent<enemy_manager>();
        inv = GameObject.Find("charecter").GetComponent<inventory>();
        quit_button.onClick.AddListener(() => quit());
        version.text = Application.version;
    }

    void play()
    {
        SceneManager.LoadScene("dm", LoadSceneMode.Single);
    }

    void quit()
    {
        inv.menu(false);
        inv.save();
        enemy_manager.lvl_var.player_position = inv.lvl_var.player_position;
        enemy_manager.save();
        

        Application.LoadLevel("main_menu");
        //SceneManager.UnloadSceneAsync("dm");
        //SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
    }
}
