# 1. Trigger 2D

## 1.1. Trigger là gì?

Trigger là một `Collider2D` dùng để **phát hiện khi một vật thể đi vào, ở trong hoặc đi ra khỏi vùng của nó**.

Khác với Collider thông thường:

- **Collider** → tạo va chạm vật lý.
    
- **Trigger** → chỉ phát hiện va chạm, không chặn vật thể.
    

Ví dụ:

```
Player → Coin
          ↑
       Trigger
```

Player có thể đi xuyên qua Coin, nhưng Coin vẫn phát hiện được Player.

---

## 1.2. Tạo Trigger trong Unity

Ví dụ với Coin:

**Coin GameObject** cần:

- `Sprite Renderer`
    
- `Collider2D`
    
- Script xử lý Coin
    

Trong `Collider2D`, bật:

```
Is Trigger ✓
```

Sau đó có thể sử dụng các hàm Trigger 2D trong script.

---

## 1.3. Các hàm Trigger 2D

### OnTriggerEnter2D

Được gọi khi một Collider2D **bắt đầu đi vào Trigger**.

```
private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Có vật thể đi vào Trigger!");
}
```

Đây là hàm thường được dùng để:

- Nhặt Coin
    
- Nhặt Item
    
- Đi vào vùng kiểm tra
    
- Kích hoạt một sự kiện
    

---

### OnTriggerStay2D

Được gọi khi Collider2D **đang ở bên trong Trigger**.

```
private void OnTriggerStay2D(Collider2D other)
{
    Debug.Log("Đang ở trong Trigger!");
}
```

Ví dụ: kiểm tra Player có đang đứng trong vùng nào đó hay không.

---

### OnTriggerExit2D

Được gọi khi Collider2D **rời khỏi Trigger**.

```
private void OnTriggerExit2D(Collider2D other)
{
    Debug.Log("Đã rời khỏi Trigger!");
}
```

---

## 1.4. Ví dụ: Player nhặt Coin

```
private void OnTriggerEnter2D(Collider2D other)
{
    Player player = other.GetComponent<Player>();

    if (player != null)
    {
        player.GetCoin();
        Destroy(gameObject);
    }
}
```

Điểm quan trọng:

```
other.GetComponent<Player>();
```

Dùng để lấy Component `Player` từ GameObject vừa chạm vào Coin.

Cần kiểm tra:

```
if (player != null)
```

để tránh lỗi khi **Enemy hoặc một vật thể khác** chạm vào Coin.

Nếu không có Component `Player`, kết quả sẽ là `null`.

---

# 2. LayerMask

## 2.1. Layer là gì?

Layer dùng để **phân loại các GameObject** trong Unity.

Ví dụ:

```
Player
Enemy
Wall
Ground
Coin
```

Mỗi GameObject có thể được gán vào một Layer.

---

## 2.2. LayerMask là gì?

`LayerMask` cho phép chọn **những Layer mà một hệ thống cần kiểm tra**.

Ví dụ Enemy chỉ cần Raycast kiểm tra:

```
Wall
Enemy
```

Có thể khai báo:

```
[SerializeField] LayerMask wall;
[SerializeField] LayerMask enemy;
```

Sau đó chọn Layer tương ứng trong Inspector.

---

## 2.3. Tạo Layer

Vào:

**Inspector → Layer → Add Layer...**

Tạo các Layer cần thiết, ví dụ:

```
Player
Enemy
Wall
```

Sau đó gán Layer cho GameObject.

Ví dụ:

```
Enemy.prefab
    Layer → Enemy
```

```
Wall
    Layer → Wall
```

---

## 2.4. LayerMask với Raycast

Ví dụ Enemy kiểm tra phía trước:

```
RaycastHit2D ray = Physics2D.Raycast(
    transform.position,
    Vector2.right * direction,
    checkDistance,
    wall | enemy
);
```

Ở đây:

