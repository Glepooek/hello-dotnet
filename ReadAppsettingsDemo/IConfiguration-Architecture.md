# .NET Configuration 体系：接口与实现类完整分析

> 基于 `Demo01.BasicReading`、`Demo03.ConfigSources`、`Demo07.CustomProvider` 代码整理

---

## 一、整体架构图

```
应用代码
    │  使用
    ▼
IConfiguration ◄──────────────────── ConfigurationRoot（实现）
    │                                      │
    │  子节点也实现                         │  持有
    ▼                                      ▼
IConfigurationSection            IConfigurationProvider[]
（视图，也实现 IConfiguration）            │
                                         │  每个 Provider 对应一个 Source
                                         ▼
                                IConfigurationSource
                                （描述"从哪里读"，工厂角色）
                                         │
                                         │  由 IConfigurationBuilder 注册
                                         ▼
                                IConfigurationBuilder
                                （ConfigurationBuilder，负责组装）
```

---

## 二、各接口/类逐一解析

### 2.1 `IConfigurationBuilder` — 组装者

**职责**：收集多个 `IConfigurationSource`，最终调用 `.Build()` 产出 `IConfigurationRoot`。  
**不**保存配置值本身，只保存"从哪里读"的描述。

```csharp
new ConfigurationBuilder()           // 创建 Builder
    .SetBasePath(...)                 // 设置基础路径
    .AddJsonFile("appsettings.json")  // 向 Sources 列表追加 JsonConfigurationSource
    .Build();                         // 触发构建，返回 IConfigurationRoot
```

**核心成员：**

| 成员 | 说明 |
|------|------|
| `Sources` | `IList<IConfigurationSource>`，已注册的数据源列表 |
| `Properties` | `IDictionary<string, object>`，跨 Source 共享的上下文数据 |
| `Add(source)` | 向 Sources 追加一个 Source |
| `Build()` | 遍历所有 Source，创建 Provider 并返回 `ConfigurationRoot` |

**实现类：**

| 实现类 | 所在包 | 说明 |
|--------|--------|------|
| `ConfigurationBuilder` | `Microsoft.Extensions.Configuration` | 唯一标准实现。`Build()` 内部遍历 `Sources`，对每个 Source 调用 `source.Build(this)` 得到 Provider，最终构造 `ConfigurationRoot` |

---

### 2.2 `IConfigurationSource` — 数据源描述符（工厂）

**职责**：描述"一个配置来源的元信息"，并通过工厂方法 `Build()` 创建对应的 `IConfigurationProvider`。

```csharp
public interface IConfigurationSource
{
    IConfigurationProvider Build(IConfigurationBuilder builder);
}
```

**实现类：**

