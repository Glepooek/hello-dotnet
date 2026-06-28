# Learning.JsonNet Demo 补全实现计划

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 将 Learning.JsonNet 解决方案补全为 Demo00~Demo19 完整 Newtonsoft.Json 学习序列，共 20 个项目。

**Architecture:** 5 个现有项目通过 `git mv` 重命名迁移，保留 git 历史；16 个新项目按演示主题各自独立创建；最后统一更新 slnx 注册所有项目并做全量构建验证。

**Tech Stack:** .NET 9.0，Newtonsoft.Json 13.0.3，JsonSubTypes 1.8.0（仅 Demo11），CPM (Directory.Packages.props)

## Global Constraints

- TargetFramework: `net9.0`
- NuGet 版本不写在 csproj，统一由 `Directory.Packages.props` 管理（CPM）
- 代码风格：顶级语句（top-level statements），无 namespace，注释用英文
- 模型类内联定义在 `Program.cs` 末尾（每个 Demo 完全独立）
- `<Nullable>enable</Nullable>` 所有新/迁移项目均须加上
- 工作目录：`E:\ProjectxPlex\WPFCodePlex\hello-dotnet\Learning.JsonNet`

---

## 文件结构总览

### 迁移（旧 → 新）

| 旧路径 | 新路径 |
|---|---|
| `Learning.JsonNet.Shared/` | `Demo00.Shared/` |
| `Learning.JsonNet.Dynamic/` | `Demo07.DynamicDeserialization/` |
| `JsonSubtypesDemo/` | `Demo11.PolyByTypeAnnotation/` |
| `JsonSubtypesDemo2/` | `Demo12.PolyByCustomConverter/` |
| `JsonSubTypesDemo3/` | `Demo13.PolyByGenericConverter/` |

### 新建（每个文件夹含 `<Name>.csproj` + `Program.cs`）

`Demo01.BasicSerialization/`, `Demo02.JsonAttributes/`, `Demo03.JsonSettings/`,
`Demo04.EnumSerialization/`, `Demo05.LinqToJson/`, `Demo06.JsonPath/`,
`Demo08.NamingStrategy/`, `Demo09.CustomConverter/`, `Demo10.SerializationCallbacks/`,
`Demo14.TypeNameHandling/`, `Demo15.CircularReference/`, `Demo16.ContractResolver/`,
`Demo17.StreamingApi/`, `Demo18.JObjectMerge/`, `Demo19.ErrorHandling/`

### 修改

- `Learning.JsonNet.slnx` — 移除旧 5 个项目，注册全部 20 个新项目

---

## Task 1: 迁移 Demo00.Shared

**Files:**
- Rename dir: `Learning.JsonNet.Shared/` → `Demo00.Shared/`
- Modify: `Demo00.Shared/Demo00.Shared.csproj` (重命名)
- Modify: `Demo00.Shared/Person.cs`, `Address.cs`, `Gender.cs` (更新 namespace)

**Interfaces:**
- Produces: namespace `Demo00.Shared`，类 `Person`、`Address`、`Gender`（供 Demo07 引用）

- [ ] **Step 1: git mv 文件夹**

```powershell
cd "E:\ProjectxPlex\WPFCodePlex\hello-dotnet\Learning.JsonNet"
git mv Learning.JsonNet.Shared Demo00.Shared
```

Expected: no error, folder renamed in git staging.

- [ ] **Step 2: 重命名 csproj 文件**

```powershell
Rename-Item "Demo00.Shared\Learning.JsonNet.Shared.csproj" "Demo00.Shared.csproj"
```

- [ ] **Step 3: 替换 csproj 内容**

将 `Demo00.Shared/Demo00.Shared.csproj` 内容替换为：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

- [ ] **Step 4: 更新三个源文件的 namespace**

`Demo00.Shared/Person.cs`:
```csharp
namespace Demo00.Shared
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public Gender Sex { get; set; }
        public Address? Address { get; set; }
    }
}
```

`Demo00.Shared/Address.cs`:
```csharp
namespace Demo00.Shared
{
    public class Address
    {
        public string Province { get; set; } = "";
        public string City { get; set; } = "";
        public string Town { get; set; } = "";
        public string Village { get; set; } = "";
    }
}
```

`Demo00.Shared/Gender.cs`:
```csharp
namespace Demo00.Shared
{
    public enum Gender
    {
        Male = 0,
        Female
    }
}
```

- [ ] **Step 5: 构建验证**

```powershell
dotnet build Demo00.Shared/Demo00.Shared.csproj
```

Expected: `Build succeeded. 0 Error(s)`

- [ ] **Step 6: Commit**

```powershell
git add Demo00.Shared/
git rm -r --cached Learning.JsonNet.Shared/ 2>$null
git commit -m "refactor: rename Learning.JsonNet.Shared -> Demo00.Shared, update namespace"
```

---

## Task 2: 迁移 Demo07.DynamicDeserialization

**Files:**
- Rename dir: `Learning.JsonNet.Dynamic/` → `Demo07.DynamicDeserialization/`
- Modify: `Demo07.DynamicDeserialization/Demo07.DynamicDeserialization.csproj`
- Modify: `Demo07.DynamicDeserialization/Program.cs`

**Interfaces:**
- Consumes: `Demo00.Shared` (ProjectReference)

- [ ] **Step 1: git mv**

```powershell
git mv Learning.JsonNet.Dynamic Demo07.DynamicDeserialization
```

- [ ] **Step 2: 重命名 csproj**

```powershell
Rename-Item "Demo07.DynamicDeserialization\Learning.JsonNet.Dynamic.csproj" "Demo07.DynamicDeserialization.csproj"
```

