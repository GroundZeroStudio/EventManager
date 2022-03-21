using DynamicArgTest;
using UnityEngine;

[DisallowMultipleComponent]
public class TestEventArg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Initialize();
        this.TestNoParams();
        this.TestOneParams();

    }

    public void TestNoParams()
    {

        EventManager.Instance.Binding(GameEvent.KEventTest, this.Test);
        EventManager.Instance.Distribute(GameEvent.KEventTest);
        EventManager.Instance.Unbinding(GameEvent.KEventTest, this.Test);
        EventManager.Instance.Distribute(GameEvent.KEventTest);

    }

    public void TestOneParams()
    {

        EventManager.Instance.Binding(GameEvent.KEventTest1, this.TestOneParams);
        EventManager.Instance.Distribute(GameEvent.KEventTest1,1);
        EventManager.Instance.Unbinding(GameEvent.KEventTest1, this.TestOneParams);
        EventManager.Instance.Distribute(GameEvent.KEventTest1,1);

    }


    private void Test(EventArg rEventArg)
    {
        Debug.LogError("DynamicArgTest");
    }

    private void TestOneParams(EventArg rEventArg)
    {
        var rParams = rEventArg.Get<int>(0);
        Debug.LogError($"DynamicArgTest: rParams = {rParams}");
    }
}
