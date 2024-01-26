using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject ballPrefab;
    public Vector3 ballSpawnPos;

    public LevelData[] levelDatas;

    private int shotCount = 0;             

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
    }

    /// <summary>
    /// Method to spawn level
    /// </summary>
    public void SpawnLevel(int levelIndex)
    {
        //we spawn the level prefab at required position
        Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        shotCount = levelDatas[levelIndex].shotCount;
        UIManager.instance.ShotText.text = shotCount.ToString();   //set the ShotText text
                                                                   //then we Instantiate the ball at spawn position
        GameObject ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);
        CameraFollow.instance.SetTarget(ball);                      //set the camera target
        GameManager.instance.gameStatus = GameStatus.Playing;      //set the game status to playing
    }

    /// <summary>
    /// Method used to reduce shot
    /// </summary>
    public void ShotTaken()
    {
        if (shotCount > 0)                                          //if shotcount is more than 0
        {
            shotCount--;                                            //reduce it by 1
            UIManager.instance.ShotText.text = "" + shotCount;      //set the text

            if (shotCount <= 0)                                     //if shotCount is less than 0
            {
                LevelFailed();                                          //Level failed
            }
        }
    }

    /// <summary>
    /// Method called when player failed the level
    /// </summary>
    public void LevelFailed()
    {
        if (GameManager.instance.gameStatus == GameStatus.Playing) //check if the gamestatus is playing
        {
            GameManager.instance.gameStatus = GameStatus.Failed;   //set gamestatus to failed
            UIManager.instance.GameResult();                        //call GameResult method
        }
    }

    /// <summary>
    /// Method called when player win the level
    /// </summary>
    public void LevelComplete()
    {
        if (GameManager.instance.gameStatus == GameStatus.Playing) //check if the gamestatus is playing
        {    //check if currentLevelIndex is less than total levels available
            if (GameManager.instance.currentLevelIndex < levelDatas.Length)    
            {
                GameManager.instance.currentLevelIndex++;  //increase the count by 1
            }
            else
            {
                //else start from level 0
                GameManager.instance.currentLevelIndex = 0;
            }
            GameManager.instance.gameStatus = GameStatus.Complete; //set gamestatus to Complete
            UIManager.instance.GameResult();                        //call GameResult method
        }
    }
}
