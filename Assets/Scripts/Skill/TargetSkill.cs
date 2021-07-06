using MobaCommon.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选择目标型的技能
/// </summary>
public class TargetSkill : MonoBehaviour
{
	/// <summary>
	/// 目标
	/// </summary>
	private Transform target;

	/// <summary>
	/// 技能ID
	/// </summary>
	private int skillId;

	/// <summary>
	/// 攻击者ID
	/// </summary>
	private int attackId;
	
	/// <summary>
	/// 目标ID
	/// </summary>
	private int targetId;

	/// <summary>
	/// 是否需要发送
	/// </summary>
	private bool send;

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="target">目标坐标</param>
	/// <param name="skillId">技能ID</param>
	/// <param name="attackId">攻击者ID</param>
	/// <param name="targetId">目标ID</param>
	/// <param name="send">是否发送</param>
	public void Init(Transform target,int skillId,int attackId,int targetId,bool send)
    {
		this.target = target;
		this.skillId = skillId;
		this.attackId = attackId;
		this.targetId = targetId;
		this.send = send;
    }

    private void Update()
    {
		//检测有没有目标
		if (target == null)
			return;

		//差值移动
		Vector3.Lerp(transform.position, target.position, 0.1f);
		float d = Vector3.Distance(transform.position, target.position);
		if (d > 1f)
			return;
		if(send)
        {
			//防止重复发送
			send = false;
			//发起伤害请求:攻击者ID,技能ID,目标ID
			PhotonManager.Instance.Request(OpCode.FightCode, OpFight.Damage, attackId, skillId, new int[] { targetId });

        }
		//销毁物体
		PoolManager.Instance.HideObjet(gameObject);
    }

}
