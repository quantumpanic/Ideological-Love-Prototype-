using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : DialogueNode
{
    [TextArea]
    public List<string> textLines = new List<string>();
    public List<Option> options = new List<Option>();
}