using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstructionSamples
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
                typeof(Person));

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

    public class Person
    {
        public string Name;
        public int Age;
    }
}
