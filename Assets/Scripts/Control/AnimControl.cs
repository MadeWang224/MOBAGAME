using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画控制脚本
/// </summary>
public class AnimControl : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	/// <summary>
	/// 播放闲置动画
	/// </summary>
	public void Free()
    {
		animator.SetBool("WALK", false);
    }

	/// <summary>
	/// 播放攻击动画
	/// </summary>
	public void Attack()
    {
		animator.SetBool("WALK", false);
		animator.SetTrigger("ATTACK");
    }

	/// <summary>
	/// 播放行走动画
	/// </summary>
	public void Walk()
    {
		animator.SetBool("WALK", true);
	}

	/// <summary>
	/// 播放死亡动画
	/// </summary>
	public void Death()
    {
		animator.SetBool("WALK", false);
		animator.SetTrigger("DEATH");
	}
}

/// <summary>
/// 动画状态
/// </summary>
public enum AnimState
{
	FREE,
	WALK,
	ATTACK,
	DEATH
}
