using System;

namespace CsharplangDemo70.Demos
{
    public static class RefLocalAndReturnDemo
    {
        // ── ref 局部变量: 引用现有变量 ───────────────────────────────────
        // ── ref 返回值: 方法返回对存储位置的引用 ─────────────────────────

        class Matrix
        {
            private double[] _data;
            private int _rows, _cols;

            public Matrix(int rows, int cols)
            {
                _rows = rows; _cols = cols;
                _data = new double[rows * cols];
            }

            // ref return: 返回对矩阵元素的引用（零拷贝，可直接修改）
            public ref double this[int row, int col]
                => ref _data[row * _cols + col];

            // ref 返回查找方法
            public ref double FindFirst(double value)
            {
                for (int i = 0; i < _data.Length; i++)
                    if (_data[i] == value) return ref _data[i];
                throw new InvalidOperationException("未找到: " + value);
            }

            public void Print()
            {
                Console.Write("  Matrix: ");
                for (int i = 0; i < _rows; i++)
                {
                    Console.Write("[");
                    for (int j = 0; j < _cols; j++)
                        Console.Write(_data[i * _cols + j] + (j < _cols - 1 ? ", " : ""));
                    Console.Write("]");
                }
                Console.WriteLine();
            }
        }

        public static void Run()
        {
            // ── ref 局部变量 ──────────────────────────────────────────────
            int x = 10;
            ref int rx = ref x;   // rx 是 x 的别名
            rx = 99;
            Console.WriteLine("  ref 局部变量: rx=99 → x=" + x);

            // ref 局部变量指向数组元素
            int[] arr = { 1, 2, 3, 4, 5 };
            ref int middle = ref arr[2];  // 指向 arr[2]
            middle = 100;
            Console.WriteLine("  ref arr[2]=100: [" + string.Join(", ", arr) + "]");

            // ── ref 返回值 ────────────────────────────────────────────────
            var mat = new Matrix(2, 3);
            mat[0, 0] = 1; mat[0, 1] = 2; mat[0, 2] = 3;
            mat[1, 0] = 4; mat[1, 1] = 5; mat[1, 2] = 6;
            mat.Print();

            // 通过 ref return 直接修改矩阵元素（零拷贝）
            ref double elem = ref mat[0, 1];
            Console.WriteLine("  mat[0,1] = " + elem);
            elem = 99;                        // 直接修改原始数据
            mat.Print();

            // 链式使用: mat[1,2] = 888
            mat[1, 2] = 888;
            mat.Print();

            // ref return 查找并原地修改
            ref double found = ref mat.FindFirst(99.0);
            found = -1;
            mat.Print();

            Console.WriteLine();
            Console.WriteLine("  ref 局部变量: 变量是目标的别名，修改即修改原值");
            Console.WriteLine("  ref 返回值:   方法返回存储位置引用，实现零拷贝访问");
        }
    }
}
