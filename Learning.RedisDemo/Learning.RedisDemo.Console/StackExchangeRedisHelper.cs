using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Learning.RedisDemo.Console
{
	public class StackExchangeRedisHelper
	{
		#region Fields

		/// <summary>
		/// Redis连接字符串
		/// </summary>
		private readonly string _redisConn = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;
		private ConnectionMultiplexer _instance = null;
		private IDatabase _database = null;

		#endregion

		#region Constructor

		public StackExchangeRedisHelper(int db = 0)
		{
			_instance = ConnectionMultiplexer.Connect(_redisConn);
			_database = _instance.GetDatabase(db);
		}

		#endregion

		#region String

		/// <summary>
		/// 缓存一个具体字符串
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="expiry"></param>
		/// <param name="when"></param>
		/// <param name="flags"></param>
		/// <typeparam name="T"></typeparam>
		public void StringSet(string key, string value, TimeSpan? expiry = default, When when = When.Always, CommandFlags flags = CommandFlags.None)
		{
			_database.StringSet(key, value, expiry, when, flags);
		}

		/// <summary>
		/// 缓存一个具体对象
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="expiry"></param>
		/// <param name="when"></param>
		/// <param name="flags"></param>
		/// <typeparam name="T"></typeparam>
		public void StringSet<T>(string key, T value, TimeSpan? expiry = default, When when = When.Always, CommandFlags flags = CommandFlags.None)
			where T : class
		{
			string json = Serialize(value);
			_database.StringSet(key, json, expiry, when, flags);
		}

		/// <summary>
		/// 根据key获取缓存字符串
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string StringGet(string key)
		{
			return _database.StringGet(key);
		}

		/// <summary>
		/// 根据key获取缓存对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public T StringGet<T>(string key) where T : class
		{
			var value = _database.StringGet(key);
			if (!value.IsNull)
			{
				return Deserialize<T>(value);
			}

			return null;
		}

		#endregion

		#region Hash

		/// <summary>
		/// set or update the HashValue for string key 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="hashkey"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool HashSet(string key, string hashkey, string value, When when = When.Always, CommandFlags flags = CommandFlags.None)
		{
			return _database.HashSet(key, hashkey, value, when, flags);
		}

		/// <summary>
		/// set or update the HashValue for string key 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="hashkey"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool HashSet<T>(string key, string hashkey, T value, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
		{
			var json = Serialize(value);
			return _database.HashSet(key, hashkey, json, when, flags);
		}

		/// <summary>
		/// 缓存一个集合
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">Redis Key</param>
		/// <param name="list">数据集合</param>
		/// <param name="getModelId"></param>
		public void HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
		{
			List<HashEntry> listHashEntry = new List<HashEntry>();
			foreach (var item in list)
			{
				string json = Serialize(item);
				listHashEntry.Add(new HashEntry(getModelId(item), json));
			}
			_database.HashSet(key, listHashEntry.ToArray());
		}

		/// <summary>
		/// 获取hashkey所有的值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public List<T> GetAllHashValues<T>(string key) where T : class
		{
			List<T> result = new List<T>();
			RedisValue[] values = _database.HashValues(key);
			foreach (var item in values)
			{
				T hashmodel = Deserialize<T>(item);
				result.Add(hashmodel);
			}

			return result;
		}

		/// <summary>
		/// get the HashValue for string key  and hashkey
		/// </summary>
		/// <param name="key">Represents a key that can be stored in redis</param>
		/// <param name="hashkey"></param>
		/// <returns></returns>
		public RedisValue GetHashValue(string key, string hashkey)
		{
			return _database.HashGet(key, hashkey);
		}

		/// <summary>
		/// get the HashValue for string key  and hashkey
		/// </summary>
		/// <param name="key">Represents a key that can be stored in redis</param>
		/// <param name="hashkey"></param>
		/// <returns></returns>
		public T GetHashValue<T>(string key, string hashkey) where T : class
		{
			RedisValue result = _database.HashGet(key, hashkey);
			if (!result.IsNull)
			{
				return Deserialize<T>(result);
			}

			return null;
		}

		/// <summary>
		/// delete hash field
		/// </summary>
		/// <param name="key"></param>
		/// <param name="hashkey"></param>
		/// <returns></returns>
		public bool DeleteHashValue(string key, string hashkey)
		{
			return _database.HashDelete(key, hashkey);
		}

		#endregion

		#region Set

		/// <summary>
		/// 将指定的(字符串)成员添加到集合中 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool SetAdd(string key, string value, CommandFlags flags = CommandFlags.None)
		{
			return _database.SetAdd(key, value, flags);
		}

		/// <summary>
		///  将指定的(对象)成员添加到集合中 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="hashkey"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool SetAdd<T>(string key, T value, CommandFlags flags = CommandFlags.None) where T : class
		{
			var json = Serialize(value);
			return _database.SetAdd(key, json, flags);
		}

		/// <summary>
		/// 获取集合中的所有(字符串)值
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public List<string> SetMembers(string key, CommandFlags flags = CommandFlags.None)
		{
			var values = _database.SetMembers(key, flags);
			List<string> results = new List<string>();
			if (values != null && values.Length > 0)
			{
				foreach (var item in values)
				{
					results.Add(item);
				}
			}

			return results;
		}

		/// <summary>
		/// 获取集合中的所有(对象)值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="hashkey"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public List<T> SetMembers<T>(string key, CommandFlags flags = CommandFlags.None) where T : class
		{
			var values = _database.SetMembers(key, flags);
			List<T> results = new List<T>();
			if (values != null && values.Length > 0)
			{
				foreach (var item in values)
				{
					results.Add(Deserialize<T>(item));
				}
			}

			return results;
		}

		#endregion

		#region Common Methods

		/// <summary>
		/// Redis内部关联的数据库
		/// </summary>
		/// <returns></returns>
		public IDatabase GetDatabase()
		{
			return _database;
		}

		/// <summary>
		/// pub/sub Subscriber
		/// </summary>
		/// <returns></returns>
		public ISubscriber GetSubscriber()
		{
			return _instance.GetSubscriber();
		}

		/// <summary>
		/// 判断是否存在该key的缓存数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Exists(string key)
		{
			return _database.KeyExists(key);
		}

		/// <summary>
		/// 移除指定key的缓存数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Remove(string key)
		{
			return _database.KeyDelete(key);
		}

		/// <summary>
		/// 序列化对象
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		private string Serialize(object o)
		{
			if (o == null)
			{
				return null;
			}

			return JsonConvert.SerializeObject(o);
		}

		/// <summary>
		/// 反序列化对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		private T Deserialize<T>(string value)
		{
			if (value == null)
			{
				return default;
			}

			return JsonConvert.DeserializeObject<T>(value);
		}

		#endregion
	}
}
