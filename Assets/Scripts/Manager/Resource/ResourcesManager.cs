using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源管理类
/// </summary>
public class ResourcesManager : Singleton<ResourcesManager>
{
	/// <summary>
	/// 已经加载的资源字典
	/// </summary>
	private Dictionary<string, object> nameAssetDict = new Dictionary<string, object>();

	/// <summary>
	/// 正在加载的列表
	/// </summary>
	private List<LoadAsset> loadingList = new List<LoadAsset>();

	/// <summary>
	/// 等待加载的队列
	/// </summary>
	private Queue<LoadAsset> waitingQue = new Queue<LoadAsset>();

    private void Update()
    {
        if(loadingList.Count>0)
        {
            for (int i = 0; i < loadingList.Count; i++)
            {
				if(loadingList[i].IsDone)
                {
					LoadAsset asset = loadingList[i];
                    for (int j = 0; j < asset.Listeners.Count; j++)
                    {
                        asset.Listeners[j].OnLoaded(asset.AssetName, asset.GetAsset);
                    }
                    nameAssetDict.Add(asset.AssetName, asset.GetAsset);
                    loadingList.RemoveAt(i);
                }
            }
        }
        while (waitingQue.Count > 0 &&loadingList.Count<5)
        {
            LoadAsset asset = waitingQue.Dequeue();
            loadingList.Add(asset);
            asset.LoadAsync();
        }
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="assetName">资源名</param>
    /// <param name="assetType">资源类型</param>
    /// <param name="listener">回调</param>
    public void Load(string assetName,Type assetType,IResourceListener listener)
    {
        //如果已经加载完成,直接返回
        if(nameAssetDict.ContainsKey(assetName))
        {
            listener.OnLoaded(assetName, nameAssetDict[assetName]);
            return;
        }
        else//还未加载完成,开始异步加载
        {
            LoadAsync(assetName, assetType, listener);
        }
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="assetName">资源名</param>
    /// <param name="assetType">资源类型</param>
    /// <param name="listener">回调</param>
    private void LoadAsync(string assetName, Type assetType, IResourceListener listener)
    {
        //资源正在被加载,还没加载完成
        foreach (LoadAsset item in loadingList)
        {
            if(item.AssetName==assetName)
            {
                item.AddListener(listener);
                return;
            }
        }
        //在等待的队列中
        foreach (LoadAsset item in waitingQue)
        {
            if (item.AssetName == assetName)
            {
                item.AddListener(listener);
                return;
            }
        }
        //都没有,先创建
        LoadAsset asset = new LoadAsset();
        asset.AssetName = assetName;
        asset.AssetType = assetType;
        asset.AddListener(listener);
        //添加到等待队列
        waitingQue.Enqueue(asset);
    }

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="assetName"></param>
    public object GetAsset(string assetName)
    {
        object asset = null;
        nameAssetDict.TryGetValue(assetName, out asset);
        return asset;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="assetName"></param>
    public void ReleaseAsset(string assetName)
    {
        if(nameAssetDict.ContainsKey(assetName))
        {
            nameAssetDict[assetName] = null;
            nameAssetDict.Remove(assetName);
        }
    }

    /// <summary>
    /// 全部释放
    /// </summary>
    public void ReleaseAll()
    {
        this.nameAssetDict.Clear();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