- [ ] **Step 3: 替换 csproj 内容**

`Demo07.DynamicDeserialization/Demo07.DynamicDeserialization.csproj`:
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

- [ ] **Step 4: 替换 Program.cs**

`Demo07.DynamicDeserialization/Program.cs`:
```csharp
using Demo00.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Three ways to deserialize JSON without a concrete model

const string json =
    @"{ 'Name': 'Alice', 'Address': { 'City': 'Beijing', 'Province': 'Beijing' }, 'Age': 30 }";

// 1. dynamic keyword — no compile-time type safety
dynamic stuff = JsonConvert.DeserializeObject(json)!;
Console.WriteLine($"[dynamic] Name={stuff.Name}, City={stuff.Address.City}");

// 2. JObject.Parse — strongly typed token access
var jobj = JObject.Parse(json);
string name = jobj["Name"]!.Value<string>()!;
string city = jobj["Address"]!["City"]!.Value<string>()!;
Console.WriteLine($"[JObject] Name={name}, City={city}");

// 3. Dictionary<string, object> — flat access only
var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!;
Console.WriteLine($"[Dictionary] Name={dict["Name"]}, Age={dict["Age"]}");

// 4. Typed deserialization using Demo00.Shared.Person
string typedJson = """{"Name":"Bob","Age":25,"Sex":1,"Address":{"City":"Shanghai","Province":"Shanghai","Town":"","Village":""}}""";
var person = JsonConvert.DeserializeObject<Person>(typedJson);
Console.WriteLine($"[Typed] Name={person!.Name}, City={person.Address?.City}, Sex={person.Sex}");
```

- [ ] **Step 5: 构建验证**

```powershell
dotnet build Demo07.DynamicDeserialization/Demo07.DynamicDeserialization.csproj
```

Expected: `Build succeeded. 0 Error(s)`

- [ ] **Step 6: 运行验证**

```powershell
dotnet run --project Demo07.DynamicDeserialization/Demo07.DynamicDeserialization.csproj
```

Expected output contains:
```
[dynamic] Name=Alice, City=Beijing
[JObject] Name=Alice, City=Beijing
[Dictionary] Name=Alice, Age=30
[Typed] Name=Bob, City=Shanghai, Sex=Female
```

- [ ] **Step 7: Commit**

```powershell
git add Demo07.DynamicDeserialization/
git commit -m "refactor: rename Learning.JsonNet.Dynamic -> Demo07.DynamicDeserialization"
```

---

## Task 3: 迁移 Demo11 / Demo12 / Demo13

**Files:**
- Rename dirs: `JsonSubtypesDemo/` → `Demo11.PolyByTypeAnnotation/`
- Rename dirs: `JsonSubtypesDemo2/` → `Demo12.PolyByCustomConverter/`
- Rename dirs: `JsonSubTypesDemo3/` → `Demo13.PolyByGenericConverter/`
- Rename csproj files in each dir
- Update csproj to add `<Nullable>enable</Nullable>`

- [ ] **Step 1: git mv 三个文件夹**

```powershell
git mv JsonSubtypesDemo  Demo11.PolyByTypeAnnotation
git mv JsonSubtypesDemo2 Demo12.PolyByCustomConverter
git mv JsonSubTypesDemo3 Demo13.PolyByGenericConverter
```

- [ ] **Step 2: 重命名 csproj 文件**

```powershell
Rename-Item "Demo11.PolyByTypeAnnotation\JsonSubtypesDemo.csproj"   "Demo11.PolyByTypeAnnotation.csproj"
Rename-Item "Demo12.PolyByCustomConverter\JsonSubtypesDemo2.csproj"  "Demo12.PolyByCustomConverter.csproj"
Rename-Item "Demo13.PolyByGenericConverter\JsonSubTypesDemo3.csproj" "Demo13.PolyByGenericConverter.csproj"
```

- [ ] **Step 3: 更新 Demo11 csproj（加 Nullable）**

`Demo11.PolyByTypeAnnotation/Demo11.PolyByTypeAnnotation.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <OutputType>Exe</OutputType>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="JsonSubTypes" />
    <PackageReference Include="Newtonsoft.Json" />
  </ItemGroup>
</Project>
```

- [ ] **Step 4: 更新 Demo12 csproj（加 Nullable）**

`Demo12.PolyByCustomConverter/Demo12.PolyByCustomConverter.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <OutputType>Exe</OutputType>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="JsonSubTypes" />
    <PackageReference Include="Newtonsoft.Json" />
  </ItemGroup>
</Project>
```

- [ ] **Step 5: 更新 Demo13 csproj（保留现有标志）**

`Demo13.PolyByGenericConverter/Demo13.PolyByGenericConverter.csproj`:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" />
    <PackageReference Include="System.Security.Permissions" />
  </ItemGroup>
</Project>
```

- [ ] **Step 6: 构建验证**

```powershell
dotnet build Demo11.PolyByTypeAnnotation/Demo11.PolyByTypeAnnotation.csproj
dotnet build Demo12.PolyByCustomConverter/Demo12.PolyByCustomConverter.csproj
dotnet build Demo13.PolyByGenericConverter/Demo13.PolyByGenericConverter.csproj
```

Expected: 三个均 `Build succeeded. 0 Error(s)`

- [ ] **Step 7: Commit**

```powershell
git add Demo11.PolyByTypeAnnotation/ Demo12.PolyByCustomConverter/ Demo13.PolyByGenericConverter/
git commit -m "refactor: rename JsonSubtypesDemo* -> Demo11/12/13, add Nullable"
```

---

## Task 4: Demo01.BasicSerialization

**Files:**
- Create: `Demo01.BasicSerialization/Demo01.BasicSerialization.csproj`
- Create: `Demo01.BasicSerialization/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo01.BasicSerialization/Demo01.BasicSerialization.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo01.BasicSerialization/Program.cs`:
```csharp
using Newtonsoft.Json;

