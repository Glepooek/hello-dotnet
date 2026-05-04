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

### 命名约定
- **私有字段**：`_camelCase` 前缀（旧代码可能用 `m_`）
- **接口**：`I` 前缀（例如：`IAnimal`、`IFrameNavigationService`）
- **项目命名**：演示项目使用 `Test.XxxDemo`

### MVVM 框架多样性
- **推荐**：`CommunityToolkit.Mvvm` - `[ObservableProperty]`, `[RelayCommand]`
- **旧版**：`MvvmLight` (ViewModelLocator), `Stylet` (IoC), 手写 `ViewModelBase`
- **原则**：匹配项目现有框架，不要强制统一（用于对比学习）

### Mapperly 对象映射
- 使用 `[Mapper]` 特性 + 静态分部方法
- `[MapProperty]` 配合 `nameof` 链进行扁平化/重命名
- 显式标注 `[MapperIgnoreSource]` 避免编译警告
- **参考**：`WpfTest/Test.MapperlyDemo/Mappers/`

### IValueConverter 模式
- 参数转小写：`parameter?.ToString()?.ToLower()`
- `ConvertBack` 返回 `Binding.DoNothing` 或抛出异常
- **参考**：`WpfTest/Test.DragControl/Converters/`

### 集中式包管理
- 版本统一在 `WpfTest/Directory.Packages.props` 声明
- 项目 `<PackageReference>` **不带** `Version` 属性
- 添加包流程：先更新 props，再在项目引用

### 多语言模式
- RESX 文件 + XAML 标记扩展：`{local:Translate Key=HelloWorld}`
- `WeakEventManager` 模式防止内存泄漏
- **参考**：`WpfTest/MultiLang/`

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

### 问题 4: 某些示例项目无法独立运行
**原因**: 依赖 WpfTest 解决方案中的共享配置
**解决**: 在 WpfTest 解决方案上下文中构建和运行

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

## 快速操作

### 添加新的 WPF 示例项目
1. 在 `WpfTest/` 下创建 `Test.XxxDemo/` 文件夹，使用标准布局
2. 使用 CommunityToolkit.Mvvm 框架 + `_camelCase` 私有字段
3. 新注释用英文，添加到 `WpfTest.slnx`

### 升级 NuGet 包
1. 编辑 `WpfTest/Directory.Packages.props` 修改版本号
2. 运行 `dotnet restore WpfTest/WpfTest.slnx`
3. 测试受影响的项目

### 添加 Mapperly 映射器
参考 `Test.MapperlyDemo/Mappers/` - 使用 `[Mapper]` 特性 + 静态分部类

### 添加 IValueConverter
参考 `Test.DragControl/Converters/` - 参数转小写模式

## 最佳参考项目

- **完整架构示例**: `WpfTest/Test.DragControl` - 演示完整的 MVVM 分层架构
- **现代 MVVM**: `WpfTest/Test.CommunityToolkitDemo` - 使用源生成器的最佳实践
- **对象映射**: `WpfTest/Test.MapperlyDemo` - Mapperly 编译时映射
- **多语言**: `WpfTest/MultiLang/Test.MultiLang1` - 国际化最佳实践

## 相关文档

- 项目地图和学习路径: `.claude/PROJECT-MAP.md`
- 架构决策记录: `.claude/ADR-*.md`
- CLAUDE.md 补充内容: `.claude/suggested-additions-for-claude-md.md`
