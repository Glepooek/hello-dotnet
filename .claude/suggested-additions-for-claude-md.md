# CLAUDE.md 建议补充内容

## 1. 常见问题和陷阱

```markdown
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
```

## 2. 架构决策记录 (ADR)

```markdown
## 架构决策记录

### ADR-001: 采用 Mapperly 而非 AutoMapper
**日期**: 2024-xx-xx
**状态**: 已接受

**上下文**: 需要对象映射库，AutoMapper 使用反射性能较低

**决策**: 使用 Riok.Mapperly 进行编译时映射

**理由**:
- 零运行时反射，性能更好
- 编译时错误检测
- 源生成器可调试
- 支持 .NET 10 的 AOT 编译

**后果**:
- 需要显式标注映射规则
- 无法处理复杂的运行时映射逻辑（需手动包装）

### ADR-002: 集中式包管理
**日期**: 2024-xx-xx
**状态**: 已接受

**上下文**: 200+ 项目，包版本管理混乱

**决策**: 在 `Directory.Packages.props` 中统一管理版本

**理由**:
- 避免版本冲突
- 批量升级更容易
- 清晰的依赖关系

**后果**:
- 新增包时需两步操作（先 props 后 csproj）
- 部分旧项目仍使用旧方式（逐步迁移中）
```

## 3. 不要做什么

```markdown
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

### 注释
- ❌ 不要用中文添加新注释（只用英文）
- ❌ 不要删除"冗余"的注释（这是教学项目，详细说明是必要的）
```

## 4. 项目地图

```markdown
## 项目地图 - 按学习路径

### 入门级（适合初学者）
1. `WpfTest/Test.HelloWorld` - WPF 基础
2. `WpfTest/Test.DataBinding` - 数据绑定入门
3. `WpfTest/Test.SimpleCommand` - 命令模式基础

### 中级（MVVM 应用）
1. `WpfTest/Test.MvvmBasic` - MVVM 模式
2. `WpfTest/Test.MapperlyDemo` - 对象映射
3. `WpfTest/MultiLang/Test.MultiLang1` - 多语言支持

### 高级（完整架构）
1. `WpfTest/Test.DragControl` - 完整 MVVM 架构（最佳参考）
2. `WpfTest/Test.SqliteEFDemo` - 数据持久化
3. `Learning.Prism` - 模块化应用

### 框架对比（教学用）
- `Learning.MvvmLight` - MvvmLight 框架（旧）
- `Learning.Stylet` - Stylet 框架
- `WpfTest/Test.CommunityToolkit` - CommunityToolkit.Mvvm（推荐）

### 特殊技术
- `WpfTest/WindowsMessage/` - 进程间通信
- `Test.VlcPlayer` - 多媒体播放
- `Test.AudioRecorder` - 音频处理
```

## 5. 快速参考指南

```markdown
## 快速参考指南

### 我想添加新的 WPF 示例项目
1. 在 `WpfTest/` 下创建 `Test.XxxDemo/` 文件夹
2. 使用标准布局：Models/, ViewModels/, Views/, Converters/
3. 使用 CommunityToolkit.Mvvm 框架
4. 私有字段用 `_camelCase`
5. 新注释用英文
6. 添加到 `WpfTest.slnx` 解决方案

### 我想升级 NuGet 包
1. 编辑 `WpfTest/Directory.Packages.props`
2. 修改 `<PackageVersion Include="PackageName" Version="x.y.z" />`
3. 运行 `dotnet restore WpfTest/WpfTest.slnx`
4. 测试受影响的项目

### 我想添加 Converter
```csharp
public class MyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var param = parameter?.ToString()?.ToLower() ?? string.Empty;
        // Your logic
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing; // or throw NotImplementedException
    }
}
```

### 我想添加 Mapperly 映射器
```csharp
[Mapper]
public static partial class MyMapper
{
    // Use static methods
    [MapProperty(nameof(Source.PropA), nameof(Dest.PropB))]
    public static partial Dest Map(Source source);

    // Ignore unmapped properties explicitly
    [MapperIgnoreSource(nameof(Source.IgnoredProp))]
    public static partial Dest Map2(Source source);
}
```
```

## 6. 关键联系人和资源

```markdown
## 关键联系人和资源

### 文档位置
- **解决方案**: `WpfTest/WpfTest.slnx`
- **包管理**: `WpfTest/Directory.Packages.props`
- **XAML 格式**: `WpfTest/Settings.XamlStyler`
- **Git 忽略**: `.gitignore`

### 最佳参考项目
- **完整架构示例**: `WpfTest/Test.DragControl`
- **现代 MVVM**: `WpfTest/Test.CommunityToolkitDemo`
- **对象映射**: `WpfTest/Test.MapperlyDemo`
- **多语言**: `WpfTest/MultiLang/Test.MultiLang1`

### 外部资源
- CommunityToolkit.Mvvm: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/
- Mapperly: https://github.com/riok/mapperly
- WPF: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/
```
