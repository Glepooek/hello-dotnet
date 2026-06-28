# Learning.JsonNet

Newtonsoft.Json 学习演示项目集合（Demo01 ~ Demo19），按难度分三层。

## 项目列表

### 基础层

| 编号 | 项目名 | 说明 |
|------|--------|------|
| 01 | `Demo01.BasicSerialization` | 序列化/反序列化基础，`Formatting.Indented` |
| 02 | `Demo02.JsonAttributes` | `[JsonProperty]`、`[JsonIgnore]`、`[JsonRequired]` |
| 03 | `Demo03.JsonSettings` | `NullValueHandling`、`DefaultValueHandling`、`DateFormatString` |
| 04 | `Demo04.EnumSerialization` | `StringEnumConverter`，枚举序列化为字符串 |

### 中级层

| 编号 | 项目名 | 说明 |
|------|--------|------|
| 05 | `Demo05.LinqToJson` | `JObject`/`JArray`/`JToken` 构造与查询 |
| 06 | `Demo06.JsonPath` | `SelectToken` / JSONPath 表达式 |
| 07 | `Demo07.DynamicDeserialization` | `dynamic`、`JObject`、`Dictionary`、有类型反序列化 |
| 08 | `Demo08.NamingStrategy` | `CamelCaseNamingStrategy`、`SnakeCaseNamingStrategy` |
| 09 | `Demo09.CustomConverter` | 完整 `ReadJson` + `WriteJson` 的自定义 Converter |
| 10 | `Demo10.SerializationCallbacks` | `[OnSerializing]`、`[OnDeserialized]` 生命周期钩子 |

### 多态反序列化（三种方案对比）

| 编号 | 项目名 | 说明 |
|------|--------|------|
| 11 | `Demo11.PolyByTypeAnnotation` | `JsonSubtypes` 包 + 特性声明 |
| 12 | `Demo12.PolyByCustomConverter` | 自定义 Converter + 枚举判别字段 |
| 13 | `Demo13.PolyByGenericConverter` | 泛型 `JsonSubTypeConverter<T>` |

### 高级层

| 编号 | 项目名 | 说明 |
|------|--------|------|
| 14 | `Demo14.TypeNameHandling` | `$type` 字段内置多态，对比 Demo11-13 |
| 15 | `Demo15.CircularReference` | `ReferenceLoopHandling`、`PreserveReferencesHandling`，ORM 循环引用处理 |
| 16 | `Demo16.ContractResolver` | `DefaultContractResolver` 动态控制序列化 |
| 17 | `Demo17.StreamingApi` | `JsonTextWriter`/`JsonTextReader` 流式低内存读写 |
| 18 | `Demo18.JObjectMerge` | `JObject.Merge` 用于 JSON Patch / 配置合并 |
| 19 | `Demo19.ErrorHandling` | `JsonSerializerSettings.Error`，批量容错反序列化 |

## 快速运行

```bash
# 运行单个 Demo
dotnet run --project Demo01.BasicSerialization/Demo01.BasicSerialization.csproj

# 构建全部项目
dotnet build Learning.JsonNet.slnx
```

## 项目约定

- 目标框架：.NET 9.0
- 代码风格：顶级语句（top-level statements），无 namespace
- NuGet 版本：通过 `Directory.Packages.props` 集中管理（CPM）
- 模型类内联定义在各自 `Program.cs` 末尾，每个 Demo 完全独立
