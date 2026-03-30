# CLAUDE.md

本文件为 Claude Code (claude.ai/code) 在此仓库中工作时提供指导。

## 仓库概述

这是一个 **.NET 学习仓库**，包含 200+ 个独立示例项目，演示各种 .NET 技术，主要聚焦于 WPF 应用程序。

**关键特征：**
- 大多数示例是独立项目，没有交叉依赖
- 项目跨越 .NET Framework 4.6.2 到 .NET 10.0
- 中文和英文注释都是标准且可接受的（旧代码）；新代码注释一律用英文
- 主 `WpfTest/` 解决方案包含 30+ 个相互关联的 WPF 演示

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

**注意：** 此仓库无自动化测试套件。`Test.*` 命名的项目是 GUI 演示应用，不是单元测试项目。

### 关键文件位置
- **解决方案文件**：`WpfTest/WpfTest.slnx`（新的 .slnx 格式）
- **集中式包管理**：`WpfTest/Directory.Packages.props`
- **XAML 格式化规则**：`WpfTest/Settings.XamlStyler`

## 高层架构

### 仓库理念
这是一个**独立学习示例集合**，而非单体应用程序。每个项目演示特定的技术或模式，不依赖其他项目。

### 项目类别
- **`Learning.*` 文件夹** - 框架特定演示（Prism、Unity、MEF、Autofac 等）
- **`WpfTest/*` 文件夹** - 30+ 个 WPF 特性演示
- **`*Samples` 文件夹** - 各种技术示例集合

### WpfTest 解决方案结构
主 WpfTest 解决方案组织为解决方案文件夹：
- **MultiLang/** - 4 个演示多语言/国际化模式的项目
- **WindowsMesage/** - 3 个展示进程间通信的项目
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

#### 3. Mapperly 对象映射
代码库采用 **Riok.Mapperly** 进行编译时对象映射：

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
- 映射器应使用静态方法；忽略属性需显式标注以避免编译警告

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

### 注释语言
- **新增代码注释一律用英文**
- 不要修改现有中文注释（保持旧代码原样）

### XAML 格式化
由 XamlStyler 扩展强制执行（配置：`Settings.XamlStyler`）：
- 使用自闭合标签：`<Button />` 而非 `<Button></Button>`
- 每行最多 1 个属性（容差为 2）
- 闭合斜杠前有空格

## 常见问题和陷阱

### 问题 1: 构建失败 - "无法找到类型或命名空间"
**原因**: 使用了集中式包管理，但 Directory.Packages.props 未包含该包版本
**解决**: 先在 `WpfTest/Directory.Packages.props` 添加版本，再在项目中引用

### 问题 2: XAML 格式不一致
**原因**: XamlStyler 配置未应用
**解决**: 确保 `Settings.XamlStyler` 在项目根目录，使用 Format Document (Ctrl+K, Ctrl+D)

### 问题 3: 旧项目使用 MvvmLight 但已过时
**原因**: 历史遗留，保留用于学习对比
**决策**: 新项目使用 CommunityToolkit.Mvvm，旧项目保持不变

## 不要做什么 ❌

### 代码风格
- ❌ 不要修改旧代码的中文注释（保持历史记录）
- ❌ 不要在演示项目中添加单元测试（这些是 GUI 演示）
- ❌ 不要将 `Test.*` 项目误认为测试项目（它们是演示应用）

### 架构
- ❌ 不要在独立示例项目间创建依赖关系（保持独立性）
- ❌ 不要在旧项目中强制升级 MVVM 框架（它们用于对比学习）
- ❌ 不要统一所有项目的目标框架（多版本共存是有意为之）

### 包管理
- ❌ 不要绕过 Directory.Packages.props 直接在 csproj 中指定版本
- ❌ 不要删除看似未使用的包（可能被其他示例项目引用）

## 最佳参考项目

- **完整架构示例**: `WpfTest/Test.DragControl` - 演示完整的 MVVM 分层架构
- **现代 MVVM**: `WpfTest/Test.CommunityToolkitDemo` - 使用源生成器的最佳实践
- **对象映射**: `WpfTest/Test.MapperlyDemo` - Mapperly 编译时映射
- **多语言**: `WpfTest/MultiLang/Test.MultiLang1` - 国际化最佳实践

## 相关文档

- 项目地图和学习路径: `.claude/PROJECT-MAP.md`
- 架构决策记录: `.claude/ADR-*.md`
- CLAUDE.md 补充内容: `.claude/suggested-additions-for-claude-md.md`
