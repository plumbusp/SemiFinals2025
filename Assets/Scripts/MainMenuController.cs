using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CustomEditor(typeof(MainMenuController))]
public class MyEditorClass : Editor
{
    public override void OnInspectorGUI()
    {
        MainMenuController mainMenuController = target as MainMenuController;

        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MainMenuController)target), typeof(MainMenuController), false);
        GUI.enabled = true;

        EditorGUILayout.LabelField("Menus", EditorStyles.boldLabel);
        mainMenuController.defaultMenu = (GameObject)EditorGUILayout.ObjectField("Default Menu", mainMenuController.defaultMenu, typeof(GameObject), true);
        mainMenuController.settingsMenu = (GameObject)EditorGUILayout.ObjectField("Settings Menu", mainMenuController.settingsMenu, typeof(GameObject), true);

        EditorGUILayout.LabelField("Click Sounds", EditorStyles.boldLabel);
        mainMenuController.usesClickSounds = EditorGUILayout.Toggle("Uses Click Sounds", mainMenuController.usesClickSounds);
        mainMenuController.clickOnlyForButtons = EditorGUILayout.Toggle("Click Only For Buttons", mainMenuController.clickOnlyForButtons);

        if (mainMenuController.usesClickSounds)  // if it true
        {
            // open new fields
            mainMenuController.includeEmptySound = EditorGUILayout.Toggle("Include Empty Sounds", mainMenuController.includeEmptySound);
            // set to false other option 
            mainMenuController.clickOnlyForButtons = false;
        }
        else if(mainMenuController.clickOnlyForButtons)
        {
            // open new fields
            mainMenuController.startButton = (Button)EditorGUILayout.ObjectField("Start Button", mainMenuController.startButton, typeof(Button), true);
            mainMenuController.settingsButton = (Button)EditorGUILayout.ObjectField("Settings Button", mainMenuController.settingsButton, typeof(Button), true);
            mainMenuController.quitButton = (Button)EditorGUILayout.ObjectField("Quit Button", mainMenuController.quitButton, typeof(Button), true);
            mainMenuController.settingsBackButton = (Button)EditorGUILayout.ObjectField("Quit Button", mainMenuController.settingsBackButton, typeof(Button), true);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

}

public class MainMenuController : MonoBehaviour
{
    //Menus
    public GameObject defaultMenu;
    public GameObject settingsMenu;

    // other buttons
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button settingsBackButton;

    //Click Sounds parameters
    public bool clickOnlyForButtons;
    public bool usesClickSounds;
    public bool includeEmptySound;

    private int rand;

    private void Start()
    {
        if (!clickOnlyForButtons)
            return;

        startButton.onClick.AddListener(PlayClickSound);
        settingsButton.onClick.AddListener(PlayClickSound);
        quitButton.onClick.AddListener(PlayClickSound);
        settingsBackButton.onClick.AddListener(PlayClickSound);
    }
    private void OnDisable()
    {
        if (!clickOnlyForButtons)
            return;

        startButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)
            CloseSettings();

        if (!usesClickSounds)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (includeEmptySound)
                rand = Random.Range(0, 3);
            else
                rand = Random.Range(1, 3);

            PlayRandomSound(rand);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        defaultMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        defaultMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void PlayClickSound()
    {
        AudioManager.instance.PlayAudio(SFXType.ClickSound2);
    }

    /// <summary>
    /// Random must be within the numbers 0 and exclusive 3
    /// </summary>
    /// <param name="caseRandom"></param>
    private void PlayRandomSound(int caseRandom)
    {
        switch (caseRandom)
        {
            case 1:
                AudioManager.instance.PlayAudio(SFXType.ClickSound1);
                break;
            case 2:
                AudioManager.instance.PlayAudio(SFXType.ClickSound2);
                break;
            default:
                break;

        }
    }

    //public void FixButton(Button button)
    //{
    //    button.enabled = false;
    //    button.enabled = true;
    //}
}
