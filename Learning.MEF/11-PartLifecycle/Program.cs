using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_PartLifecycle
{
	/*
	 * 部件的共享性（Shareability）是通过使用 PartCreationPolicyAttribute 定义的。
	* PartCreationPolicyAttribute 提供以下几种值：
	 * Shared：部件所有者告知 MEF 一个或多个部件的实例存在于容器。
	 * NonShared： 部件所有者告知 MEF 每次对于部件导出的请求将会被一个新的实例处理。
	* Any 或者不支持的值： 部件所有者允许部件用作“Share”或者“NonShared”。
	 * 
	 * ****************************/
	class Program
	{
		static void Main(string[] args)
		{
		}
	}
}
