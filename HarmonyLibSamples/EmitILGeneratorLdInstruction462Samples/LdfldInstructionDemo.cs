using HarmonyPatch.Shared;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitILGeneratorLdInstruction462Samples
{
    internal class LdfldInstructionDemo
    {
        public static void Run()
        {
            Person person = new Person { Name = "Alice", Age = 30 };
            FieldInfo ageField = typeof(Person).GetField("Age");
            DynamicMethod method = new DynamicMethod(
                "GetPersonAge",
                typeof(object),
                new[] { typeof(Person) },
                typeof(LdfldInstructionDemo));

            ILGenerator il = method.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, ageField);
            if (ageField.FieldType.IsValueType)
            {
                il.Emit(OpCodes.Box, typeof(int));
            }
            il.Emit(OpCodes.Ret);

            Func<Person, object> getNameFunc = (Func<Person, object>)method.CreateDelegate(typeof(Func<Person, object>));
            object age = getNameFunc(person);

            Console.WriteLine($"Person Age: {age}"); // 输出：Person Age: 30
        }
    }
}
