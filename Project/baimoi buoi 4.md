# 1. Trigger

## 1.1. Trigger là gì?

Trigger là một **Collider2D có** `**Is Trigger**` **được bật**.

Trigger dùng để phát hiện một Collider2D khác:

- vừa đi vào vùng → **Enter**
    
- vẫn đang ở trong vùng → **Stay**
    
- vừa đi ra khỏi vùng → **Exit**
    

Khác với Collider dùng cho va chạm vật lý, Trigger chủ yếu dùng để **phát hiện** sự đi vào/ra khỏi một vùng.

Ví dụ trong project:

```
Player → Coin
```

Khi Player đi vào vùng Trigger của Coin:

```
Player Collider2D
        ↓
Coin Trigger
        ↓
OnTriggerEnter2D()
        ↓
GetCoin()
        ↓
Destroy Coin
```

## 1.2. `OnTriggerEnter2D()`

### Cú pháp

```
private void OnTriggerEnter2D(Collider2D other)
{
}
```

### Khi nào được gọi?

Hàm được Unity gọi **một lần khi một Collider2D bắt đầu đi vào Trigger**.

```
Ở ngoài
   ↓
bắt đầu đi vào
   ↓
OnTriggerEnter2D()
   ↓
Ở trong
```

### `Collider2D other` là gì?

`other` là **Collider2D của object đã đi vào Trigger**.

Ví dụ:

```
Player
└── CapsuleCollider2D

Coin
└── CircleCollider2D
    Is Trigger = ON
```

Khi Player đi vào Coin Trigger, `other` sẽ tham chiếu tới Collider2D của Player.

Vì vậy có thể viết:

```
Player player = other.GetComponent<Player>();
```

Nghĩa là: lấy component `Player` từ object có Collider vừa đi vào Trigger.

### Hàm có trả về gì không?

Không.

```
void
```

Hàm nhận:

```
Collider2D other
```

nhưng không trả về giá trị bằng `return`.

## 1.3. Ví dụ từ Coin hiện tại

```
private void OnTriggerEnter2D(Collider2D other)
{
    Player player = other.GetComponent<Player>();
    player.GetCoin();
    Destroy(gameObject);
}
```

Luồng chạy:

```
Player chạm Coin
      ↓
Coin Trigger phát hiện
      ↓
OnTriggerEnter2D()
      ↓
other = Collider2D của Player
      ↓
GetComponent<Player>()
      ↓
player.GetCoin()
      ↓
CoinCount++
      ↓
Destroy Coin
```

Trong `Player`:

```
public void GetCoin()
{
    CoinCount++;
    Debug.Log("Coin:" + CoinCount);
}
```

## 1.4. Unity cần setup gì?

### Player

```
Player
├── Rigidbody2D
├── Collider2D
└── Player.cs
```

Player trong project có `Rigidbody2D` để tham gia 2D Physics.

### Coin

```
Coin
├── SpriteRenderer
├── Collider2D
└── Coin.cs
```

Collider của Coin cần:

```
Is Trigger = ON
```

Để `OnTriggerEnter2D()` hoạt động, các Collider phải được phép tương tác trong Physics 2D; trong setup hiện tại, Player đã có Rigidbody2D.

## 1.5. `OnTriggerStay2D()`

### Cú pháp

```
private void OnTriggerStay2D(Collider2D other)
{
}
```

### Khi nào được gọi?

Được gọi **liên tục trong thời gian Collider vẫn nằm trong Trigger**, theo nhịp physics.

```
Đi vào
  ↓
OnTriggerEnter2D()
  ↓
Đang ở trong
  ↓
OnTriggerStay2D()
OnTriggerStay2D()
OnTriggerStay2D()
...
  ↓
Đi ra
  ↓
OnTriggerExit2D()
```

### Có trả về gì không?

Không, kiểu trả về là `void`.

### Ví dụ từ bài Player/Coin

Có thể dùng một vùng đặc biệt để kiểm tra Player vẫn đang đứng trong đó:

```
private void OnTriggerStay2D(Collider2D other)
{
    Player player = other.GetComponent<Player>();

    if (player == null)
        return;

    Debug.Log("Player vẫn ở trong vùng");
}
```

## 1.6. `OnTriggerExit2D()`

### Cú pháp

```
private void OnTriggerExit2D(Collider2D other)
{
}
```

### Khi nào được gọi?

Được gọi **một lần khi Collider rời khỏi Trigger**.

