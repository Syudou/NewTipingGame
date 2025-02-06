using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class VirtualKeyboardTutorial : MonoBehaviour
{
    public Dictionary<char, Image> keyMapping;
    public Color defaultColor = Color.white;
    

    void Start()
    {
        keyMapping = new Dictionary<char, Image>();

        foreach (Transform child in transform)
        {
            Image keyImage = child.GetComponent<Image>(); //  �e�L�[�� Image ���擾
            if (keyImage != null)
            {
                string keyName = child.gameObject.name;
                if (keyName.Length > 0)
                {
                    char keyChar = keyName[0]; //  "AKey" �̍ŏ��̕��� 'A' ���擾

                    if (!keyMapping.ContainsKey(keyChar))
                    {
                        keyMapping.Add(keyChar, keyImage);
                    }

                    char lowerKey = char.ToLower(keyChar); //  ���������o�^
                    if (!keyMapping.ContainsKey(lowerKey))
                    {
                        keyMapping.Add(lowerKey, keyImage);
                    }
                }
            }
        }

    }

    //  �^�C�s���O�Ώۂ̕����ɉ����ăL�[�����点��
    public void HighlightKeyWithColor(char key, Color highlightColor)
    {
        ResetKeyColors();

        if (keyMapping.ContainsKey(key))
        {
            keyMapping[key].color = highlightColor; // �L�[�̐F���w�̐F�ɐݒ�
        }
    }

    public void ResetKeyColors()
    {
        foreach (var key in keyMapping.Values)
        {
            key.color = defaultColor;
        }
    }
}