// Serialize a single object with indentation
var person = new Person { Name = "Alice", Age = 30, City = "Beijing" };
string json = JsonConvert.SerializeObject(person, Formatting.Indented);
Console.WriteLine("=== Serialize single object ===");
Console.WriteLine(json);

// Deserialize back to typed object
var restored = JsonConvert.DeserializeObject<Person>(json);
Console.WriteLine($"\n=== Deserialize back ===");
Console.WriteLine($"Name={restored!.Name}, Age={restored.Age}, City={restored.City}");

// Serialize a list
var people = new List<Person>
{
    new() { Name = "Bob",   Age = 25, City = "Shanghai" },
    new() { Name = "Carol", Age = 28, City = "Guangzhou" }
};
Console.WriteLine("\n=== Serialize list ===");
Console.WriteLine(JsonConvert.SerializeObject(people, Formatting.Indented));

// Compact (no indentation) output
Console.WriteLine("\n=== Compact output ===");
Console.WriteLine(JsonConvert.SerializeObject(person));

class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string City { get; set; } = "";
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo01.BasicSerialization/Demo01.BasicSerialization.csproj
```

Expected output contains:
```
=== Serialize single object ===
{
  "Name": "Alice",
  "Age": 30,
  "City": "Beijing"
}
=== Deserialize back ===
Name=Alice, Age=30, City=Beijing
```

- [ ] **Step 4: Commit**

```powershell
git add Demo01.BasicSerialization/
git commit -m "feat: add Demo01.BasicSerialization"
```

---

## Task 5: Demo02.JsonAttributes

**Files:**
- Create: `Demo02.JsonAttributes/Demo02.JsonAttributes.csproj`
- Create: `Demo02.JsonAttributes/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo02.JsonAttributes/Demo02.JsonAttributes.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo02.JsonAttributes/Program.cs`:
```csharp
using Newtonsoft.Json;

var user = new User
{
    UserName    = "alice_dev",
    Password    = "secret123",
    Email       = "alice@example.com",
    DisplayName = "Alice"
};

// Password is ignored; UserName serializes as "user_name"
string json = JsonConvert.SerializeObject(user, Formatting.Indented);
Console.WriteLine("=== Serialized (Password absent, user_name renamed) ===");
Console.WriteLine(json);

// Deserialize — JsonRequired means missing Email throws
string input = """{"user_name":"bob","email":"bob@example.com","display_name":"Bob"}""";
var restored = JsonConvert.DeserializeObject<User>(input);
Console.WriteLine($"\n=== Deserialized ===");
Console.WriteLine($"UserName={restored!.UserName}, Email={restored.Email}, DisplayName={restored.DisplayName}");

// Missing required field → exception
Console.WriteLine("\n=== Missing [JsonRequired] field throws ===");
try
{
    JsonConvert.DeserializeObject<User>("""{"user_name":"carol","display_name":"Carol"}""");
}
catch (JsonSerializationException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

class User
{
    [JsonProperty("user_name")]
    public string UserName { get; set; } = "";

    [JsonIgnore]
    public string Password { get; set; } = "";

    [JsonRequired]
    public string Email { get; set; } = "";

    [JsonProperty("display_name")]
    public string DisplayName { get; set; } = "";
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo02.JsonAttributes/Demo02.JsonAttributes.csproj
```

Expected output contains:
```
=== Serialized (Password absent, user_name renamed) ===
{
  "user_name": "alice_dev",
  "email": "alice@example.com",
  "display_name": "Alice"
}
```

- [ ] **Step 4: Commit**

```powershell
git add Demo02.JsonAttributes/
git commit -m "feat: add Demo02.JsonAttributes"
```

---

## Task 6: Demo03.JsonSettings

**Files:**
- Create: `Demo03.JsonSettings/Demo03.JsonSettings.csproj`
- Create: `Demo03.JsonSettings/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo03.JsonSettings/Demo03.JsonSettings.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo03.JsonSettings/Program.cs`:
```csharp
using Newtonsoft.Json;

// 1. NullValueHandling.Ignore — null properties omitted from output
var nullSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
var product = new Product { Name = "Widget", Description = null, Price = 9.99m };
Console.WriteLine("=== NullValueHandling.Ignore ===");
Console.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented, nullSettings));

// 2. DefaultValueHandling.Ignore — zero/false/empty string omitted
var defaultSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };
var order = new Order { Id = 1, Quantity = 0, IsActive = false };
Console.WriteLine("\n=== DefaultValueHandling.Ignore ===");
Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented, defaultSettings));

// 3. DateFormatString — custom date format
var dateSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd" };
var ev = new Event { Title = "Conference", Date = new DateTime(2026, 6, 28) };
Console.WriteLine("\n=== DateFormatString yyyy-MM-dd ===");
Console.WriteLine(JsonConvert.SerializeObject(ev, Formatting.Indented, dateSettings));

class Product
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

class Order
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
}

