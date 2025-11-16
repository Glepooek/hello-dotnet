using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HarmonyPatch.Shared;

namespace EmitILGeneratorCallInstruction462Samples
{
    internal class CallInstructionDemo
    {
        public static void Run()
        {
            CallStaticMethod();
            CallInstanceMethod();
            CallVirtualMethod();

            void CallStaticMethod()
            {
                DynamicMethod dynamicMethod = new DynamicMethod(
                    "CallStaticMethod",
                    typeof(int),
                    new Type[] { typeof(int), typeof(int) },
                    typeof(CallInstructionDemo).Module);

                ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
                iLGenerator.Emit(OpCodes.Ldarg_0); // 加载第一个参数
                iLGenerator.Emit(OpCodes.Ldarg_1); // 加载第二个参数
                // 调用静态AddNumbers方法
                iLGenerator.Emit(OpCodes.Call, typeof(CallInstructionDemo).GetMethod(nameof(AddNumbers), new Type[] { typeof(int), typeof(int) }));
                iLGenerator.Emit(OpCodes.Ret); // 返回结果

                Func<int, int, int> addDelegate = (Func<int, int, int>)dynamicMethod.CreateDelegate(typeof(Func<int, int, int>));
                int result = addDelegate(10, 20);
                Console.WriteLine($"10 + 20 = {result}");
            }

            void CallInstanceMethod()
            {
                DynamicMethod dynamicMethod = new DynamicMethod(
                    "CallInstanceMethod",
                    typeof(void),
                    new Type[] { typeof(Person) },
                    typeof(CallInstructionDemo).Module);
                ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
                iLGenerator.Emit(OpCodes.Ldarg_0);// 加载第一个参数（Person实例）
                iLGenerator.Emit(OpCodes.Ldstr, "Hi");
                MethodInfo syaHelloMethodInfo = typeof(Person).GetMethod("SayHello", new Type[] { typeof(string) });
                // 调用实例SayHello方法
                iLGenerator.Emit(OpCodes.Call, syaHelloMethodInfo);
                MethodInfo writeLineMethodInfo = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
                // 调用静态WriteLine方法
                iLGenerator.Emit(OpCodes.Call, writeLineMethodInfo);
                iLGenerator.Emit(OpCodes.Ret); // 返回结果

                Action<Person> addDelegate = (Action<Person>)dynamicMethod.CreateDelegate(typeof(Action<Person>));
                addDelegate(new Person { Name = "张三", Age = 20 });
            }

            void CallVirtualMethod()
            {
                DynamicMethod dynamicMethod = new DynamicMethod(
                    "CallVirtualMethod",
                    typeof(void),
                    new Type[] { typeof(Animal) },
                    typeof(CallInstructionDemo).Module);

                ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
                iLGenerator.Emit(OpCodes.Ldarg_0);// 加载第一个参数（Animal实例）
                MethodInfo speakMethodInfo = typeof(Animal).GetMethod("Speak", BindingFlags.Public | BindingFlags.Instance);
                // ++++++++++ OpCodes.Call：强制调用父类 Animal 的实现（忽略多态）++++++++++ 
                //iLGenerator.Emit(OpCodes.Call, speakMethodInfo);
                // ++++++++++ OpCodes.Callvirt：根据运行时类型 Person，调用重写的实现（多态）++++++++++ 
                iLGenerator.Emit(OpCodes.Callvirt, speakMethodInfo);
                iLGenerator.Emit(OpCodes.Ret); // 返回结果

                Action<Animal> speakDelegate = (Action<Animal>)dynamicMethod.CreateDelegate(typeof(Action<Animal>));
                Animal person = new Person();
                speakDelegate(person);
            }
        }

        public static int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
