using System;
using System.Reflection;

namespace Test.Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            TestReflections testReflections = new TestReflections("anyu");

            // 获取类的构造函数
            Console.WriteLine("==========获取类的构造函数==========");
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            Type type = testReflections.GetType();
            ConstructorInfo[] constructorInfos = type.GetConstructors(bindingFlags);
            Console.WriteLine($"Constructor count:{constructorInfos.Length}");
            foreach (var item in constructorInfos)
            {
                ParameterInfo[] parameterInfos = item.GetParameters();
                foreach (var item1 in parameterInfos)
                {
                    Console.WriteLine($"Constructor parameter type:{item1.ParameterType}, parameter name:{item1.Name}");
                }
            }


            // 初始化对象并调用方法
            Console.WriteLine("==========初始化对象并调用方法==========");
            Type type1 = typeof(TestReflections);
            ConstructorInfo constructorInfo = type1.GetConstructor(new Type[] { typeof(string) });
            object obj = constructorInfo.Invoke(new object[] { "anyu" });
            (obj as TestReflections).Show();

            // 初始化对象并调用方法
            TestReflections testReflections1 = (TestReflections)Activator.CreateInstance(type1, "anyu1");
            testReflections1.Show();

            // 获取类中的属性
            Console.WriteLine("==========获取类中的属性==========");
            PropertyInfo[] propertyInfos = type1.GetProperties();
            foreach (var property in propertyInfos)
            {
                Console.WriteLine(property.Name);
            }

            // 获取类中的方法
            Console.WriteLine("==========获取类中的方法==========");
            MethodInfo[] methodInfos = type1.GetMethods(bindingFlags);
            foreach (var method in methodInfos)
            {
                Console.WriteLine(method.Name);
            }

            // 获取类中的字段
            Console.WriteLine("==========获取类中的字段==========");
            FieldInfo[] fieldInfos = type1.GetFields(bindingFlags);
            foreach (var field in fieldInfos)
            {
                Console.WriteLine(field.Name);
            }

            // 获取类中的事件
            Console.WriteLine("==========获取类中的事件==========");
            EventInfo[] eventInfos = type1.GetEvents(bindingFlags);
            foreach (var ev in eventInfos)
            {
                Console.WriteLine(ev.Name);
            }

            Console.ReadLine();
        }
    }
}
