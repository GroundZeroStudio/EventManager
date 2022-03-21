using System;
using UnityEngine;

public class TestB : MonoBehaviour
{
    // B加入群聊
    private void Awake()
    {
        MessageManager.Instance.AddMessage(GameEvent.KEventTest1, this, new EventHandler<UIEventArgs>(OnHeroMoveToB));
    }

    // 如果活动有变化的话 B的处理
    void OnHeroMoveToB(object sender, UIEventArgs e)
    {
        Debug.Log($"调用者：{sender} 参数：{e},活动有变化！！");
        // 被鸽子了，这个人拉黑吧！！！
    }
}