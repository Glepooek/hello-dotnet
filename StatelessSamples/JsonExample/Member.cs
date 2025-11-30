using System;
using Newtonsoft.Json;
using Stateless;

namespace JsonExample
{
    public class Member
    {
        private enum MemberTriggers
        {
            /// <summary>
            /// 暂停
            /// </summary>
            Suspend,
            /// <summary>
            /// 中断
            /// </summary>
            Terminate,
            /// <summary>
            /// 重新激活
            /// </summary>
            Reactivate
        }

        /// <summary>
        /// 会员状态
        /// </summary>
        public enum MembershipState
        {
            /// <summary>
            /// 未激活
            /// </summary>
            Inactive,
            /// <summary>
            /// 激活
            /// </summary>
            Active,
            /// <summary>
            /// 中断
            /// </summary>
            Terminated
        }

        public string Name { get; }
        private readonly StateMachine<MembershipState, MemberTriggers> _stateMachine;
        public MembershipState State => _stateMachine.State;

        public Member(string name)
        {
            _stateMachine = new StateMachine<MembershipState, MemberTriggers>(MembershipState.Active);
            Name = name;

            ConfigureStateMachine();
        }

        [JsonConstructor]
        private Member(string state, string name)
        {
            MembershipState memberState = Enum.Parse<MembershipState>(state);
            _stateMachine = new StateMachine<MembershipState, MemberTriggers>(memberState);
            Name = name;

            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
            _stateMachine.Configure(MembershipState.Active)
                .Permit(MemberTriggers.Suspend, MembershipState.Inactive)
                .Permit(MemberTriggers.Terminate, MembershipState.Terminated);

            _stateMachine.Configure(MembershipState.Inactive)
                .Permit(MemberTriggers.Reactivate, MembershipState.Active)
                .Permit(MemberTriggers.Terminate, MembershipState.Terminated);

            _stateMachine.Configure(MembershipState.Terminated)
                .Permit(MemberTriggers.Reactivate, MembershipState.Active);

            _stateMachine.OnTransitioned(t =>
            {
                Console.WriteLine($"Member {Name} transitioned from {t.Source} to {t.Destination} via {t.Trigger}");
            });
        }

        public void Terminate()
        {
            _stateMachine.Fire(MemberTriggers.Terminate);
        }

        public void Suspend()
        {
            _stateMachine.Fire(MemberTriggers.Suspend);
        }

        public void Reactivate()
        {
            _stateMachine.Fire(MemberTriggers.Reactivate);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Member FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<Member>(jsonString);
        }

        public bool Equals(Member anotherMember)
        {
            return State == anotherMember.State && Name == anotherMember.Name;
        }
    }
}