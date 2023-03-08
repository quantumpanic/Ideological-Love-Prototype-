using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Module Controller", menuName = "Assets/New Module Controller")]
public class ModuleController : ScriptableObject
{
    public string ControllerID;
    public List<DialogueModule> modules = new List<DialogueModule>();
}
