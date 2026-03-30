# 项目地图 - WPF 学习仓库导航

## 项目总览
- **总项目数**: 200+
- **主解决方案**: WpfTest/WpfTest.slnx (30+ 项目)
- **项目类型**: 独立学习示例（非单体应用）
- **技术跨度**: .NET Framework 4.6.2 → .NET 10.0

## 项目分类地图

### 1. 入门级示例（适合 WPF 初学者）
```
WpfTest/
├── Test.HelloWorld/              # WPF 基础 - 第一个窗口
├── Test.DataBinding/             # 数据绑定入门 - OneWay, TwoWay
├── Test.SimpleCommand/           # 命令模式基础 - ICommand
└── Test.ControlsBasic/           # 基础控件使用
```

### 2. MVVM 模式学习路径
```
A. 手写 MVVM（理解原理）
   └── Test.ManualMvvm/           # 手动实现 INotifyPropertyChanged

B. MvvmLight（旧框架 - 用于对比）
   └── Learning.MvvmLight/        # ViewModelLocator + SimpleIoc

C. Stylet（中间选择）
   └── Learning.Stylet/           # IoC + 引导程序

D. CommunityToolkit.Mvvm（现代推荐 ⭐）
   └── Test.CommunityToolkitDemo/ # [ObservableProperty] + [RelayCommand]
```

### 3. 完整架构示例（生产级参考）
```
⭐ Test.DragControl/               # 最完整示例 - 所有层齐全
   ├── Models/                    # 领域模型
   ├── ViewModels/                # 视图模型
   ├── Views/                     # XAML 视图
   ├── Services/                  # 业务服务
   ├── Converters/                # 值转换器
   └── Utils/                     # 工具类

   Test.SqliteEFDemo/             # 数据持久化示例
   Test.MapperlyDemo/             # 对象映射示例
```

### 4. 框架和模式演示
```
Learning.Prism/                   # Prism 模块化框架
Learning.Unity/                   # Unity IoC 容器
Learning.MEF/                     # MEF 可扩展性框架
Learning.Autofac/                 # Autofac IoC 容器
```

### 5. 高级特性
```
WpfTest/MultiLang/
├── Test.MultiLang1/              # RESX 资源文件方式
├── Test.MultiLang2/              # 动态语言切换
├── Test.MultiLang3/              # 标记扩展方式
└── Test.MultiLang4/              # WeakEventManager 模式

WpfTest/WindowsMessage/
├── Test.IpcNamedPipe/            # 命名管道通信
├── Test.IpcMemoryMap/            # 内存映射通信
└── Test.WinMsgHook/              # Windows 消息钩子
```

### 6. 数据访问层
```
Test.SqliteEFDemo/                # Entity Framework Core + SQLite
Test.SqlSugarDemo/                # SqlSugar ORM
Test.AdoNetBasic/                 # ADO.NET 原生
```

### 7. 对象映射
```
⭐ Test.MapperlyDemo/              # Mapperly (编译时) - 推荐
   Test.AutoMapperDemo/           # AutoMapper (运行时) - 旧方案
```

### 8. 多媒体
```
Test.VlcPlayer/                   # LibVLCSharp 视频播放
Test.AudioRecorder/               # NAudio 音频录制
Test.MediaPlayer/                 # Windows Media Foundation
```

### 9. UI 库演示
```
Test.HandyControl/                # HandyControl 3.2.0
Test.MaterialDesign/              # Material Design in XAML
Test.ModernWpf/                   # Modern WPF UI Library
```

### 10. 特殊技术
```
Test.DragDrop/                    # 拖放功能
Test.CustomControl/               # 自定义控件
Test.AttachedProperty/            # 附加属性
Test.Behavior/                    # XAML Behaviors
```

## 关键文件位置

### 解决方案级别
```
WpfTest/
├── WpfTest.slnx                  # 主解决方案（新格式）
├── Directory.Packages.props      # 集中式包管理 ⚠️
└── Settings.XamlStyler           # XAML 格式化规则
```

