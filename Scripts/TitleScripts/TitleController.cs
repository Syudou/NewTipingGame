using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void TutorialScene()
    {
        SceneManager.LoadScene("Tutorial"); // Main シーンをロード
    }
    // 「大文字・小文字編」ボタン
    public void LoadUpperLowerCaseScene()
    {
        SceneManager.LoadScene("Main"); // Main シーンをロード
    }

    // 「単語編」ボタン
    public void LoadWordModeScene()
    {
        SceneManager.LoadScene("Main2"); // Main2 シーンをロード
    }
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
