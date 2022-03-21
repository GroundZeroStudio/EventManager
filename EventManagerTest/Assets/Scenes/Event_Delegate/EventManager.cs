using System;
using System.Collections.Generic;
namespace DelegateTest
{
    public delegate void CallBack();
    public delegate void CallBack<T>(T arg);
    public delegate void CallBack<T, X>(T arg1, X arg2);
    public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);
    public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
    public delegate void CallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);

    public class EventManager : TSingleton<EventManager>
    {
        private Dictionary<GameEvent, Delegate> mEventDict;

        public void Initialize()
        {
            this.mEventDict = new Dictionary<GameEvent, Delegate>();
        }

        private EventManager()
        {

        }
        private void OnListenerAdding(GameEvent rGameEvent, Delegate callBack)
        {
            if (!this.mEventDict.ContainsKey(rGameEvent))
            {
                this.mEventDict.Add(rGameEvent, null);
            }
            Delegate d = this.mEventDict[rGameEvent];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", rGameEvent, d.GetType(), callBack.GetType()));
            }
        }
        private void OnListenerRemoving(GameEvent rGameEvent, Delegate callBack)
        {
            if (this.mEventDict.ContainsKey(rGameEvent))
            {
                Delegate d = this.mEventDict[rGameEvent];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", rGameEvent));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", rGameEvent, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", rGameEvent));
            }
        }
        private void OnListenerRemoved(GameEvent rGameEvent)
        {
            if (this.mEventDict[rGameEvent] == null)
            {
                this.mEventDict.Remove(rGameEvent);
            }
        }

        public void Binding(GameEvent rGameEvent, CallBack callBack)
        {
            OnListenerAdding(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack)this.mEventDict[rGameEvent] + callBack;
        }

        public void Binding<T>(GameEvent rGameEvent, CallBack<T> callBack)
        {
            OnListenerAdding(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T>)this.mEventDict[rGameEvent] + callBack;
        }

        public void Binding<T, X>(GameEvent rGameEvent, CallBack<T, X> callBack)
        {
            OnListenerAdding(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X>)this.mEventDict[rGameEvent] + callBack;
        }

        public void Binding<T, X, Y>(GameEvent rGameEvent, CallBack<T, X, Y> callBack)
        {
            OnListenerAdding(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X, Y>)this.mEventDict[rGameEvent] + callBack;
        }

        public void Binding<T, X, Y, Z>(GameEvent rGameEvent, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerAdding(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X, Y, Z>)this.mEventDict[rGameEvent] + callBack;
        }

        public void Binding<T, X, Y, Z, W>(GameEvent rGameEvent, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X, Y, Z, W>)this.mEventDict[rGameEvent] + callBack;
        }

        //no parameters
        public void Unbinding(GameEvent rGameEvent, CallBack callBack)
        {
            OnListenerRemoving(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack)this.mEventDict[rGameEvent] - callBack;
            OnListenerRemoved(rGameEvent);
        }
        //single parameters
        public void Unbinding<T>(GameEvent rGameEvent, CallBack<T> callBack)
        {
            OnListenerRemoving(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T>)this.mEventDict[rGameEvent] - callBack;
            OnListenerRemoved(rGameEvent);
        }
        //two parameters
        public void Unbinding<T, X>(GameEvent rGameEvent, CallBack<T, X> callBack)
        {
            OnListenerRemoving(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X>)this.mEventDict[rGameEvent] - callBack;
            OnListenerRemoved(rGameEvent);
        }
        //three parameters
        public void Unbinding<T, X, Y>(GameEvent rGameEvent, CallBack<T, X, Y> callBack)
        {
            OnListenerRemoving(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X, Y>)this.mEventDict[rGameEvent] - callBack;
            OnListenerRemoved(rGameEvent);
        }
        //four parameters
        public void Unbinding<T, X, Y, Z>(GameEvent rGameEvent, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X, Y, Z>)this.mEventDict[rGameEvent] - callBack;
            OnListenerRemoved(rGameEvent);
        }
        //five parameters
        public void Unbinding<T, X, Y, Z, W>(GameEvent rGameEvent, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(rGameEvent, callBack);
            this.mEventDict[rGameEvent] = (CallBack<T, X, Y, Z, W>)this.mEventDict[rGameEvent] - callBack;
            OnListenerRemoved(rGameEvent);
        }


        //no parameters
        public void Distribute(GameEvent rGameEvent)
        {
            Delegate d;
            if (this.mEventDict.TryGetValue(rGameEvent, out d))
            {
                CallBack callBack = d as CallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", rGameEvent));
                }
            }
        }
        //single parameters
        public void Distribute<T>(GameEvent rGameEvent, T arg)
        {
            Delegate d;
            if (this.mEventDict.TryGetValue(rGameEvent, out d))
            {
                CallBack<T> callBack = d as CallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", rGameEvent));
                }
            }
        }
        //two parameters
        public void Distribute<T, X>(GameEvent rGameEvent, T arg1, X arg2)
        {
            Delegate d;
            if (this.mEventDict.TryGetValue(rGameEvent, out d))
            {
                CallBack<T, X> callBack = d as CallBack<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", rGameEvent));
                }
            }
        }
        //three parameters
        public void Distribute<T, X, Y>(GameEvent rGameEvent, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (this.mEventDict.TryGetValue(rGameEvent, out d))
            {
                CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", rGameEvent));
                }
            }
        }
        //four parameters
        public void Distribute<T, X, Y, Z>(GameEvent rGameEvent, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (this.mEventDict.TryGetValue(rGameEvent, out d))
            {
                CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", rGameEvent));
                }
            }
        }
        //five parameters
        public void Distribute<T, X, Y, Z, W>(GameEvent rGameEvent, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (this.mEventDict.TryGetValue(rGameEvent, out d))
            {
                CallBack<T, X, Y, Z, W> callBack = d as CallBack<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", rGameEvent));
                }
            }
        }
    }
}
