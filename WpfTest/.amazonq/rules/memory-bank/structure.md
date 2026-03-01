# Project Structure

## Solution Layout
`WpfTest.slnx` is the solution file (new `.slnx` format). All projects share centralized NuGet version management via `Directory.Packages.props`. XAML formatting is enforced by `Settings.XamlStyler`.

```
WpfTest/
├── Directory.Packages.props        # Central NuGet version management (CPM)
├── Settings.XamlStyler             # XAML formatting rules
├── WpfTest.slnx                    # Solution file (.slnx format)
│
├── MultiLang/                      # Multilanguage solution folder
│   ├── ChangeLanguage/             # Runtime language switching demo (WPF, .NET 4.x)
│   ├── LanguageResource/           # Shared language resource library
│   ├── Test.ChangeLang/            # RESX-based language change demo
│   └── Test.Multilanguage/         # Custom markup extension i18n demo
│
├── WindowsMesage/                  # Windows messaging solution folder
│   ├── Test.MainWpfApp/            # Sender app using WM_COPYDATA
│   ├── Test.Shared/                # Shared WindowsMessageHelper library
│   └── Test.SubWpfApp/             # Receiver app for inter-process messages
│
├── Test.AttachedProperties/        # Attached properties and behaviors demo
├── Test.BrushDemo/                 # WPF brush types demo (Drawing, Visual, OpacityMask)
├── Test.ComboboxDemo/              # ComboBox with custom templates and enums
├── Test.ControlAutoHide/           # Auto-hide control with mouse monitoring
├── Test.CountDown/                 # Custom countdown control (Stylet MVVM)
├── Test.CovariantAndContravariant/ # C# generic variance demo (console)
├── Test.DataGridDemo/              # DataGrid with ViewModel and converters
├── Test.DocumentViewer/            # XPS document viewer (Stylet MVVM)
├── Test.DragBehaviorDemo/          # Drag behavior implementations
├── Test.DragControl/               # Full MVVM app: drag, audio, SQLite, navigation
├── Test.HardwareInfoDemo/          # Hardware info via WMI (console)
├── Test.HwndHostDemo/              # HwndHost + LibVLC video embedding
├── Test.IconFonts/                 # Icon font (TTF) custom control
├── Test.InkCanvasDemo/             # InkCanvas drawing demo
├── Test.ListBoxPage/               # ListBox with paged ViewModel
├── Test.ListViewDemo/              # ListView: sort, group, templates
├── Test.MapperlyDemo/              # Riok.Mapperly object mapping demo
├── Test.Moonpdf/                   # PDF viewer via MoonPdf/MuPDF
├── Test.MultiTouch/                # Multi-touch and mouse wheel zoom
├── Test.Screenshot/                # Win32 screenshot capture (Stylet MVVM)
├── Test.SqliteEFDemo/              # SQLite with Entity Framework Core
├── Test.SqliteNetDemo/             # SQLite with sqlite-net ORM
└── Test.TransformDemo/             # WPF layout and render transforms
```

## Per-Project Internal Structure

Most WPF projects follow this layout:
```
Test.XxxDemo/
├── Models/          # Domain/data models
├── ViewModels/      # MVVM ViewModels (or *ViewModel.cs at root)
├── Views/           # XAML views (or MainWindow.xaml at root)
├── Converters/      # IValueConverter implementations
├── Controls/        # Custom controls
├── Utils/ or Helper/ # Utility classes, helpers, behaviors
├── Services/        # Service interfaces and implementations
├── Themes/          # Generic.xaml for custom control templates
├── App.xaml         # Application entry point
└── *.csproj         # Project file
```

The most feature-complete project (Test.DragControl) uses the full layered structure:
- `Controls/` — custom TextBox controls
- `Helper/` — NAudio, SqlSugar, TTS, logging helpers
- `Models/` — data entities
- `Services/` — IFrameNavigationService + NavigationService
- `Utils/` — BindingProxy, Converters, DelegateCommand, ViewModelBase, Behaviors
- `ViewModels/` — per-page ViewModels + ViewModelLocator
- `Views/` — per-page XAML views

## Core Components and Relationships

### MVVM Stack
- ViewModels expose `ObservableCollection<T>` and commands to Views via data binding
- Multiple MVVM frameworks coexist: MvvmLight (older projects), CommunityToolkit.Mvvm (newer), Stylet (CountDown, DocumentViewer, Screenshot)
- `ViewModelLocator` (MvvmLight IoC) wires ViewModels in Test.DragControl

### Mapping Layer (Test.MapperlyDemo)
```
Models/ (Order, Customer, Address, OrderItem, OrderStatus)
    ↓  [Riok.Mapperly source generator]
Mappers/OrderMapper.cs  →  Dtos/ (OrderDto, AddressDto, OrderItemDto)
```
- Compile-time code generation, no runtime reflection
- Manual post-processing for computed properties (TotalAmount, SubTotal, CustomerName)

### Language Resource Layer (MultiLang)
```
LanguageResource (library)
    ├── LanguageManager.cs          # Singleton, raises LanguageChanged event
    ├── LanguageChangedEventManager.cs  # WeakEventManager pattern
    ├── TranslateExtension.cs       # XAML markup extension
    ├── LanguageSelectionViewModel.cs
    └── CurrentCultureConverter.cs
```

### Windows Messaging Layer
```
Test.Shared/WindowsMessageHelper.cs  ← shared by both apps
Test.MainWpfApp  →  WM_COPYDATA  →  Test.SubWpfApp
```

## Architectural Patterns

- **MVVM** throughout all WPF projects; code-behind is minimal (only wiring)
- **Behavior pattern**: `Microsoft.Xaml.Behaviors.Wpf` for attached behaviors (drag, scroll, focus)
- **Source generation**: Mapperly generates mapper code at compile time
- **Central Package Management**: All NuGet versions declared once in `Directory.Packages.props`
- **WeakEventManager**: Used in LanguageResource to avoid memory leaks on event subscriptions
- **Service locator / DI**: ViewModelLocator in Test.DragControl; Stylet IoC bootstrapper in others
- **Partial class pattern**: Mapperly mappers use `partial` class + `partial` methods split between hand-written and generated code
