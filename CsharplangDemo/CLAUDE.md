# CLAUDE.md

本文件为 Claude Code (claude.ai/code) 在此仓库中工作时提供指导。

## 仓库概述

这是一个 **C# 语言特性演示**仓库，包含多个控制台应用项目，分别对应特定的 C# 语言版本。所有项目均运行在 .NET 10.0 运行时上。

## 构建与运行命令

```bash
# 构建所有项目
dotnet build CsharplangDemo.sln

# 运行特定版本的演示
dotnet run --project CsharplangDemo9/CsharplangDemo9.csproj
dotnet run --project CsharplangDemo10/CsharplangDemo10.csproj
dotnet run --project CsharplangDemo11/CsharplangDemo11.csproj
dotnet run --project CsharplangDemo12/CsharplangDemo12.csproj
dotnet run --project CsharplangDemo13/CsharplangDemo13.csproj
dotnet run --project CsharplangDemo14/CsharplangDemo14.csproj
```

## 项目结构

| 项目 | 语言版本 | 运行时 |
|---|---|---|
| `CsharplangDemo9` | C# 9 新特性 | net10.0 |
| `CsharplangDemo10` | C# 10 新特性 | net10.0 |
| `CsharplangDemo11` | C# 11 新特性 | net10.0，`LangVersion=11` |
| `CsharplangDemo12` | C# 12 新特性 | net10.0，`LangVersion=12` |
| `CsharplangDemo13` | C# 13 新特性 | net10.0，`LangVersion=preview` |
| `CsharplangDemo14` | C# 14 新特性 | net10.0（默认语言版本） |

`CsharplangDemo13` 下的演示文件均位于 `Demos/` 子目录，每个特性对应一个文件，`Program.cs` 作为统一入口依次调用。

## 关键配置

所有项目的基础配置：
- 已启用 `ImplicitUsings` 和 `Nullable`
- `PublishAot=true` — 代码必须**AOT 兼容**（避免使用反射、`dynamic`、`Activator.CreateInstance` 等）
- `InvariantGlobalization=true` — 不使用特定文化区域的字符串行为

`CsharplangDemo13` 额外配置 `<LangVersion>preview</LangVersion>`，用于启用 `field` 上下文关键字（C# 13 预览，C# 14 正式稳定）。

## 添加语言特性演示

向 `Program.cs` 添加 C# 语言特性演示时：

1. 将特性放在与其**引入版本**对应的项目中（例如：`record` 类型 → `CsharplangDemo9`）
2. 若需严格约束语言边界，在 `.csproj` 中显式设置 `<LangVersion>`：
   ```xml
   <LangVersion>9.0</LangVersion>
   ```
3. 保持 AOT 兼容性——优先使用静态方法，避免运行时代码生成
