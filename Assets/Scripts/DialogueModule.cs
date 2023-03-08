using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Module", menuName = "Assets/New Dialogue Module")]
public class DialogueModule : ScriptableObject
{
    public string moduleID;
    public string targetID;
    public string SpeakerName;

    public string BG_SpriteName;
    public string Char_SpriteName;

    public bool doFadeIn;
    public bool doFadeOut;

    // only process lines, options, and outcomes if all conditions met
    // else, skip this module and immediately go to target module (must be set)
    // will NOT halt the loop and is immediately resolved
    public List<Condition> conditions = new List<Condition>();

    // change all the named variables based on the amount
    // happens before any lines or options are shown
    // will NOT halt the loop and is immediately resolved
    public List<Outcome> outcomes = new List<Outcome>();

    // all lines will be read in order, then options will be shown
    // option targets will override the module's default target (if set)
    // will halt the loop until the current dialogue line is skipped
    [TextArea] public List<string> textLines = new List<string>();

    // options shown after all dialogue lines are read
    // will halt the loop until an option is selected
    public List<Option> options = new List<Option>();

}

[System.Serializable]
public struct Condition{
    public string varName;
    // public string varType;
    public int minimum;
}

[System.Serializable]
public struct Option{
    public string optionLabel;
    public string target;
}

[System.Serializable]
public struct Outcome{
    public string varName;
    public int amount;
}