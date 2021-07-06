using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI的基类
/// </summary>
public abstract class UIBase : MonoBehaviour
{
	/// <summary>
	/// 名称
	/// </summary>
	public abstract string UIName();

	protected CanvasGroup canvasGroup;

    private void Awake()
    {
		canvasGroup = gameObject.AddComponent<CanvasGroup>();

		Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public abstract void Init();

	/// <summary>
	/// 显示
	/// </summary>
	public virtual void OnShow()
    {
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
	}

	/// <summary>
	/// 隐藏
	/// </summary>
	public virtual void OnHide()
	{
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}

	/// <summary>
	/// 销毁
	/// </summary>
	public abstract void OnDestroy();
}
