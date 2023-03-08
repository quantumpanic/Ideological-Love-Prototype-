using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DialogueSerializer
{
    public static string filePath = "/Editor/TxtFiles/File.txt";
    public static string directoryPath = "/Editor/TxtFiles/";
    public static string folderPath = "S1D01/";

    [MenuItem("Utilities/GenerateDialogueModule")]
    public static void GenerateScriptableObject()
    {
        // string[] allLines = File.ReadAllLines(Application.dataPath + filePath);

        string[] files = Directory.GetFiles(Application.dataPath + directoryPath + folderPath, "*.txt", SearchOption.TopDirectoryOnly);

        foreach (string file in files)
        {
            string[] allLines = File.ReadAllLines(file);

            DialogueModule module = ScriptableObject.CreateInstance<DialogueModule>();
            module.textLines = new List<string>();
            module.options = new List<Option>();

            foreach (string s in allLines)
            {
                string[] splitData = s.Split(':');
                int tryParse;

                switch (splitData[0])
                {
                    case "ID":
                        module.moduleID = splitData[1];
                        break;
                    case "Target":
                        module.targetID = splitData[1];
                        break;
                    case "Speaker":
                        module.SpeakerName = splitData[1];
                        break;
                    case "BG":
                        module.BG_SpriteName = splitData[1];
                        break;
                    case "Sprite":
                        module.Char_SpriteName = splitData[1];
                        break;
                    case "FadeIn":
                        module.doFadeIn = splitData[1] == "Y" ? true : false;
                        break;
                    case "FadeOut":
                        module.doFadeOut = splitData[1] == "Y" ? true : false;
                        break;
                    case "Condition":
                        // start serializing the conditions here
                        Condition newCondition = new Condition();

                        // int tryParse; //TODO is it okay to remove this??
                        if (int.TryParse(splitData[3], out tryParse))
                        {
                            newCondition.varName = splitData[2];
                            newCondition.minimum = tryParse;
                            module.conditions.Add(newCondition);
                        }
                        else throw new System.Exception("string is not a number");

                        break;
                    case "Outcome":
                        // start serializing the outcomes here
                        Outcome newOutcome = new Outcome();

                        // int tryParse; //TODO is it okay to remove this??
                        if (int.TryParse(splitData[3], out tryParse))
                        {
                            newOutcome.varName = splitData[2];
                            newOutcome.amount = tryParse;
                            module.outcomes.Add(newOutcome);
                        }
                        else throw new System.Exception("string is not a number");

                        break;
                    case "Line":
                        // start serializing the lines here
                        module.textLines.Add(splitData[2]);
                        break;
                    case "Option":
                        // start serializing the options here
                        Option newOption = new Option();
                        newOption.optionLabel = splitData[2];
                        newOption.target = splitData[3];

                        module.options.Add(newOption);
                        break;
                }
            }

            AssetDatabase.CreateAsset(module, $"Assets/Modules/out/{module.moduleID}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}