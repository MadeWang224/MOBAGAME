using MobaCommon.Config;
using MobaCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour,IResourceListener
{
    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Image imgBg;
    [SerializeField]
    private Text txtState;
    [SerializeField]
    private Image imgHead;

    public void OnLoaded(string assetName, object asset)
    {
        Sprite s = asset as Sprite;
        imgHead.sprite = s;
    }

    public void UpdateView(SelectModel model)
    {
        txtName.text = model.playerName;
        imgBg.color = Color.white;
        //判断玩家是否进入
        if(!model.isEnter)
        {
            ResourcesManager.Instance.Load(Paths.RES_HEAD + "no-Connect",typeof(Sprite),this);
            return;
        }
        else//进入之后
        {
            ResourcesManager.Instance.Load(Paths.RES_HEAD + "no-Select", typeof(Sprite), this);
        }
        //选择英雄
        if (model.heroId!=-1)
        {
            string assetName = Paths.RES_HEAD + HeroData.GetHeroData(model.heroId).Name;
            ResourcesManager.Instance.Load(assetName, typeof(Sprite), this);
        }
        else
        {
            ResourcesManager.Instance.Load(Paths.RES_HEAD + "no-Select", typeof(Sprite), this);
        }
        //判断是否准备
        if(model.isReady)
        {
            imgBg.color = Color.green;
            txtState.text = "已选择";
        }
        else
        {
            imgBg.color = Color.white;
            txtState.text = "正在选择...";
        }
    }
}
