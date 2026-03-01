# Product Overview

## Project Purpose
WpfTest is a multi-project WPF learning and demonstration workspace. It serves as a hands-on reference collection of WPF and .NET techniques, covering UI patterns, data access, MVVM architecture, and advanced C# language features. Each sub-project is a self-contained demo targeting a specific concept or technology.

## Key Features and Capabilities

### UI & Controls
- **Brush demos**: DrawingBrush, VisualBrush, OpacityMask, TileBrush effects
- **Transform demos**: LayoutTransform vs RenderTransform, animated transforms
- **Custom controls**: CountDown control, IconFont radio button, HwndHost for native Win32 embedding
- **InkCanvas**: Digital ink/drawing input demo
- **DataGrid & ListView**: Sorting, grouping, custom templates, header/cell templates
- **ComboBox**: Custom item templates with images and enums
- **ListBox**: Paged data loading with ViewModel
- **Drag & Drop**: Behavior-based draggable elements, drag-move behaviors

### Architecture & Patterns
- **MVVM**: Multiple MVVM frameworks (MvvmLight, CommunityToolkit.Mvvm, Stylet)
- **Attached Properties & Behaviors**: Custom attached properties, Microsoft.Xaml.Behaviors
- **Navigation**: Frame-based navigation service with page key constants
- **Locator pattern**: ViewModelLocator for DI/service resolution

### Data & Persistence
- **SQLite (EF Core)**: Entity Framework Core with SQLite backend
- **SQLite (sqlite-net)**: Lightweight ORM via sqlite-net library
- **SqlSugar ORM**: Fluent ORM for .NET
- **Object Mapping**: Riok.Mapperly source-generator-based mapper (compile-time, zero-reflection)

### Multimedia & System
- **PDF viewing**: MoonPdf (MuPDF-based) and XPS DocumentViewer
- **Video playback**: LibVLCSharp + HwndHost integration
- **Audio**: NAudio playback, System.Speech TTS, Tencent Cloud TTS
- **Screenshot**: Win32 API-based screen capture
- **Hardware info**: System.Management WMI queries via Hardware.Info
- **Multi-touch**: Touch and mouse wheel zoom/pan gestures

### Multilanguage
- **Resource-based i18n**: RESX resource files with culture switching at runtime
- **Custom markup extension**: TranslateExtension for XAML binding to language resources
- **LanguageManager**: Centralized culture change with WeakEventManager pattern

### Interop & Messaging
- **Windows Messages**: WM_COPYDATA inter-process communication between WPF apps
- **HwndHost**: Embedding native Win32 windows inside WPF

### C# Language Features
- **Covariance & Contravariance**: Generic interface variance demonstrations

## Target Users
- .NET/WPF developers learning advanced UI patterns
- Developers evaluating MVVM frameworks and ORM libraries
- Engineers needing reference implementations for WPF-specific features (HwndHost, InkCanvas, multi-touch, etc.)

## Use Cases
- Copy-paste reference for WPF control patterns
- Evaluating and comparing MVVM/ORM/mapping libraries
- Learning WPF brush, transform, and animation techniques
- Prototyping inter-process communication in WPF desktop apps
