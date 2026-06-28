# Learning.JsonNet Demo 补全设计

**日期：** 2026-06-28  
**目标：** 将 Learning.JsonNet 解决方案补全为 Demo00~Demo19 完整学习序列

---

## 现状

| 现有文件夹 | 对应 Demo | 状态 |
|---|---|---|
| `Learning.JsonNet.Shared` | Demo00 | 需重命名 |
| `Learning.JsonNet.Dynamic` | Demo07 | 需重命名迁移 |
| `JsonSubtypesDemo` | Demo11 | 需重命名 |
| `JsonSubtypesDemo2` | Demo12 | 需重命名 |
| `JsonSubTypesDemo3` | Demo13 | 需重命名 |

Demo01~06、Demo08~10、Demo14~19 均不存在，需新建。

---

## 变更清单

### 1. 重命名/迁移现有项目

| 旧文件夹 | 新文件夹 | csproj 新名 | 额外操作 |
|---|---|---|---|
| `Learning.JsonNet.Shared` | `Demo00.Shared` | `Demo00.Shared.csproj` | namespace → `Demo00.Shared`，升级 net9.0，启用 Nullable |
| `Learning.JsonNet.Dynamic` | `Demo07.DynamicDeserialization` | `Demo07.DynamicDeserialization.csproj` | ProjectReference 改指向 Demo00.Shared |
| `JsonSubtypesDemo` | `Demo11.PolyByTypeAnnotation` | `Demo11.PolyByTypeAnnotation.csproj` | — |
| `JsonSubtypesDemo2` | `Demo12.PolyByCustomConverter` | `Demo12.PolyByCustomConverter.csproj` | — |
| `JsonSubTypesDemo3` | `Demo13.PolyByGenericConverter` | `Demo13.PolyByGenericConverter.csproj` | — |

旧文件夹在 git 中通过 `git mv` 执行，保留历史。

### 2. 新增项目（16 个）

每个项目：`net9.0`，顶级语句，无 namespace，注释英文，模型类内联在 `Program.cs`。

| 编号 | 文件夹 | 核心演示 |
|---|---|---|
| 01 | `Demo01.BasicSerialization` | `SerializeObject/DeserializeObject`，`Formatting.Indented` |
| 02 | `Demo02.JsonAttributes` | `[JsonProperty]`、`[JsonIgnore]`、`[JsonRequired]` |
| 03 | `Demo03.JsonSettings` | `NullValueHandling`、`DefaultValueHandling`、`DateFormatString` |
| 04 | `Demo04.EnumSerialization` | `StringEnumConverter`，枚举序列化为字符串 |
| 05 | `Demo05.LinqToJson` | `JObject/JArray/JToken` 构造与查询 |
| 06 | `Demo06.JsonPath` | `SelectToken` / JSONPath 表达式 |
| 08 | `Demo08.NamingStrategy` | `CamelCaseNamingStrategy`、`SnakeCaseNamingStrategy` |
| 09 | `Demo09.CustomConverter` | 完整 `ReadJson + WriteJson` 自定义 Converter |
| 10 | `Demo10.SerializationCallbacks` | `[OnSerializing]`、`[OnDeserialized]` 生命周期钩子 |
| 14 | `Demo14.TypeNameHandling` | `TypeNameHandling.Auto`，`$type` 字段内置多态 |
| 15 | `Demo15.CircularReference` | `ReferenceLoopHandling.Ignore/Serialize` |
| 16 | `Demo16.ContractResolver` | `DefaultContractResolver` 动态控制序列化 |
| 17 | `Demo17.StreamingApi` | `JsonTextWriter/JsonTextReader` 流式低内存读写 |
| 18 | `Demo18.JObjectMerge` | `JObject.Merge`，JSON Patch / 配置合并 |
| 19 | `Demo19.ErrorHandling` | `JsonSerializerSettings.Error`，批量容错反序列化 |

---

## csproj 模板

**标准项目（无 ProjectReference）：**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" />
  </ItemGroup>
</Project>
```

**Demo07（需引用 Demo00.Shared）：**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Demo00.Shared\Demo00.Shared.csproj" />
  </ItemGroup>
</Project>
```

**Demo11（需引用 JsonSubTypes）：**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="JsonSubTypes" />
    <PackageReference Include="Newtonsoft.Json" />
  </ItemGroup>
</Project>
```

---

## 代码规范

- 顶级语句（top-level statements），无 namespace
- 注释用英文
- 每个 `Program.cs` 包含完整可运行示例，带 `Console.WriteLine` 验证输出
- 模型类直接内联定义在 `Program.cs` 尾部（保持各 Demo 完全独立）

---

## slnx 最终结构

```xml
<Solution>
  <Folder Name="/Solution Items/">
    <File Path="README.md" />
  </Folder>
  <Project Path="Demo00.Shared/Demo00.Shared.csproj" />
  <Project Path="Demo01.BasicSerialization/Demo01.BasicSerialization.csproj" />
  <Project Path="Demo02.JsonAttributes/Demo02.JsonAttributes.csproj" />
  <Project Path="Demo03.JsonSettings/Demo03.JsonSettings.csproj" />
  <Project Path="Demo04.EnumSerialization/Demo04.EnumSerialization.csproj" />
  <Project Path="Demo05.LinqToJson/Demo05.LinqToJson.csproj" />
  <Project Path="Demo06.JsonPath/Demo06.JsonPath.csproj" />
  <Project Path="Demo07.DynamicDeserialization/Demo07.DynamicDeserialization.csproj" />
  <Project Path="Demo08.NamingStrategy/Demo08.NamingStrategy.csproj" />
  <Project Path="Demo09.CustomConverter/Demo09.CustomConverter.csproj" />
  <Project Path="Demo10.SerializationCallbacks/Demo10.SerializationCallbacks.csproj" />
  <Project Path="Demo11.PolyByTypeAnnotation/Demo11.PolyByTypeAnnotation.csproj" />
  <Project Path="Demo12.PolyByCustomConverter/Demo12.PolyByCustomConverter.csproj" />
  <Project Path="Demo13.PolyByGenericConverter/Demo13.PolyByGenericConverter.csproj" />
  <Project Path="Demo14.TypeNameHandling/Demo14.TypeNameHandling.csproj" />
  <Project Path="Demo15.CircularReference/Demo15.CircularReference.csproj" />
  <Project Path="Demo16.ContractResolver/Demo16.ContractResolver.csproj" />
  <Project Path="Demo17.StreamingApi/Demo17.StreamingApi.csproj" />
  <Project Path="Demo18.JObjectMerge/Demo18.JObjectMerge.csproj" />
  <Project Path="Demo19.ErrorHandling/Demo19.ErrorHandling.csproj" />
</Solution>
```

---

## 验收标准

- `dotnet build Learning.JsonNet.slnx` 零错误
- 每个 Demo 可单独 `dotnet run` 并输出预期结果
- 旧文件夹（5 个）通过 `git mv` 迁移，git 历史保留
