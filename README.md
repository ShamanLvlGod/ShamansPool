# 🔮 Shamans Pool

A lightweight, plug-and-play **object pooling** system designed for **pure C#** and **Unity** — built with ❤️ by [@ShamanLvlGod](https://github.com/shamanlvlgod).  
This is **not a Unity-specific-only pool** — it’s a **general-purpose pooling system** with a Unity-aware integration layer for prefab and `MonoBehaviour` support.

---

## ✨ Features

- ⚡ Simple and dependency-free core
- 🧱 Works with **any type**, not just Unity objects
- 🧠 Optional `IPoolable` interface for lifecycle hooks
- 🪝 Built-in `OnBeforeGet` and `OnBeforeReturn` actions for full control
- 🎮 Unity-specific component for prefab pooling, activation, and parenting
- 🧩 Just plug your prefab in the Inspector and go

---

## 📦 Installation

Add this to your Unity project's `manifest.json`:

```json
"com.shamanlvlgod.shamanspool": "https://github.com/shamanlvlgod/ShamansPool.git"
```

> ✅ Unity will automatically fetch and link the package via Git.

---

## 🪝 Built-in Pool Hooks

You can assign global lifecycle actions on any pool:

```csharp
pool.OnBeforeGet = obj => Debug.Log("About to use: " + obj);
pool.OnBeforeReturn = obj => Debug.Log("Returning to pool: " + obj);
```

These fire **before the object is handed to you**, and **before it goes back into the pool**.

---

## 🧪 Example: Unity Object with IPoolable

### 1. Pooled prefab with optional `IPoolable`:

```csharp
public class Enemy : MonoBehaviour, IPoolable
{
    public void OnTakenFromPool()
    {
        Debug.Log("Spawned!");
    }

    public void OnReturnedToPool()
    {
        Debug.Log("Despawned.");
    }

    private float _spawnTime;

    private void OnEnable()
    {
        _spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _spawnTime > 2f)
        {
            FindObjectOfType<EnemySpawner>().Release(this);
        }
    }
}
```

> Implementing `IPoolable` is optional. If the object does, the pool automatically calls the hooks.

---

### 2. Your Unity pool component:

```csharp
public class EnemySpawner : UnityPool<Enemy>
{
    private void Start()
    {
        Enemy enemy = Get();
        enemy.transform.position = Vector3.zero;
    }
}
```

---

### 3. In your scene:
- Create a GameObject and attach the `EnemySpawner` component
- Assign your `Enemy` prefab in the Inspector
- Set `Preload Amount` if desired
- Done. You're pooling like a pro 🔁

---

## 🧪 Example: Pure C# Pool (non-MonoBehaviour)

```csharp
public class BulletData
{
    public int Damage;
    public float Speed;
}
```

```csharp
var pool = new Pool<BulletData>(
    createFunc: () => new BulletData(),
    preload: 10
);

pool.OnBeforeGet = bullet => bullet.Damage = 10;

BulletData b = pool.Get();
// use it...
pool.Release(b);
```

> No Unity involved — works like a pure, reusable data structure.

---

## 🧰 Folder Structure

```
ShamansPool/
├── package.json
├── LICENSE
├── README.md
└── Runtime/
    ├── Pool.cs
    ├── UnityPool.cs
    ├── IPoolable.cs
    └── IPool.cs
```

---

## 📝 License

Licensed under the [MIT License](LICENSE)  
Built with ☕, discipline, and a deep hatred for runtime allocations.

---

## 🙏 Credits

Created by [@shamanlvlgod](https://github.com/shamanlvlgod)