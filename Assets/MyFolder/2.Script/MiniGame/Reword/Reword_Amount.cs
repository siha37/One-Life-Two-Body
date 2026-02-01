using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Reword_Amount : MonoBehaviour
{
    RewordManager manager;
    TextMeshProUGUI amount_text;
    Animator animator;

    [System.Serializable]
    public struct MinMax
    {
        public float min;
        public float max;
    }

    [SerializeField] List<MinMax> minmaxs = new List<MinMax>();

    public void Rorring_Start(RewordManager.RewordType _type)
    {
        float Amount =0 ;
        animator =GetComponent<Animator>();
        amount_text = GetComponent<TextMeshProUGUI>();
        manager = transform.parent.parent.GetComponent<RewordManager>();
        switch (_type)
        {
            case RewordManager.RewordType.HP_UP:
                Amount = Random.Range(minmaxs[0].min, minmaxs[0].max);
                break;
            case RewordManager.RewordType.ENRGY:
                Amount = Random.Range(minmaxs[0].min, minmaxs[0].max);
                break;
            case RewordManager.RewordType.MANA:
                Amount = Random.Range(minmaxs[0].min, minmaxs[0].max);
                break;
            case RewordManager.RewordType.SPEED:
                Amount = Random.Range(minmaxs[0].min, minmaxs[0].max);
                break;
            case RewordManager.RewordType.HEAL:
                Amount = Random.Range(minmaxs[0].min, minmaxs[0].max);
                break;
            default:
                break;
        }
        amount_text.text = ((int)Amount).ToString();
        if (_type == RewordManager.RewordType.SPEED)
        {
            amount_text.text = amount_text.text + "%";
        }
        animator.SetTrigger("SET");
        manager.Amount_get((int)Amount);
    }
    public void Text_disable()
    {
        amount_text.enabled = false;
    }
}