class Event
{
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo03.JsonSettings/Demo03.JsonSettings.csproj
```

Expected output contains:
```
=== NullValueHandling.Ignore ===
{
  "Name": "Widget",
  "Price": 9.99
}
```

- [ ] **Step 4: Commit**

```powershell
git add Demo03.JsonSettings/
git commit -m "feat: add Demo03.JsonSettings"
```

---

## Task 7: Demo04.EnumSerialization

**Files:**
- Create: `Demo04.EnumSerialization/Demo04.EnumSerialization.csproj`
- Create: `Demo04.EnumSerialization/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo04.EnumSerialization/Demo04.EnumSerialization.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo04.EnumSerialization/Program.cs`:
```csharp
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Default: enum serializes as integer
var order = new Order { Id = 1, Status = OrderStatus.Processing };
Console.WriteLine("=== Default (integer) ===");
Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));

// Global StringEnumConverter via settings
var settings = new JsonSerializerSettings();
settings.Converters.Add(new StringEnumConverter());
Console.WriteLine("\n=== StringEnumConverter via settings ===");
Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented, settings));

// Deserialize from string enum
string json = """{"Id":2,"Status":"Shipped"}""";
var restored = JsonConvert.DeserializeObject<Order>(json, settings);
Console.WriteLine($"\n=== Deserialized from string ===");
Console.WriteLine($"Id={restored!.Id}, Status={restored.Status} ({(int)restored.Status})");

// [JsonConverter] attribute on the enum type itself
var item = new Item { Name = "Book", Category = Category.Education };
Console.WriteLine("\n=== [JsonConverter] on enum type ===");
Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));

enum OrderStatus { Pending, Processing, Shipped, Delivered }

class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
enum Category { Electronics, Education, Sports }

class Item
{
    public string Name { get; set; } = "";
    public Category Category { get; set; }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo04.EnumSerialization/Demo04.EnumSerialization.csproj
```

Expected output contains:
```
=== Default (integer) ===
{
  "Id": 1,
  "Status": 1
}
=== StringEnumConverter via settings ===
{
  "Id": 1,
  "Status": "Processing"
}
```

- [ ] **Step 4: Commit**

```powershell
git add Demo04.EnumSerialization/
git commit -m "feat: add Demo04.EnumSerialization"
```

---

## Task 8: Demo05.LinqToJson

**Files:**
- Create: `Demo05.LinqToJson/Demo05.LinqToJson.csproj`
- Create: `Demo05.LinqToJson/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo05.LinqToJson/Demo05.LinqToJson.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo05.LinqToJson/Program.cs`:
```csharp
using Newtonsoft.Json.Linq;

// Build JObject manually
var person = new JObject
{
    ["name"] = "Alice",
    ["age"]  = 30,
    ["address"] = new JObject { ["city"] = "Beijing", ["zip"] = "100000" }
};
Console.WriteLine("=== Constructed JObject ===");
Console.WriteLine(person.ToString());

// Add a JArray property
person["tags"] = new JArray("csharp", "dotnet", "json");
Console.WriteLine("\n=== After adding JArray ===");
Console.WriteLine(person.ToString());

// Query typed values
string name = person["name"]!.Value<string>()!;
string city = person["address"]!["city"]!.Value<string>()!;
Console.WriteLine($"\n=== Query ===\nName={name}, City={city}");

// Parse array and iterate
string json = """[{"id":1,"title":"Post A"},{"id":2,"title":"Post B"},{"id":3,"title":"Post C"}]""";
var posts = JArray.Parse(json);
Console.WriteLine("\n=== JArray iteration ===");
foreach (JObject post in posts)
    Console.WriteLine($"  id={post["id"]}, title={post["title"]}");

// LINQ on JArray
var ids = posts.Select(p => p["id"]!.Value<int>()).ToList();
Console.WriteLine($"\n=== LINQ select ids ===\n{string.Join(", ", ids)}");

// Filter with LINQ where
var filtered = posts.Where(p => p["id"]!.Value<int>() > 1).Select(p => p["title"]).ToList();
Console.WriteLine($"\n=== LINQ filter id > 1 ===\n{string.Join(", ", filtered)}");
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo05.LinqToJson/Demo05.LinqToJson.csproj
```

Expected output contains:
```
=== Constructed JObject ===
{
  "name": "Alice",
  "age": 30,
  ...
}
```

- [ ] **Step 4: Commit**

```powershell
git add Demo05.LinqToJson/
git commit -m "feat: add Demo05.LinqToJson"
```

---

## Task 9: Demo06.JsonPath

**Files:**
- Create: `Demo06.JsonPath/Demo06.JsonPath.csproj`
- Create: `Demo06.JsonPath/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo06.JsonPath/Demo06.JsonPath.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo06.JsonPath/Program.cs`:
```csharp
using Newtonsoft.Json.Linq;

string json = """
{
  "store": {
    "book": [
      { "category": "fiction",  "title": "A Tale", "price": 8.99  },
      { "category": "science",  "title": "Cosmos", "price": 14.99 },
      { "category": "fiction",  "title": "Dune",   "price": 12.99 }
    ],
    "bicycle": { "color": "red", "price": 19.99 }
  }
}
""";

var root = JObject.Parse(json);

// Single value via dot notation
var bicycleColor = root.SelectToken("store.bicycle.color");
Console.WriteLine($"=== SelectToken single ===\nBicycle color: {bicycleColor}");

// Wildcard: all book titles
var titles = root.SelectTokens("store.book[*].title");
Console.WriteLine("\n=== SelectTokens all titles ===");
foreach (var t in titles) Console.WriteLine($"  {t}");

// Filter expression: books where price < 13
var cheap = root.SelectTokens("store.book[?(@.price < 13)]");
Console.WriteLine("\n=== Filter price < 13 ===");
foreach (JObject book in cheap)
    Console.WriteLine($"  {book["title"]} - ${book["price"]}");

// Recursive descent: all price values in the store subtree
var allPrices = root.SelectTokens("store..price");
Console.WriteLine("\n=== Recursive descent all prices ===");
foreach (var p in allPrices) Console.WriteLine($"  ${p}");

// Array index access
var secondTitle = root.SelectToken("store.book[1].title");
Console.WriteLine($"\n=== Array index [1].title ===\n  {secondTitle}");
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo06.JsonPath/Demo06.JsonPath.csproj
```

Expected output contains:
```
=== SelectToken single ===
Bicycle color: red
=== SelectTokens all titles ===
  A Tale
  Cosmos
  Dune
```

- [ ] **Step 4: Commit**

```powershell
git add Demo06.JsonPath/
git commit -m "feat: add Demo06.JsonPath"
```

---

## Task 10: Demo08.NamingStrategy

**Files:**
- Create: `Demo08.NamingStrategy/Demo08.NamingStrategy.csproj`
- Create: `Demo08.NamingStrategy/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo08.NamingStrategy/Demo08.NamingStrategy.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo08.NamingStrategy/Program.cs`:
```csharp
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var user = new UserProfile
{
    FirstName   = "Alice",
    LastName    = "Wang",
    EmailAddress = "alice@example.com",
    PhoneNumber = "13800000000"
};

// CamelCase: FirstName -> firstName
var camelSettings = new JsonSerializerSettings
{
    ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    }
};
Console.WriteLine("=== CamelCase ===");
Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented, camelSettings));

