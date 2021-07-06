using MobaCommon.Code;
using MobaCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightView : MonoBehaviour,IResourceListener
{
    #region 基础字段
    /// <summary>
    /// 头像
    /// </summary>
    [SerializeField]
    private Image imgHead;
    /// <summary>
    /// 经验条
    /// </summary>
    [SerializeField]
    private Slider barExp;
    /// <summary>
    /// 等级
    /// </summary>
    [SerializeField]
    private Text txtLv;
    /// <summary>
    /// 血条
    /// </summary>
    [SerializeField]
    private Slider barHp;
    /// <summary>
    /// 血量
    /// </summary>
    [SerializeField]
    private Text txtHp;
    /// <summary>
    /// 蓝条
    /// </summary>
    [SerializeField]
    private Slider barMp;
    /// <summary>
    /// 蓝量
    /// </summary>
    [SerializeField]
    private Text txtMp;
    /// <summary>
    /// 统计面板
    /// </summary>
    [SerializeField]
    private Text txtKDA;
    /// <summary>
    /// 钱
    /// </summary>
    [SerializeField]
    private Text txtMoney;
    /// <summary>
    /// 攻击力
    /// </summary>
    [SerializeField]
    private Text txtAttack;
    /// <summary>
    /// 防御力
    /// </summary>
    [SerializeField]
    private Text txtDefense;
    /// <summary>
    /// 技能
    /// </summary>
    [SerializeField]
    private UISkill[] skills;

    #endregion

    private void Start()
    {
        //释放不必要的资源
        ResourcesManager.Instance.ReleaseAll();
        //加载战斗场景背景音乐
        ResourcesManager.Instance.Load(Paths.RES_SOUND_FIGHT + "FightBGM", typeof(AudioClip), this);

        //向服务器发起进入请求
        PhotonManager.Instance.Request(OpCode.FightCode, OpFight.Enter, GameData.Player.id);

    }

    /// <summary>
    /// 初始化视图显示
    /// </summary>
    public void InitView(HeroModel hero)
    {
        //头像
        ResourcesManager.Instance.Load(Paths.RES_HEAD + hero.Name, typeof(Sprite), this);
        //血
        barHp.value = (float)hero.CurrHp / hero.MaxHp;
        txtHp.text = string.Format("{0} / {1}", hero.CurrHp, hero.MaxHp);
        //蓝
        barMp.value = (float)hero.CurrMp / hero.MaxMp;
        txtMp.text = string.Format("{0} / {1}", hero.CurrMp, hero.MaxMp);
        //经验
        barExp.value = (float)hero.Exp / (hero.Level * 100);
        //等级
        txtLv.text = "Lv."+hero.Level.ToString();
        //统计
        txtKDA.text = string.Format("Kill : {0}          Dead : {1}", hero.Kill, hero.Dead);
        //钱
        txtMoney.text = hero.Money.ToString();
        //属性
        txtAttack.text = hero.Attack.ToString();
        txtDefense.text = hero.Defense.ToString();
        //更新技能列表
        for (int i = 0; i < hero.Skills.Length; i++)
        {
            skills[i].Init(hero.Skills[i]);
        }
    }

    /// <summary>
    /// 更新技能
    /// </summary>
    public void UpdateSkills(HeroModel hero)
    {
        for (int i = 0; i < hero.Skills.Length; i++)
        {
            SkillModel skill = hero.Skills[i];
            //判断人物需要技能满足的等级
            if(hero.Level>skill.LearnLevel)
            {
                //显示升级按钮
                if (hero.Points > 0)
                    skills[i].UpInteractable = true;
                else
                    skills[i].UpInteractable = false;
            }
            else
                skills[i].UpInteractable = false;
            //保存这个技能的信息
            skills[i].skill = skill;
            //隐藏遮罩
            skills[i].Reset();
        }
    }

    /// <summary>
    /// 刷新技能冷却
    /// </summary>
    /// <param name="skillId"></param>
    public void UpdateCoolDown(int skillId)
    {
        foreach (var item in skills)
        {
            if(item.skill.Id==skillId)
            {
                item.Use(item.skill.CoolDown);
                break;
            }
        }
    }

    public void OnLoaded(string assetName, object asset)
    {
        if (asset is AudioClip)
        {
            SoundManager.Instance.PlayBgMusic(asset as AudioClip);
            SoundManager.Instance.BGVolume = 1f;
        }
        else if(asset is Sprite)
        {
            imgHead.sprite = asset as Sprite;
        }
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateView(HeroModel hero)
    {
        //血
        barHp.value = (float)hero.CurrHp / hero.MaxHp;
        txtHp.text = string.Format("{0} / {1}", hero.CurrHp, hero.MaxHp);
        //死亡检测
        if(hero.CurrHp==0)
        {
            //显示黑白屏幕
        }
        //蓝
        barMp.value = (float)hero.CurrMp / hero.MaxMp;
        txtMp.text = string.Format("{0} / {1}", hero.CurrMp, hero.MaxMp);
        //经验
        barExp.value = (float)hero.Exp / (hero.Level * 100);
        //等级
        txtLv.text = "Lv." + hero.Level.ToString();
        //统计
        txtKDA.text = string.Format("Kill : {0}          Dead : {1}", hero.Kill, hero.Dead);
        //钱
        txtMoney.text = hero.Money.ToString();
        //属性
        txtAttack.text = hero.Attack.ToString();
        txtDefense.text = hero.Defense.ToString();
    }

    #region 商店

    [SerializeField]
    private Image ItemPanel;

    public void OnItemPanelClick()
    {
        bool active = ItemPanel.gameObject.activeSelf;
        ItemPanel.gameObject.SetActive(!active);
    }

    #endregion

    #region 结束
    [SerializeField]
    private Image WinPanel;
    [SerializeField]
    private Image LosePanel;

    public void GameOver(bool isWin)
    {
        if (isWin)
            WinPanel.gameObject.SetActive(true);
        else
            LosePanel.gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}
