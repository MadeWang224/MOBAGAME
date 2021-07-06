using MobaCommon.Code;
using MobaCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    [SerializeField]
    private KeyCode Skill_E = KeyCode.E;

    [SerializeField]
    private UISkill uiSkill_E;

    private void Update()
    {
        #region 鼠标右键点击

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouse = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit[] his = Physics.RaycastAll(ray);
            for (int i = his.Length-1; i >= 0 ; i--)
            {
                RaycastHit hit = his[i];
                //如果点到了敌方单位就攻击
                if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
                {
                    attack(hit.collider.gameObject);
                    break;
                }
                //如果点到了地面,移动
                else if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
                {
                    Move(hit.point);
                }
            }
        }

        #endregion

        #region 空格

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //聚焦到自己的英雄
            Camera.main.GetComponent<CameraControl>().FocusOn();
        }

        #endregion

        #region 技能释放

        if(Input.GetKeyDown(Skill_E)&& uiSkill_E.CanUse)
        {
            Vector2 mouse = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //释放技能
                skill(3, hit.point);
            }
        }

        #endregion
    }

    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="target"> 目标</param>
    private void attack(GameObject target)
    {
        //获取目标的ID
        int targetId = target.GetComponent<BaseControl>().Model.Id;
        //int myId = GameData.MyControl.Model.Id;
        //向服务器发起请求:技能的ID,攻击者ID,目标ID
        int attackId = GameData.MyControl.Model.Id;
        PhotonManager.Instance.Request(OpCode.FightCode, OpFight.Skill, 1,attackId, targetId,-1f,-1f,-1f);

    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="point"></param>
    private void Move(Vector3 point)
    {
        //显示一个特效
        GameObject go = PoolManager.Instance.GetObject("ClickMove");
        go.transform.position = point + Vector3.up;
        //给服务器发一个请求
        PhotonManager.Instance.Request(OpCode.FightCode, OpFight.Walk, point.x, point.y, point.z);
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="index"></param>
    /// <param name="targetPos"></param>
    private void skill(int index,Vector3 targetPos)
    {
        HeroModel myHero = (HeroModel)GameData.MyControl.Model;
        int skillId = myHero.Skills[index - 1].Id;
        int attackId = myHero.Id;
        PhotonManager.Instance.Request(OpCode.FightCode, OpFight.Skill, skillId, attackId, -1, targetPos.x, targetPos.y, targetPos.z);
    }
}
