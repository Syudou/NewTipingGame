using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class VirtualKeyboardTutorial : MonoBehaviour
{
    public Dictionary<char, Image> keyMapping;
    public Color defaultColor = Color.white;
    //public Color highlightColor = Color.yellow;

    void Start()
    {
        keyMapping = new Dictionary<char, Image>();

        foreach (Transform keyTransform in transform)
        {
            Image keyImage = keyTransform.GetComponent<Image>();

            if (keyImage != null)
            {
                string keyName = keyTransform.name; // ��F�uAKey�v�uBKey�v
                if (keyName.Length > 0)
                {
                    char keyChar = keyName[0];

                    if (!keyMapping.ContainsKey(keyChar))
                    {
                        keyMapping.Add(keyChar, keyImage);
                    }

                    char lowerKey = char.ToLower(keyChar);
                    if (!keyMapping.ContainsKey(lowerKey))
                    {
                        keyMapping.Add(lowerKey, keyImage);
                    }
                }
            }
        }
        //keyMapping = new Dictionary<char, Image>();

        ////  VirtualKeyboardPanel �� Image �͎擾���Ȃ�
        //foreach (Transform keyTransform in transform)
        //{
        //    Image keyImage = keyTransform.GetComponent<Image>(); //�e�L�[�iVKey�Ȃǁj�� Image ���擾

        //    if (keyImage != null) //  Image ������I�u�W�F�N�g�̂ݏ���
        //    {
        //        string keyName = keyTransform.name; //  ��F�uAKey�v�uBKey�v�uCKey�v
        //        if (keyName.Length > 0)
        //        {
        //            char keyChar = keyName[0]; //  �ŏ���1�������擾�iAKey �� 'A'�j

        //            if (!keyMapping.ContainsKey(keyChar))
        //            {
        //                keyMapping.Add(keyChar, keyImage);
        //            }

        //            char lowerKey = char.ToLower(keyChar); //���������o�^
        //            if (!keyMapping.ContainsKey(lowerKey))
        //            {
        //                keyMapping.Add(lowerKey, keyImage);
        //            }
        //        }
        //    }
        //}
    }

    //  �^�C�s���O�Ώۂ̕����ɉ����ăL�[�����点��
    public void HighlightKey(char key, Color color)
    {
        ResetKeyColors();

        char lowerKey = char.ToLower(key);

        if (keyMapping.ContainsKey(lowerKey))
        {
            keyMapping[lowerKey].color = color;
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
