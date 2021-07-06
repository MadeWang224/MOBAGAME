using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔的敌人检测
/// </summary>
public class TurretCheck : MonoBehaviour
{
    //表示当前炮塔的队伍
    [SerializeField]
    private int team;
    public void SetTeam(int team)
    { 
        this.team = team; 
    }

    /// <summary>
    /// 检测到的敌人列表
    /// </summary>
    public List<BaseControl> conList = new List<BaseControl>();

    private void OnTriggerEnter(Collider other)
    {
        BaseControl con = other.GetComponent<BaseControl>();
        if(con!=null&&con.Model.Team!=team)
        {
            conList.Add(con);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BaseControl con = other.GetComponent<BaseControl>();
        if(con!=null&&conList.Contains(con))
        {
            conList.Remove(con);
        }

    }
}
