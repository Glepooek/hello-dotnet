// “协变”->”很自然的变化”->string->object :协变，具体->抽象。 
// “逆变”->”不正常的变化”->object->string :逆变，抽象->具体。 

// 协变和逆变是泛型类型参数的一种特性。它们允许将派生类型对象赋值给基类型或者将基类型对象赋值给派生类型。
// 协变(Covariance)是指在泛型接口或者委托的类型参数中，可以将派生类型对象赋值给基类型对象。
// 逆变(Contravariance)是指在泛型接口或者委托的返回结果类型参数中，可以将基类型对象赋值给派生类型对象。
// out: 输出(作为返回结果，协变)，in:输入(作为参数，逆变)，
// IEnumerable<out T> T为协变
// IEnumerator<out T>
// IQueryable<out T>
// IGrouping<out T1,out T2>

// IComparable<in T> T为逆变
// IComparer<in T>
// IEqualityComparer<in T>

// Action<in T1, in T2>
// Func<in T, out TReslt> T为逆变，TReslt为协变
// Predicate<in T>
// Comparison<in T>，此委托由Array类的 Sort<T>(T[], Comparison<T>)方法重载和List<T>类的Sort(Comparison<T>) 方法重载使用，用于对数组或列表中的元素进行排序。
// Converter<in TInput, out TOutput> T为逆变，TReslt为协变。作用于泛型集合的类型转换，主要是用于ConverAll函数

// 定义一个接口
interface IAnimal
{
    void MakeSound();
}

// 由IAnimal派生出一个Dog类实现接口
class Dog : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("狗会叫");
    }
}

// 由IAnimal派生出一个Cat类实现接口
class Cat : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("猫会叫");
    }
}

// 定义一个接收泛型协变参数的接口
interface IAnimalList<out T>
{
    T GetAnimal();
}

// 实现协变接口
class DogList : IAnimalList<Dog>
{
    public Dog GetAnimal()
    {
        return new Dog();
    }
}

// 定义一个接收泛型逆变参数的委托
delegate void AnimalAction<in T>(T animal);

// 示例代码
class Program
{
    // 逆变参数的委托方法
    static void AnimalSound(IAnimal animal)
    {
        animal.MakeSound();
    }

    static void Main(string[] args)
    {
        // 协变
        IAnimalList<IAnimal> animalList = new DogList();
        IAnimal animal = animalList.GetAnimal();
        animal.MakeSound(); // 输出："狗会叫"

        // 逆变
        AnimalAction<Cat> catAction = AnimalSound;
        catAction(new Cat());
        // 输出："猫会叫"

        Console.ReadLine();
    }
}