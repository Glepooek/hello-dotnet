# CLAUDE.md

本文件为 Claude Code (claude.ai/code) 在此仓库中工作时提供指导。

## 仓库概述

这是一个**中英文双语 .NET 学习仓库**，包含 89+ 个独立示例项目，演示各种 .NET 技术，主要聚焦于 WPF 应用程序。

**关键特征：**
- 大多数示例是独立项目，没有交叉依赖
- 项目跨越 .NET Framework 4.6.2 到 .NET 10.0
- 中文和英文注释都是标准且可接受的
- 主 `WpfTest/` 解决方案包含 27+ 个相互关联的 WPF 演示

## 快速入门命令

### 构建命令
```bash
# 构建整个 WpfTest 解决方案
dotnet build WpfTest/WpfTest.slnx

# 构建特定项目
dotnet build <项目路径>.csproj

# 恢复 WpfTest 解决方案的包
dotnet restore WpfTest/WpfTest.slnx
```

### 运行命令
```bash
# 运行特定项目
dotnet run --project <项目路径>.csproj
```
大多数项目是 WPF 应用程序，运行时会显示窗口。

### 关键文件位置
- **解决方案文件**：`WpfTest/WpfTest.slnx`（新的 .slnx 格式）
- **集中式包管理**：`WpfTest/Directory.Packages.props`
- **XAML 格式化规则**：`WpfTest/Settings.XamlStyler`

## 高层架构

### 仓库理念
这是一个**独立学习示例集合**，而非单体应用程序。每个项目演示特定的技术或模式，不依赖其他项目。

### 项目类别
- **`Learning.*` 文件夹** - 框架特定演示（Prism、Unity、MEF、Autofac 等）
- **`WpfTest/*` 文件夹** - 27+ 个 WPF 特性演示
- **`*Samples` 文件夹** - 各种技术示例集合

### WpfTest 解决方案结构
主 WpfTest 解决方案组织为解决方案文件夹：
- **MultiLang/** - 4 个演示多语言/国际化模式的项目
- **WindowsMessage/** - 3 个展示进程间通信的项目
- **单独的演示** - Test.MapperlyDemo、Test.DragControl、Test.SqliteEFDemo 等

**功能最完整的示例**：`Test.DragControl` 演示了包含所有层的完整 MVVM 架构。

### 标准 WPF 项目布局
```
Test.XxxDemo/
├── Models/           # 领域模型
├── ViewModels/       # MVVM 视图模型
├── Views/            # XAML 视图
├── Converters/       # IValueConverter 实现
├── Utils/ 或 Helper/ # 工具类、行为
├── Services/         # 服务接口/实现
├── App.xaml          # 应用程序入口点
```

## 关键开发模式

### 基本模式总结

#### 1. 命名约定
- **私有字段**：`_camelCase` 前缀（例如：`_key`、`_execute`、`_canExecute`）
  - 一些旧代码使用 `m_` 前缀
- **接口**：`I` 前缀（例如：`IAnimal`、`IFrameNavigationService`）
- **项目命名**：演示项目使用 `Test.XxxDemo`

#### 2. MVVM 框架多样性
此代码库展示了多种 MVVM 方法：
- **较新项目**：使用源生成器的 `CommunityToolkit.Mvvm`（`[ObservableProperty]`、`[RelayCommand]`）
- **旧版项目**：带有 `ViewModelLocator` 和 `SimpleIoc` 的 `MvvmLight`
- **某些项目**：带有 IoC 引导程序的 `Stylet` 框架
- **更早项目**：实现 `INotifyPropertyChanged` 的自定义 `ViewModelBase`

匹配你正在处理的项目所使用的 MVVM 方法。

#### 3. Mapperly 对象映射（近期重点）
代码库最近采用了 **Riok.Mapperly** 进行编译时对象映射：

```csharp
[Mapper]
public partial class OrderMapper
{
    // 公共包装器处理计算属性
    public OrderDto ToDto(Order order)
    {
        var dto = ToOrderDto(order);
        dto.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);
        return dto;
    }

    // 私有分部方法 - Mapperly 自动实现
    [MapProperty(nameof(Order.Customer) + "." + nameof(Customer.Email),
                 nameof(OrderDto.CustomerEmail))]
    [MapProperty(nameof(Order.CreatedAt), nameof(OrderDto.CreatedAt),
                 StringFormat = "yyyy-MM-dd HH:mm:ss")]
    private partial OrderDto ToOrderDto(Order order);
}
```

**要点：**
- 在分部类上使用 `[Mapper]` 特性
- 私有分部方法用于自动实现
- 公共包装器方法手动处理计算属性
- 使用 `[MapProperty]` 和 `nameof` 链进行扁平化/重命名
- 最近的提交显示重构为静态方法和正确的属性忽略处理

#### 4. IValueConverter 模式
```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // 始终在比较前将参数转为小写
        var param = parameter?.ToString()?.ToLower() ?? string.Empty;
        // ... 转换逻辑
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // 要么抛出 NotImplementedException，要么返回 Binding.DoNothing
        throw new NotImplementedException();
    }
}
```

#### 5. 集中式包管理
- 所有 NuGet 版本在 `WpfTest/Directory.Packages.props` 中声明
- 单个项目使用 `<PackageReference>` **不带** `Version` 属性
- 添加包时，首先更新 `Directory.Packages.props`

#### 6. 多语言模式
MultiLang 项目演示国际化：
- 每种文化的 RESX 文件：`Resources.zh.resx`、`Resources.en.resx`
- XAML 标记扩展：`<TextBlock Text="{local:Translate Key=HelloWorld}" />`
- 使用 WeakEventManager 模式防止语言更改事件的内存泄漏

## 关键技术

**框架**：.NET Framework 4.6.2、.NET 9.0、.NET 10.0

**MVVM**：CommunityToolkit.Mvvm 8.4.0、MvvmLight 5.4.1.1、Stylet 1.3.7

**对象映射**：Riok.Mapperly 4.3.1（编译时源生成）

**UI 库**：HandyControl 3.2.0、Microsoft.Xaml.Behaviors.Wpf 1.1.135

**数据访问**：SQLite（sqlite-net、EF Core）、SqlSugar 5.0.3.2

**多媒体**：LibVLCSharp 3.9.5、NAudio 1.10.0

## 开发注意事项

### 双语注释
- 整个代码库中中文和英文注释都是标准的
- **不要**删除或翻译现有的中文注释
- 添加新注释时，匹配周围文件的风格

### XAML 格式化
由 XamlStyler 扩展强制执行（配置：`Settings.XamlStyler`）：
- 使用自闭合标签：`<Button />` 而非 `<Button></Button>`
- 每行最多 1 个属性（容差为 2）
- 闭合斜杠前有空格

### 最近开发重点
基于最近的提交：
- Mapperly 对象映射改进
- 将映射器重构为使用静态方法
- 正确处理忽略的属性以避免警告
- 改进映射器代码组织
