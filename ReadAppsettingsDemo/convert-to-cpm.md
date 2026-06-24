# CPM 转换报告 — AppConfigurationDemo

## 1. 转换概览

| 项目 | 值 |
|------|----|
| 范围 | `AppConfigurationDemo.slnx`（解决方案级别） |
| 转换项目数 | 11 个 |
| 集中管理包数 | 9 个唯一包 |
| 跳过的项目 | 无 |
| MSBuild 属性版本 | 无 |
| `Directory.Packages.props` 位置 | `ReadAppsettingsDemo/Directory.Packages.props` |

所有 11 个项目的所有 `PackageReference` 均已移除 `Version` 属性，统一由 `Directory.Packages.props` 管理。

---

## 2. 版本冲突处理

发现 2 个包存在版本冲突（`ConfigurationJsonDemo` 使用 9.0.10，其余 10 个项目使用 10.0.9），用户选择**对齐到最高版本 10.0.9**。

| 包名 | 原版本（ConfigurationJsonDemo） | 中心版本 | 影响 |
|------|--------------------------------|---------|------|
| `Microsoft.Extensions.Configuration.Binder` | 9.0.10 → **10.0.9** | 10.0.9 | ConfigurationJsonDemo 小版本升级 |
| `Microsoft.Extensions.Configuration.Json` | 9.0.10 → **10.0.9** | 10.0.9 | ConfigurationJsonDemo 小版本升级 |

其余 9 个项目版本无变化。

---

## 3. 包对比：基线 vs 转换后

### 变更项

| 项目 | 包名 | 基线版本 | 转换后版本 | 说明 |
|------|------|---------|-----------|------|
| ConfigurationJsonDemo | `Microsoft.Extensions.Configuration.Binder` | 9.0.10 | 10.0.9 | 对齐到最高版本 |
| ConfigurationJsonDemo | `Microsoft.Extensions.Configuration.Json` | 9.0.10 | 10.0.9 | 对齐到最高版本 |

### 无变化项（所有其他包）

其余 9 个项目的所有包解析版本与基线完全一致，转换对它们完全透明。

---

## 4. 风险评估

**[低风险]**

- 构建在转换前后均成功（0 警告，0 错误）
- 仅 `ConfigurationJsonDemo` 的 2 个包做了 minor 版本升级（9.0.10 → 10.0.9），属于同系列补丁升级，API 兼容
- 未使用任何 `VersionOverride`，集中管理完整有效
- 建议运行应用手动验证，确认运行时行为无回归

---

## 5. 后续事项

1. **验证 ConfigurationJsonDemo 运行时行为**：虽然 minor 版本升级通常向后兼容，建议手动运行 `ConfigurationJsonDemo` 确认输出正常。
2. **考虑将此模式推广**：父目录 `hello-dotnet/` 下其他子解决方案也可使用 CPM 管理，避免未来出现版本漂移。

---

## 6. 产物说明

| 文件 | 说明 |
|------|------|
| `baseline.binlog` | 转换前 MSBuild 构建日志，可用 `dotnet-binlog` 或 VS 打开 |
| `after-cpm.binlog` | 转换后 MSBuild 构建日志 |
| `baseline-packages.json` | 转换前各项目包版本快照 |
| `after-cpm-packages.json` | 转换后各项目包版本快照 |
| `Directory.Packages.props` | CPM 中心版本文件，后续升级包版本只需修改此文件 |
| `convert-to-cpm.md` | 本报告 |
