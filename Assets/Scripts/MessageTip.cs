using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageTip :Singleton<MessageTip>
{
    /// <summary>
    /// 显示文字
    /// </summary>
	[SerializeField]
	private Text txtContent;
    /// <summary>
    /// 物体
    /// </summary>
    [SerializeField]
    private GameObject tip;
    /// <summary>
    /// 点击之后调用
    /// </summary>
    private Action onCompleted;

    /// <summary>
    /// 显示文字
    /// </summary>
    /// <param name="text"></param>
    public void Show(string text,Action action=null,float showTime=-1)
    {
        tip.SetActive(true);
        txtContent.text = text;
        onCompleted = action;
        if(showTime!=-1)
        {
            Invoke("Hide", showTime);
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public void Hide()
    {
        tip.SetActive(false);
    }

    /// <summary>
    /// 点击确定按钮
    /// </summary>
    public void OnClick()
    {
        tip.SetActive(false);
        if(onCompleted!=null)
        {
            onCompleted();
            onCompleted = null;
        }
    }
}
