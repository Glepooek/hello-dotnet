# Development Guidelines

## Code Quality Standards

### Naming Conventions
- **PascalCase** for classes, interfaces, properties, methods, enums, and enum members
- **camelCase** with `_` prefix for private fields: `_key`, `_execute`, `_canExecute`, `m_Execute` (older style uses `m_` prefix)
- **Interfaces** prefixed with `I`: `IAnimal`, `IAnimalList<T>`, `IFrameNavigationService`
- **Generic type parameters**: `T`, `TResult`, `TInput`, `TOutput` — descriptive when context warrants
- **Namespace** mirrors folder structure: `Test.DragControl.Utils`, `LanguageResource`, `Test.MapperlyDemo.Mappers`
- **Project naming**: `Test.XxxDemo` for demo projects, plain name for shared libraries (e.g., `LanguageResource`)

### File Organization
- One primary class per file; related small classes (DTOs, enums) may share a file
- Converters grouped in a single `Converters.cs` file per project
- Models, ViewModels, Views, Utils/Helper, Services, Controls each in their own folder
- `Properties/` folder for AssemblyInfo, Resources.resx, Settings

### Comments and Documentation
- XML doc comments (`<summary>`, `<param>`, `<returns>`) on public APIs and constructors
- Inline Chinese comments are common and acceptable — this codebase is bilingual (Chinese + English)
- Explanatory block comments at file top for conceptual demos (see covariance/contravariance example)
- Commented-out code blocks are used as reference/documentation (e.g., HardwareInfo refresh methods)

---

## MVVM Patterns

### ViewModel Base
Older projects implement a minimal custom base:
```csharp
public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```
Newer projects use `CommunityToolkit.Mvvm` source generators (`[ObservableProperty]`, `[RelayCommand]`).

### Commands
Custom `DelegateCommand` / `DelegateCommand<T>` implementing `ICommand`:
- Supports both parameterless `Action` and `Action<T>` overloads
- `CanExecuteChanged` hooks into `CommandManager.RequerySuggested`
- `ConvertBack` on one-way converters returns `Binding.DoNothing` or `null` (not `NotImplementedException`) when the back-conversion is intentionally unsupported

### ViewModelLocator
Used in Test.DragControl with MvvmLight IoC:
```csharp
// Register in locator constructor
SimpleIoc.Default.Register<MainViewModel>();
// Expose as property
public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
```

---

## WPF-Specific Patterns

### IValueConverter Implementation
All converters follow this structure:
```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // parameter-driven behavior: check parameter?.ToString().ToLower()
        var param = parameter == null ? string.Empty : parameter.ToString();
        param = param.ToLower();
        if (value is bool boolVal)
        {
            return boolVal ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // or return Binding.DoNothing
    }
}
```
- Converter parameter is always lowercased before comparison
- `ConvertBack` throws `NotImplementedException` for one-way converters, or returns `Binding.DoNothing` for two-way converters that don't need back-conversion

### Attached Behaviors
Behaviors extend `Behavior<T>` from `Microsoft.Xaml.Behaviors`:
```csharp
public class DraggableBehavior : Behavior<UIElement>
{
    protected override void OnAttached() { /* subscribe events */ }
    protected override void OnDetaching() { /* unsubscribe events */ }
}
```

### Custom Controls
Custom controls use `Generic.xaml` in `Themes/` folder with `DefaultStyleKeyProperty.OverrideMetadata`:
```csharp
static CountDownControl()
{
    DefaultStyleKeyProperty.OverrideMetadata(typeof(CountDownControl),
        new FrameworkPropertyMetadata(typeof(CountDownControl)));
}
```

### XAML Formatting (XamlStyler)
Enforced by `Settings.XamlStyler`:
- Max 1 attribute per line (after tolerance of 2)
- `RemoveEndingTagOfEmptyElement = true` — use `<Button />` not `<Button></Button>`
- `SpaceBeforeClosingSlash = true` — `<Button />`
- `FormatMarkupExtension = true`
- `Binding` and `x:Bind` kept on same line (`NoNewLineMarkupExtensions`)
- Thickness attributes (`Margin`, `Padding`, `BorderThickness`) use comma separator

---

## Object Mapping with Mapperly

