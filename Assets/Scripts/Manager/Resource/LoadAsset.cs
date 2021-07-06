using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源类
/// </summary>
public class LoadAsset 
{
	/// <summary>
	/// 资源信息
	/// </summary>
	public ResourceRequest Request;

	/// <summary>
	/// 资源名称
	/// </summary>
	public string AssetName;

	/// <summary>
	/// 资源类型
	/// </summary>
	public Type AssetType;

	/// <summary>
	/// 加载是否完成
	/// </summary>
	public bool IsDone
    {
        get { return Request != null && Request.isDone; }
    }

	/// <summary>
	/// 获取资源
	/// </summary>
	public object GetAsset
    {
        get 
		{
			if (Request == null)
				return null;
			return Request.asset; 
		}
    }

	/// <summary>
	/// 异步加载
	/// </summary>
	public void LoadAsync()
    {
		this.Request = Resources.LoadAsync(AssetName, AssetType);
    }

	/// <summary>
	/// 回调的集合
	/// </summary>
	public List<IResourceListener> Listeners; 

	/// <summary>
	/// 添加回调
	/// </summary>
	/// <param name="listener"></param>
	public void AddListener(IResourceListener listener)
    {
		if(Listeners==null)
        {
			Listeners = new List<IResourceListener>();
        }
		if(Listeners.Contains(listener))
        {
			return;
        }
		Listeners.Add(listener);
    }
}
