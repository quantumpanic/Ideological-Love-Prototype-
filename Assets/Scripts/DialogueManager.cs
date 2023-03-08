using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isReading) NextDialogueLine();
        }

        // if (Input.GetKey(KeyCode.Return))
        // {
        //     Restart();
        // }
        // TODO Enter key causes bug making dialogues overlap

        if (Input.GetMouseButtonDown(1))
        {
            Restart();
        }
    }

    public void GetTap()
    {
        if (isReading) NextDialogueLine();
    }

    void Restart()
    {
        restartText.text = "";
        ReadFirstModule();
    }

    public Text speakerLabel;
    public GameObject dialogueBoxObj;
    public Text dialogueBoxText;

    public Text restartText; //TODO remove this

    public Transform optionsPanel;
    bool isReading;
    bool isReadingCurrentLine;
    public DialogueModule currentActiveModule;

    public string startingModuleID;
    public string nextModuleID;

    // the dialogue modules list last
    public List<DialogueModule> dialogueModules = new List<DialogueModule>();

    private void OnEnable()
    {
        ReadFirstModule();
    }

    private void ReadFirstModule(){
        foreach (DialogueModule module in dialogueModules)
        {
            if (module.moduleID == startingModuleID) currentActiveModule = module;
        }

        if (!currentActiveModule) throw new System.Exception("Could not find a starting module!");
        else ReadAllLines();
    }

    public void GetNextModule()
    {
        // get the next module based on the last module's reference
        // or get the starting module

        // Debug.Log(currentActiveModule.nextModuleID);

        bool foundNewModule = false;

        foreach (DialogueModule module in dialogueModules)
        {
            // Debug.Log(module.moduleID + " - " + currentActiveModule.nextModuleID);
            if (module.moduleID == currentActiveModule.targetID)
            {
                currentActiveModule = module;
                foundNewModule = true;
                break;
            }
        }

        if (foundNewModule)
        {
            Debug.Log("found module");
            ReadAllLines();
        }
        else CheckForOptions();
    }

    void CheckForOptions()
    {
        // if no options show end of dialogue
        // TODO this shouldn't happen

        if (currentActiveModule.options.Count <= 0) EndOfDialogue();
        else ShowDialogueOptions();
    }

    public void OptionChosen()
    {
        if (nextModuleID == "") throw new System.Exception("This option doesn't have a target!");
        else GoToNextModuleFromChoice();

        var items = GameObject.FindGameObjectsWithTag("Choice");
        foreach (GameObject go in items)
        {
            Destroy(go);
        }
    }

    void GoToNextModuleFromChoice()
    {
        bool foundNewModule = false;

        foreach (DialogueModule module in dialogueModules)
        {
            // Debug.Log(module.moduleID + " - " + currentActiveModule.nextModuleID);
            if (module.moduleID == nextModuleID)
            {
                currentActiveModule = module;
                foundNewModule = true;
                break;
            }
        }

        if (foundNewModule)
        {
            // Debug.Log("found module");
            ReadAllLines();
        }
        else throw new System.Exception("This choice doesn't have a valid target module!");
    }

    public OptionButton optionButtonPrefab;

    void ShowDialogueOptions()
    {
        foreach (Option optionData in currentActiveModule.options)
        {
            // instantiate option button and attach to the option panel
            OptionButton optionButton = Instantiate(optionButtonPrefab).GetComponent<OptionButton>();
            optionButton.target = optionData.target;
            optionButton.optionLabel = optionData.optionLabel;

            optionButton.transform.SetParent(optionsPanel.transform);
            optionButton.ResetSize();
            optionButton.SetText();
        }
    }

    void EndOfDialogue()
    {
        restartText.text = "[End of dialogue] \nPress enter to restart";
    }

    void ReadAllLines()
    {
        // coroutine, try to read all text lines in the queue
        // before next is pressed, loop will pause

        if (isReading) return;

        StartCoroutine(ReadTextLines_Coroutine(currentActiveModule));
    }

    IEnumerator ReadTextLines_Coroutine(DialogueModule module)
    {
        // set the speaker name
        speakerLabel.text = module.SpeakerName;

        // set the background sprite
        SetBackground(module.BG_SpriteName);

        // fade in
        if (module.doFadeIn)
        {
            FadeIn();
            yield return new WaitForSeconds(1);

            // make the dialogue box visible
            dialogueBoxObj.SetActive(true);
        }

        isReading = true;

        foreach (string line in module.textLines)
        {
            // display the text, then wait for user to click
            DisplayTextLine(line);
            isReadingCurrentLine = true;
            while (isReadingCurrentLine) yield return null;
        }

        // fade out
        if (module.doFadeOut)
        {
            // hide the dialogue box
            dialogueBoxObj.SetActive(false);

            FadeOut();
            yield return new WaitForSeconds(1);
        }
        isReading = false;

        GetNextModule();
    }

    void DisplayTextLine(string textLine)
    {
        dialogueBoxText.text = textLine;
    }

    void NextDialogueLine()
    {
        if (!isReading) return;
        isReadingCurrentLine = false;
    }

    void ChooseOption()
    {

    }

    public Image fadePanel;

    void FadeIn()
    {
        fadePanel.CrossFadeAlpha(0, 1f, false);
    }

    void FadeOut()
    {
        fadePanel.CrossFadeAlpha(1, 1f, false);
    }

    public Image background;

    void SetBackground(string spriteName)
    {
        // find the sprite in resources
    }
}
