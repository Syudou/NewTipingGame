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
            Image keyImage = child.GetComponent<Image>(); //  各キーの Image を取得
            if (keyImage != null)
            {
                string keyName = child.gameObject.name;
                if (keyName.Length > 0)
                {
                    char keyChar = keyName[0]; //  "AKey" の最初の文字 'A' を取得

                    if (!keyMapping.ContainsKey(keyChar))
                    {
                        keyMapping.Add(keyChar, keyImage);
                    }

                    char lowerKey = char.ToLower(keyChar); //  小文字も登録
                    if (!keyMapping.ContainsKey(lowerKey))
                    {
                        keyMapping.Add(lowerKey, keyImage);
                    }
                }
            }
        }

    }

    //  タイピング対象の文字に応じてキーを光らせる
    public void HighlightKeyWithColor(char key, Color highlightColor)
    {
        ResetKeyColors();

        if (keyMapping.ContainsKey(key))
        {
            keyMapping[key].color = highlightColor; // キーの色を指の色に設定
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