// SnakeCase: FirstName -> first_name
var snakeSettings = new JsonSerializerSettings
{
    ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    }
};
Console.WriteLine("\n=== SnakeCase ===");
Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented, snakeSettings));

// Deserialize snake_case JSON back to PascalCase model
string snakeJson = """
    {"first_name":"Bob","last_name":"Li","email_address":"bob@example.com","phone_number":"13900000000"}
    """;
var restored = JsonConvert.DeserializeObject<UserProfile>(snakeJson, snakeSettings);
Console.WriteLine($"\n=== Deserialized from snake_case ===");
Console.WriteLine($"FirstName={restored!.FirstName}, Email={restored.EmailAddress}");

class UserProfile
{
    public string FirstName    { get; set; } = "";
    public string LastName     { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string PhoneNumber  { get; set; } = "";
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo08.NamingStrategy/Demo08.NamingStrategy.csproj
```

Expected output contains:
```
=== CamelCase ===
{
  "firstName": "Alice",
  ...
}
=== SnakeCase ===
{
  "first_name": "Alice",
  ...
}
```

- [ ] **Step 4: Commit**

```powershell
git add Demo08.NamingStrategy/
git commit -m "feat: add Demo08.NamingStrategy"
```

---

## Task 11: Demo09.CustomConverter

**Files:**
- Create: `Demo09.CustomConverter/Demo09.CustomConverter.csproj`
- Create: `Demo09.CustomConverter/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo09.CustomConverter/Demo09.CustomConverter.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo09.CustomConverter/Program.cs`:
```csharp
using Newtonsoft.Json;

// ColorConverter serializes Color as "#RRGGBB" hex string instead of an object
var palette = new Palette
{
    Name       = "Sunset",
    Background = new Color(255, 94, 58),
    Foreground = new Color(255, 255, 255)
};

string json = JsonConvert.SerializeObject(palette, Formatting.Indented);
Console.WriteLine("=== Serialized ===");
Console.WriteLine(json);

var restored = JsonConvert.DeserializeObject<Palette>(json);
Console.WriteLine($"\n=== Deserialized ===");
Console.WriteLine($"Background: R={restored!.Background.R} G={restored.Background.G} B={restored.Background.B}");
Console.WriteLine($"Foreground: R={restored.Foreground.R} G={restored.Foreground.G} B={restored.Foreground.B}");

// ColorConverter: read/write Color as "#RRGGBB"
class ColorConverter : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        => writer.WriteValue($"#{value.R:X2}{value.G:X2}{value.B:X2}");

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        var hex = reader.Value!.ToString()!.TrimStart('#');
        return new Color(
            Convert.ToInt32(hex[0..2], 16),
            Convert.ToInt32(hex[2..4], 16),
            Convert.ToInt32(hex[4..6], 16));
    }
}

class Palette
{
    public string Name { get; set; } = "";

    [JsonConverter(typeof(ColorConverter))]
    public Color Background { get; set; }

    [JsonConverter(typeof(ColorConverter))]
    public Color Foreground { get; set; }
}

