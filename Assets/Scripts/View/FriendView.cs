using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendView : MonoBehaviour
{
	public int Id;

	[SerializeField]
	private Text txtName;

	[SerializeField]
	private Text txtState;

	[SerializeField]
	private Image imgBg;

	/// <summary>
	/// 显示更新
	/// </summary>
	/// <param name="name"></param>
	/// <param name="state"></param>
	public void InitView(int id,string name,bool isOnline)
    {
		this.Id = id;
		txtName.text = name;
		string state = isOnline ? "在线" : "离线";
		txtState.text = "状态 : " + state;
		imgBg.color = isOnline ? Color.green : Color.red;
    }

	public void UpdateView(bool isOnline)
    {
		string state = isOnline ? "在线" : "离线";
		txtState.text = "状态:" + state;
		imgBg.color = isOnline ? Color.green : Color.red;
	}
}
