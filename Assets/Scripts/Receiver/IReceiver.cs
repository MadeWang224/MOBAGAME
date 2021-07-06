using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 收到服务器响应的接收接口
/// </summary>
public interface IReceiver 
{
	/// <summary>
	/// 收到服务器的响应
	/// </summary>
	/// <param name="subCode">子操作</param>
	/// <param name="response">对应的响应</param>
	void OnReceive(byte subCode, OperationResponse response);
}
