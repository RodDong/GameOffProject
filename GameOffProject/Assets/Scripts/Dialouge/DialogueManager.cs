using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.1f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueSubPanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    //[SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject tachieObject;
    [SerializeField] private Animator tachieAnimator; // tachie is 立绘
    // private Animator layoutAnimator;
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private StatusBarManager statusBarManager;
    [SerializeField] private PlayerMove player;
    [SerializeField] private PlayerStatus playerStatus;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private bool toBattle = false;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private bool panelFadeIn = false;

    private bool panelFadeOut = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    //private const string PORTRAIT_TAG = "portrait";
    private const string TACHIE_TAG = "tachie";
    private const string BATTLE_TAG = "battle";
    private const string NO_TEXT_TAG = "notext";
    private const string PROGRESS_TAG = "progress";
    private const string LOAD_SCENE_TAG = "scene";
    private const string TELEPORT_TAG = "position";
    private const string SCALE_TAG = "scale";

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.GetComponent<CanvasGroup>().alpha = 0.0f;
        dialoguePanel.SetActive(false);

        // get the layout animator
        // layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        statusBarManager = FindObjectOfType<StatusBarManager>();
    }

    private void Update() 
    {
        // return right away if dialogue isn't playing
        UpdateDialoguePanel();
        
        if (!dialogueIsPlaying) 
        {
            return;
        }
        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 
            && Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        panelFadeIn = true;
        panelFadeOut = false;
        dialoguePanel.SetActive(true);
        // reset portrait and speaker
        displayNameText.text = "???";
        //portraitAnimator.Play("default");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.2f);
        
        dialogueIsPlaying = false;
        panelFadeOut = true;
        panelFadeIn = false;
        dialoguePanel.SetActive(false);
        
        dialogueText.text = "";

        player.ExitDialogueMode();
        
        if (toBattle) {
            toBattle = false;
            statusBarManager.ResetEnemyStatus();
            player.EnterBattleMode();
        }
    }

    private void UpdateDialoguePanel()
    {
        if (panelFadeIn)
        {
            FadeIn();
        }
        else if (panelFadeOut)
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        CanvasGroup dialoguePanelCanvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        if(dialoguePanelCanvasGroup.alpha < 1.0f)
        {
            dialoguePanelCanvasGroup.alpha += Time.deltaTime * 5.0f;
        }
        else
        {
            panelFadeIn = false;
        }

    }

    private void FadeOut()
    {
        CanvasGroup dialoguePanelCanvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        if (dialoguePanelCanvasGroup.alpha > 0.0f)
        {
            dialoguePanelCanvasGroup.alpha -= Time.deltaTime * 5.0f;
        }
        else
        {
            panelFadeOut = false;
        }
    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line) 
    {
        if (line.Length > 0 && !dialogueSubPanel.activeSelf) {
            dialogueSubPanel.SetActive(true);
        }
        // empty the dialogue text
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        int i = 0; // used to create a delay between input registration
        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {   
            i++;
            // if the submit button is pressed, finish up displaying the line right away
            if (i > 5 && Input.GetMouseButton(0)) 
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else 
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
    }

    private async void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) 
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                // case PORTRAIT_TAG:
                //     portraitAnimator.Play(tagValue);
                //     break;
                // case TACHIE_TAG:
                //     tachieObject.SetActive(tagValue != "none");
                //     tachieAnimator.Play(tagValue);
                //     break;
                case BATTLE_TAG:
                    toBattle = true;
                    break;
                case NO_TEXT_TAG:
                    dialogueSubPanel.SetActive(false);
                    break;
                case PROGRESS_TAG:
                    progressManager.transitionToNextState(int.Parse(tagValue));
                    break;
                case LOAD_SCENE_TAG:
                    ExitDialogueMode();
                    SceneManager.LoadScene(tagValue, LoadSceneMode.Single);
                    break;
                case TELEPORT_TAG:
                    await Task.Delay(200);
                    string[] position = tagValue.Split(',');
                    player.transform.position = new Vector3(float.Parse(position[0]), float.Parse(position[1]), float.Parse(position[2]));
                    break;
                case SCALE_TAG:
                    await Task.Delay(200);
                    string[] scale = tagValue.Split(',');
                    player.transform.localScale = new Vector3(float.Parse(scale[0]), float.Parse(scale[1]), float.Parse(scale[2]));
                    break;
                default:
                    // Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            if ((progressManager.currentProgress == 1 || progressManager.currentProgress == 2) && choice.text == "Go to Office and Clinic") {
                break; // at the beginning, can only go to office
            } else if (progressManager.currentProgress == 2 && choice.text == "Go Home") {
                break;
            }
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public async void MakeChoiceAsync(int choiceIndex)
    {
        if (canContinueToNextLine) 
        {
            Choice choice = currentStory.currentChoices[choiceIndex];
            currentStory.ChooseChoiceIndex(choiceIndex);
            switch (choice.text) {
                case "Go Home":
                    Debug.Log("Go Home");
                    StartCoroutine(ExitDialogueMode());
                    SceneManager.LoadScene("10street_outside_home", LoadSceneMode.Single);
                    await Task.Delay(200);
                    player.transform.position = new Vector3(-10.0f, -4.0f, 1.0f);
                    player.transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                    break;
                case "Go to Office and Clinic":
                    Debug.Log("Go to Office");
                    StartCoroutine(ExitDialogueMode());
                    SceneManager.LoadScene("12street_outside_clinic", LoadSceneMode.Single);
                    await Task.Delay(200);
                    player.transform.position = new Vector3(-13.0f, -4.0f, 1.0f);
                    player.transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                    break;
                case "Go to Bar and Restaurant":
                    Debug.Log("Go to Bar");
                    StartCoroutine(ExitDialogueMode());
                    SceneManager.LoadScene("11street_outside_restaurant", LoadSceneMode.Single);
                    await Task.Delay(200);
                    player.transform.position = new Vector3(-13.0f, -4.0f, 1.0f);
                    player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    break;
                case "Investigate Chef's Phone":
                    playerStatus.addClue(0);
                    break;
                case "Investigate Chef's Guest List":
                    playerStatus.addClue(1);
                    break;
                case "Investigate Chef's Supply List":
                    playerStatus.addClue(2);
                    break;
                case "Investigate Patient Information":
                    playerStatus.addClue(3);
                    break;
                case "Investigate The Pictures":
                    playerStatus.addClue(4);
                    break;
                case "Investigate Menu":
                    playerStatus.addClue(5);
                    break;
                case "Investigate Record of Surgeries":
                    playerStatus.addClue(6);
                    break;
                case "note":
                    playerStatus.addClue(7);
                    break;
                case "phone":
                    playerStatus.addClue(8);
                    break;
                case "ledger":
                    playerStatus.addClue(9);
                    break;
                case "doctor's phone":
                    playerStatus.addClue(10);
                    break;
                case "doctor's namelist":
                    playerStatus.addClue(11);
                    break;
                case "doctor's cargo":
                    playerStatus.addClue(12);
                    break;
            }

            ContinueStory();
        }
    }

}