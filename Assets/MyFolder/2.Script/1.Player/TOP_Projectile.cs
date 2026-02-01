using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SHOTTING;
using Spine.Unity;


#if UNITY_EDITOR
namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute), true)]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        // Necessary since some properties tend to collapse smaller than their content
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        // Draw a disabled property field
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = !Application.isPlaying && ((ReadOnlyAttribute)attribute).runtimeOnly;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif
[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute
{
    public readonly bool runtimeOnly;
    public ReadOnlyAttribute(bool runtimeOnly = false)
    {
        this.runtimeOnly = runtimeOnly;
    }
}


namespace SHOTTING
{
    public enum ShotType { Click =0,ClickANDHold =1, Hold=2,Passive=3 }
    public enum Use_Resource {NONE =0, MANA=1, ENERGY=2 }
}
public class TOP_Projectile : MonoBehaviour
{
    #region Data
    //Testing Data
    [SerializeField] protected bool Testing = false;

    //TOP DATA
    protected Gamemanager myChar;
    TopGameData TOP;
    protected ManaUpgradeFunction UP_function;

    [Tooltip("플레이어 데이터")]
    [ReadOnly][SerializeField] protected GameObject Player;
    [Tooltip("총알 부모 오브젝트")]
    [ReadOnly][SerializeField] protected GameObject BulletCollection;
    [Tooltip("스킬 UI 스프라이트")]
    [SerializeField] public Sprite Skill_UI_Image;
    [Tooltip("Player_Sound_Manager")]
    [SerializeField] protected Shot_Sound_Manager Sound_Manager;
    [Tooltip("MAXLEVEL")]
    public bool MaxLevel_Chack = false;
    public string Pivot_boneName;
    public string Charging_anim;
    public string Shot_anim;



    //All
    [Tooltip("발사하는 방식 타입")]
    [ReadOnly] public ShotType S_TYPE;
    [Tooltip("소모 자원 타입")]
    [ReadOnly] public Use_Resource Resource_TYPE;
    [Tooltip("이름")]
    [ReadOnly] public string Name;
    [Tooltip("해당 스킬 최고 레벨")]
    protected int MaxLevel;
    [Tooltip("이펙트 애니메이션 넘버")]
    protected int EffectNUM;
    [Tooltip("현 레벨")]
    [ReadOnly] [SerializeField] protected int Level =0;
    public int GetLEVEL { get { return Level; } }
    [Tooltip("소모 자원 양")]
    protected float Resource_Amount;
    [Tooltip("스피드(%)")]
    protected float Per_Speed;
    [Tooltip("공격량(%)")]
    protected float Per_Damage;
    [Tooltip("현 스피드")]
    protected float BasicSpeed;
    [Tooltip("현 공격량")]
    protected float BasicDamage;
    [Tooltip("발사 후 딜레이")]
    protected float Shot_Delay;
    [Tooltip("차징 시 딜레이")]
    protected float Charging_Delay;
    [Tooltip("발사체 유지 시간")]
    protected float Destroy_Time_Pjt;
    [Tooltip("차징 가능 횟수")]
    protected int Charging_Count;
    [Tooltip("발사체의 관통 가능 횟수")]
    protected int Penetrate_Count;
    [Tooltip("한번 발사 당 발사체 개수")]
    protected int Projectile_Amount;
    [Tooltip("레벨 업 시 변동점 설명문")]
    public string NextLevel_Ex;
    [Tooltip("현 쿨타임")]
    protected float CurrentyCoolTime;
    [Tooltip("너백 파워")]
    protected float KnockBack_Power;
    [Tooltip("공격력 증가")]
    protected float UP_Damage =0;
    [Tooltip("스피드 증가")]
    protected float UP_Speed = 0;
    [Tooltip("총알 개수 증가")]
    protected float UP_ProjectileAmount = 0;
    [Tooltip("사이즈 증가")]
    protected float UP_Size = 0;
    [Tooltip("관통 증가")]
    protected float UP_Penetrate_Count = 0;
    [Tooltip("길이 증가")]
    protected float UP_Distance = 0;
    [Header("SOUND")]
    [Tooltip("발사 시 소리")]
    [SerializeField] protected AudioClip[] Shoting_Sound =  new AudioClip[1];
    [Tooltip("차징 시 소리")]
    [SerializeField] protected AudioClip Charging_Sound;
    /// <summary>
    /// 현 쿨타임 접근 프로퍼티
    /// </summary>
    public float GET_SET_CurrentTime { get { return CurrentyCoolTime; }set { CurrentyCoolTime = value; } }

    //Only Hold
    [ReadOnly] public bool Shoting;

    //Passive 
    protected float NowTime;
    

    protected Arm_Shot Shot;
    protected Level_UP level_up_;
    protected GameObject Projectile;
    protected Transform Spawn_Point;
    protected Transform Pivot_Rot;
    protected Projectile_Data_SAVE_LOAD dataBase;
    protected P_Status p_Status;
    protected Level_UP level_UP;

    #endregion

    #region DATA_SETTING
    protected void BaseData_Input(bool Active)
    {
        myChar = Gamemanager.myChar;
        TOP = TopGameData.Dataset;
        level_UP = myChar.Player.GetComponent<Level_UP>();  
        UP_function = GetComponent<ManaUpgradeFunction>();
        Conect_P();
        if (Active)
        {
            Spawn_Point = transform.GetChild(0).transform;
            Sound_Manager = transform.parent.GetChild(1).GetComponent<Shot_Sound_Manager>();
            Pivot_Rot = transform.parent;
            Shot = GetComponent<Arm_Shot>();
        }
        else
        {
            Spawn_Point = transform;
            level_up_ = transform.parent.GetComponent<Level_UP>();
        }

        if (Testing)
        {
            LEVEL_UP();
        }
    }
    virtual public void Data_Input()
    {
        Shot.Shot_Delay = Shot_Delay;
        Shot.Charging_Delay = Charging_Delay;
        Shot.Charge_Count = Charging_Count;
        Shot.Resource_Type = Resource_TYPE;
        Shot.Resource_Amount = Resource_Amount;
        Shot.PivotBone_Set(Pivot_boneName);
    }
    private void Conect_P()
    {
        Player = myChar.Player;
        BulletCollection = myChar.BulletCollection;
        dataBase = Player.GetComponent<Projectile_Data_SAVE_LOAD>();
        p_Status = Player.GetComponent<P_Status>();
        level_UP = Player.GetComponent<Level_UP>();
    }
    #endregion

    #region Projectile
    virtual public void ProjectileCreat()
    {
        switch (S_TYPE)
        {
            case ShotType.Click:
                Use_Resource(0);
                break;
            //지속형
            case ShotType.Hold:
                Use_Resource_Roof();
                break;
            case ShotType.Passive:
                Use_Resource(0);
                break;
            default:
                break;
        }
    }
    virtual public void ProjectileCreat(int _Charging_Count)
    {
        //switch (S_TYPE)
        //{
        //    //단일형
        //    case ShotType.ClickANDHold:
        //        Use_Resource(_Charging_Count);
        //        break;
        //    default:
        //        break;
        //}
    }
    virtual public void  Charging_Projectile_Set(int Bullet_Count)
    {

    }

    virtual public void StopShot()
    {

    }
    
    virtual public void Charing_Effect()
    {

    }

    #endregion

    #region LEVEL_SETTING
    virtual public bool LEVEL_UP()
    {
        if(MaxLevel == Level)
        {
            MaxLevel_Chack = true;
            return true;
        }
        return false;
    }
    public void Percent_Data()
    {
        BasicDamage =  TOP.Damage_P * Per_Damage/100;
        BasicSpeed = TOP.Speed_P * Per_Speed/100;
    }
    virtual public void Level_Setting(int _level)
    {
    }
    protected void First_Level_Set(Basic_DataBase dataBase)
    {
        Name = dataBase.Name;
        NextLevel_Ex = dataBase.NextLevel_Ex;
    }
    protected void Level_Setting(Basic_DataBase dataBase,int Count)
    {
        Level = dataBase.Level;
        if (dataBase.Level == 1)
        {
            MaxLevelSet(Count);
            Name = dataBase.Name;
            S_TYPE = (ShotType)dataBase.Shot_Type;
            Resource_TYPE = (SHOTTING.Use_Resource)dataBase.Resource_TYPE;
            UP_Damage = dataBase.UP_Damage;
            UP_Distance = dataBase.UP_Distance;
            UP_Speed = dataBase.UP_Speed;
            UP_Size = dataBase.UP_Size;
            UP_Penetrate_Count = dataBase.UP_Penetrate_Count;
            UP_ProjectileAmount = dataBase.UP_ProjectileAmount;
        }
        Resource_Amount = dataBase.Resource_Amount;
        Per_Damage = dataBase.Damage;
    }
    private void MaxLevelSet(int Count)
    {
        MaxLevel = Count;
    }
    #endregion

    #region RESOURCE
    private void Use_Resource_Roof()
    {
        Use_Resource(0);
        if (Shoting)
        {
            Invoke("Use_Resource_Roof",Time.deltaTime);
        }
    }
    protected void Use_Resource(int _Charging_Count)
    {
        float Amount = Resource_Amount;
        if (_Charging_Count != 0)
        {
            Amount = Resource_Amount * _Charging_Count;
        }
        p_Status.Use_Resource_Caclulation(Resource_TYPE, -Amount);
    }
    protected void Use_Resource_Passive(float Amount)
    {
        p_Status.Use_Resource_Caclulation_Passive(Resource_TYPE, -Amount);
    }
    #endregion

    #region UI_Data_GET
    public float Get_NowTime()
    {
        return NowTime;
    }
    public float Get_Shot_Dealy()
    {
        return Shot_Delay;
    }
    #endregion
}