readonly struct Color(int r, int g, int b)
{
    public int R { get; } = r;
    public int G { get; } = g;
    public int B { get; } = b;
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo09.CustomConverter/Demo09.CustomConverter.csproj
```

Expected output contains:
```
=== Serialized ===
{
  "Name": "Sunset",
  "Background": "#FF5E3A",
  "Foreground": "#FFFFFF"
}
```

- [ ] **Step 4: Commit**

```powershell
git add Demo09.CustomConverter/
git commit -m "feat: add Demo09.CustomConverter"
```

---

## Task 12: Demo10.SerializationCallbacks

**Files:**
- Create: `Demo10.SerializationCallbacks/Demo10.SerializationCallbacks.csproj`
- Create: `Demo10.SerializationCallbacks/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo10.SerializationCallbacks/Demo10.SerializationCallbacks.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo10.SerializationCallbacks/Program.cs`:
```csharp
using Newtonsoft.Json;
using System.Runtime.Serialization;

var order = new Order
{
    Id    = 42,
    Items = new List<string> { "Apple", "Banana", "Cherry" },
    CreatedAt = new DateTime(2026, 6, 28)
};

Console.WriteLine("=== Serialize ===");
string json = JsonConvert.SerializeObject(order, Formatting.Indented);
Console.WriteLine(json);

Console.WriteLine("\n=== Deserialize ===");
var restored = JsonConvert.DeserializeObject<Order>(json);
Console.WriteLine($"Id={restored!.Id}");
Console.WriteLine($"ItemCount (computed in OnDeserialized)={restored.ItemCount}");
Console.WriteLine($"Summary={restored.Summary}");

class Order
{
    public int Id { get; set; }
    public List<string> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }

    // These are computed after deserialization — not stored in JSON
    [JsonIgnore] public int ItemCount { get; private set; }
    [JsonIgnore] public string Summary { get; private set; } = "";

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
        => Console.WriteLine($"  [OnSerializing]  Id={Id}, Items={Items.Count}");

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
        => Console.WriteLine($"  [OnSerialized]   done");

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
        => Console.WriteLine($"  [OnDeserializing] starting...");

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
        // Recompute derived state after all properties are set
        ItemCount = Items.Count;
        Summary   = $"Order #{Id}: {ItemCount} items";
        Console.WriteLine($"  [OnDeserialized] ItemCount={ItemCount}");
    }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo10.SerializationCallbacks/Demo10.SerializationCallbacks.csproj
```

Expected output contains:
```
  [OnSerializing]  Id=42, Items=3
  [OnSerialized]   done
  [OnDeserializing] starting...
  [OnDeserialized] ItemCount=3
```

- [ ] **Step 4: Commit**

```powershell
git add Demo10.SerializationCallbacks/
git commit -m "feat: add Demo10.SerializationCallbacks"
```

---

## Task 13: Demo14.TypeNameHandling

**Files:**
- Create: `Demo14.TypeNameHandling/Demo14.TypeNameHandling.csproj`
- Create: `Demo14.TypeNameHandling/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo14.TypeNameHandling/Demo14.TypeNameHandling.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo14.TypeNameHandling/Program.cs`:
```csharp
using Newtonsoft.Json;

// TypeNameHandling.Auto: emits $type only when declared type != actual type
var settings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.Auto,
    Formatting       = Formatting.Indented
};

var shapes = new List<Shape>
{
    new Circle    { Color = "Red",   Radius = 5.0 },
    new Rectangle { Color = "Blue",  Width = 10.0, Height = 4.0 },
    new Circle    { Color = "Green", Radius = 3.5 }
};

string json = JsonConvert.SerializeObject(shapes, settings);
Console.WriteLine("=== Serialized with $type ===");
Console.WriteLine(json);

// Deserialize: $type drives concrete type selection
var restored = JsonConvert.DeserializeObject<List<Shape>>(json, settings);
Console.WriteLine("\n=== Deserialized (polymorphic) ===");
foreach (var shape in restored!)
    Console.WriteLine($"  {shape.GetType().Name,-12} Color={shape.Color,-6}  Area={shape.Area(),7:F2}");

abstract class Shape
{
    public string Color { get; set; } = "";
    public abstract double Area();
}

class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area() => Math.PI * Radius * Radius;
}

class Rectangle : Shape
{
    public double Width  { get; set; }
    public double Height { get; set; }
    public override double Area() => Width * Height;
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo14.TypeNameHandling/Demo14.TypeNameHandling.csproj
```

Expected output contains:
```
=== Deserialized (polymorphic) ===
  Circle       Color=Red    Area=  78.54
  Rectangle    Color=Blue   Area=  40.00
  Circle       Color=Green  Area=  38.48
```

- [ ] **Step 4: Commit**

```powershell
git add Demo14.TypeNameHandling/
git commit -m "feat: add Demo14.TypeNameHandling"
```

---

## Task 14: Demo15.CircularReference

**Files:**
- Create: `Demo15.CircularReference/Demo15.CircularReference.csproj`
- Create: `Demo15.CircularReference/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo15.CircularReference/Demo15.CircularReference.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo15.CircularReference/Program.cs`:
```csharp
using Newtonsoft.Json;

// Build object graph with circular references: Employee <-> Department
var dept  = new Department { Name = "Engineering" };
var alice = new Employee   { Name = "Alice", Department = dept };
var bob   = new Employee   { Name = "Bob",   Department = dept, Manager = alice };
dept.Employees.Add(alice);
dept.Employees.Add(bob);

// 1. ReferenceLoopHandling.Ignore — stops recursion silently
var ignoreSettings = new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    Formatting            = Formatting.Indented
};
Console.WriteLine("=== ReferenceLoopHandling.Ignore ===");
Console.WriteLine(JsonConvert.SerializeObject(alice, ignoreSettings));

// 2. PreserveReferencesHandling — keeps full graph using $id/$ref
var preserveSettings = new JsonSerializerSettings
{
    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
    Formatting                 = Formatting.Indented
};
Console.WriteLine("\n=== PreserveReferencesHandling.Objects ===");
Console.WriteLine(JsonConvert.SerializeObject(alice, preserveSettings));

// 3. Default (no handling) — throws on loop
Console.WriteLine("\n=== Default throws on circular reference ===");
try
{
    JsonConvert.SerializeObject(alice);
}
catch (JsonSerializationException ex)
{
    Console.WriteLine($"Caught: {ex.Message[..60]}...");
}

class Department
{
    public string Name { get; set; } = "";
    public List<Employee> Employees { get; set; } = new();
}

