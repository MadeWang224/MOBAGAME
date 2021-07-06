using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化
/// </summary>
public class GameInit : MonoBehaviour
{
    private void Start()
    {
        //加载登录UI
        UIManager.Instance.ShowUIPanel(UIDefinit.UIAccount);
    }
}