### Mapper Class Pattern
```csharp
[Mapper]
public partial class OrderMapper
{
    // Public entry point — handles computed properties manually
    public OrderDto ToDto(Order order)
    {
        var dto = ToOrderDto(order);
        dto.CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}";
        dto.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);
        return dto;
    }

    // Private partial — Mapperly generates the implementation
    [MapProperty(nameof(Order.Customer) + "." + nameof(Customer.Email), nameof(OrderDto.CustomerEmail))]
    [MapProperty(nameof(Order.CreatedAt), nameof(OrderDto.CreatedAt), StringFormat = "yyyy-MM-dd HH:mm:ss")]
    private partial OrderDto ToOrderDto(Order order);

    // Nested/collection mappers declared as partial — auto-implemented by Mapperly
    private partial OrderItemDto ToItemDto(OrderItem item);
    private partial AddressDto ToAddressDto(Address address);

    // Custom enum-to-string conversion
    private string MapStatus(OrderStatus status) => status.ToString();
}
```
- Use `[MapProperty]` with `nameof` chains for flattened/renamed properties
- Computed properties (aggregations, concatenations) are handled manually in the public wrapper method
- Nested object and collection mappers are declared as `private partial` methods — Mapperly discovers and calls them automatically

---

## Multilanguage / i18n Pattern

### Resource Files
- RESX files per culture: `Resources.zh.resx`, `Resources.en.resx`
- Designer files auto-generated by Visual Studio

### Runtime Culture Switching
```csharp
// LanguageManager: static singleton holding ResourceManager
public static ResourceManager Resource => _resourceManager;
public static ICollection<CultureInfo> Languages => _cultures;
```

### XAML Markup Extension
```xaml
<TextBlock Text="{local:Translate Key=HelloWorld}" />
```
`TranslateExtension` returns a `Binding` to a `TranslationProvider` that implements `IWeakEventListener` — culture changes propagate without memory leaks.

### WeakEventManager Pattern
```csharp
// Publisher raises event
LanguageChangedEventManager.AddListener(LanguageChangedEventPublisher.Instance, this);

// Listener implements IWeakEventListener
public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
{
    if (managerType == typeof(LanguageChangedEventManager))
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        return true;
    }
    return false;
}
```

---

## Data Access Patterns

### SQLite with sqlite-net
```csharp
// Table attribute on model
[Table("stocks")]
public class Stock { [PrimaryKey, AutoIncrement] public int Id { get; set; } }

// Helper wraps SQLiteConnection
var db = new SQLiteConnection(dbPath);
db.CreateTable<Stock>();
db.Insert(new Stock { ... });
var list = db.Table<Stock>().ToList();
```

### SQLite with EF Core (Code First)
```csharp
// DbContext with SQLite provider
public class SQLiteDbContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.db");
}
```

---

## Console / Non-WPF Projects

### Top-Level Statements
Console demos use top-level statements (no explicit `Program` class or `Main` method) for newer projects:
```csharp
// Test.HardwareInfoDemo/Program.cs
IHardwareInfo hardwareInfo = new HardwareInfo();
hardwareInfo.RefreshAll();
Console.WriteLine(hardwareInfo.OperatingSystem);
Console.ReadLine();
```

### Conceptual Demo Structure
Conceptual demos (covariance, etc.) define all types in a single file with a `Program` class and `Main` method, preceded by extensive explanatory comments.

---

## Error Handling Conventions
- `try/catch` with `Console.WriteLine(ex)` and early `return` for console demos
- WPF apps generally do not have global exception handlers in demos (learning context)
- Null checks use `null != obj` style (older code) or `obj is null` / `obj?.Member` (newer code)

---

## Dependency Injection / IoC
- **Stylet**: `Bootstrapper<ShellViewModel>` configures the IoC container; ViewModels resolved automatically
- **MvvmLight**: `SimpleIoc.Default.Register<T>()` + `ViewModelLocator` exposes instances
- **CommunityToolkit.Mvvm**: Constructor injection or direct instantiation in newer projects

---

## Frequently Used Idioms

```csharp
// Null-coalescing for parameter handling in converters
var param = parameter == null ? string.Empty : parameter.ToString();

// Collection expression initializer (C# 12+)
public List<OrderItem> Items { get; set; } = [];

// Tuple deconstruction with Zip
foreach (var (item, itemDto) in order.Items.Zip(dto.Items)) { }

// nameof chains for MapProperty
[MapProperty(nameof(Order.Customer) + "." + nameof(Customer.Email), nameof(OrderDto.CustomerEmail))]

// Static readonly fields for singletons/constants
private static readonly ResourceManager _resourceManager = new ResourceManager(...);
```

---

## Popular Annotations / Attributes

| Attribute | Usage |
|---|---|
| `[Mapper]` | Marks a Mapperly mapper class |
| `[MapProperty(src, dest)]` | Renames or flattens a property in Mapperly |
| `[MapProperty(..., StringFormat = "...")]` | Formats a value during mapping |
| `[ConstructorArgument("key")]` | Marks XAML markup extension constructor parameter |
| `[Table("name")]` | sqlite-net table name |
| `[PrimaryKey, AutoIncrement]` | sqlite-net primary key |
| `[ObservableProperty]` | CommunityToolkit.Mvvm source-gen property |
| `[RelayCommand]` | CommunityToolkit.Mvvm source-gen command |
