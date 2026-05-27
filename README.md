# 3D Infinite Runner

Unity で制作した 3D エンドレスランゲームです。  
プレイヤーキャラクターが自動前進する中、左右移動・ジャンプで障害物を回避しながらアイテムを収集してスコアを競います。

## ゲームプレイ動画 / スクリーンショット
[![デモ動画のサムネイル](https://img.youtube.com/vi/6MqNY3DHQnE/0.jpg)](https://www.youtube.com/watch?v=6MqNY3DHQnE)

---

## 目次

- [プロジェクト概要](#プロジェクト概要)
- [使用技術・環境](#使用技術環境)
- [主な機能と実装の工夫](#主な機能と実装の工夫)
- [アーキテクチャ](#アーキテクチャ)
- [セットアップ方法](#セットアップ方法)
- [操作方法](#操作方法)
- [ディレクトリ構成](#ディレクトリ構成)

---

## プロジェクト概要

| 項目 | 詳細 |
|------|------|
| ジャンル | 3D エンドレスランゲーム |
| プラットフォーム | PC（Windows / Mac） |
| 開発形態 | 個人開発 |
| Unity バージョン | 6000.3.11f1 |
| レンダリングパイプライン | Universal Render Pipeline (URP) |

### ゲームの流れ

1. ゲーム開始と同時にキャラクターが自動で前進（時間経過で加速）
2. A / D キーまたは方向キーで左右に移動し、障害物を回避
3. スペースキーでジャンプして高い障害物を乗り越える
4. フィールド上のアイテムを収集してスコアを稼ぐ
5. 障害物に衝突するとゲームオーバー → 自動リスタート

---

## 使用技術・環境

| カテゴリ | 技術 |
|----------|------|
| **ゲームエンジン** | Unity 6 (6000.3.11f1) |
| **グラフィクス** | Universal Render Pipeline (URP) 17.3.0 |
| **言語** | C# |
| **UI** | TextMesh Pro |
| **入力** | Unity Input System 1.19.0 |
| **アニメーション** | Unity Animator (State Machine) |
| **物理演算** | Unity Physics (Rigidbody / Collider) |
| **バージョン管理** | Git / GitHub |
| **開発環境** | Windows 11 / Visual Studio |

---

## 主な機能と実装の工夫

### 1. 手続き的レベル生成（Procedural Generation）

`GenerateLevels.cs` にて、4 種類のレベル Prefab をランダムに選択・連続生成することで無限のコースを実現しています。

```csharp
IEnumerator GenerateLvl()
{
    lvlNum = Random.Range(0, 4);
    Instantiate(level[lvlNum], new Vector3(0, 0, zPos), Quaternion.identity);
    zPos += 12;
    yield return new WaitForSeconds(3);
    creatingLevel = false;
}
```

**工夫点:** `Coroutine` を利用して生成タイミングを制御し、ゲームプレイのテンポを保ちつつ CPU 過負荷を防止しています。

### 2. メモリ管理（Sweeper パターン）

`Sweeper.cs` がプレイヤーの背後に到達したレベルオブジェクトを自動削除することで、無限生成によるメモリ増大を防いでいます。

```csharp
public void OnTriggerEnter(Collider collision)
{
    if (collision.gameObject.CompareTag("Level"))
        Destroy(collision.gameObject);
}
```

**工夫点:** 生成と破棄を独立したコンポーネントに分離することで、単一責任の原則に基づいた設計を実現しています。

### 3. 物理ベースのジャンプ

`Rigidbody.AddForce` に `ForceMode.Impulse` を使用し、自然な放物線軌道のジャンプを実装しています。`OnCollisionStay` / `OnCollisionExit` でグラウンド判定を行い、空中での多段ジャンプを防止しています。

```csharp
if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
{
    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    isGrounded = false;
}
```

### 4. 難易度の動的上昇

フレームごとに移動速度を加算することで、プレイ時間に応じて自然に難易度が上昇します。

```csharp
transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
moveSpeed += Time.deltaTime * acceleration;
```

### 5. Animator State Machine によるアニメーション制御

Animator の `SetTrigger` / `SetBool` を使い、走り・ジャンプ・アイドルの各ステートをコードから制御しています。Animator Controller でステートマシンを構築し、アニメーション遷移ロジックをスクリプトから分離することで保守性を高めています。

### 6. コンポーネント指向の疎結合設計

各スクリプトが単一の責務を持ち、MonoBehaviour コンポーネントとして疎結合に設計されています。`GameManager` がスコアとオーディオの管理を一元化し、他コンポーネントからの参照を最小限に抑えています。

---

## アーキテクチャ

```
PlayerMovement      ゲーム状態 (isGamePlaying) を管理・プレイヤー制御
      │
      ├─ ObstacleCrush   衝突検知 → isGamePlaying = false (ゲームオーバー)
      │
GameManager         スコア計算・音声再生を一元管理
      │
      └─ Collectable     アイテム回転 + 取得検知 → GameManager.UpdateScore()

GenerateLevels      3 秒ごとにランダムなレベル Prefab を無限生成
      │
Sweeper             通過済みレベルオブジェクトを自動削除してメモリを解放
```

---

## セットアップ方法

### 動作要件

- Unity 6 (6000.3.11f1) 以降
- Universal Render Pipeline サポート環境

### 手順

```bash
# 1. リポジトリのクローン
git clone https://github.com/nasu-cell/3DRunGame.git

# 2. Unity Hub でプロジェクトを開く
#    [Open] → クローンしたフォルダを選択

# 3. Unity バージョンを確認
#    Unity 6000.3.11f1 以降を使用（バージョン不一致時はアップグレードを選択）

# 4. シーンを開く
#    Assets/Scenes/ からメインシーンを開く

# 5. Play ボタンでゲーム開始
```

> **注意:** Unity のバージョンが異なる場合、プロジェクトのアップグレードダイアログが表示されます。  
> 推奨バージョン以降であれば「Upgrade」を選択してください。

---

## 操作方法

| 操作 | キー |
|------|------|
| 左移動 | `A` / `←` |
| 右移動 | `D` / `→` |
| ジャンプ | `Space` |

---

## ディレクトリ構成

```
Assets/
├── Animations/               # キャラクターアニメーション (idle / run / jump)
├── Materials/                # マテリアル・シェーダー設定
├── Prefabs/                  # ゲームオブジェクトテンプレート
│   ├── StartingLevel.prefab  # 開始レベル
│   ├── Level (1-3).prefab    # ランダム生成レベル
│   ├── Collectable.prefab    # 収集アイテム
│   └── character.prefab      # プレイヤーキャラクター
├── Scenes/                   # ゲームシーン
├── Scripts/                  # C# スクリプト
│   ├── PlayerMovement.cs     # プレイヤー制御・ゲーム状態管理
│   ├── GameManager.cs        # スコア・オーディオ管理
│   ├── GenerateLevels.cs     # 手続き的レベル生成
│   ├── Collectable.cs        # アイテム取得処理
│   ├── ObstacleCrush.cs      # 障害物衝突・ゲームオーバー検知
│   └── Sweeper.cs            # 不要オブジェクトの自動削除
├── Settings/                 # URP レンダリング設定
└── [Third-party Assets]      # 外部アセット
    ├── Casual SoundFX Pack
    ├── Forest Pack
    ├── Free Orchestral Music Pack
    └── Supercyan Character Pack
```

---

## 今後の改善予定

- [ ] ハイスコアのローカル保存（PlayerPrefs）
- [ ] モバイル向けスワイプ・タップ操作対応
- [ ] オブジェクトプーリングによるさらなるパフォーマンス最適化
- [ ] レベルバリエーションの追加

---

## ライセンス

本プロジェクトに含まれるサードパーティアセットは各ライセンスに従います。

- [Supercyan Character Pack](https://assetstore.unity.com/packages/3d/characters/supercyan-character-pack-animal-people-sample-136183) — Unity Asset Store EULA
- Casual SoundFX Pack — Unity Asset Store EULA
- Free Orchestral Music Pack — Unity Asset Store EULA
- Forest Pack — Unity Asset Store EULA
