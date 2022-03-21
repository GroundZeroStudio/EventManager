using DelegateTest;
using System;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.Initialize();

        this.TestEvent();

        this.TestEvent1();
    }
    private void TestEvent()
    {
        EventManager.Instance.Binding(GameEvent.KEventTest, this.Test);
        EventManager.Instance.Distribute(GameEvent.KEventTest);
        EventManager.Instance.Unbinding(GameEvent.KEventTest, this.Test);
        EventManager.Instance.Distribute(GameEvent.KEventTest);
    }
    private void TestEvent1()
    {
        EventManager.Instance.Binding<int>(GameEvent.KEventTest1, this.Test1);
        EventManager.Instance.Distribute(GameEvent.KEventTest1, 1);
        EventManager.Instance.Unbinding<int>(GameEvent.KEventTest1, this.Test1);
        EventManager.Instance.Distribute(GameEvent.KEventTest1, 1);
    }
    private void OnDestroy()
    {
        
    }

    public void Test()
    {
        Debug.LogError("DelegateTest测试");
    }

    public void Test1(int nArg)
    {
        Debug.LogError($"DelegateTest测试{nArg}");
    }
}