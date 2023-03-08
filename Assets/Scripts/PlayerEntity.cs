using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public static PlayerEntity Instance;
    [SerializeField] int Stamina;
    [SerializeField] int StaminaMax = 100;
    [SerializeField] int Money;
    [SerializeField] bool Fatigue;
    [SerializeField] int MoveCost = -10;
    [SerializeField] int AddMoveCost = 0;
    [SerializeField] float speed;
    [SerializeField] MapLocation startLocation;
    [SerializeField] MapLocation currentLocation;
    private bool playerIsMoving;

    public int MoneyMax = 100;
    public int BankMax = 1000;

    public VariableController variableController;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void OnEnable()
    {
        Init();
        SetLocation(startLocation);
        currentLocation.ActivateLocation();
    }

    string GetVar(string ID)
    {
        foreach (Variable v in variableController.variables)
        {
            if (v.varName == ID)
            {
                return v.value;
            }
        }

        return null;
    }

    void Init()
    {
        // get the vars
        string fatigueStr = GetVar("Fatigue");
        string moneyStr = GetVar("Money");
        string bankStr = GetVar("Bank");

        if (fatigueStr == "1") Fatigue = true;

        // if Fatigue debuff is active, starting Stamina is 30
        if (Fatigue) SetStamina(30);
        else SetStamina(50);

        int moneyInt = int.Parse(moneyStr);
        SetMoney(moneyInt);
    }

    public void SetStamina(int amount)
    {
        Stamina = amount;
    }


    public void SetMoney(int amount)
    {
        Money = amount;
    }

    public bool ModifyStamina(int amount)
    {
        int sum = Stamina + amount;
        if (sum >= 0 && sum <= StaminaMax)
        {
            Stamina += amount;
            return true;
        }
        return false;
    }

    public bool ModifyMoney(int amount)
    {
        int sum = Money + amount;
        if (sum >= 0 && sum <= MoneyMax)
        {
            Money += amount;
            return true;
        }
        return false;
    }

    public bool ModifyBankMoney(int amount)
    {
        return false;
    }

    public void LocationChosen(MapLocation loc)
    {
        if (loc.CanMoveHere(currentLocation))
        {
            CheckStaminBeforeMove(loc);
        }
        else
        {
            Debug.Log("Invalid location");
            // throw new System.Exception("That isn't an adjacent location");
        }
    }

    public void CheckStaminBeforeMove(MapLocation loc)
    {
        bool enoughStamina = false;
        int totalCost = MoveCost + AddMoveCost;

        if (ModifyStamina(totalCost)) enoughStamina = true;

        if (enoughStamina){
            // do movement
            MoveToLocation(loc);
        }

        else{
            Debug.Log("Not enough stamina!");
        }
    }

    void MoveToLocation(MapLocation loc)
    {
        if (playerIsMoving) return;

        // calculate the movement cost

        // remove all passive effects
        currentLocation.LeaveLocation();

        // begin moving
        StartCoroutine(CR_MoveToLocation(loc));
    }

    IEnumerator CR_MoveToLocation(MapLocation loc)
    {  
        playerIsMoving = true;

        Transform player = transform;
        Vector2 destination = loc.transform.position;

        while (Vector2.Distance(player.position, destination) > 0.1f)
        {
            var step = speed * Time.deltaTime; // calculate distance to move

            // Move our position a step closer to the target.
            player.position = Vector2.MoveTowards(player.position, destination, step);

            yield return null;
        }

        // set the current location
        SetLocation(loc);
        ActivateLocation(loc);

        playerIsMoving = false;
    }

    public void SetLocation(MapLocation loc)
    {
        transform.position = loc.transform.position;
        currentLocation = loc;
    }

    void ActivateLocation(MapLocation loc)
    {
        loc.ActivateLocation();
    }
}
