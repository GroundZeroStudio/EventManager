using System;
using UnityEngine;

public class TestSender : MonoBehaviour
{
    // B加入群聊
    private void Awake()
    {
        MessageManager.Instance.SendMessage(GameEvent.KEventTest1, new UIEventArgs<int>(0));
    }

}