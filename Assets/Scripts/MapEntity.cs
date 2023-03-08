using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public EntityType Type;
    public string Name;
    public string ID;

    public bool isRevealed;
    public bool canBeInteracted;
    public bool hasBeenInteracted;
    public bool canBeUsed;
    public int _UsesLeft;
    public bool isDepleted;

    [SerializeField] string _Interact_ID;
    [SerializeField] string _PassiveFX_ID;
    [SerializeField] List<Outcome> _UseOutcome_ID = new List<Outcome>();

    private void Awake() {
        if (!isRevealed) Hide();
    }

    private void OnEnable() {
    }

    public void Interact(){
        // interact with this entity
        Debug.Log("Start a dialogue with this entity");
    }

    public void EnablePassiveEffect(){
        // effect while standing in this location
        Debug.Log("Activate passive effect");

        MapManager.Instance.EnablePassiveEffect(_PassiveFX_ID);
    }

    public void DisablePassiveEffect(){
        MapManager.Instance.DisablePassiveEffect(_PassiveFX_ID);
    }

    public void Use(){
        // use this entity
        Debug.Log("Using this entity");

        // foreach (Outcome oc in _UseOutcome_ID){
        //     MapManager.Instance.ModifyVariable(oc.varName,oc.amount.ToString());
        // }
    }

    public void Reveal(){
        // reveal this entity
        gameObject.SetActive(true);
    }

    public void Hide(){
        // hide this entity
        gameObject.SetActive(false);
    }

    public void ShowOptions(){
        // show the options available for this entity
    }

    private void OnDestroy() {
        
    }
}

public enum EntityType{
    Generic,
    NPC,
    Event,
}