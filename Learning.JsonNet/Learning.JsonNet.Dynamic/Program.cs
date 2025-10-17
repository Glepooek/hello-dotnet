using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

// 反序列化JSON数据，本地不需要新建一个Model

const string mJson = 
    @"{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }";

dynamic stuff = JsonConvert.DeserializeObject(mJson);
string name = stuff.Name;
string address = stuff.Address.City;
Console.WriteLine($"Name: {name}, Ctity:{address}");

dynamic stuff1 = JObject.Parse(mJson);
string name1 = stuff1.Name;
string address1 = stuff1.Address.City;
Console.WriteLine($"Name1: {name1}, Ctity1:{address1}");

//var stuff2 = JObject.Parse(mJson);
var stuff2 = JsonConvert.DeserializeObject<JObject>(mJson);
string name2 = stuff2["Name"].ToObject<string>();
string address2 = stuff2["Address"].ToString();
Console.WriteLine($"Name2: {name2}, Ctity2:{address2}");

var stuff3 = JsonConvert.DeserializeObject<Dictionary<string, object>>(mJson);
string name3 = stuff3["Name"].ToString();
string address3 = stuff3["Address"].ToString();
dynamic test = JsonConvert.DeserializeObject(address3);
Console.WriteLine($"Name3: {name3}, Ctity3:{test.City}");

Console.ReadLine();
