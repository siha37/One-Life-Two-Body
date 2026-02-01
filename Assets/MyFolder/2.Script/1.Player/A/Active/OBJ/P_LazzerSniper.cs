using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LazzerSniper : BulletBasic
{
    public void Shot(float _damage)
    {
        damage = _damage;
        LazzerDestory(0.2f,this.GetComponent<BoxCollider2D>());
    }
    public void Shot(float _damage,float Size)
    {
        damage = _damage;
        Vector3 size = this.transform.localScale;
        size.x = Size;
        transform.localScale = size;
        LazzerDestory(0.2f, this.GetComponent<BoxCollider2D>());
    }

    void LazzerDestory(float time,Collider2D col)
    { 
        StartCoroutine(LazzerDestory_Delay(time,col));
    }
    IEnumerator LazzerDestory_Delay(float time,Collider2D col)
    {
        yield return new WaitForSeconds(time);
        col.enabled = false;
        Vector3 Osize = transform.localScale;
        Vector3 TargetSize = new Vector3(0, transform.localScale.y, 1);
        for (float i = 1; i >= 0f; i -= 0.01f)
        {
            transform.localScale = Vector3.Lerp(TargetSize, Osize, i);
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            col.GetComponent<Enemy_Status>().Hit(damage);
        }
    }
}
