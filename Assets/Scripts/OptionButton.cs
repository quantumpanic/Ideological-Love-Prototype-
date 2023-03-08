using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public string optionLabel;
    public string target;

    public void SetTargetForCurrentModule()
    {
        DialogueManager.Instance.nextModuleID = target;
        DialogueManager.Instance.OptionChosen();
    }

    public void ResetSize(){
        transform.localScale = Vector3.one;
    }

    public void SetText(){
        GetComponentInChildren<Text>().text = optionLabel;
    }
}
