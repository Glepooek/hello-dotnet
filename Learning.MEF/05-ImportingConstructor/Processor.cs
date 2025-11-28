using System.ComponentModel.Composition;

namespace _05_ImportingConstructor
{
	[Export]
	class Processor
	{
		public IMessageSender Sender { get; set; }

		#region 04 构造函数导入

		// 为每个导入添加参数
		// 当一个导入有多个导出时，要为参数指定导入契约
		[ImportingConstructor]
		public Processor([Import("TCPSender")]IMessageSender tcpSender)
		{
			this.Sender = tcpSender;
		}

		#endregion
	}
}
