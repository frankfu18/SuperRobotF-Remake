# 配置表生成规则

以后生成项目配置表时，默认遵循本项目沉淀的 XML + xlsx 配置习惯。该规则可跨 Unity 项目复用，除非目标项目已有更明确的本地规范。

## XML 规则

- 每个配置模块使用一个 XML 文件定义，例如 `cfg/arena/arena.xml`。
- XML 文件头和根节点固定为：

```xml
<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<module name="cfg.xxx">
    ...
</module>
```

- XML 内部内容顺序固定为：
  1. `enum`
  2. `bean`
  3. `config`
- `enum` 必须放在最上面，`bean` 放在 `enum` 后面，所有 `config` 放在最后。
- 字段中文注释写在 `<var .../>` 后面：

```xml
<var name="playerSide" type="EPlayerSide"/> 玩家位置
```

- 新增模块 XML 后，需要在总入口 `cfg/cfg_sc.xml` 中 include：

```xml
<include name="arena/arena.xml"/>
```

## 命名规则

- 所有枚举名必须以 `E` 开头。
  - 正确：`EPlayerSide`
  - 正确：`EEquipmentSlot`
  - 正确：`EWeaponHandMode`
  - 不使用：`PlayerSide`、`EquipmentSlot`、`WeaponHandMode`
- `config` 名使用大驼峰，例如 `ArenaEquipment`、`ArenaAI`。
- xlsx 文件名使用小驼峰，例如 `arenaEquipment.xlsx`、`arenaAI.xlsx`。
- `bean` 名使用大驼峰，例如 `ArenaEquipmentInfo`、`MainWeapon`、`SubWeapon`。
- config 字段名和 bean 字段名使用小驼峰，例如 `playerSide`、`guardProbability`。

## xlsx 表规则

- xlsx 文件放在对应配置模块目录下，例如 `cfg/arena/`。
- 每张表前两行不打进正式配置数据。
- 第 1 行是中文说明行，第一列前加 `##`，例如 `##玩家位置`。
- 第 2 行是字段名行，第一列前加 `##`，例如 `##playerSide`。
- 普通字段需要在前两行分别写中文说明和英文字段名。
- 表头、字段、数据同一行不要手动换行，方便策划查看和复制。
- 默认不加备注列。
- 如果确实需要备注列，备注列放在 `id` 的下一列，不放在表尾。
- 全局单行表在 XML 的 `config` 上加 `mode="one"`。
- 全局单行表不需要 `id` 字段，也不需要备注字段。
- 不需要的字段不要额外添加。

## bean 字段规则

- 可以在 XML 中定义父 bean 和子 bean。
- 父 bean 的字段，子 bean 都可以读取。
- 子 bean 的字段，只属于该子 bean。
- 如果某个 config 字段类型是 bean，例如：

```xml
<var name="equipmentInfo" type="ArenaEquipmentInfo"/> 装备参数
```

- xlsx 中该字段列填写具体子 bean 名称，例如：

```text
MainWeapon
SubWeapon
```

- bean 后续参数列从该 bean 类型列后面开始。
- bean 后续参数列按 XML 中子 bean 定义的字段顺序横向填写。
- 每个 bean 字段占一列。
- bean 展开参数列不需要在前两行写中文说明和英文字段名，保持空列即可。
- 同一张表里如果有多个子 bean，共用同一片 bean 参数列区域；每一行按自己填写的子 bean 类型解释后续参数。

## 配置生成规则

- 修改 XML 或 xlsx 后执行配置生成：

```powershell
cmd /c "cd /d E:\Work\Projects\Gladihoppers\client && gen_config.bat < nul"
```

- 生成后检查：
  - 生成 C# 字段是否包含新增字段。
  - bytes 数据是否更新。
  - `cfg/cfg_sc.xml` 是否 include 了新增模块 XML。
  - 项目是否能正常编译。

- 常用编译命令：

```powershell
dotnet build client\HotUpdate.csproj --no-restore
```

## 示例

```xml
<enum name="EPlayerSide">
    <var alias="左边" name="Left" value="1"/>
    <var alias="右边" name="Right" value="2"/>
</enum>

<config name="ArenaInput" input="arenaInput.xlsx">
    <var name="playerSide" type="EPlayerSide"/> 玩家位置
    <var name="moveLeftKey" type="string"/> 左移按键
    <var name="moveRightKey" type="string"/> 右移按键
    <var name="guardKey" type="string"/> 防御按键
    <var name="skillKey" type="string"/> 技能按键
</config>
```