class Employee
{
    public string Name { get; set; } = "";
    public Department? Department { get; set; }
    public Employee? Manager { get; set; }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo15.CircularReference/Demo15.CircularReference.csproj
```

Expected output contains:
```
=== ReferenceLoopHandling.Ignore ===
=== PreserveReferencesHandling.Objects ===
=== Default throws on circular reference ===
Caught: ...
```

- [ ] **Step 4: Commit**

```powershell
git add Demo15.CircularReference/
git commit -m "feat: add Demo15.CircularReference"
```

---

## Task 15: Demo16.ContractResolver

**Files:**
- Create: `Demo16.ContractResolver/Demo16.ContractResolver.csproj`
- Create: `Demo16.ContractResolver/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo16.ContractResolver/Demo16.ContractResolver.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo16.ContractResolver/Program.cs`:
```csharp
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var user = new User { FirstName = "Alice", LastName = "Wang", Age = 30, InternalId = 99 };

// 1. Built-in CamelCasePropertyNamesContractResolver
var camelSettings = new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    Formatting       = Formatting.Indented
};
Console.WriteLine("=== CamelCase resolver ===");
Console.WriteLine(JsonConvert.SerializeObject(user, camelSettings));

// 2. Custom resolver that hides InternalId
var hiddenSettings = new JsonSerializerSettings
{
    ContractResolver = new HideInternalIdResolver(),
    Formatting       = Formatting.Indented
};
Console.WriteLine("\n=== HideInternalId resolver ===");
Console.WriteLine(JsonConvert.SerializeObject(user, hiddenSettings));

// 3. Dynamic property selection via ShouldSerialize delegate
var conditionalSettings = new JsonSerializerSettings
{
    ContractResolver = new AgeAbove28Resolver(),
    Formatting       = Formatting.Indented
};
Console.WriteLine("\n=== AgeAbove28 resolver (Age omitted because Age=30 > 28 is shown; test with Age=20) ===");
var young = new User { FirstName = "Bob", LastName = "Li", Age = 20, InternalId = 1 };
Console.WriteLine(JsonConvert.SerializeObject(young, conditionalSettings));

// HideInternalIdResolver: suppresses InternalId from output
class HideInternalIdResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (prop.PropertyName == nameof(User.InternalId))
            prop.ShouldSerialize = _ => false;
        return prop;
    }
}

// AgeAbove28Resolver: omits Age when value <= 28
class AgeAbove28Resolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (prop.PropertyName == nameof(User.Age))
            prop.ShouldSerialize = obj => obj is User u && u.Age > 28;
        return prop;
    }
}

class User
{
    public string FirstName  { get; set; } = "";
    public string LastName   { get; set; } = "";
    public int    Age        { get; set; }
    public int    InternalId { get; set; }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo16.ContractResolver/Demo16.ContractResolver.csproj
```

Expected output contains:
```
=== CamelCase resolver ===
{
  "firstName": "Alice",
  ...
}
=== HideInternalId resolver ===
```
(InternalId absent in output)

- [ ] **Step 4: Commit**

```powershell
git add Demo16.ContractResolver/
git commit -m "feat: add Demo16.ContractResolver"
```

---

## Task 16: Demo17.StreamingApi

**Files:**
- Create: `Demo17.StreamingApi/Demo17.StreamingApi.csproj`
- Create: `Demo17.StreamingApi/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo17.StreamingApi/Demo17.StreamingApi.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo17.StreamingApi/Program.cs`:
```csharp
using Newtonsoft.Json;

// JsonTextWriter: stream-write large JSON without building the full object graph in memory
var sb = new System.Text.StringBuilder();
using (var sw     = new System.IO.StringWriter(sb))
using (var writer = new JsonTextWriter(sw) { Formatting = Formatting.Indented })
{
    writer.WriteStartArray();
    for (int i = 1; i <= 5; i++)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("id");    writer.WriteValue(i);
        writer.WritePropertyName("name");  writer.WriteValue($"Item {i}");
        writer.WritePropertyName("price"); writer.WriteValue(Math.Round(i * 9.99, 2));
        writer.WriteEndObject();
    }
    writer.WriteEndArray();
}
Console.WriteLine("=== JsonTextWriter output ===");
Console.WriteLine(sb.ToString());

