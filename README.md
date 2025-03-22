# 🔮 Shamans Pool

A lightweight, plug-and-play **object pooling** system for Unity, built with ❤️ by [@ShamanLvlGod](https://github.com/shamanlvlgod).  
It features a **pure C# generic core** and a powerful **Unity-aware layer** for pooling `MonoBehaviour`-based objects cleanly and efficiently.

---

## ✨ Features

- ⚡ Simple and dependency-free
- 🧠 Optional `IPoolable` interface for lifecycle hooks
- 🧱 Generic pool works with any type
- 🎮 Unity-specific layer for prefab management, parenting, and activation
- 🧩 Just plug your prefab in the Inspector and go

---

## 📦 Installation

Add this to your Unity project's `manifest.json`:

```json
"com.shamanlvlgod.shamanspool": "https://github.com/shamanlvlgod/ShamansPool.git"
```

> ✅ Unity will automatically fetch and link the package via Git.

---

## 🧪 Example Usage

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

> Implementing `IPoolable` is optional. If the object does, the pool will automatically call the hooks.

---

### 2. Your pool component:

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
