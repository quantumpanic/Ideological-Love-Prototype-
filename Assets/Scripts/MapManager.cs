using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public List<MapLocation> MapLocations = new List<MapLocation>();
    // [SerializeField] GameObject playerEntity;


    private void OnEnable()
    {
        AddRandomEntities();
    }

    public void LocationChosen(MapLocation loc)
    {
        PlayerEntity.Instance.LocationChosen(loc);
    }

    public void EnablePassiveEffect(string ID)
    {
        VariableModifier.Instance.ModifyVariable(ID, "1");
    }

    public void DisablePassiveEffect(string ID)
    {
        VariableModifier.Instance.ModifyVariable(ID, "0");
    }

    public void ModifyVariable(string ID, string amt)
    {
        VariableModifier.Instance.ModifyVariable(ID, amt);
    }

    void AddRandomEntities()
    {
        foreach (MapLocation loc in MapLocations)
        {
            // yes or no
            int num = Random.Range(0, 2);
            bool doSpawn = num == 1 ? true : false;

            if (doSpawn)
            {
                loc.CreateRandomEntity();
            }
        }
    }

    void GetEntityDataFor(MapEntity ent)
    {

    }

    void SpawnEntityAtLocation(MapLocation loc)
    {
        MapEntity ent = loc.CreateEntity();
    }
}
