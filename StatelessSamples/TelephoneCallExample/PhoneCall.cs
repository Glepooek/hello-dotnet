using Stateless;
using Stateless.Graph;
using System;

namespace TelephoneCallExample
{
    public class PhoneCall
    {
        enum Trigger
        {
            /// <summary>
            /// 拨打电话
            /// </summary>
            CallDialed,
            /// <summary>
            /// 电话接通
            /// </summary>
            CallConnected,
            /// <summary>
            /// 留言
            /// </summary>
            LeftMessage,
            /// <summary>
            /// 电话置于保持状态
            /// </summary>
            PlacedOnHold,
            /// <summary>
            /// 电话从保持状态恢复
            /// </summary>
            TakenOffHold,
            /// <summary>
            /// 电话被摔坏
            /// </summary>
            PhoneHurledAgainstWall,
            /// <summary>
            /// 静音麦克风
            /// </summary>
            MuteMicrophone,
            /// <summary>
            /// 取消静音麦克风
            /// </summary>
            UnmuteMicrophone,
            /// <summary>
            /// 设置音量
            /// </summary>
            SetVolume
        }

        enum State
        {
            /// <summary>
            /// 挂断状态
            /// </summary>
            OffHook,
            /// <summary>
            /// 响铃状态
            /// </summary>
            Ringing,
            /// <summary>
            /// 已接通状态
            /// </summary>
            Connected,
            /// <summary>
            /// 保持状态
            /// </summary>
            OnHold,
            /// <summary>
            /// 电话损坏状态
            /// </summary>
            PhoneDestroyed
        }

        State _state = State.OffHook;
        
        StateMachine<State, Trigger> _machine;

        /// <summary>
        /// 设置音量触发器
        /// </summary>
        StateMachine<State, Trigger>.TriggerWithParameters<int> _setVolumeTrigger;
        /// <summary>
        /// 设置被呼叫方触发器
        /// </summary>
        StateMachine<State, Trigger>.TriggerWithParameters<string> _setCalleeTrigger;

        /// <summary>
        /// 呼叫方
        /// </summary>
        string _caller;
        /// <summary>
        /// 被呼叫方
        /// </summary>
        string _callee;

        public PhoneCall(string caller)
        {
            _caller = caller;
            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

            _setVolumeTrigger = _machine.SetTriggerParameters<int>(Trigger.SetVolume);
            _setCalleeTrigger = _machine.SetTriggerParameters<string>(Trigger.CallDialed);

            _machine.Configure(State.OffHook)
	            .Permit(Trigger.CallDialed, State.Ringing);

            _machine.Configure(State.Ringing)
                .OnEntryFrom(_setCalleeTrigger, callee => OnDialed(callee), "Caller number to call")
	            .Permit(Trigger.CallConnected, State.Connected);

            _machine.Configure(State.Connected)
                .OnEntry(t => StartCallTimer())
                .OnExit(t => StopCallTimer())
                .InternalTransition(Trigger.MuteMicrophone, t => OnMute())
                .InternalTransition(Trigger.UnmuteMicrophone, t => OnUnmute())
                .InternalTransition<int>(_setVolumeTrigger, (volume, t) => OnSetVolume(volume))
                .Permit(Trigger.LeftMessage, State.OffHook)
	            .Permit(Trigger.PlacedOnHold, State.OnHold);

            _machine.Configure(State.OnHold)
                .SubstateOf(State.Connected)
                .Permit(Trigger.TakenOffHold, State.Connected)
                .Permit(Trigger.PhoneHurledAgainstWall, State.PhoneDestroyed);

            _machine.OnTransitioned(t => Console.WriteLine($"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ",  t.Parameters)})"));
            _machine.OnTransitionCompleted(t => Console.WriteLine($"OnTransitionCompleted: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ", t.Parameters)})"));
        }

        void OnSetVolume(int volume)
        {
            Console.WriteLine("Volume set to " + volume + "!");
        }

        void OnUnmute()
        {
            Console.WriteLine("Microphone unmuted!");
        }

        void OnMute()
        {
            Console.WriteLine("Microphone muted!");
        }

        void OnDialed(string callee)
        {
            _callee = callee;
            Console.WriteLine("[Phone Call] placed for : [{0}]", _callee);
        }

        void StartCallTimer()
        {
            Console.WriteLine("[Timer:] Call started at {0}", DateTime.Now);
        }

        void StopCallTimer()
        {
            Console.WriteLine("[Timer:] Call ended at {0}", DateTime.Now);
        }

        public void Mute()
        {
            _machine.Fire(Trigger.MuteMicrophone);
        }

        public void Unmute()
        {
            _machine.Fire(Trigger.UnmuteMicrophone);
        }

        public void SetVolume(int volume)
        {
            _machine.Fire(_setVolumeTrigger, volume);
        }

        public void Print()
        {
            Console.WriteLine("[{1}] placed call and [Status:] {0}", _machine.State, _caller);
        }

        /// <summary>
        /// 拨号
        /// </summary>
        /// <param name="callee">被呼叫方</param>
        public void Dialed(string callee)
        {           
            _machine.Fire(_setCalleeTrigger, callee);
        }

        public void Connected()
        {
            _machine.Fire(Trigger.CallConnected);
        }

        public void Hold()
        {
            _machine.Fire(Trigger.PlacedOnHold);
        }

        public void Resume()
        {
            _machine.Fire(Trigger.TakenOffHold);
        }

        public string ToDotGraph()
        {
            return UmlDotGraph.Format(_machine.GetInfo());
        }
    }
}