# AI 文档总览

## 这份 README 是做什么的
- 这是 `ai-doc` 目录的总入口。
- 它帮助开发人员和 AI 快速找到“规则在哪、手册在哪、示例在哪、产出放哪”。

## 先看哪里

### 如果你是开发人员
1. 先看 [`developer-helper/README.md`](./developer-helper/README.md)
2. 再看需求文档 [`requirements/requirements.md`](./requirements/requirements.md)
3. 再看开发方案示例 [`demo-game-setup.md`](./demo-game-setup.md)

### 如果你是 AI
1. 先看 [`prompt.txt`](./prompt.txt)
2. 再看 [`developer-helper/README.md`](./developer-helper/README.md)
3. 再结合项目源码、`docs/api` 和当前需求做分析

## 目录说明
- [`prompt.txt`](./prompt.txt)
  - 当前项目的技术规则、目录职责、实现边界和协作约束。
- [`developer-helper`](./developer-helper/README.md)
  - 面向开发人员的 AI 协作开发手册和 onboarding 文档。
- [`requirements`](./requirements/requirements.md)
  - 当前需求文档目录，用来说明“需求应该整理成什么样”。
- [`demo-game-setup.md`](./demo-game-setup.md)
  - 当前示例开发方案文档，用来说明“开发方案应该怎么写、怎么落地”。
- [`demo-game-setup-images`](./demo-game-setup-images)
  - `demo-game-setup.md` 的配套结构图和说明图。
- [`develop-setup`](./develop-setup/README.md)
  - 新功能或新项目的开发方案产出目录，建议使用 `md + svg` 配套输出。

## 当前项目文档

- [`requirements/单路王者_PVP自走棋_V0.2_开发规格.md`](./requirements/单路王者_PVP自走棋_V0.2_开发规格.md)
  - 单路王者玩法规则、基础数值和首轮测试目标。
- [`develop-setup/单路王者-轻量ECS战斗框架方案.md`](./develop-setup/单路王者-轻量ECS战斗框架方案.md)
  - 当前战斗 ECS、配置链路与表现边界。
- [`develop-setup/单路王者-V0.3经济与战斗数值调整.md`](./develop-setup/单路王者-V0.3经济与战斗数值调整.md)
  - 2026-08-04 首轮宏观经济与终局节奏平衡记录。
- [`develop-setup/单路王者-V0.3三选一强化明细.md`](./develop-setup/单路王者-V0.3三选一强化明细.md)
  - 2026-08-04 三选一动态数值、技能说明与配置语义修正记录。
- [`develop-setup/单路王者-V0.3小兵单点队列与接敌索敌.md`](./develop-setup/单路王者-V0.3小兵单点队列与接敌索敌.md)
  - 2026-08-04 小兵固定点逐个出生、接敌前直线编队与索敌范围记录。
- [`develop-setup/单路王者-V0.3英雄部署区域遮罩.md`](./develop-setup/单路王者-V0.3英雄部署区域遮罩.md)
  - 2026-08-04 英雄拖拽部署遮罩、嵌套 Canvas 投影修正及金币不足置灰记录。

## 一句话区分
- `需求文档` 解决“要做成什么样”。
- `开发方案` 解决“准备在当前项目里怎么落”。
- `prompt.txt` 解决“实现时必须遵守什么规则”。

## 建议的使用顺序
1. 先确认需求是否已经足够清楚。
2. 如果不清楚，先整理到 `requirements` 这种结构。
3. 如果清楚了，再在 `develop-setup` 里产出本次功能的开发方案。
4. 方案确认后，再进入代码实现。
