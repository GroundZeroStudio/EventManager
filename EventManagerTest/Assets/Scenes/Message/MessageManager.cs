using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : TSingleton<MessageManager>
{

    private struct MsgObj
    {
        public MsgObj(object sender, EventHandler<UIEventArgs> handler)
        {
            this.sender = sender;
            this.handler = handler;
        }
        public object sender;
        public EventHandler<UIEventArgs> handler;
    }

    private Dictionary<GameEvent, List<MsgObj>> m_MsgMap = new Dictionary<GameEvent, List<MsgObj>>();
    private MessageManager()
    {

    }
    public void AddMessage(GameEvent msgName, object sender, EventHandler<UIEventArgs> callBack)
    {
        List<MsgObj> callbacks = null;
        MsgObj newMsgObj = new MsgObj(sender, callBack);

        if (!m_MsgMap.TryGetValue(msgName, out callbacks))
        {
            callbacks = new List<MsgObj>();
            m_MsgMap[msgName] = callbacks;
        }
        else
        {
            if (callbacks.Contains(newMsgObj))
            {
                Debug.LogWarning("GameMsg.AddMessage duplicate, " + msgName);
                return;
            }
        }
        callbacks.Add(newMsgObj);
    }

    public void RemoveMessage(GameEvent msgName)
    {
        List<MsgObj> callbacks = null;
        if (m_MsgMap.TryGetValue(msgName, out callbacks))
        {
            m_MsgMap.Remove(msgName);
        }
    }

    public void SendMessage(GameEvent msgName, UIEventArgs args)
    {
        List<MsgObj> callbacks = null;
        if (m_MsgMap.TryGetValue(msgName, out callbacks))
        {
            foreach (MsgObj msgObj in callbacks)
            {
                msgObj.handler(msgObj.sender, args);
            }
        }
    }
}