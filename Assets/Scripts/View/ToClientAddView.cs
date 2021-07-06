using MobaCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToClientAddView : MonoBehaviour
{
	[SerializeField]
	private Text txtInfo;

    public int Id;

    public void UpdateView(PlayerDto player)
    {
        this.Id = player.id;
        txtInfo.text = string.Format("姓名:{0}\n等级: {1}\n好友个数: {2}\n逃跑场次: {3}", player.name, player.lv, player.friends.Length, player.runCount);
    }
}
