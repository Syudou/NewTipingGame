using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FingerIndicatorTutorial : MonoBehaviour
{
    public SpriteRenderer leftPinky, leftRing, leftMiddle, leftIndex;
    public SpriteRenderer rightIndex, rightMiddle, rightRing, rightPinky;

    private Dictionary<char, SpriteRenderer> fingerMapping;
    private Color defaultColor = Color.white;
    private Dictionary<SpriteRenderer, Color> fingerColors; // �w���Ƃ̐F�ݒ�

    public VirtualKeyboardTutorial virtualKeyboard;

    void Start()
    {
        //�w���Ƃ̐F��ݒ�
        fingerColors = new Dictionary<SpriteRenderer, Color>()
        {
            { leftPinky, Color.red }, { leftRing, new Color(1.0f, 0.5f, 0.0f) },  // �ԁA�I�����W
            { leftMiddle, Color.yellow }, { leftIndex, Color.green },             // ���A��
            { rightIndex, Color.blue }, { rightMiddle, Color.cyan },              // �A���F
            { rightRing, new Color(0.5f, 0.0f, 0.5f) }, { rightPinky, Color.magenta } // ���A�s���N
        };

        // �L�[�Ǝw�̑Ή��t���i���{�̃L�[�{�[�h�z��j
        fingerMapping = new Dictionary<char, SpriteRenderer>()
        {
            // �����w
            {'1', leftPinky}, {'q', leftPinky}, {'Q', leftPinky}, {'a', leftPinky}, {'A', leftPinky}, {'z', leftPinky}, {'Z', leftPinky}, {'\t', leftPinky}, {'\n', leftPinky},

            // ����w
            {'2', leftRing}, {'w', leftRing}, {'W', leftRing}, {'s', leftRing}, {'S', leftRing}, {'x', leftRing}, {'X', leftRing},

            // �����w
            {'3', leftMiddle}, {'e', leftMiddle}, {'E', leftMiddle}, {'d', leftMiddle}, {'D', leftMiddle}, {'c', leftMiddle}, {'C', leftMiddle},

            // ���l�����w
            {'4', leftIndex}, {'5', leftIndex}, {'r', leftIndex}, {'R', leftIndex}, {'t', leftIndex}, {'T', leftIndex}, {'f', leftIndex}, {'F', leftIndex},
            {'g', leftIndex}, {'G', leftIndex}, {'v', leftIndex}, {'V', leftIndex}, {'b', leftIndex}, {'B', leftIndex},

            // �E�l�����w
            {'6', rightIndex}, {'7', rightIndex}, {'y', rightIndex}, {'Y', rightIndex}, {'u', rightIndex}, {'U', rightIndex}, {'h', rightIndex}, {'H', rightIndex},
            {'j', rightIndex}, {'J', rightIndex}, {'n', rightIndex}, {'N', rightIndex}, {'m', rightIndex}, {'M', rightIndex},

            // �E���w
            {'8', rightMiddle}, {'i', rightMiddle}, {'I', rightMiddle}, {'k', rightMiddle}, {'K', rightMiddle}, {',', rightMiddle},

            // �E��w
            {'9', rightRing}, {'o', rightRing}, {'O', rightRing}, {'l', rightRing}, {'L', rightRing}, {'.', rightRing},

            // �E���w
            {'0', rightPinky}, {'p', rightPinky}, {'P', rightPinky}, {'@', rightPinky}, {'-', rightPinky}, {':', rightPinky}, {';', rightPinky}, {'\b', rightPinky}
        };
    }


    public void UpdateFingerDisplay(char key)
    {
        ResetFingerColors();


        if (fingerMapping.ContainsKey(key)) 
        {
            SpriteRenderer finger = fingerMapping[key];
            Color highlightColor = fingerColors[finger]; // �w���Ƃ̐F���擾
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
            return fingerColors[finger]; //  �w�̐F��Ԃ�
        }
        return Color.white; //�ǂ̎w�ɂ��Y�����Ȃ��ꍇ�̓f�t�H���g�F
    }
}
