# Technology Stack

## Programming Languages
- **C# 13** (net10.0-windows projects) — primary language for all new projects
- **C# (legacy)** — older projects target net462 with older language features
- **XAML** — UI markup for all WPF projects

## Target Frameworks
| Framework | Projects |
|---|---|
| `net10.0-windows` | Test.MapperlyDemo and newer projects |
| `net462` | Test.DragControl, Test.ListBoxPage, Test.Moonpdf, Test.MultiTouch, older demos |
| Mixed | Some projects use net6/net8-windows (check individual .csproj) |

## Build System
- **MSBuild / .NET SDK** via `dotnet` CLI or Visual Studio
- **Solution format**: `.slnx` (new lightweight XML solution format)
- **Central Package Management (CPM)**: `Directory.Packages.props` at solution root — all `PackageReference` entries omit `Version`; versions are declared once centrally
- `AppendTargetFrameworkToOutputPath=false` and `AppendRuntimeIdentifierToOutputPath=false` set globally

## Key NuGet Dependencies

### MVVM Frameworks
| Package | Version | Usage |
|---|---|---|
| `CommunityToolkit.Mvvm` | 8.4.0 | Modern source-gen MVVM (newer projects) |
| `MvvmLight` / `MvvmLightLibs` | 5.4.1.1 | Legacy MVVM (Test.DragControl) |
| `Stylet` | 1.3.7 | Lightweight MVVM (CountDown, DocumentViewer, Screenshot) |

### UI / Controls
| Package | Version | Usage |
|---|---|---|
| `HandyControl` | 3.2.0 | Extended WPF control library |
| `Microsoft.Xaml.Behaviors.Wpf` | 1.1.135 | Attached behaviors |
| `MvvmDialogs` | 8.0.0 | Dialog service abstraction |

### Data Access
| Package | Version | Usage |
|---|---|---|
| `SQLite.CodeFirst` | 1.7.0.34 | EF Code-First with SQLite |
| `sqlite-net` | 1.6.292 | Lightweight SQLite ORM |
| `System.Data.SQLite.x64` | 1.0.118 | SQLite ADO.NET provider |
| `SqlSugar` | 5.0.3.2 | Fluent ORM |
| `MySql.Data` | 8.0.27 | MySQL connector |

### Object Mapping
| Package | Version | Usage |
|---|---|---|
| `Riok.Mapperly` | 4.3.1 | Compile-time source-generator mapper |

### Multimedia
| Package | Version | Usage |
|---|---|---|
| `LibVLCSharp` | 3.9.5 | VLC media engine bindings |
| `LibVLCSharp.WPF` | 3.9.5 | WPF VideoView control |
| `VideoLAN.LibVLC.Windows` | 3.0.23 | Native VLC binaries |
| `NAudio` | 1.10.0 | Audio playback |
| `System.Speech` | 5.0.0 | TTS via SAPI |
| `TencentCloudSDK` | 3.0.384 | Tencent Cloud TTS API |

### Networking & Serialization
| Package | Version | Usage |
|---|---|---|
| `Newtonsoft.Json` | 13.0.1 | JSON serialization |
| `RestSharp` | 112.1.0 | HTTP client |
| `JsonSubTypes` | 1.8.0 | Polymorphic JSON deserialization |

### System & Utilities
| Package | Version | Usage |
|---|---|---|
| `Hardware.Info` | 101.0.1.1 | WMI hardware queries |
| `System.Management` | 8.0.0 | WMI access |
| `log4net` | 3.0.3 | Logging |
| `K4os.Compression.LZ4.Streams` | 1.2.15 | LZ4 compression |
| `CommonServiceLocator` | 2.0.6 | IoC service locator |

## Development Tools
- **Visual Studio 2022** (recommended, supports .slnx and WPF designer)
- **XamlStyler** extension — XAML auto-formatting on save (`Settings.XamlStyler` config at root)
- **dotnet CLI** for build/restore

## Common Build Commands
```bash
# Restore all packages
dotnet restore WpfTest.slnx

# Build entire solution
dotnet build WpfTest.slnx

# Build specific project
dotnet build Test.MapperlyDemo/Test.MapperlyDemo.csproj

# Run a specific project
dotnet run --project Test.MapperlyDemo/Test.MapperlyDemo.csproj
```

## C# Language Features in Use
- **Nullable reference types** (`<Nullable>enable</Nullable>`) — newer projects
- **Implicit usings** (`<ImplicitUsings>enable</ImplicitUsings>`) — newer projects
- **Collection expressions** (`[]` initializer syntax) — e.g., `List<OrderItem> Items { get; set; } = []`
- **Partial classes/methods** — used with Mapperly source generator
- **Pattern matching** — used in converters and helpers
- **LINQ** — throughout ViewModels and mappers
- **Generic variance** (`in`/`out`) — demonstrated in Test.CovariantAndContravariant
- **Tuple deconstruction** — e.g., `foreach (var (item, itemDto) in order.Items.Zip(dto.Items))`
