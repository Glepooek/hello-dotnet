using System;
using System.Collections.Generic;
using System.Text;

namespace HarmonyPatch.Shared
{
    public class Foo
    {
        private const string X = "x";
        private static readonly string Y = "y";

        struct Bar
        {
            static string secret = "hello";
            public string ModifiedSecret() => secret.ToUpper();
        }

        Bar MyBar
        {
            get
            {
                return new Bar();
            }
        }

        public string GetSecret() => MyBar.ModifiedSecret();

        Foo()
        {
        }

        static Foo MakeFoo() => new Foo();
    }
}
