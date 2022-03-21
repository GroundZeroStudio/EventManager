using System;
using UnityEngine;

public class TestA : MonoBehaviour
{
    // A加入群聊
    private void Awake()
    {
        MessageManager.Instance.AddMessage(GameEvent.KEventTest1, this, new EventHandler<UIEventArgs>(OnHeroMoveToA));
    }

    // 如果活动有变化的话 A的处理
    void OnHeroMoveToA(object sender, UIEventArgs e)
    {
        Debug.Log($"调用者：{sender} 参数：{e},活动有变化！！");
        // 有人要鸽咱们，抄家伙！！！
    }
}