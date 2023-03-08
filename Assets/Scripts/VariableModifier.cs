using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableModifier : MonoBehaviour
{
    public static VariableModifier Instance;
    [SerializeField] VariableController variableController;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public bool ModifyVariable(string ID, string amt)
    {
        if (ID == "") return false;

        // bool effectExists = false;
        Variable getVar = GetVar(ID);

        if (getVar == null) throw new System.Exception("Variable ID doesn't exist");

        bool validOperation = false;

        switch (ID)
        {
            case "Stamina":
                validOperation = ResolveStamina(amt, getVar);
                break;
            case "Money":
                validOperation = ResolveMoney(amt, getVar);
                break;
            case "BazaarHinder":
                BazaarHinder(amt, getVar);
                validOperation = true;
                break;
        }

        return validOperation;
    }

    Variable GetVar(string ID)
    {
        foreach (Variable v in variableController.variables)
        {
            if (v.varName == ID)
            {
                return v;
            }
        }

        return null;
    }

    bool ResolveStamina(string amt, Variable v)
    {
        // parse the amount
        int addInt = int.Parse(amt);

        // parse the variable
        int varInt = int.Parse(v.value);

        int result = varInt + addInt;
        if (result < 0)
        {
            // not enough stamina, abort
            Debug.Log("Not enough Stamina!");
        }

        else
        {
            v.value = result.ToString();
            return true;
        }

        return false;
    }

    bool ResolveMoney(string amt, Variable v)
    {
        // parse the amount
        int addInt = int.Parse(amt);

        // parse the variable
        int varInt = int.Parse(v.value);

        // parse the bank max
        int bankMax = int.Parse(GetVar("BankMax").value);

        int result = varInt + addInt;
        if (addInt < 0)
        {
            if (result < 0)
            {
                // not enough stamina, abort
                Debug.Log("Not enough Money!");
            }

            else
            {
                v.value = result.ToString();
                return true;
            }
        }

        if (addInt > 0)
        {
            if (result > bankMax)
            {
                // not enough stamina, abort
                Debug.Log("Not enough Money!");
            }

            else
            {
                v.value = result.ToString();
                return true;
            }
        }

        return false;
    }

    void BazaarHinder(string amt, Variable v)
    {
        v.value = amt;
    }

    void ResolveVariable(string ID, string amt)
    {

        if (ID == "")
        {
            Debug.Log("Entity has no passive effect");
            return;
        }
    }
}