### 项目级别
```
Test.XxxDemo/
├── Test.XxxDemo.csproj           # 项目文件
├── App.xaml                      # 应用程序入口
├── Models/                       # 数据模型
├── ViewModels/                   # 视图模型
├── Views/                        # XAML 视图
├── Converters/                   # 值转换器
├── Services/                     # 服务接口
└── Utils/ 或 Helper/             # 工具类
```

## 学习路径建议

### 路径 1: WPF 基础 → MVVM → 完整应用
```
1. Test.HelloWorld          # 了解 WPF 窗口和控件
2. Test.DataBinding         # 掌握数据绑定
3. Test.SimpleCommand       # 理解命令模式
4. Test.ManualMvvm          # 手写 MVVM 理解原理
5. Test.CommunityToolkitDemo # 现代 MVVM 框架
6. Test.DragControl         # 完整架构参考
```

### 路径 2: 框架对比学习
```
1. Test.ManualMvvm          # 手写 MVVM（基准）
2. Learning.MvvmLight       # MvvmLight（2016）
3. Learning.Stylet          # Stylet（2018）
4. Test.CommunityToolkitDemo # CommunityToolkit（2024）

对比维度：
- 代码量
- 类型安全
- 性能
- 源生成器支持
```

### 路径 3: 企业级应用技能
```
1. Test.DragControl         # 分层架构
2. Test.SqliteEFDemo        # 数据持久化
3. Test.MapperlyDemo        # 对象映射
4. Test.MultiLang1          # 国际化
5. Learning.Prism           # 模块化设计
6. Test.IpcNamedPipe        # 进程间通信
```

## 快速查找指南

### 我想学习...
- **数据绑定**: Test.DataBinding
- **命令模式**: Test.SimpleCommand
- **MVVM 模式**: Test.CommunityToolkitDemo
- **完整架构**: Test.DragControl ⭐
- **对象映射**: Test.MapperlyDemo
- **多语言**: WpfTest/MultiLang/
- **数据库**: Test.SqliteEFDemo
- **自定义控件**: Test.CustomControl
- **拖放**: Test.DragDrop
- **多媒体**: Test.VlcPlayer

### 我想看最佳实践...
- **现代 MVVM**: Test.CommunityToolkitDemo
- **编译时映射**: Test.MapperlyDemo
- **分层架构**: Test.DragControl
- **集中包管理**: WpfTest/Directory.Packages.props

### 我想对比学习...
- **MVVM 框架演进**: ManualMvvm → MvvmLight → Stylet → CommunityToolkit
- **对象映射**: AutoMapper vs Mapperly
- **IoC 容器**: Unity vs Autofac

## 项目状态标识

- ⭐ = 推荐参考（现代最佳实践）
- 🏛️ = 历史项目（保留用于对比学习）
- 🚧 = 开发中
- ⚠️ = 关键配置文件

## 技术栈速查

### .NET 版本分布
```
.NET Framework 4.6.2    [████░░░░░░] 40+ 项目（历史）
.NET 9.0                [██████░░░░] 60+ 项目（稳定）
.NET 10.0               [██████████] 100+ 项目（最新）⭐
```

### MVVM 框架分布
```
Manual                  [███░░░░░░░] 30%（教学）
MvvmLight               [██░░░░░░░░] 20%（历史）
Stylet                  [█░░░░░░░░░] 10%（历史）
CommunityToolkit.Mvvm   [█████░░░░░] 40%（推荐）⭐
```

## 注意事项

⚠️ **不要假设所有项目使用相同的框架** - 这是教学仓库，故意展示多种方案

⚠️ **不要在独立项目间创建依赖** - 每个示例应该独立运行

⚠️ **不要升级所有项目到最新框架** - 保留历史版本用于对比学习

⚠️ **Test.* 不是单元测试** - 它们是 GUI 演示应用程序