| 实现类 | 所在包 | 注册方式 | 说明 |
|--------|--------|----------|------|
| `JsonConfigurationSource` | `Microsoft.Extensions.Configuration.Json` | `.AddJsonFile(path)` | 描述 JSON 文件路径、是否可选、是否热重载。创建 `JsonConfigurationProvider` |
| `MemoryConfigurationSource` | `Microsoft.Extensions.Configuration` | `.AddInMemoryCollection(dict)` | 持有一个 `IEnumerable<KeyValuePair<string,string?>>` 初始数据集。创建 `MemoryConfigurationProvider`。Demo03 用于设置基础默认值 |
| `EnvironmentVariablesConfigurationSource` | `Microsoft.Extensions.Configuration.EnvironmentVariables` | `.AddEnvironmentVariables()` | 可指定 `Prefix` 过滤前缀（如只读 `MYAPP_` 开头的变量）。Demo03 演示其覆盖 JSON 的优先级 |
| `CommandLineConfigurationSource` | `Microsoft.Extensions.Configuration.CommandLine` | `.AddCommandLine(args)` | 接受 `string[] args`，支持 `key=value`、`--key value`、`/key value` 三种格式。Demo03 中优先级最高 |
| `IniConfigurationSource` | `Microsoft.Extensions.Configuration.Ini` | `.AddIniFile(path)` | 解析 INI 格式文件，`[Section]` 映射为冒号前缀 `Section:Key` |
| `XmlConfigurationSource` *(内置)* | `Microsoft.Extensions.Configuration.Xml` | `.AddXmlFile(path)` | 解析标准 XML 配置，将 `<section><key>` 展开为 `section:key` |
| `UserSecretsConfigurationSource` | `Microsoft.Extensions.Configuration.UserSecrets` | `.AddUserSecrets<TProgram>()` | 开发期专用。从 `%APPDATA%\Microsoft\UserSecrets\<guid>\secrets.json` 读取，不进入版本控制 |
| `StreamConfigurationSource` | `Microsoft.Extensions.Configuration` | `.AddStreamConfiguration(stream)` | 从任意 `Stream` 读取，抽象基类，子类需实现具体解析逻辑（如 `JsonStreamConfigurationSource`） |
| `ChainedConfigurationSource` | `Microsoft.Extensions.Configuration` | `.AddConfiguration(existingConfig)` | 将一个已有的 `IConfiguration` 作为新 Builder 的数据源，用于配置复用/嵌套 |
| `XmlConfigurationSource` *(自定义)* | 项目本地 `Demo07` | `.AddXmlFile(path)`（扩展方法） | Demo07 手写示例。持有文件路径，`Build()` 返回自定义 `XmlConfigurationProvider`。演示如何扩展配置体系 |

---

### 2.3 `IConfigurationProvider` — 真正的数据读取器

**职责**：持有实际配置数据（通常是内部的 `Dictionary<string, string?>`），执行真正的 I/O 操作。

```csharp
public interface IConfigurationProvider
{
    bool TryGet(string key, out string? value);   // 按 key 查值
    void Set(string key, string? value);          // 写入值（运行时覆盖）
    IChangeToken GetReloadToken();                // 支持热重载通知
    void Load();                                  // 执行实际 I/O 读取
    IEnumerable<string> GetChildKeys(            // 枚举子节点键
        IEnumerable<string> earlierKeys,
        string? parentPath);
}
```

**抽象基类：**

| 基类 | 说明 |
|------|------|
| `ConfigurationProvider`（抽象类） | 所在包 `Microsoft.Extensions.Configuration`。提供 `Data`（`Dictionary<string,string?>`）字段及 `TryGet`/`Set`/`GetChildKeys` 的标准实现。自定义 Provider 只需继承此类并覆写 `Load()`，如 Demo07 的 `XmlConfigurationProvider` |
| `FileConfigurationProvider`（抽象类） | 继承 `ConfigurationProvider`，额外封装文件监视（`PhysicalFileProvider`）和热重载逻辑。所有基于文件的 Provider（JSON、INI 等）均继承此类 |

**具体实现类：**

