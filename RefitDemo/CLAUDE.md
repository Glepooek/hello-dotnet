# CLAUDE.md

本文件为 Claude Code (claude.ai/code) 在此仓库中工作时提供指导。

## 项目概述

一个 WPF 演示应用，展示 **Refit**（类型安全的 REST 客户端）结合 **CommunityToolkit.Mvvm**、**Options 模式**和 **Service 层**进行 HttpClient 配置与业务逻辑封装。使用 `jsonplaceholder.typicode.com` 作为演示 API。

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
├── RefitDemo/                          # WPF 应用程序 (net10.0-windows)
│   ├── App.xaml.cs                     # Host.CreateDefaultBuilder() DI 容器配置
│   ├── appsettings.json                # API 地址和超时配置
│   ├── MainWindow.xaml                 # 帖子列表 + 加载/错误状态 UI
│   ├── MainWindowViewModel.cs          # 依赖 IPostService，含 IsLoading/ErrorMessage
│   ├── RefitOptions.cs                 # Options 模式：BaseUrl、BaseUri、TimeoutSeconds
│   ├── RefitConfigureOptions.cs        # 从 IConfiguration 读取 Refit 配置
│   └── Converters/
│       └── InverseBoolToVisibilityConverter.cs
└── RefitDemo.Common/                   # 共享库 (net10.0-windows)
    ├── Models/
    │   ├── Post.cs                     # 帖子 DTO
    │   ├── User.cs                     # 用户 DTO
    │   └── OperationResult.cs          # API 操作结果包装（成功/失败/状态码）
    └── Services/
        ├── IJsonPlaceholderApi.cs      # Refit 接口定义（含 CancellationToken）
        ├── IPostService.cs             # 业务逻辑接口
        ├── PostService.cs             # 业务逻辑实现，catch ApiException 返回 OperationResult
        ├── IAuthTokenStore.cs          # Token 提供抽象
        ├── NoOpTokenStore.cs           # 默认空实现（返回 null）
        ├── AuthHeaderHandler.cs        # DelegatingHandler，注入 Bearer token
        ├── LoggingHandler.cs           # DelegatingHandler，Debug 输出请求/响应
        ├── ApiClientFactory.cs         # 静态工厂（替代方案，非 DI）
        └── CustomDateUrlParameterFormatter.cs
```

### 核心模式

**请求管线（DelegatingHandler 链）**：
- 注册顺序：`LoggingHandler` → `AuthHeaderHandler` → `HttpClientHandler`
- `AuthHeaderHandler` 注入 `IAuthTokenStore`，从 token store 获取 Bearer token
- `LoggingHandler` 通过 `Debug.WriteLine` 输出请求方法/URI 和响应状态码
- `IAuthTokenStore` 必须在 DI 中注册，否则 `AuthHeaderHandler` 激活失败

**Service 层错误处理**：
- `PostService` 包装 `IJsonPlaceholderApi` 调用，`catch(ApiException)` 返回 `OperationResult<T>`
- `OperationResult<T>` 包含 `IsSuccess`、`Data`、`ErrorMessage`、`StatusCode`
- ViewModel 通过 `OperationResult` 判断成功/失败，更新 `IsLoading`/`ErrorMessage` 状态

**DI 容器配置**：
- `App.xaml.cs` 使用 `Host.CreateDefaultBuilder()` 构建 DI 容器
- `IConfiguration` 读取 `appsettings.json`，通过 `IConfigureOptions<RefitOptions>` 输出到 Options 系统
- `Ioc.Default.ConfigureServices()` 将容器传递给 CommunityToolkit.Mvvm

**CommunityToolkit.Mvvm 模式**：
- `MainWindowViewModel` 继承自 `ObservableObject`
- 命令使用 `AsyncRelayCommand` / `AsyncRelayCommand<T>`（支持异步）
- `IsLoading`/`HasError`/`ErrorMessage` 属性驱动 UI 加载和错误状态

**替代方案：ApiClientFactory**：
- `ApiClientFactory.Create<T>(baseUrl)` 提供静态工厂方法手动创建 Refit 客户端
- 不依赖 DI，适用于简单场景或单元测试

### 配置

`appsettings.json` 配置 Refit 客户端：

```json
{
  "Refit": {
    "BaseUrl": "https://jsonplaceholder.typicode.com",
    "TimeoutSeconds": 20
  }
}
```

`RefitConfigureOptions` 读取 `IConfiguration.GetSection("Refit")`，提供默认值回退。

## 关键依赖

| 包名 | 用途 |
|------|------|
| Refit | 类型安全的 REST 客户端（基于接口） |
| Refit.HttpClientFactory | Refit 的 DI 集成 |
| CommunityToolkit.Mvvm | MVVM 框架（ObservableObject、AsyncRelayCommand） |
| Microsoft.Extensions.DependencyInjection | DI 容器 |
| Microsoft.Extensions.Hosting | 通用主机（Host.CreateDefaultBuilder） |
| Microsoft.Extensions.Configuration.Json | appsettings.json 配置读取 |
| Microsoft.Extensions.Options.ConfigurationExtensions | Options 模式 |
| Microsoft.Xaml.Behaviors.Wpf | XAML 事件到命令的绑定 |

## 常见陷阱

**DI 链式依赖**：`AddHttpMessageHandler<AuthHeaderHandler>()` 要求 `IAuthTokenStore` 已注册。如果未注册，运行时抛出 `InvalidOperationException: Unable to resolve service for type 'IAuthTokenStore'`，编译器不会报错。

**Central Package Management**：使用 `Directory.Packages.props` 集中管理版本。`<PackageReference>` 不带 `Version` 属性，版本在 `Directory.Packages.props` 的 `<PackageVersion>` 中声明。新增包时需同步更新两个文件。

**DelegatingHandler vs HttpClientHandler**：`AuthHeaderHandler` 继承 `DelegatingHandler`（非 `HttpClientHandler`），才能通过 `.AddHttpMessageHandler<T>()` 注册到 DI 管线并与其他 Handler 链式组合。
