using System;

namespace CsharplangDemo70.Demos
{
    public static class PatternMatchingDemo
    {
        // ── C# 7.0: is 类型模式和 switch 模式匹配 ───────────────────────

        // 形状类层次
        abstract class Shape
        {
            public abstract double Area();
        }
        class Circle : Shape
        {
            public double Radius;
            public Circle(double r) { Radius = r; }
            public override double Area() => Math.PI * Radius * Radius;
        }
        class Rectangle : Shape
        {
            public double W, H;
            public Rectangle(double w, double h) { W = w; H = h; }
            public override double Area() => W * H;
        }
        class Triangle : Shape
        {
            public double B, H;
            public Triangle(double b, double h) { B = b; H = h; }
            public override double Area() => B * H / 2;
        }

        // ── is 类型模式 ───────────────────────────────────────────────────
        static string DescribeShape(object shape)
        {
            if (shape is Circle c)
                return "圆形 r=" + c.Radius.ToString("F1") + " 面积=" + c.Area().ToString("F2");
            if (shape is Rectangle r)
                return "矩形 " + r.W + "×" + r.H + " 面积=" + r.Area().ToString("F2");
            if (shape is Triangle t)
                return "三角形 底=" + t.B + " 高=" + t.H + " 面积=" + t.Area().ToString("F2");
            if (shape is null)
                return "null";
            return "未知形状: " + shape.GetType().Name;
        }

        // ── switch 语句模式匹配（C# 7.0 的 switch case 支持模式）──────────
        static string ClassifyShape(Shape s)
        {
            switch (s)
            {
                case Circle ci when ci.Radius > 10:
                    return "大圆 (r=" + ci.Radius + ")";
                case Circle ci:
                    return "小圆 (r=" + ci.Radius + ")";
                case Rectangle rect when rect.W == rect.H:
                    return "正方形 (边=" + rect.W + ")";
                case Rectangle rect:
                    return "矩形 (" + rect.W + "×" + rect.H + ")";
                case null:
                    return "null";
                default:
                    return "其他: " + s.GetType().Name;
            }
        }

        // ── is 常量模式 ───────────────────────────────────────────────────
        static string CheckValue(object obj)
        {
            if (obj is null) return "null";
            if (obj is 0) return "零";
            if (obj is true) return "真";
            if (obj is "hello") return "hello";
            return "其他: " + obj;
        }

        public static void Run()
        {
            // is 类型模式
            Console.WriteLine("  is 类型模式:");
            object[] shapes =
            {
                new Circle(5),
                new Rectangle(4, 3),
                new Triangle(6, 4), null
            };
            foreach (var s in shapes)
                Console.WriteLine("    " + DescribeShape(s));

            // switch 模式匹配
            Console.WriteLine("  switch 模式:");
            Shape[] shapeArr =
            {
                new Circle(15), 
                new Circle(3),
                new Rectangle(5, 5), 
                new Rectangle(4, 6)
            };
            foreach (var s in shapeArr)
                Console.WriteLine("    " + ClassifyShape(s));

            // is 常量模式
            Console.WriteLine("  is 常量模式:");
            object[] values = { null, 0, true, "hello", 42 };
            foreach (var v in values)
                Console.WriteLine("    " + CheckValue(v));
        }
    }
}
