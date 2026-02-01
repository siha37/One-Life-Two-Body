using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using UnityEngine.Audio;
internal static class YieldInstructionCache
{
    class FloatComparer : IEqualityComparer<float>
    {
        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
        bool IEqualityComparer<float>.Equals (float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode (float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!_timeInterval.TryGetValue(seconds, out wfs))
            _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}


public class Gamemanager : MonoBehaviour
{
    private static Gamemanager gamemanager = null;

    private void Start()
    {
        if (gamemanager != null)
        {
            if (gamemanager != this)
            {
                Destroy(gameObject);
            }
        }
        Option_Load();
    }
    public static Gamemanager myChar
    {
        get
        {
            if (gamemanager == null)
            {
                gamemanager = FindObjectOfType(typeof(Gamemanager)) as Gamemanager;
                if (gamemanager == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    gamemanager = obj.AddComponent(typeof(Gamemanager)) as Gamemanager;
                    DontDestroyOnLoad(obj);
                }
            }
            return gamemanager;
        }
    }

    public CinemachineVirtualCamera CinemachinCam;
    public Main_PostProcessing MainVolume;
    public GameObject Player;
    public GameObject P2;
    public GameObject EXPS;
    public float CurrentTIMER;
    public GameObject BulletCollection;
    public Transform EnemyDamageUI_Collection;
    public Enemy_Database Enemy_database;


    //Pause
    public bool Pause =false;

    //OPTION
    public bool ABLE_DamageUI = true;
    public float MASTER_Volume = 0;
    public float SFX_Volume = 0f;
    public float BGM_Volume = 0f;

    //Cheet
    public bool CHEETING = true;

    //Audio
    public AudioMixer audioMixer;

    public void Option_Save()
    {
        SoundSave();
    }
    public void Option_Load()
    {
        SoundLoad();
    }
    void SoundSave()
    {
        PlayerPrefs.SetFloat("Master",MASTER_Volume);
        PlayerPrefs.SetFloat("BGM", BGM_Volume);
        PlayerPrefs.SetFloat("SFX", SFX_Volume);
    }

    void SoundLoad()
    {
        if(PlayerPrefs.HasKey("Master"))
        {
            MASTER_Volume = PlayerPrefs.GetFloat("Master");
            BGM_Volume = PlayerPrefs.GetFloat("BGM");
            SFX_Volume = PlayerPrefs.GetFloat("SFX");
            audioMixer.SetFloat("Master", MASTER_Volume);
            audioMixer.SetFloat("BGM", BGM_Volume);
            audioMixer.SetFloat("SFX", SFX_Volume);
        }
    }
}
