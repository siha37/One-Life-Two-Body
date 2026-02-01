using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Pause_Panel : MonoBehaviour
{
    [SerializeField]
    private Level_UP skill_data;
    [SerializeField]
    private GameObject skill_prefab;
    [SerializeField]
    private Transform SpawnTarget;
    private List<Pause_Skill_Icon> UI_Skills = new List<Pause_Skill_Icon>();
    private List<TOP_Projectile> p1_skills;
    private List<TOP_Projectile> p2_skills;

    public void Skill_Data_set()
    {
        if (skill_data.Active_P_Skill != null)
        {
            p1_skills = skill_data.Active_P_Skill;
        }
        if (skill_data.Active_A_Skill != null)
        {
            p2_skills = skill_data.Active_A_Skill;
        }
        if(UI_Skills.Count < p1_skills.Count+p2_skills.Count)
        {
            int CreatNUM = p1_skills.Count + p2_skills.Count - UI_Skills.Count;
            for(int j =0; j < CreatNUM; j++ )
            {
                UI_Skills.Add(Instantiate(skill_prefab, SpawnTarget.transform).GetComponent<Pause_Skill_Icon>());
            }
        }
        int i =0;
        foreach (TOP_Projectile item in p1_skills)
        {
            UI_Skills[i].DataSet(item.Skill_UI_Image, item.GetLEVEL);
            i++;
        }
        foreach (TOP_Projectile item in p2_skills)
        {
            UI_Skills[i].DataSet(item.Skill_UI_Image, item.GetLEVEL);
            i++;
        }
    }
}
