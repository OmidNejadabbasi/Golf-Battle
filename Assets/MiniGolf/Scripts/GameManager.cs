using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevelIndex;
    public GameStatus gameStatus = GameStatus.None;

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
}

[System.Serializable]
public enum GameStatus
{
    None,
    Playing,
    Failed,
    Complete
}