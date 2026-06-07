# CLAUDE.md

本文件为 Claude Code (claude.ai/code) 在此仓库中工作时提供指导。

## 项目概述

一个 WPF 演示应用，展示 **Refit**（类型安全的 REST 客户端）结合 **CommunityToolkit.Mvvm** 和 **Options 模式**进行 HttpClient 配置。使用 `jsonplaceholder.typicode.com` 作为演示 API。

## 构建与运行

```bash
# 构建整个解决方案
dotnet build RefitDemo.sln

# 构建单个项目
dotnet build RefitDemo/RefitDemo.csproj
dotnet build RefitDemo.Common/RefitDemo.Common.csproj

# 运行 WPF 应用
dotnet run --project RefitDemo/RefitDemo.csproj
```

无测试项目。未配置代码检查工具。

## 架构

### 解决方案结构

```
RefitDemo.sln
├── RefitDemo/              # WPF 应用程序 (net10.0-windows)
│   ├── App.xaml.cs         # DI 容器配置、Refit 客户端注册
│   ├── MainWindow.xaml     # 帖子列表与删除按钮
│   ├── MainWindowViewModel.cs  # 使用 IJsonPlaceholderApi 的 MVVM ViewModel
│   ├── RefitOptions.cs     # Options 模式：BaseUrl、BaseUri、TimeoutSeconds
│   └── RefitConfigureOptions.cs  # 从常量配置 RefitOptions
└── RefitDemo.Common/       # 共享库 (netstandard2.0)
    ├── Models/             # Post、User 数据传输对象
    └── Services/           # IJsonPlaceholderApi 接口、ApiClientFactory、AuthHeaderHandler
```

### 核心模式

**Refit API 客户端注册（基于 DI）**：
- `App.xaml.cs` 通过 `AddRefitClient<IJsonPlaceholderApi>()` 注册 `IJsonPlaceholderApi`
- HttpClient 通过 `IOptions<RefitOptions>` 使用 Options 模式进行配置
- `RefitConfigureOptions` 实现 `IConfigureOptions<RefitOptions>` 设置基础 URL 和超时时间
- ViewModel 通过构造函数注入接收 `IJsonPlaceholderApi`

**替代方案：基于工厂的客户端创建**：
- `ApiClientFactory.Create<T>(baseUrl)` 提供静态工厂方法手动创建 Refit 客户端
- 支持自定义 `HttpClientHandler` 注入（如 `AuthHeaderHandler`）
- 配置 `CollectionFormat.Csv` 和 `CustomDateUrlParameterFormatter`

**CommunityToolkit.Mvvm 的 MVVM 模式**：
- `MainWindowViewModel` 继承自 `ObservableObject`
- 命令使用 `RelayCommand` / `RelayCommand<T>`，采用惰性初始化模式
- `Ioc.Default.ConfigureServices()` 设置 CommunityToolkit 服务提供程序

### Refit 接口约定

`RefitDemo.Common/Services/` 中的 API 接口使用 Refit 特性：
- `[Get]`、`[Post]`、`[Put]`、`[Delete]` 对应 HTTP 方法
- `[Body]` 用于请求体序列化
- `[Query(CollectionFormat.Csv)]` 用于数组查询参数

### 自定义格式化

`CustomDateUrlParameterFormatter` 将 `DateTime` 参数格式化为 `yyyyMMdd` 字符串。通过 `ApiClientFactory` 中的 `RefitSettings.UrlParameterFormatter` 应用。

## 关键依赖

| 包名 | 用途 |
|------|------|
| Refit | 类型安全的 REST 客户端（基于接口） |
| Refit.HttpClientFactory | Refit 的 DI 集成 |
| CommunityToolkit.Mvvm | MVVM 框架（ObservableObject、RelayCommand） |
| Microsoft.Extensions.DependencyInjection | DI 容器 |
| Microsoft.Extensions.Options.ConfigurationExtensions | Options 模式 |
| Microsoft.Xaml.Behaviors.Wpf | XAML 事件到命令的绑定 |