```
Ở trong
   ↓
bắt đầu rời khỏi
   ↓
OnTriggerExit2D()
   ↓
Ở ngoài
```

### Ví dụ

```
private void OnTriggerExit2D(Collider2D other)
{
    Player player = other.GetComponent<Player>();

    if (player == null)
        return;

    Debug.Log("Player đã rời khỏi vùng");
}
```

## 1.7. Ghi nhớ Trigger

|Hàm|Khi nào được gọi?|
|---|---|
|`OnTriggerEnter2D`|Bắt đầu đi vào Trigger|
|`OnTriggerStay2D`|Vẫn đang ở trong Trigger|
|`OnTriggerExit2D`|Rời khỏi Trigger|

```
ENTER → vào
STAY  → ở trong
EXIT  → ra
```

---

# 2. Collision

## 2.1. Collision là gì?

Collision là **va chạm vật lý giữa các Collider2D**.

Ví dụ:

```
Player → Wall
```

Wall chặn Player thay vì để Player đi xuyên qua.

Nếu Collider có:

```
Is Trigger = OFF
```

nó hoạt động như Collider va chạm bình thường.

## 2.2. `OnCollisionEnter2D()`

### Cú pháp

```
private void OnCollisionEnter2D(Collision2D collision)
{
}
```

### Khi nào được gọi?

Khi hai Collider2D bắt đầu xảy ra va chạm vật lý.

### Tham số `Collision2D collision`

`collision` chứa thông tin về lần va chạm.

Ví dụ:

```
collision.collider
collision.gameObject
```

Có thể viết:

```
private void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Va chạm với: " + collision.gameObject.name);
}
```

### Có trả về gì không?

Không. Kiểu trả về là `void`.

## 2.3. `OnCollisionStay2D()`

```
private void OnCollisionStay2D(Collision2D collision)
{
    Debug.Log("Vẫn đang va chạm với: " + collision.gameObject.name);
}
```

Được gọi trong thời gian hai Collider vẫn tiếp xúc với nhau.

## 2.4. `OnCollisionExit2D()`

```
private void OnCollisionExit2D(Collision2D collision)
{
    Debug.Log("Đã rời khỏi: " + collision.gameObject.name);
}
```

Được gọi khi hai Collider kết thúc va chạm.

## 2.5. So sánh Trigger và Collision

|Trigger|Collision|
|---|---|
|`OnTriggerEnter2D`|`OnCollisionEnter2D`|
|`OnTriggerStay2D`|`OnCollisionStay2D`|
|`OnTriggerExit2D`|`OnCollisionExit2D`|
|Nhận `Collider2D`|Nhận `Collision2D`|
|Dùng để phát hiện vùng|Dùng cho va chạm vật lý|
|Collider thường `Is Trigger = ON`|Collider thường `Is Trigger = OFF`|

Liên kết với project:

```
Coin
  ↓
Collider + Is Trigger ON
  ↓
OnTriggerEnter2D()
  ↓
GetCoin()
```

Trong khi Ground/Wall có thể dùng:

```
Ground / Wall
  ↓
Collider + Is Trigger OFF
  ↓
Va chạm vật lý với Player
```

---

# 3. Raycast2D

## 3.1. Raycast là gì?

Raycast là một **tia kiểm tra được bắn từ một điểm theo một hướng** để tìm Collider trên đường đi.

```
Player
   ●
   │
   │ Raycast
   │
   ↓
────────────
 Ground
```

Raycast không tạo Collider mới. Nó chỉ kiểm tra môi trường vật lý.

## 3.2. `Physics2D.Raycast()`

### Cú pháp cơ bản

```
RaycastHit2D hit = Physics2D.Raycast(
    transform.position,
    Vector2.down,
    5f
);
```

Các tham số chính:

```
transform.position
→ điểm bắt đầu

Vector2.down
→ hướng bắn

5f
→ độ dài tia
```

### Khi nào được gọi?

Khác Trigger và Collision, `Physics2D.Raycast()` **không được Unity tự động gọi**.

Nó chỉ chạy khi code gọi:

```
Physics2D.Raycast(...)
```

Có thể gọi từ `Update()`, `FixedUpdate()` hoặc một hàm khác.

### Trả về gì?

```
RaycastHit2D
```

Ví dụ:

```
if (hit.collider != null)
{
    Debug.Log("Đã chạm: " + hit.collider.name);
}
```

Nếu không chạm gì:

```
hit.collider == null
```

Một số thông tin thường dùng:

