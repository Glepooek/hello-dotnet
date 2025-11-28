using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Learning.RedisDemo.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			StackExchangeRedisHelper redisHelper = new StackExchangeRedisHelper();
			var db = redisHelper.GetDatabase();

			//创建对象
			Demo demo = new Demo()
			{
				Name = "苍",
				Age = 18,
				Height = 1.83f
			};

			//TestString(redisHelper, demo);
			//TestHashSet(redisHelper, demo);
			//TestList(db, demo);
			TestSet(redisHelper, demo);
			//TestPubSub(redisHelper);

			System.Console.ReadLine();
		}

		private static void TestString(StackExchangeRedisHelper redisHelper, Demo demo)
		{
			redisHelper.StringSet("anyu", "lff");
			string wife = redisHelper.StringGet("anyu");
			System.Console.WriteLine(wife);

			redisHelper.StringSet("demo", demo);
			var demo2 = redisHelper.StringGet<Demo>("demo");
			System.Console.WriteLine(demo2);
		}

		private static void TestHashSet(StackExchangeRedisHelper redisHelper, Demo demo)
		{
			redisHelper.HashSet<Demo>("user", "cang", demo);
			redisHelper.HashSet<Demo>("user", "shan", demo);
			redisHelper.HashSet<Demo>("user", "yun", demo);
			var temp = redisHelper.GetHashValue<Demo>("user", "cang");
			System.Console.WriteLine(temp);

			IList<Demo> demolist = redisHelper.GetAllHashValues<Demo>("user");
		}

		private static void TestList(IDatabase db, Demo demo)
		{
			for (int i = 0; i < 10; i++)
			{
				db.ListRightPush("list", i);//从底部插入数据
			}

			for (int i = 10; i < 20; i++)
			{
				db.ListLeftPush("list", i);//从顶部插入数据
			}

			var length = db.ListLength("list");//长度 20
			var rightPop = db.ListRightPop("list");//从底部拿出数据
			var leftpop = db.ListLeftPop("list");//从顶部拿出数据
			var list = db.ListRange("list");

			System.Console.WriteLine($"list length:{length} ");
		}

		private static void TestSet(StackExchangeRedisHelper redisHelper, Demo demo)
		{
			redisHelper.SetAdd("lff", "123456lff");
			redisHelper.SetAdd("lff", "654321lff");

			List<string> strs = redisHelper.SetMembers("lff");

			redisHelper.SetAdd<Demo>("setdemo", demo);
			List<Demo> demos = redisHelper.SetMembers<Demo>("setdemo");
		}

		private static void TestSortedSet(IDatabase db, Demo demo)
		{
			//db.SortedSetAdd("anyu", "123456lff");
		}

		private static void TestPubSub(StackExchangeRedisHelper redisHelper)
		{
			ISubscriber sub = redisHelper.GetSubscriber();

			//订阅 Channel1 频道
			sub.Subscribe("Channel1", new Action<RedisChannel, RedisValue>((channel, message) =>
			{
				System.Console.WriteLine("Channel1" + " 订阅收到消息：" + message);
			}));

			for (int i = 0; i < 10; i++)
			{
				sub.Publish("Channel1", "msg" + i);//向频道 Channel1 发送信息
				if (i == 3)
				{
					sub.Unsubscribe("Channel1");//取消订阅
				}
			}
		}
	}

	[Serializable]
	public class Demo
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public float Height { get; set; }

		public override string ToString()
		{
			return $"Name: {Name} Age: {Age} Height:{Height}";
		}
	}
}
