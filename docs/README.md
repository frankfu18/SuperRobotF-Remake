# 项目文档总览（docs）

本目录用于存放项目 API 文档，目标是让 AI 或开发者在不读全量源码的前提下，快速理解脚手架支持的能力、入口和使用方式。

## 目录结构

- [API 文档总索引](./api/INDEX.md)（推荐 AI 首先阅读）
- [文档格式规范](./api/STYLE_GUIDE.md)（参考 Unity C# 常见最佳实践）
- [系统能力文档](./api/systems/SYSTEM_OVERVIEW.md)
- [模块协作文档](./api/modules/MODULE_WORKFLOW.md)
- [UI 扩展文档](./api/ui/UIPanel.md)
- [事件系统文档](./api/events/EVENT_SYSTEM.md)
- [数据库文档](./api/database/DATABASE_USAGE.md)
- [工具链文档](./api/tools/TOOLCHAIN.md)
- [新增文档模板](./api/templates/API_CLASS_TEMPLATE.md)

## 维护约定

- 新增或修改系统能力时，同步更新 `docs/api/INDEX.md` 与对应分册。
- 文档优先描述“可用能力”和“正确调用方式”，避免与业务实现细节耦合。
- 自动生成代码（如 `AutoGen`）只记录生成来源与使用方式，不记录业务逻辑。
