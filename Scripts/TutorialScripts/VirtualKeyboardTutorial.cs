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
                string keyName = keyTransform.name; // 例：「AKey」「BKey」
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

        ////  VirtualKeyboardPanel の Image は取得しない
        //foreach (Transform keyTransform in transform)
        //{
        //    Image keyImage = keyTransform.GetComponent<Image>(); //各キー（VKeyなど）の Image を取得

        //    if (keyImage != null) //  Image があるオブジェクトのみ処理
        //    {
        //        string keyName = keyTransform.name; //  例：「AKey」「BKey」「CKey」
        //        if (keyName.Length > 0)
        //        {
        //            char keyChar = keyName[0]; //  最初の1文字を取得（AKey → 'A'）

        //            if (!keyMapping.ContainsKey(keyChar))
        //            {
        //                keyMapping.Add(keyChar, keyImage);
        //            }

        //            char lowerKey = char.ToLower(keyChar); //小文字も登録
        //            if (!keyMapping.ContainsKey(lowerKey))
        //            {
        //                keyMapping.Add(lowerKey, keyImage);
        //            }
        //        }
        //    }
        //}
    }

    //  タイピング対象の文字に応じてキーを光らせる
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
