using System.ComponentModel;

namespace _07_ExportMetadata
{
	// 接口修饰符为public
	// 属性为只读
	public interface ITransport
	{
		TransportType TransportType { get; }
		[DefaultValue(true)]
		bool IsSecure { get; }
	}
}