```
hit.collider
hit.point
hit.normal
hit.distance
```

## 3.3. Raycast trong Player hiện tại

Project đang dùng:

```
void GroundCheck()
{
    RaycastHit2D ray = Physics2D.Raycast(
        this.transform.position,
        Vector2.down,
        groundCheckDistance,
        ground
    );

    if (ray.collider == null)
    {
        isJump = true;
        return;
    }

    isJump = false;
}
```

Luồng:

```
Player
  ↓
Raycast hướng xuống
  ↓
Kiểm tra Ground
  ↓
Có Collider
  → isJump = false

Không có Collider
  → isJump = true
```

Ở đây Raycast trả lời câu hỏi:

> “Bên dưới Player có Collider thuộc Layer được kiểm tra hay không?”

## 3.4. Raycast có LayerMask

Phiên bản trong project:

```
Physics2D.Raycast(
    this.transform.position,
    Vector2.down,
    groundCheckDistance,
    ground
);
```

Tham số cuối:

```
ground
```

là `LayerMask`, dùng để giới hạn những Layer mà Raycast được phép kiểm tra.

---

## 3.5. `Physics2D.RaycastAll()`

Khi cần lấy tất cả các hit trên đường ray:

```
RaycastHit2D[] hits = Physics2D.RaycastAll(
    transform.position,
    Vector2.down,
    10f
);
```

Kiểu trả về:

```
RaycastHit2D[]
```

Ví dụ:

```
foreach (RaycastHit2D hit in hits)
{
    Debug.Log("Chạm: " + hit.collider.name);
}
```

So sánh:

```
Raycast()
→ lấy một kết quả hit phù hợp

RaycastAll()
→ lấy tất cả hit trên đường ray
```

## 3.6. `Physics2D.RaycastNonAlloc()`

Dùng để ghi nhiều kết quả vào mảng có sẵn:

```
RaycastHit2D[] hits = new RaycastHit2D[10];

int count = Physics2D.RaycastNonAlloc(
    transform.position,
    Vector2.down,
    hits,
    10f
);
```

Giá trị trả về:

```
int count
```

là số lượng hit được ghi vào mảng.

Ví dụ:

```
for (int i = 0; i < count; i++)
{
    Debug.Log(hits[i].collider.name);
}
```

---

# 4. Layer và LayerMask

## 4.1. Layer là gì?

Layer là cách Unity **phân loại GameObject**.

Ví dụ:

```
Player
Enemy
Ground
Coin
```

Mỗi GameObject có thể được đặt vào một Layer.

Layer rất hữu ích khi Physics cần biết:

> “Chỉ muốn kiểm tra nhóm object nào?”

## 4.2. Layer và LayerMask không giống nhau

### Layer

Một Layer là một nhóm/index Layer của Unity.

Có thể lấy Layer index từ tên:

```
int groundLayer = LayerMask.NameToLayer("Ground");
```

Luồng:

```
"Ground"
   ↓
NameToLayer()
   ↓
Layer index (int)
```

### LayerMask

`LayerMask` biểu diễn **một tập hợp Layer được chọn**.

Trong project:

```
[SerializeField] LayerMask ground;
```

Trong Inspector có thể chọn:

```
Ground
☑
```

Sau đó truyền vào Raycast:

```
Physics2D.Raycast(
    transform.position,
    Vector2.down,
    groundCheckDistance,
    ground
);
```

Raycast chỉ kiểm tra Collider nằm trên Layer được chọn.

## 4.3. Từ tên Layer → Layer index

```
int layer = LayerMask.NameToLayer("Ground");
```

Kết quả là một `int` đại diện cho Layer đó.

Nếu cần tạo mask từ Layer index:

```
int mask = 1 << layer;
```

Có thể nhớ:

```
Layer index
    ↓
1 << layer
    ↓
LayerMask
```

Trong project hiện tại không cần dùng cách này nếu đã khai báo:

```
[SerializeField] LayerMask ground;
```

và chọn Layer trực tiếp trong Inspector.

## 4.4. Tạo Layer trong Unity

Có thể tạo Layer từ Inspector:

```
Inspector
→ Layer
→ Add Layer...
```

Tạo:

```
Ground
```

Sau đó đặt Ground object vào Layer đó:

```
Ground GameObject
→ Layer = Ground
```

Trong Player Script:

```
[SerializeField] LayerMask ground;
```

sẽ xuất hiện ô LayerMask trong Inspector để chọn Layer.

---