- `transform.position` → điểm bắt đầu Raycast
    
- `Vector2.right * direction` → hướng Raycast
    
- `checkDistance` → độ dài Raycast
    
- `wall | enemy` → các Layer mà Raycast cần kiểm tra
    

Ví dụ Enemy có thể dùng Raycast để:

```
Enemy → Wall
       → quay đầu

Enemy → Enemy
       → xử lý Enemy phía trước
```

---

# 3. Prefab

## 3.1. Prefab là gì?

Prefab là một **GameObject được lưu thành mẫu để có thể tái sử dụng**.

Ví dụ một Enemy có:

- Sprite Renderer
    
- Collider2D
    
- Rigidbody2D
    
- EnemyMovement.cs
    

Có thể lưu thành:

```
Enemy.prefab
```

Prefab giống như một **khuôn mẫu** của GameObject.

---

## 3.2. Tạo Prefab

Ví dụ tạo Prefab cho Enemy:

1. Tạo Enemy trong `Hierarchy`.
    
2. Thêm và thiết lập các Component cần thiết.
    
3. Kéo Enemy từ `Hierarchy` vào:
    

```
Assets/Prefabs
```

4. Unity sẽ tạo:
    

```
Enemy.prefab
```

---

## 3.3. Prefab dùng để làm gì?

Prefab phù hợp với các GameObject được sử dụng nhiều lần:

- Enemy
    
- Bullet
    
- Coin
    
- Item
    
- Effect
    
- Projectile
    

Ví dụ thay vì tạo từng Enemy thủ công, chỉ cần tạo một:

```
Enemy.prefab
```

sau đó có thể tạo nhiều Enemy từ Prefab.

---

# 4. Instantiate

## 4.1. Instantiate là gì?

`Instantiate()` dùng để **tạo một bản sao của GameObject hoặc Prefab trong Scene**.

Ví dụ:

```
Instantiate(enemyPrefab);
```

Nếu:

```
enemyPrefab = Enemy.prefab
```

Unity sẽ tạo một Enemy mới trong Scene.

---

## 4.2. Instantiate tại một vị trí

Có thể chỉ định vị trí và rotation:

```
Instantiate(
    enemyPrefab,
    transform.position,
    Quaternion.identity
);
```

Trong đó:

- `enemyPrefab` → Object cần tạo
    
- `transform.position` → vị trí tạo
    
- `Quaternion.identity` → rotation mặc định
    

---

# 5. Spawn Enemy bằng Prefab

## 5.1. Tạo EnemySpawner

Tạo một Empty GameObject:

```
Hierarchy
└── EnemySpawner
```

Đặt `EnemySpawner` tại vị trí muốn Enemy xuất hiện.

---

## 5.2. EnemySpawner.cs

```
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    void Start()
    {
        Instantiate(
            enemyPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}
```

Script này sẽ tạo một Enemy tại vị trí của `EnemySpawner` khi bắt đầu game.

---

## 5.3. Setup trong Unity

Chọn:

```
EnemySpawner
```

Trong Inspector sẽ có:

```
Enemy Prefab
```

Kéo:

```
Assets/Prefabs/Enemy.prefab
```

vào ô `Enemy Prefab`.

Khi bấm **Play**:

```
EnemySpawner
      ↓
Instantiate()
      ↓
Enemy(Clone)
```

Enemy sẽ xuất hiện tại vị trí của Spawner.

---

# 6. Tóm tắt

| Kiến thức       | Công dụng                               |
| --------------- | --------------------------------------- |
| **Trigger**     | Phát hiện vật thể đi vào/ở trong/đi ra  |
| **Layer**       | Phân loại GameObject                    |
| **LayerMask**   | Chọn Layer cần kiểm tra                 |
| **Prefab**      | Lưu GameObject thành mẫu để tái sử dụng |
| **Instantiate** | Tạo bản sao của GameObject/Prefab       |