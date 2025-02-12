using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelController : MonoBehaviour
{
    public GameObject helpPanel; // �����p�l��
    public TextMeshProUGUI helpText; // �����e�L�X�g
    public Button openHelpButton; // �������J���{�^��
    public Button closeHelpButton; // ���������{�^��

    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false); // �ŏ��̓p�l�����\��

        openHelpButton.onClick.AddListener(OpenHelpPanel);
        closeHelpButton.onClick.AddListener(CloseHelpPanel);
    }

    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true); // �p�l����\��
    }

    public void CloseHelpPanel()
    {
        helpPanel.SetActive(false); // �p�l�����\��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
