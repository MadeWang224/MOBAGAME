using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源回调监听接口
/// </summary>
public interface IResourceListener 
{
	/// <summary>
	/// 加载完成时调用
	/// </summary>
	/// <param name="asset">资源</param>
	void OnLoaded(string assetName, object asset);
}
