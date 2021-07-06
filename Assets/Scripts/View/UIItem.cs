using MobaCommon.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    [SerializeField]
    private int Id;

    public void OnClick()
    {
        //向服务器发起购买请求
        PhotonManager.Instance.Request(OpCode.FightCode, OpFight.Buy, this.Id);
    }
}
