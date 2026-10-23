# Unity C# API 文档格式规范

本规范用于统一项目 API 文档风格，参考 Unity/C# 常见最佳实践（类型职责清晰、接口可检索、示例可复制）。

## 1. 文档拆分原则

- 按领域拆分：
  - `systems/`：System 层
  - `modules/`：Module 层
  - `tools/`：工具链与生成脚本
- 按类型建文档：一类一文档（如 `AssetSystem.md`、`HttpSystem.md`）。
- 总览文档只放索引与关系图，不堆叠具体 API 细节。

## 2. 单篇文档标准结构

每篇文档建议包含以下章节（顺序固定）：

1. `Summary`
2. `Type Metadata`
3. `Lifecycle`
4. `Public APIs`
5. `Dependencies`
6. `Usage Example`
7. `Constraints`
8. `Common Pitfalls`
9. `Change Log`（可选）

## 3. 字段规范（便于 AI 解析）

- `Namespace`：完整命名空间
- `Type`：类/接口/枚举
- `Layer`：`Framework` / `Scripts` / `HotUpdate`
- `Thread`：`MainThread` / `Any`
- `Generated`：`true` / `false`
- `Source Path`：源码路径

## 4. API 表格规范

推荐用固定列：

- `Method`
- `Signature`
- `Returns`
- `When To Use`
- `Notes`

参数说明可附在方法后，用 `Parameter` 小节列出。

## 5. 示例规范（Unity C#）

- 示例必须是可落地的最小片段。
- 示例需体现生命周期位置（如 `Init`、`OnRegisterListener`、`OnUnbind`）。
- 避免伪 API，示例中的类型名必须来自项目真实代码或明确标注占位。

## 6. 术语统一

- 启动流程节点使用 `Procedure`
- 全局能力使用 `System`
- 业务域使用 `Module`
- 具体管理实现使用 `Manager`
