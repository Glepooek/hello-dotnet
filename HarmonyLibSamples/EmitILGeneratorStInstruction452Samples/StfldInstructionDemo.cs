using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorStInstruction452Samples
{
    public class Person
    {
        public string Name;
        public int Age;
    }

    internal class StfldInstructionDemo
    {
        public static void Run()
        {
            Person person = new Person
            {
                Name = "Alice",
                Age = 30
            };

            FieldInfo idInfo = typeof(Person).GetField("Age");
            var dynamicMethod = new DynamicMethod(
                "SetValue",
                typeof(void),
                new[] { typeof(Person), typeof(int) },
                typeof(StfldInstructionDemo));

            var ilGen = dynamicMethod.GetILGenerator();

            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldarg_1);
            ilGen.Emit(OpCodes.Stfld, idInfo);
            ilGen.Emit(OpCodes.Ret);

            dynamicMethod.Invoke(null, new object[] { person, 111 });

            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
        }
    }
}