| 实现类 | 基类 | 说明 |
|--------|------|------|
| `JsonConfigurationProvider` | `FileConfigurationProvider` | `Load()` 内将 JSON 文件解析并展开为扁平键值对：`{"Database":{"Host":"localhost"}}` → `"Database:Host"="localhost"`。Demo01 直接使用 |
| `MemoryConfigurationProvider` | `ConfigurationProvider` | 直接将构造时传入的字典写入 `Data`，无 I/O 操作。Demo03 用于最低优先级默认值 |
| `EnvironmentVariablesConfigurationProvider` | `ConfigurationProvider` | `Load()` 读取 `Environment.GetEnvironmentVariables()`，将双下划线 `__` 转换为冒号 `:` 以支持嵌套（如 `Database__Host` → `Database:Host`）。Demo03 演示覆盖 JSON |
| `CommandLineConfigurationProvider` | `ConfigurationProvider` | 解析命令行参数。支持 `key=value`、`--key value`、`-key value`、`/key value` 四种格式。Demo03 中最高优先级 |
| `IniConfigurationProvider` | `FileConfigurationProvider` | 解析 INI 文件。`[Section]` 块映射为 `Section:Key` 前缀 |
| `XmlConfigurationProvider` *(内置)* | `FileConfigurationProvider` | 解析 XML 文件，支持 `name` 特性作为键名区分同名元素 |
| `UserSecretsConfigurationProvider` | `JsonConfigurationProvider` | 本质是读取本地 `secrets.json`，继承 JSON Provider。通过 `userSecretsId`（GUID）定位文件路径 |
| `StreamConfigurationProvider` | `ConfigurationProvider` | 从 `Stream` 读取，抽象类。`JsonStreamConfigurationProvider`、`IniStreamConfigurationProvider` 等继承此类 |
| `ChainedConfigurationProvider` | `ConfigurationProvider` | `TryGet` 直接委托给包装的 `IConfiguration` 实例，无独立存储 |
| `XmlConfigurationProvider` *(自定义)* | `ConfigurationProvider` | Demo07 手写实现。`Load()` 用 `XDocument` 解析 XML，递归展开元素为 `Parent:Child` 键，写入 `Data` 字典。演示最小化自定义 Provider 的实现路径 |

---

### 2.4 `IConfigurationRoot` — 根配置对象

**职责**：`.Build()` 的产物，聚合所有 Provider 并实现**多来源覆盖逻辑**（后注册的 Provider 优先级更高）。

```csharp
public interface IConfigurationRoot : IConfiguration
{
    void Reload();                                         // 重新调用所有 Provider.Load()
    IEnumerable<IConfigurationProvider> Providers { get; } // 暴露所有 Provider（可用于调试）
}
```

**实现类：**

| 实现类 | 所在包 | 说明 |
|--------|--------|------|
| `ConfigurationRoot` | `Microsoft.Extensions.Configuration` | 唯一标准实现。查值时按 `Providers` 列表**逆序**遍历（后注册优先），第一个 `TryGet` 成功的值获胜。`GetSection()` 返回 `ConfigurationSection` 实例，`GetChildren()` 聚合所有 Provider 的子键 |

---

### 2.5 `IConfiguration` — 统一消费接口

**职责**：暴露给应用层的最小化读取契约。`ConfigurationRoot` 和 `ConfigurationSection` 都实现它，因此可用同一接口遍历整棵树。

```csharp
public interface IConfiguration
{
    string? this[string key] { get; set; }               // 按路径索引（冒号分隔）
    IConfigurationSection GetSection(string key);        // 获取子节点（不触发 I/O）
    IEnumerable<IConfigurationSection> GetChildren();    // 枚举直接子节点
}
```

**实现类：**

| 实现类 | 说明 |
|--------|------|
| `ConfigurationRoot` | 根节点实现，详见 2.4 |
| `ConfigurationSection` | 子节点视图，详见 2.6 |

---

### 2.6 `IConfigurationSection` — 子树视图

**职责**：表示配置树中的某个子节点，也实现 `IConfiguration`，因此可以递归嵌套使用。底层数据仍存储在 Provider 中，Section 只是重定位了路径前缀。

```csharp
public interface IConfigurationSection : IConfiguration
{
    string Key { get; }    // 此节点自身的名称，如 "Database"
    string Path { get; }   // 从根到此节点的完整路径，如 "Database"
    string? Value { get; set; } // 叶子节点的值；非叶子节点为 null
}
```

```csharp
// 两种等价写法
string? host1 = config["Database:Host"];
string? host2 = config.GetSection("Database")["Host"];
```

**实现类：**

| 实现类 | 所在包 | 说明 |
|--------|--------|------|
| `ConfigurationSection` | `Microsoft.Extensions.Configuration` | 唯一标准实现。持有对 `IConfigurationRoot` 的引用和路径前缀，所有操作实际都委托给 Root，通过拼接 `Path:key` 来定位数据 |

---