# 5. Ví dụ liên kết toàn bộ kiến thức

Giả sử scene có:

```
Player
Ground
Coin
Wall
```

## Player

```
Player
├── Rigidbody2D
├── CapsuleCollider2D
└── Player.cs
```

## Ground

```
Ground
├── BoxCollider2D
└── Layer = Ground
```

## Coin

```
Coin
├── CircleCollider2D
└── Is Trigger = ON
```

### Trường hợp 1 — Player kiểm tra Ground bằng Raycast

Code:

```
RaycastHit2D ray = Physics2D.Raycast(
    transform.position,
    Vector2.down,
    groundCheckDistance,
    ground
);
```

Liên kết:

```
Ground GameObject
       ↓
Layer = Ground
       ↓
LayerMask ground
       ↓
Physics2D.Raycast()
       ↓
RaycastHit2D
       ↓
Player biết có Ground bên dưới
```

### Trường hợp 2 — Player nhặt Coin bằng Trigger

```
Player Collider
       ↓
Coin Collider
Is Trigger = ON
       ↓
OnTriggerEnter2D()
       ↓
Collider2D other
       ↓
GetComponent<Player>()
       ↓
GetCoin()
       ↓
CoinCount++
       ↓
Destroy(gameObject)
```

### Trường hợp 3 — Player chạm Wall

```
Player Collider
       ↓
Wall Collider
Is Trigger = OFF
       ↓
Va chạm vật lý
       ↓
OnCollisionEnter2D()
```

---

# 6. Sơ đồ tổng thể

```
                 UNITY 2D PHYSICS
                        │
          ┌─────────────┴─────────────┐
          │                           │
      Collider2D                 Rigidbody2D
          │                           │
          │                     chuyển động
          │                     vật lý / gravity
          │
    ┌─────┴─────┐
    │           │
 Trigger     Collision
    │           │
 Enter       Enter
 Stay        Stay
 Exit        Exit
    │           │
    └─────┬─────┘
          │
          ▼
       Raycast
          │
    ┌─────┴──────┐
    │            │
 Raycast()   RaycastAll()
    │            │
    └─────┬──────┘
          │
      RaycastHit2D
          │
      LayerMask
          │
   giới hạn Layer
      được kiểm tra
```

---

# 7. Bảng ghi nhớ nhanh

|Thành phần|Dùng để làm gì?|Được gọi/chạy khi nào?|Kết quả chính|
|---|---|---|---|
|`OnTriggerEnter2D`|Phát hiện đi vào Trigger|Bắt đầu đi vào|`void`, nhận `Collider2D`|
|`OnTriggerStay2D`|Phát hiện vẫn ở trong Trigger|Trong thời gian còn ở trong|`void`, nhận `Collider2D`|
|`OnTriggerExit2D`|Phát hiện rời Trigger|Khi bắt đầu rời khỏi|`void`, nhận `Collider2D`|
|`OnCollisionEnter2D`|Phát hiện bắt đầu va chạm|Khi bắt đầu va chạm|`void`, nhận `Collision2D`|
|`OnCollisionStay2D`|Phát hiện vẫn va chạm|Trong thời gian tiếp xúc|`void`, nhận `Collision2D`|
|`OnCollisionExit2D`|Phát hiện kết thúc va chạm|Khi rời nhau|`void`, nhận `Collision2D`|
|`Physics2D.Raycast`|Bắn một tia kiểm tra|Khi code gọi|`RaycastHit2D`|
|`Physics2D.RaycastAll`|Lấy nhiều hit trên tia|Khi code gọi|`RaycastHit2D[]`|
|`Physics2D.RaycastNonAlloc`|Lấy nhiều hit vào mảng có sẵn|Khi code gọi|`int` số hit|
|`LayerMask`|Chọn nhóm Layer để kiểm tra|Được truyền vào Physics API|Mask các Layer|

---

# 8. Những điều cần nhớ nhất

```
Trigger:
Enter → vừa vào
Stay  → đang ở trong
Exit  → vừa ra

Collision:
Enter → bắt đầu va chạm
Stay  → vẫn va chạm
Exit  → kết thúc va chạm

Raycast:
→ không tự động được Unity gọi
→ phải gọi bằng code
→ trả về RaycastHit2D

RaycastAll:
→ lấy nhiều hit
→ trả về RaycastHit2D[]

Layer:
→ một Layer/index dùng để phân loại object

LayerMask:
→ tập hợp Layer được phép kiểm tra
```