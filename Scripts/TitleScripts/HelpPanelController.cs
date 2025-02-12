using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelController : MonoBehaviour
{
    public GameObject helpPanel; // 説明パネル
    public TextMeshProUGUI helpText; // 説明テキスト
    public Button openHelpButton; // 説明を開くボタン
    public Button closeHelpButton; // 説明を閉じるボタン

    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false); // 最初はパネルを非表示

        openHelpButton.onClick.AddListener(OpenHelpPanel);
        closeHelpButton.onClick.AddListener(CloseHelpPanel);
    }

    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true); // パネルを表示
    }

    public void CloseHelpPanel()
    {
        helpPanel.SetActive(false); // パネルを非表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
