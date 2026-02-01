using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI_Effect : MonoBehaviour
{
    TextMeshPro m_TextMeshPro;
    Color m_Color;
    float Alpha = 1;
    [SerializeField] float Plus_Alpha;

    [SerializeField] private float limit;
    float time;

    [SerializeField] private float speed;

    private void Start()
    {
        time = 0;
        m_TextMeshPro = GetComponent<TextMeshPro>();
        m_Color = m_TextMeshPro.color;
    }

    private void Update()
    {
        if(time >= limit)
        {
            time = 0;
            Alpha -= Plus_Alpha;
            m_Color.a = Alpha;
            m_TextMeshPro.color = m_Color;
            if (Alpha <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            time+=Time.deltaTime;
        }
        transform.Translate(new Vector3(0,speed,0)*Time.deltaTime);
    }
}
