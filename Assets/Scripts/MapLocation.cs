using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation : MonoBehaviour
{
    public List<MapEntity> Entities = new List<MapEntity>();
    public List<MapLocation> AdjacentLocations = new List<MapLocation>();

    [SerializeField] private GameObject entityPrefab;
    [SerializeField] private GameObject entityContainer;

    public void MoveHere(){
        MapManager.Instance.LocationChosen(this);
    }

    public bool CanMoveHere(MapLocation loc){
        // map manager checks if player can move to this location from their position
        return AdjacentLocations.Contains(loc);
    }

    public void CreateRandomEntity(){
        MapEntity ent = CreateEntity();
        ent.isRevealed = false;
    }

    public MapEntity CreateEntity(){
        MapEntity ent = Instantiate(entityPrefab).GetComponent<MapEntity>();
        ent.transform.SetParent(entityContainer.transform,false);
        Entities.Add(ent);

        return ent;
    }

    public void SpawnEntity(MapEntity prefab){
        MapEntity ent = Instantiate(prefab).GetComponent<MapEntity>();
        ent.transform.SetParent(entityContainer.transform,false);
        Entities.Add(ent);
    }

    public void RemoveEntity(MapEntity ent){
        Entities.Remove(ent);
    }

    public void ActivateLocation(){
        foreach (MapEntity ent in Entities){
            ent.Reveal();
            ent.EnablePassiveEffect();
        }
    }

    public void LeaveLocation(){
        foreach (MapEntity ent in Entities){
            ent.DisablePassiveEffect();
        }
    }
}
