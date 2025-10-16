using System;
using System.Reflection;

// https://zhuanlan.zhihu.com/p/41282759
// https://blog.csdn.net/donet_code/article/details/2456483?utm_medium=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7EBlogCommendFromMachineLearnPai2%7Edefault-1.essearch_pc_relevant&depth_1-utm_source=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7EBlogCommendFromMachineLearnPai2%7Edefault-1.essearch_pc_relevant
// https://blog.csdn.net/jbjwpzyl3611421/article/details/10999777

namespace Test.Reflections1
{
    class Program
    {
        private static string DllName = "Test.Reflections.dll";
        private static string DllPath = AppDomain.CurrentDomain.BaseDirectory + "Test.Reflections.dll";
        static void Main(string[] args)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            // 加载程序集
            // 用该方法，程序集需要引用
            //Assembly assembly = Assembly.Load("Test.Reflections");
            Assembly assembly = Assembly.LoadFrom(DllName);
            //Assembly assembly = Assembly.LoadFrom(DllPath);
            //Assembly assembly = Assembly.LoadFile(DllPath);// 参数为dll路径，不能相对路径
            // 获取程序集中的类,参数是类的完全限定名
            Type type = assembly.GetType("Test.Reflections.TestReflections");
            // 获取程序集中的所有类型
            Type[] types = assembly.GetTypes();

            // 获取类中的方法
            Console.WriteLine("==========获取类中的方法==========");
            MethodInfo[] methodInfos = type.GetMethods(bindingFlags);
            foreach (var method in methodInfos)
            {
                Console.WriteLine(method.Name);
            }

            // 实例化对象后调用方法
            Console.WriteLine("==========实例化对象后调用方法==========");
            object o = Activator.CreateInstance(type, "anyu");
            MethodInfo methodInfo = type.GetMethod("Show");
            methodInfo.Invoke(o, null);

            MethodInfo methodInfo1 = type.GetMethod("Confirm", bindingFlags);
            methodInfo1.Invoke(o, null);

            // 获取类中的字段及设置私有字段的值
            Console.WriteLine("==========获取类中的字段及设置私有字段的值==========");
            FieldInfo fieldInfo = type.GetField("mName", bindingFlags);
            fieldInfo.SetValue(o, "12344");
            Console.WriteLine(fieldInfo.GetValue(o));


            // 获取类中的委托，已给委托赋值（方法）
            // https://www.jianshu.com/p/417ff4ad954c
            Console.WriteLine("==========获取类中的委托==========");
            // 因为委托实际上是字段，所以用GetField来查找到它的信息
            FieldInfo actionField = type.GetField("mAction", bindingFlags);
            // 通过字段信息获取到字段对应的值
            object actionObject = actionField.GetValue(o);
            // 通过字段值获取到Invoke方法的元数据
            MethodInfo handlerMethod = actionObject.GetType().GetMethod("Invoke");
            // 调用委托了，记得传入字段对应的参数
            handlerMethod.Invoke(actionObject, new object[] { "Call By Reflection!" });

            // 动态创建委托
            // https://www.cnblogs.com/cyjb/archive/2013/03/21/DelegateBuilder.html
            //创建代理，传入类型、创建代理的对象以及方法名称
            var method1 = Delegate.CreateDelegate(typeof(Action<string>), o, "InvokeAction");
            method1.DynamicInvoke("hello");

            //// 反射事件
            //// https://www.bbsmax.com/A/MyJxnk2Xdn/
            //MethodInfo mi = type.GetMethod("TriggerEvent");
            //EventInfo ee = type.GetEvent("TestHandler");
            //ee.AddEventHandler(o, Delegate.CreateDelegate(ee.EventHandlerType, mi));
            //mi.Invoke(o, null);

            Console.ReadLine();
        }
    }
}
