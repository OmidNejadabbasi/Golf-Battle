using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxSource;                            //reference to audiosource which we will use for FX
    public AudioClip gameOverFx, gameCompleteFx, shotFx;    //fx audio clips

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

    public void PlayFx(FxTypes fxTypes)
    {
        switch (fxTypes)                                //switch case is used to run respective logic for respective FxType
        {
            case FxTypes.GAMEOVERFX:                    //if its GAMEOVER
                fxSource.PlayOneShot(gameOverFx);       //play GAMEOVER fx
                break;
            case FxTypes.GAMECOMPLETEFX:                //if its GAMEWIN
                fxSource.PlayOneShot(gameCompleteFx);   //play GAMEWIN fx
                break;
        }

    }
}

public enum FxTypes
{
    GAMEOVERFX, 
    GAMECOMPLETEFX, 
    SHOTFX
}
