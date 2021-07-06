using ExitGames.Client.Photon;
using MobaCommon.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountReceiver : MonoBehaviour, IReceiver
{
    [SerializeField]
    private AccountView view;

    public void OnReceive(byte subCode, OperationResponse response)
    {
        switch(subCode)
        {
            case OpAccount.Login:
                onLogin(response.ReturnCode,response.DebugMessage);
                break;
            case OpAccount.Register:
                onRegister(response.ReturnCode,response.DebugMessage);
                break;
        }
    }

    /// <summary>
    /// 登录的处理
    /// </summary>
    /// <param name="retCode"></param>
    private void onLogin(short retCode,string mess)
    {
        switch (retCode)
        {
            case 0:
                //成功 进入下一个场景
                UIManager.Instance.HideUIPanel(UIDefinit.UIAccount);
                UIManager.Instance.ShowUIPanel(UIDefinit.UIMain);
                break;
            case -1:
                //失败 玩家在线
                MessageTip.Instance.Show(mess);
                view.EnterInteractable = true;
                view.ResetLoginInput();
                break;
            case -2:
                //失败 账号密码错误
                MessageTip.Instance.Show(mess);
                view.EnterInteractable = true;
                view.ResetLoginInput();
                break;
        }
    }
    /// <summary>
    /// 注册的处理
    /// </summary>
    /// <param name="retCode"></param>
    private void onRegister(short retCode,string mess)
    {
        switch (retCode)
        {
            case 0:
                //成功
                MessageTip.Instance.Show(mess);
                break;
            case -1:
                //失败 账号重复
                MessageTip.Instance.Show(mess);
                break;
        }
    }
}
