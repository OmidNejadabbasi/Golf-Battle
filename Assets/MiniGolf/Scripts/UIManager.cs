using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to control game UI
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image powerBar;        //ref to powerBar image
    [SerializeField] private Text shotText;         //ref to shot info text
    [SerializeField] private GameObject mainMenu, gameUI, gameOverPanel, retryBtn, nextBtn;   //important gameobjects
    [SerializeField] private GameObject container, lvlBtnPrefab;    //important gameobjects

    public Text ShotText { get { return shotText; } }   //getter for shotText
    public Image PowerBar { get => powerBar; }          //getter for powerBar

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        powerBar.fillAmount = 0;                        //set the fill amount to zero
    }

    void Start()
    {
        if (GameManager.instance.gameStatus == GameStatus.None)    //if gamestatus is none
        {   
            CreateLevelButtons();
        }     //we check for game status, failed or complete
        else if (GameManager.instance.gameStatus == GameStatus.Failed ||
            GameManager.instance.gameStatus == GameStatus.Complete)
        {
            mainMenu.SetActive(false);
            gameUI.SetActive(true);
            LevelManager.instance.SpawnLevel(GameManager.instance.currentLevelIndex);  //spawn level
        }
    }

    void CreateLevelButtons()
    {
        //total count is number of level datas
        for (int i = 0; i < LevelManager.instance.levelDatas.Length; i++)
        {
            GameObject buttonObj = Instantiate(lvlBtnPrefab, container.transform);
            buttonObj.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnClick(button));
        }
    }

    void OnClick(Button btn)
    {
        mainMenu.SetActive(false);                                                      //deactivate main menu
        gameUI.SetActive(true);                                                       //activate game manu
        GameManager.instance.currentLevelIndex = btn.transform.GetSiblingIndex(); ;    //set current level equal to sibling index on button
        LevelManager.instance.SpawnLevel(GameManager.instance.currentLevelIndex);      //spawn level
    }

    public void GameResult()
    {
        switch (GameManager.instance.gameStatus)
        {
            case GameStatus.Complete:                       //if completed
                gameOverPanel.SetActive(true);              //activate game finish panel
                nextBtn.SetActive(true);                    //activate next button
                SoundManager.instance.PlayFx(FxTypes.GAMECOMPLETEFX);
                break;
            case GameStatus.Failed:                         //if failed
                gameOverPanel.SetActive(true);              //activate game finish panel
                retryBtn.SetActive(true);                   //activate retry button
                SoundManager.instance.PlayFx(FxTypes.GAMEOVERFX);
                break;
        }
    }

    public void HomeBtn()
    {
        GameManager.instance.gameStatus = GameStatus.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