## 三、完整数据流（以 Demo01 为例）

```
.AddJsonFile("appsettings.json")
    │
    └─► 创建 JsonConfigurationSource（描述文件路径等元信息）

.Build()
    │
    ├─ 对每个 Source 调用 source.Build(builder)
    │   └─► 得到 JsonConfigurationProvider
    │
    ├─ JsonConfigurationProvider.Load()
    │   └─► 读取 appsettings.json，展开为扁平键值对：
    │         "AppName"                            = "BasicReadingDemo"
    │         "Database:Host"                      = "localhost"
    │         "Database:Port"                      = "5432"
    │         "ConnectionStrings:DefaultConnection" = "Server=..."
    │
    └─► 返回 ConfigurationRoot（持有所有 Provider 的引用）

config["Database:Host"]
    └─ ConfigurationRoot 逆序遍历 Providers
       └─ JsonConfigurationProvider.TryGet("Database:Host") → "localhost" ✓

config.GetConnectionString("DefaultConnection")
    └─ 扩展方法，等价于 config["ConnectionStrings:DefaultConnection"]
```

---

## 四、多数据源优先级覆盖（Demo03）

```
后注册的 Source → 优先级更高（逆序查找）

Priority: InMemory < JSON < EnvironmentVariables < CommandLine

new ConfigurationBuilder()
    .AddInMemoryCollection(...)      // Provider[0]  优先级最低
    .AddJsonFile("appsettings.json") // Provider[1]
    .AddEnvironmentVariables()       // Provider[2]
    .AddCommandLine(args)            // Provider[3]  优先级最高
    .Build();

// 查询 "AppName" 时：从 Provider[3] 开始逆序查，找到即返回
```

---

## 五、自定义 Provider 最小实现模式（Demo07）

```csharp
// Step 1: Source — 工厂 + 元信息
public class XmlConfigurationSource : IConfigurationSource
{
    public string FilePath { get; }
    public XmlConfigurationSource(string filePath) => FilePath = filePath;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
        => new XmlConfigurationProvider(this);  // 工厂方法
}

// Step 2: Provider — 继承 ConfigurationProvider，只需覆写 Load()
public class XmlConfigurationProvider : ConfigurationProvider
{
    public override void Load()
    {
        // 将 XML 递归展开为冒号分隔的扁平键值对
        // 写入 Data（基类提供的 Dictionary<string, string?>）
        Data = FlattenXml(_source.FilePath);
    }
}

// Step 3: 扩展方法 — 融入 Builder 流式 API
public static IConfigurationBuilder AddXmlFile(
    this IConfigurationBuilder builder, string filePath)
    => builder.Add(new XmlConfigurationSource(filePath));
```

**关键点**：`ConfigurationProvider` 基类已实现 `TryGet`/`Set`/`GetChildKeys`，自定义 Provider 无需关心查询逻辑，只需在 `Load()` 中填充 `Data` 字典即可。

---

## 六、关系汇总表

| 接口/类 | 职责 | 数量关系 | 唯一标准实现 |
|---------|------|----------|-------------|
| `IConfigurationBuilder` | 收集 Sources，触发 Build | 每次构建 1 个 | `ConfigurationBuilder` |
| `IConfigurationSource` | 描述数据来源（工厂） | 每种来源 1 个 | 多种（JSON/Env/CLI/INI/Memory 等） |
| `IConfigurationProvider` | 实际 I/O + 存储键值对 | 与 Source 一一对应 | 多种，自定义继承 `ConfigurationProvider` |
| `IConfigurationRoot` | 聚合所有 Provider，覆盖逻辑 | Build 产物，1 个 | `ConfigurationRoot` |
| `IConfiguration` | 统一消费接口 | Root 和 Section 均实现 | `ConfigurationRoot`、`ConfigurationSection` |
| `IConfigurationSection` | 子树视图，路径偏移 | 按需创建，无限个 | `ConfigurationSection` |
