using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FingerIndicatorTutorial : MonoBehaviour
{
    public SpriteRenderer leftPinky, leftRing, leftMiddle, leftIndex;
    public SpriteRenderer rightIndex, rightMiddle, rightRing, rightPinky;

    private Dictionary<char, SpriteRenderer> fingerMapping;
    private Color defaultColor = Color.white;
    private Dictionary<SpriteRenderer, Color> fingerColors; // 指ごとの色設定

    public VirtualKeyboardTutorial virtualKeyboard;

    void Start()
    {
        //指ごとの色を設定
        fingerColors = new Dictionary<SpriteRenderer, Color>()
        {
            { leftPinky, Color.red }, { leftRing, new Color(1.0f, 0.5f, 0.0f) },  // 赤、オレンジ
            { leftMiddle, Color.yellow }, { leftIndex, Color.green },             // 黄、緑
            { rightIndex, Color.blue }, { rightMiddle, Color.cyan },              // 青、水色
            { rightRing, new Color(0.5f, 0.0f, 0.5f) }, { rightPinky, Color.magenta } // 紫、ピンク
        };

        // キーと指の対応付け（日本のキーボード配列）
        fingerMapping = new Dictionary<char, SpriteRenderer>()
        {
            // 左小指
            {'1', leftPinky}, {'q', leftPinky}, {'Q', leftPinky}, {'a', leftPinky}, {'A', leftPinky}, {'z', leftPinky}, {'Z', leftPinky}, {'\t', leftPinky}, {'\n', leftPinky},

            // 左薬指
            {'2', leftRing}, {'w', leftRing}, {'W', leftRing}, {'s', leftRing}, {'S', leftRing}, {'x', leftRing}, {'X', leftRing},

            // 左中指
            {'3', leftMiddle}, {'e', leftMiddle}, {'E', leftMiddle}, {'d', leftMiddle}, {'D', leftMiddle}, {'c', leftMiddle}, {'C', leftMiddle},

            // 左人差し指
            {'4', leftIndex}, {'5', leftIndex}, {'r', leftIndex}, {'R', leftIndex}, {'t', leftIndex}, {'T', leftIndex}, {'f', leftIndex}, {'F', leftIndex},
            {'g', leftIndex}, {'G', leftIndex}, {'v', leftIndex}, {'V', leftIndex}, {'b', leftIndex}, {'B', leftIndex},

            // 右人差し指
            {'6', rightIndex}, {'7', rightIndex}, {'y', rightIndex}, {'Y', rightIndex}, {'u', rightIndex}, {'U', rightIndex}, {'h', rightIndex}, {'H', rightIndex},
            {'j', rightIndex}, {'J', rightIndex}, {'n', rightIndex}, {'N', rightIndex}, {'m', rightIndex}, {'M', rightIndex},

            // 右中指
            {'8', rightMiddle}, {'i', rightMiddle}, {'I', rightMiddle}, {'k', rightMiddle}, {'K', rightMiddle}, {',', rightMiddle},

            // 右薬指
            {'9', rightRing}, {'o', rightRing}, {'O', rightRing}, {'l', rightRing}, {'L', rightRing}, {'.', rightRing},

            // 右小指
            {'0', rightPinky}, {'p', rightPinky}, {'P', rightPinky}, {'@', rightPinky}, {'-', rightPinky}, {':', rightPinky}, {';', rightPinky}, {'\b', rightPinky}
        };
    }


    public void UpdateFingerDisplay(char key)
    {
        ResetFingerColors();


        if (fingerMapping.ContainsKey(key)) 
        {
            SpriteRenderer finger = fingerMapping[key];
            Color highlightColor = fingerColors[finger]; // 指ごとの色を取得
            finger.color = highlightColor;
        }
    }

    void ResetFingerColors()
    {
        foreach (var finger in fingerMapping.Values)
        {
            finger.color = defaultColor;
        }
    }

    public Color GetFingerColor(char key)
    {
        if (fingerMapping.ContainsKey(key))
        {
            SpriteRenderer finger = fingerMapping[key];
            return fingerColors[finger]; //  指の色を返す
        }
        return Color.white; //どの指にも該当しない場合はデフォルト色
    }
}