// JsonTextReader: stream-read without deserializing the whole document
Console.WriteLine("\n=== JsonTextReader: extract all 'name' values ===");
using var sr     = new System.IO.StringReader(sb.ToString());
using var reader = new JsonTextReader(sr);
while (reader.Read())
{
    if (reader.TokenType == JsonToken.PropertyName
        && reader.Value?.ToString() == "name")
    {
        reader.Read();   // advance to the value token
        Console.WriteLine($"  name = {reader.Value}");
    }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo17.StreamingApi/Demo17.StreamingApi.csproj
```

Expected output contains:
```
=== JsonTextWriter output ===
[
  {
    "id": 1,
    "name": "Item 1",
    "price": 9.99
  },
  ...
]
=== JsonTextReader: extract all 'name' values ===
  name = Item 1
  name = Item 2
```

- [ ] **Step 4: Commit**

```powershell
git add Demo17.StreamingApi/
git commit -m "feat: add Demo17.StreamingApi"
```

---

## Task 17: Demo18.JObjectMerge

**Files:**
- Create: `Demo18.JObjectMerge/Demo18.JObjectMerge.csproj`
- Create: `Demo18.JObjectMerge/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo18.JObjectMerge/Demo18.JObjectMerge.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo18.JObjectMerge/Program.cs`:
```csharp
using Newtonsoft.Json.Linq;

// Base config represents defaults; prod override provides environment-specific values
var baseConfig = JObject.Parse("""
{
  "app": "MyApp",
  "version": "1.0.0",
  "database": { "host": "localhost", "port": 5432, "name": "mydb" },
  "features": { "darkMode": false, "beta": false }
}
""");

var prodOverride = JObject.Parse("""
{
  "version": "1.2.3",
  "database": { "host": "prod-db.example.com", "name": "mydb_prod" },
  "features": { "beta": true }
}
""");

Console.WriteLine("=== Base config ===");
Console.WriteLine(baseConfig.ToString());

// Deep merge: nested objects merged, null overrides ignored
baseConfig.Merge(prodOverride, new JsonMergeSettings
{
    MergeNullValueHandling = MergeNullValueHandling.Ignore,
    MergeArrayHandling     = MergeArrayHandling.Union
});
Console.WriteLine("\n=== After prod override merge ===");
Console.WriteLine(baseConfig.ToString());

// Null-patch: set a key to null to delete it
var patch = JObject.Parse("""{"features":{"darkMode":null}}""");
baseConfig.Merge(patch, new JsonMergeSettings
{
    MergeNullValueHandling = MergeNullValueHandling.Merge
});

// Strip all null-valued properties after merge
var nullProps = baseConfig.Descendants()
    .OfType<JProperty>()
    .Where(p => p.Value.Type == JTokenType.Null)
    .ToList();
foreach (var p in nullProps) p.Remove();

Console.WriteLine("\n=== After null-patch (darkMode removed) ===");
Console.WriteLine(baseConfig.ToString());
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo18.JObjectMerge/Demo18.JObjectMerge.csproj
```

Expected output contains:
```
=== After prod override merge ===
```
- `version` changed to `1.2.3`
- `database.host` changed to `prod-db.example.com`
- `database.port` still `5432` (not overridden)
- `darkMode` removed after null-patch

- [ ] **Step 4: Commit**

```powershell
git add Demo18.JObjectMerge/
git commit -m "feat: add Demo18.JObjectMerge"
```

---

## Task 18: Demo19.ErrorHandling

**Files:**
- Create: `Demo19.ErrorHandling/Demo19.ErrorHandling.csproj`
- Create: `Demo19.ErrorHandling/Program.cs`

- [ ] **Step 1: 创建 csproj**

`Demo19.ErrorHandling/Demo19.ErrorHandling.csproj`:
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

- [ ] **Step 2: 创建 Program.cs**

`Demo19.ErrorHandling/Program.cs`:
```csharp
using Newtonsoft.Json;

// Batch deserialize: bad items are skipped, good ones succeed
string json = """
[
  {"Id":1,"Name":"Alice","Score":95},
  {"Id":2,"Name":"Bob",  "Score":"not-a-number"},
  {"Id":3,"Name":"Carol","Score":88},
  {"Id":"bad-id","Name":"Dave","Score":72},
  {"Id":5,"Name":"Eve",  "Score":100}
]
""";

var errors = new List<string>();
var settings = new JsonSerializerSettings
{
    Error = (_, args) =>
    {
        errors.Add($"  Path={args.ErrorContext.Path}  →  {args.ErrorContext.Error.Message}");
        args.ErrorContext.Handled = true;  // mark handled so deserialization continues
    }
};

var results = JsonConvert.DeserializeObject<List<Student>>(json, settings) ?? [];

Console.WriteLine($"=== Successfully deserialized ({results.Count} items) ===");
foreach (var s in results)
    Console.WriteLine($"  Id={s.Id}, Name={s.Name}, Score={s.Score}");

Console.WriteLine($"\n=== Errors ({errors.Count}) ===");
foreach (var e in errors) Console.WriteLine(e);

class Student
{
    public int    Id    { get; set; }
    public string Name  { get; set; } = "";
    public int    Score { get; set; }
}
```

- [ ] **Step 3: 构建并运行**

```powershell
dotnet run --project Demo19.ErrorHandling/Demo19.ErrorHandling.csproj
```

Expected output contains:
```
=== Successfully deserialized (3 items) ===
  Id=1, Name=Alice, Score=95
  Id=3, Name=Carol, Score=88
  Id=5, Name=Eve,   Score=100
=== Errors (2) ===
```

- [ ] **Step 4: Commit**

```powershell
git add Demo19.ErrorHandling/
git commit -m "feat: add Demo19.ErrorHandling"
```

---

## Task 19: 更新 slnx + 全量构建验证

**Files:**
- Modify: `Learning.JsonNet.slnx` (注册全部 20 个项目，移除旧 5 个)

- [ ] **Step 1: 替换 slnx 内容**

`Learning.JsonNet.slnx`:
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

- [ ] **Step 2: 全量构建验证**

```powershell
dotnet build Learning.JsonNet.slnx
```

Expected:
```
Build succeeded.
    0 Error(s)
    0 Warning(s)
```

- [ ] **Step 3: Commit**

```powershell
git add Learning.JsonNet.slnx
git commit -m "chore: update slnx to register all Demo00-Demo19 projects"
```

---

## 自查

| 需求 | 对应 Task |
|---|---|
| Demo00.Shared 重命名，namespace 更新 | Task 1 |
| Demo07 迁移，引用 Demo00.Shared | Task 2 |
| Demo11/12/13 重命名 | Task 3 |
| 旧文件夹删除（git mv 自动处理） | Task 1-3 |
| Demo01-06 新建 | Task 4-9 |
| Demo08-10 新建 | Task 10-12 |
| Demo14-19 新建 | Task 13-18 |
| slnx 更新 + 全量构建 | Task 19 |
| 所有项目 net9.0 + Nullable | 全部 Task |
| 顶级语句，英文注释 | 全部 Task |
| CPM（csproj 无版本号） | 全部 Task |
