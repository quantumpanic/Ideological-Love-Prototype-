using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable Controller", menuName = "Assets/New Variable Controller")]
public class VariableController : ScriptableObject
{
    [SerializeField] public string ID;
    [SerializeField] public List<Variable> variables = new List<Variable>();
}

[System.Serializable]
public class Variable{
    public string varName;
    // public string varType;
    public string value;
}