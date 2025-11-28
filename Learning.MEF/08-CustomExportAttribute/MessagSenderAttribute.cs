using System;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace _08_CustomExportAttribute
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class MessagSenderAttribute : ExportAttribute
	{
		public MessagSenderAttribute(Type contractType)
			: base(contractType)
		{

		}

		public TransportType TransportType { get; set; }

		[DefaultValue(true)]
		public bool IsSecure { get; set; }
	}
}
