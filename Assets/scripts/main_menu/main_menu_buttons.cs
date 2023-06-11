using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_menu_buttons : MonoBehaviour
{
    public Button play_button;
    public Button quit_button;
    public Text version;

    public AsyncOperation loading;

    public GameObject loadingPanel;
    public Text loadingText;

    public string load_lvl;

    void Start()
    {
        Cursor.visible = true;
        Application.targetFrameRate = 59;

    }
    // Update is called once per frame
    void Update()
    {
        play_button.onClick.AddListener(() => play());
        quit_button.onClick.AddListener(() => quit());
        version.text = Application.version;
    }

    void play()
    {
        //Application.LoadLevel("dm");
        if (loading == null)
        {
            LoadLevel(load_lvl);

        }
        //SceneManager.LoadScene("dm", LoadSceneMode.Additive);
    }

    void quit()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync(string levelName)
    {
        loading = Application.LoadLevelAsync(levelName);

        while (!loading.isDone)
        {
            float prog = Mathf.Clamp01(loading.progress / .9f);

            loadingText.text = prog * 100f + "%";
            //Debug.Log(prog);

            yield return null;
        }



    }
}
