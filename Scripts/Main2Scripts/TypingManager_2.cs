using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Question
{
    public string japanese;
    public string roman;
}

public class TypingManager_2 : MonoBehaviour
{
    [SerializeField] private Question[] questions;

    [SerializeField] private TextMeshProUGUI textJapanese; // ここに日本語表示のTextMeshProをアタッチする。
    [SerializeField] private TextMeshProUGUI textRoman; // ここにローマ字表示のTextMeshProをアタッチする。

    private readonly List<char> _roman = new List<char>();

    private int _romanIndex;

    private void Start()
    {
        InitializeQuestion();
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown)
        {
            switch (InputKey(GetCharFromKeyCode(Event.current.keyCode)))
            {
                case 1: // 正解タイプ時
                    _romanIndex++;
                    if (_roman[_romanIndex] == '@') // 「@」がタイピングの終わりの判定となる。
                    {
                        InitializeQuestion();
                    }
                    else
                    {
                        textRoman.text = GenerateTextRoman();
                    }
                    break;
                case 2: // ミスタイプ時
                    break;
            }
        }
    }

    void InitializeQuestion()
    {
        Question question = questions[UnityEngine.Random.Range(0, questions.Length)];

        _roman.Clear();

        _romanIndex = 0;

        char[] characters = question.roman.ToCharArray();

        foreach (char character in characters)
        {
            _roman.Add(character);
        }

        _roman.Add('@');

        textJapanese.text = question.japanese;
        textRoman.text = GenerateTextRoman();
    }

    string GenerateTextRoman()
    {
        string text = "<style=typed>";
        for (int i = 0; i < _roman.Count; i++)
        {
            if (_roman[i] == '@')
            {
                break;
            }

            if (i == _romanIndex)
            {
                text += "</style><style=untyped>";
            }

            text += _roman[i];
        }

        text += "</style>";

        return text;
    }

    int InputKey(char inputChar)
    {
        if (inputChar == '\0')
        {
            return 0;
        }

        if (inputChar == _roman[_romanIndex])
        {
            return 1;
        }

        return 2;
    }

    char GetCharFromKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return 'a';
            case KeyCode.B:
                return 'b';
            case KeyCode.C:
                return 'c';
            case KeyCode.D:
                return 'd';
            case KeyCode.E:
                return 'e';
            case KeyCode.F:
                return 'f';
            case KeyCode.G:
                return 'g';
            case KeyCode.H:
                return 'h';
            case KeyCode.I:
                return 'i';
            case KeyCode.J:
                return 'j';
            case KeyCode.K:
                return 'k';
            case KeyCode.L:
                return 'l';
            case KeyCode.M:
                return 'm';
            case KeyCode.N:
                return 'n';
            case KeyCode.O:
                return 'o';
            case KeyCode.P:
                return 'p';
            case KeyCode.Q:
                return 'q';
            case KeyCode.R:
                return 'r';
            case KeyCode.S:
                return 's';
            case KeyCode.T:
                return 't';
            case KeyCode.U:
                return 'u';
            case KeyCode.V:
                return 'v';
            case KeyCode.W:
                return 'w';
            case KeyCode.X:
                return 'x';
            case KeyCode.Y:
                return 'y';
            case KeyCode.Z:
                return 'z';
            case KeyCode.Alpha0:
                return '0';
            case KeyCode.Alpha1:
                return '1';
            case KeyCode.Alpha2:
                return '2';
            case KeyCode.Alpha3:
                return '3';
            case KeyCode.Alpha4:
                return '4';
            case KeyCode.Alpha5:
                return '5';
            case KeyCode.Alpha6:
                return '6';
            case KeyCode.Alpha7:
                return '7';
            case KeyCode.Alpha8:
                return '8';
            case KeyCode.Alpha9:
                return '9';
            case KeyCode.Minus:
                return '-';
            case KeyCode.Caret:
                return '^';
            case KeyCode.Backslash:
                return '\\';
            case KeyCode.At:
                return '@';
            case KeyCode.LeftBracket:
                return '[';
            case KeyCode.Semicolon:
                return ';';
            case KeyCode.Colon:
                return ':';
            case KeyCode.RightBracket:
                return ']';
            case KeyCode.Comma:
                return ',';
            case KeyCode.Period:
                return '.';
            case KeyCode.Slash:
                return '/';
            case KeyCode.Underscore:
                return '_';
            case KeyCode.Backspace:
                return '\b';
            case KeyCode.Return:
                return '\r';
            case KeyCode.Space:
                return ' ';
            default: //上記以外のキーが押された場合は「null文字」を返す。
                return '\0';
        }
    }
}