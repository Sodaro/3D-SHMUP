using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
[CreateAssetMenu(fileName = "InputValidator - Digits.asset", menuName = "TextMeshPro/Input Validators/Digits", order = 100)]
public class TMP_DigitsValidator : TMP_InputValidator
{
    // Custom text input validation function
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (ch >= '0' && ch <= '9')
        {
            text += ch;
            pos += 1;
            if (text.Length >= 3)
            {
                int val = int.Parse(text);
                if (val > 100)
                    text = "100";
                return ' ';
            }
            return ch;
        }
        return (char)0;
    }
}
