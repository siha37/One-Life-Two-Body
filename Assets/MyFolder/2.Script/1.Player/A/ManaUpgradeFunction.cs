using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUpgradeFunction : MonoBehaviour
{
    public float UP_Damage(float _damage,float multiple)
    {
        if (multiple != 0)
        {
            return _damage * multiple;
        }
        return _damage;
    }
    public float UP_Speed(float _speed,float multiple)
    {
        if (multiple != 0)
        {
            return _speed * multiple;
        }
        return _speed;
    }
    public int UP_ProjectileAmount(int _projectilAmount,float multiple)
    {
        if (multiple != 0)
        {
            return (int)(_projectilAmount * multiple);
        }
        return _projectilAmount;
    }
    public Vector3 UP_Size(Vector3 _size ,float multiple)
    {
        if (multiple != 0)
        {
            return _size * multiple;
        }
        return _size;
    }
    public float UP_Size(float _size, float multiple)
    {
        if(multiple != 0)
        {
            return _size * multiple;
        }
        return _size;
    }
    public int UP_Penetrate_Count(int penetrate_count, float multiple)
    {
        if (multiple != 0)
        {
            return (int)(penetrate_count * multiple);
        }
        return penetrate_count;
    }
    public float UP_Distance(float _distance,float multiple)
    {
        if (multiple != 0)
        {
            return _distance * multiple;
        }
        return _distance;
    }
}
