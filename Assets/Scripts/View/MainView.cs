using MobaCommon.Code;
using MobaCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : UIBase,IResourceListener
{
    AudioClip acClick;
    [HideInInspector]
    public AudioClip acCountDown;

    #region UIBase

    public override void Init()
    {
        acClick = ResourcesManager.Instance.GetAsset(Paths.GetSoundFullName("Click")) as AudioClip;
        ResourcesManager.Instance.Load(Paths.RES_SOUND_UI + "CountDown", typeof(AudioClip), this);

        //添加监听
        btnCreate.onClick.AddListener(OnCreateClick);

        //向服务器获取角色信息
        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.GetInfo);

    }

    public void OnLoaded(string assetName, object asset)
    {
        if (assetName == Paths.RES_SOUND_UI + "CountDown")
            acCountDown = asset as AudioClip;
    }

    public override void OnDestroy()
    {

    }

    public override string UIName()
    {
        return UIDefinit.UIMain;
    }

    #endregion

    #region 创建模块
    [Header("创建模块")]
    [SerializeField]
    private InputField inName;
    [SerializeField]
    private Button btnCreate;
    [SerializeField]
    private GameObject createPanel;

    /// <summary>
    /// 创建面板可见
    /// </summary>
    public bool CreatePanelActive
    {
        set { createPanel.SetActive(value); }
    }
    /// <summary>
    /// 创建按钮能否点击
    /// </summary>
    public bool CreateInteractable
    {
        set { btnCreate.interactable = value; }
    }

    public void OnCreateClick()
    {
        //播放声音
        SoundManager.Instance.PlayEffectMusic(acClick);

        //输入检测
        if (string.IsNullOrEmpty(inName.text))
            return;

        //发起创建请求
        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.Create, inName.text);

        //禁用按钮
        CreateInteractable = false;
    }

    #endregion

    [Header("角色信息")]
    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Slider barExp;


    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateView(PlayerDto player)
    {
        //名字
        txtName.text = player.name;
        //经验
        barExp.value = (float)player.exp / (player.lv * 100);
        //加载好友列表
        Friend[] friends = player.friends;
        friendList.Clear();
        GameObject go = null;
        foreach (Friend item in friends)
        {
            if (item == null)
                continue;
            go = Instantiate(UIFriend);
            go.transform.SetParent(friendTran);
            FriendView fv = go.GetComponent<FriendView>();
            fv.InitView(item.Id, item.Name, item.IsOnline);
            friendList.Add(fv);
        }
    }

    #region 好友模块

    [SerializeField]
    private Transform friendTran;

    private List<FriendView> friendList = new List<FriendView>();

    [Header("好友信息的预设")]
    [SerializeField]
    private GameObject UIFriend;

    [SerializeField]
    private InputField inAddName;


    /// <summary>
    /// 点击添加好友按钮
    /// </summary>
    public void OnAddClick()
    {
        //播放声音
        SoundManager.Instance.PlayEffectMusic(acClick);
        //输入检测
        if (string.IsNullOrEmpty(inAddName.text))
            return;
        //发送添加好友请求
        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.RequestAdd, inAddName.text);
    }

    [SerializeField]
    private ToClientAddView toClientAddPanel;

    /// <summary>
    /// 显示加好友面板
    /// </summary>
    public void ShowToClientAddPanel(PlayerDto dto)
    {
        toClientAddPanel.gameObject.SetActive(true);
        toClientAddPanel.UpdateView(dto);
    }
    /// <summary>
    /// 添加结果点击事件
    /// </summary>
    public void OnResClick(bool result)
    {
        int id = toClientAddPanel.Id;
        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.ToClientAdd, result,id);
        toClientAddPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新好友界面
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="isOnline"></param>
    public void UpdateFriendView(int friendId,bool isOnline)
    {
        foreach (FriendView item in friendList)
        {
            if (item.Id == friendId)
                item.UpdateView(isOnline);
        }
    }
    #endregion

    #region 匹配模块

    [Header("匹配模块")]
    [SerializeField]
    private Button btnOne;

    [SerializeField]
    private Button btnTwo;

    [SerializeField]
    private MatchView matchView;

    /// <summary>
    /// 单人匹配
    /// </summary>
    public void OnOneMatch()
    {
        //播放声音
        SoundManager.Instance.PlayEffectMusic(acClick);
        //发起请求
        int id = GameData.Player.id;

        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.StartMatch, id);
        //显示匹配面板
        matchView.StartMatch();
        //禁用按钮
        btnOne.interactable = false;
        btnTwo.interactable = false;
    }

    /// <summary>
    ///  停止匹配
    /// </summary>
    public void OnStopMatch()
    {
        //播放声音
        SoundManager.Instance.PlayEffectMusic(acClick);
        //发起请求
        int id = GameData.Player.id;

        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.StopMatch, id);
        matchView.StopMatch();
        btnOne.interactable = true;
        btnTwo.interactable = true;
    }

    /// <summary>
    /// 多人匹配
    /// </summary>
    public void OntTwoMatch(int[] ids)
    {
        PhotonManager.Instance.Request(OpCode.PlayerCode, OpPlayer.StartMatch, ids);
        //TODO
    }

    #endregion

}
