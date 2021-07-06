using MobaCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MobaCommon.Code;

public class UISkill : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IResourceListener
{
	/// <summary>
	/// 技能信息
	/// </summary>
	public SkillModel skill;
	/// <summary>
	/// 技能图标
	/// </summary>
	[SerializeField]
	private Image imgSkill;
	/// <summary>
	/// 遮罩显示
	/// </summary>
	[SerializeField]
	private Image imgMask;
	/// <summary>
	/// 升级按钮
	/// </summary>
	[SerializeField]
	private Button btnUp;
	/// <summary>
	/// 设置按钮是否可点
	/// </summary>
	public bool UpInteractable { set { btnUp.interactable = value; } }
	/// <summary>
	/// 技能是否可用
	/// </summary>
	private bool canUse;
	public bool CanUse { get { return canUse; } }
	/// <summary>
	/// 冷却时间
	/// </summary>
	private float cdTime;
	/// <summary>
	/// 当前时间
	/// </summary>
	private float curTime;

	/// <summary>
	/// 初始化技能
	/// </summary>
	/// <param name="skill"></param>
	public void Init(SkillModel skill)
    {
		//保存数据
		this.skill = skill;
		//加载图片

		ResourcesManager.Instance.Load(Paths.RES_SKILL + skill.Id, typeof(Sprite), this);
		//显示遮罩层
		imgMask.gameObject.SetActive(true);
    }

    private void Update()
    {
		//当技能不可用的时候,计算冷却
        if(!canUse)
        {
			curTime -= Time.deltaTime;
			if(curTime<=0)
            {
				//恢复技能使用
				canUse = true;
				cdTime = 0f;
				curTime = 0f;
				imgMask.gameObject.SetActive(false);
            }
			//给fillAmount赋值
			imgMask.fillAmount = curTime / cdTime;
        }
    }

	/// <summary>
	/// 使用技能
	/// </summary>
	/// <param name="cd"></param>
	public void Use(int cd)
    {
		if (!canUse)
			return;
		cdTime = cd;
		curTime = cd;
		//显示遮罩层
		imgMask.gameObject.SetActive(true);
		//设置不可用
		canUse = false;
    }

	/// <summary>
	/// 隐藏遮罩层
	/// </summary>
    public void Reset()
    {
		if (!canUse)
			return;
		if (skill.Level > 0)
			imgMask.gameObject.SetActive(false);
    }

	/// <summary>
	/// 当鼠标进入
	/// </summary>
	/// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //显示节能描述
    }

	/// <summary>
	/// 当鼠标离开
	/// </summary>
	/// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //关闭提示信息
    }

    public void OnLoaded(string assetName, object asset)
    {
		imgMask.sprite = asset as Sprite;
		imgSkill.sprite = asset as Sprite;
    }

	/// <summary>
	/// 当升级按钮点击时调用
	/// </summary>
	public void OnUpClick()
    {
		//向服务器发起一个升级的请求:技能的id
		PhotonManager.Instance.Request(OpCode.FightCode, OpFight.SkillUp, this.skill.Id);
    }
}
