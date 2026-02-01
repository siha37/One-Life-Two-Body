using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBasic : MonoBehaviour
{
    protected Gamemanager myChar;
    protected float speed =0;
    public float damage =0;
    protected float time;
    protected float DestoryTime;
    protected float Penetrate;
    protected Rigidbody2D rd;
    [SerializeField] protected AudioSource audio_s;
    [SerializeField] protected AudioClip clip;
    private void Awake()
    {
        myChar = Gamemanager.myChar;
    }
    protected void BulletDestroy(float time , Collider2D col)
    {
        StartCoroutine(BulletDestoryCoroutine(time,col));
    }
    protected IEnumerator BulletDestoryCoroutine(float time, Collider2D col)
    {
        yield return new WaitForSeconds(time);
        col.enabled = false;
        Vector3 Osize = transform.localScale;
        speed = 0;
        for (float i = 1; i >= 0f; i -= 0.01f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Osize, i);
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
    protected IEnumerator DownSpeed()
    {
        for (float i = 1; i >= 0.8f; i -= 0.01f)
        {
            speed = Mathf.Lerp(0, speed, i);
            rd.velocity = Vector3.up * speed * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
    protected void Sound_Play()
    {
        audio_s.PlayOneShot(clip);
    }
}
