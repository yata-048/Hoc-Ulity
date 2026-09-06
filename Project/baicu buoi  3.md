# 1. Input System

## 1.1. Input System là gì?

**Input System** là hệ thống giúp Unity nhận thao tác từ người chơi, chẳng hạn như:

- Nhấn phím.
    
- Nhấn chuột.
    
- Nhấn nút tay cầm.
    
- Di chuyển analog.
    

Luồng cơ bản:

```
Người chơi thao tác
        ↓
   Input System
        ↓
    Input Action
        ↓
       Code
        ↓
    Game Object
```

Ví dụ trong Player:

```
movement = MoveAction.ReadValue<float>();
```

`movement` nhận giá trị từ Input Action `Move`.

---

## 1.2. Input Action Editor / New Action

Để code có thể nhận input, trước tiên cần tạo **Input Actions Asset**.

Trong Unity:

```
Project
→ Create
→ Input Actions
```

Sau khi tạo, mở Input Actions Asset bằng cách nhấp đúp vào file.

Trong **Input Action Editor**, có thể tạo:

```
Action Map
└── Actions
    ├── Move
    ├── Jump
    └── Attack
```

### Action Map

**Action Map** là nhóm các Input Action có liên quan.

Ví dụ:

```
Player
├── Move
├── Jump
└── Attack
```

Có thể có nhiều Action Map trong một game, ví dụ:

```
Player
Menu
Vehicle
```

### Action

**Action** đại diện cho một hành động trong game.

Ví dụ:

```
Move
Jump
Attack
```

Action không nhất thiết phải gắn với một phím cụ thể. Việc phím nào thực hiện Action được cấu hình thông qua Binding.

### Binding

**Binding** xác định thiết bị hoặc nút nào kích hoạt Action.

Ví dụ:

```
Move
├── A / D
└── Left Arrow / Right Arrow
```

### Control Type

Control Type cho biết kiểu dữ liệu mà Action trả về.

Ví dụ:

```
Move → Axis
Jump → Button
Attack → Button
```

Vì `Move` của Player dùng:

```
MoveAction.ReadValue<float>();
```

nên Action này cần được cấu hình để trả về dữ liệu phù hợp với `float`.

---

## 1.3. Cách 1 – `InputSystem.actions.FindAction()`

Đây là cách đang dùng trong code hiện tại.

```
InputAction MoveAction;
InputAction JumpAction;
InputAction AttackAction;

void Awake()
{
    MoveAction = InputSystem.actions.FindAction("Move");
    JumpAction = InputSystem.actions.FindAction("Jump");
    AttackAction = InputSystem.actions.FindAction("Attack");
}
```

Sau đó đọc input:

```
movement = MoveAction.ReadValue<float>();
```

### Cách hoạt động

```
Input Action có tên "Move"
        ↓
FindAction("Move")
        ↓
MoveAction
        ↓
ReadValue<float>()
        ↓
movement
```

### Setup trong Unity

Cần có:

```
Input Actions Asset
└── Action Map
    ├── Move
    ├── Jump
    └── Attack
```

Tên Action trong Editor phải khớp với tên truyền vào `FindAction()`.

---

## 1.4. Cách 2 – `InputActionReference`

Thay vì tìm Action bằng tên, có thể lưu trực tiếp tham chiếu đến Action.

```
[SerializeField] InputActionReference moveAction;

void Update()
{
    float movement = moveAction.action.ReadValue<float>();
}
```

### Setup trong Unity

Trong Inspector của Script sẽ có:

```
Move Action
[ None ]
```

Kéo Action `Move` từ Input Actions Asset vào ô này.

### Khác với `FindAction()`

```
FindAction()
→ Script tự tìm Action bằng tên

InputActionReference
→ Kéo Action trực tiếp vào Inspector
```

---

## 1.5. Cách 3 – Callback

Input System có thể gọi một hàm khi trạng thái của Action thay đổi.

```
InputAction MoveAction;
float movement;

void Awake()
{
    MoveAction = InputSystem.actions.FindAction("Move");

    MoveAction.performed += OnMove;
    MoveAction.canceled += OnMove;
}

void OnMove(InputAction.CallbackContext context)
{
    movement = context.ReadValue<float>();
}

void OnDestroy()
{
    MoveAction.performed -= OnMove;
    MoveAction.canceled -= OnMove;
}
```

### Ý tưởng

Thay vì liên tục hỏi:

```
MoveAction.ReadValue<float>();
```

ta để Input System gọi:

```
OnMove(...)
```

khi input xảy ra hoặc thay đổi.

---

## 1.6. Cách 4 – `PlayerInput`

`PlayerInput` là component giúp quản lý Input Actions và kết nối Action với Script.

Player có thể được setup như sau:

```
Player
├── Rigidbody2D
├── Collider2D
├── PlayerInput
└── Player.cs
```

Trong `PlayerInput` chọn Input Actions Asset.

Có thể sử dụng các cách như **Send Messages**, **Unity Events** hoặc cơ chế callback tùy cấu hình.

Ví dụ với callback:

```
public void Jump(InputAction.CallbackContext context)
{
    if (context.performed)
    {
        // Xử lý Jump
    }
}
```

---

## 1.7. So sánh 4 cách Input

|Cách|Cách hoạt động|
|---|---|
|`FindAction()`|Script tìm Action theo tên|
|`InputActionReference`|Kéo trực tiếp Action vào Inspector|
|Callback|Input System gọi hàm khi Action thay đổi|
|`PlayerInput`|Component quản lý và kết nối Input Actions với Script|

---

# 2. Rigidbody2D

## 2.1. Rigidbody2D là gì?

`Rigidbody2D` là component giúp GameObject tham gia vào **Physics 2D** của Unity.

Nó liên quan đến:

- Vận tốc.
    
- Trọng lực.
    
- Lực.
    
- Chuyển động vật lý.
    
- Tương tác với Collider2D.
    

Ví dụ Player:

```
Player
├── Rigidbody2D
├── CapsuleCollider2D
└── Player.cs
```

---

## 2.2. Khai báo Rigidbody2D trong code

Code hiện tại:

```
[SerializeField] Rigidbody2D rb;
```

`rb` là biến dùng để **tham chiếu đến Rigidbody2D** của Player.

Sau khi có tham chiếu, code có thể sử dụng:

```
rb.linearVelocity
rb.AddForce(...)
rb.MovePosition(...)
rb.position
```

### Setup trong Unity

Player phải có component:

```
Rigidbody2D
```

Sau đó trong Inspector của `Player.cs` sẽ có trường:

```
Rb
[ None ]
```

Kéo **Rigidbody2D của Player** vào trường `Rb`.

Luồng hoạt động:

```
Rigidbody2D trong Unity
        ↓
[SerializeField] Rigidbody2D rb
        ↓
rb
        ↓
Code điều khiển Rigidbody2D
```

Nếu không gán Rigidbody2D vào `rb`, biến sẽ không tham chiếu đến component cần sử dụng.

---

## 2.3. Code di chuyển hiện tại – `linearVelocity`

Code của Player:

```
movement = MoveAction.ReadValue<float>();

rb.linearVelocity = new Vector2(
    movement * speed,
    rb.linearVelocityY
);
```

### Bước 1 – đọc Input

```
movement = MoveAction.ReadValue<float>();
```

Ví dụ:

```
A      → -1
Không  →  0
D      →  1
```

### Bước 2 – tạo vận tốc theo trục X

```
movement * speed
```

Nếu:

```
movement = 1
speed = 10
```

thì:

```
velocity X = 10
```

### Bước 3 – giữ nguyên vận tốc Y

```
rb.linearVelocityY
```

Không đặt Y về `0` vì Player cần giữ các tác động theo chiều dọc như gravity và jump.

---

## 2.4. Bốn cách điều khiển Rigidbody2D

Các ví dụ dưới đây giữ nguyên Input của Player và chỉ thay cách tác động lên Rigidbody2D.

### Cách 1 – `linearVelocity`

```
rb.linearVelocity = new Vector2(
    movement * speed,
    rb.linearVelocityY
);
```

**Ý tưởng:** đặt vận tốc của Rigidbody2D.

---

### Cách 2 – `AddForce()`

```
void FixedUpdate()
{
    rb.AddForce(
        Vector2.right * movement * speed
    );
}
```

Input có thể được đọc trong `Update()`:

```
void Update()
{
    movement = MoveAction.ReadValue<float>();
}
```

**Ý tưởng:** tác dụng một lực lên Rigidbody2D.

Khác với `linearVelocity`, `AddForce()` thiên về mô phỏng gia tốc và lực.

---

### Cách 3 – `MovePosition()`

```
void FixedUpdate()
{
    rb.MovePosition(
        rb.position +
        Vector2.right * movement * speed * Time.fixedDeltaTime
    );
}
```

**Ý tưởng:** đưa Rigidbody2D đến vị trí mới.

---

### Cách 4 – `Rigidbody2D.position`

```
void FixedUpdate()
{
    rb.position +=
        Vector2.right * movement * speed * Time.fixedDeltaTime;
}
```

**Ý tưởng:** thay đổi trực tiếp vị trí của Rigidbody2D.

---

## 2.5. `Update()` và `FixedUpdate()`

### `Update()`

Chạy theo từng frame render.

Phù hợp cho việc đọc Input:

```
movement = MoveAction.ReadValue<float>();
```

### `FixedUpdate()`

Được dùng cho các bước xử lý physics theo khoảng thời gian cố định.

Các thao tác như:

```
rb.AddForce(...);
rb.MovePosition(...);
```

thường được đặt trong `FixedUpdate()`.

Có thể hình dung:

```
Update()
   ↓
Đọc Input
   ↓
movement

FixedUpdate()
   ↓
Tác động Physics
   ↓
Rigidbody2D
```

---

## 2.6. Các thuộc tính Rigidbody2D quan trọng

### Body Type

Có 3 loại chính:

```
Dynamic
Kinematic
Static
```

**Dynamic**: Rigidbody2D chịu tác động của physics như gravity và lực. Player của bài này đang dùng Dynamic.

**Kinematic**: thường dùng cho object được điều khiển chủ yếu bằng code và có cách tương tác physics khác Dynamic.

**Static**: dùng cho object gần như cố định trong thế giới, chẳng hạn nhiều thành phần của môi trường.

---

### Mass

Khối lượng của Rigidbody2D.

Ví dụ trong ảnh:

```
Mass = 1
```

Mass có ý nghĩa rõ hơn khi sử dụng lực như:

```
rb.AddForce(...);
```

---

### Linear Damping

Làm giảm chuyển động tuyến tính theo thời gian.

```
Damping thấp
→ chuyển động giảm chậm

Damping cao
→ chuyển động giảm nhanh hơn
```

Đặc biệt dễ nhận thấy khi dùng `AddForce()`.

---

### Angular Damping

Làm giảm chuyển động quay.

Đối với Player 2D, thuộc tính này thường ít quan trọng hơn Linear Damping nếu nhân vật đã khóa rotation.

---

### Gravity Scale

Điều chỉnh mức ảnh hưởng của gravity.

```
0 → không chịu gravity
1 → gravity bình thường
2 → gravity mạnh hơn
```

Player hiện tại:

```
Gravity Scale = 1
```

---

### Collision Detection

Quy định cách Unity phát hiện collision.

`Discrete` là thiết lập thông thường và hiện đang được dùng trong ảnh.

Đối với object chuyển động rất nhanh, có thể cần kiểu detection phù hợp hơn để hạn chế khả năng bỏ sót va chạm.

---

### Interpolate

Dùng để làm chuyển động của Rigidbody2D nhìn mượt hơn giữa các bước physics.

Trong ảnh:

```
Interpolate = None
```

---

### Constraints

Cho phép khóa chuyển động hoặc rotation theo trục.

Trong ảnh của Player:

```
Freeze Rotation Z ✓
```

Điều này giúp Player 2D không bị xoay quanh trục Z.

---

### Simulated

Cho biết Rigidbody2D có tham gia vào mô phỏng Physics 2D hay không.

Player đang bật:

```
Simulated ✓
```

---

# 3. Collider2D

## 3.1. Collider2D là gì?

`Collider2D` là component tạo ra **hình dạng va chạm** cho GameObject.

Có thể hiểu đơn giản:

```
Rigidbody2D
→ xử lý vật lý và chuyển động

Collider2D
→ xác định hình dạng dùng để va chạm
```

Ví dụ Player của bài này dùng:

```
CapsuleCollider2D
```

---

## 3.2. Các loại Collider2D cơ bản

### `BoxCollider2D`

Hình chữ nhật.

```
┌─────────┐
│         │
└─────────┘
```

### `CircleCollider2D`

Hình tròn.

```
  ○
```

### `CapsuleCollider2D`

Hình capsule, thường phù hợp với nhân vật.

### `PolygonCollider2D`

Cho phép tạo Collider theo hình dạng nhiều cạnh.

---

## 3.3. Collider trong code Coin

Code hiện tại:

```
private void OnTriggerEnter2D(Collider2D other)
{
    Player player = other.GetComponent<Player>();
    player.GetCoin();
    Destroy(gameObject);
}
```

Tham số:

```
Collider2D other
```

là Collider2D của object đi vào Trigger.

Ví dụ Player đi vào Coin:

```
Player Collider
      ↓
Coin Trigger
      ↓
OnTriggerEnter2D()
      ↓
GetCoin()
      ↓
Destroy()
```

---

## 3.4. `Is Trigger`

Đây là thuộc tính quan trọng nhất trong code Coin hiện tại.

### `Is Trigger = OFF`

Collider hoạt động như Collider vật lý thông thường.

Ví dụ Player và Ground:

```
Player Collider
      ↓
Ground Collider
```

Player có thể va chạm và không đi xuyên qua Ground theo cách vật lý thông thường.

### `Is Trigger = ON`

Collider trở thành vùng Trigger.

Object đi vào vùng này sẽ tạo ra callback như:

```
OnTriggerEnter2D()
```

Coin sử dụng cách này.

---

## 3.5. Setup Collider trong Unity

### Player

```
Player
├── Rigidbody2D
└── CapsuleCollider2D
```

Trong Collider:

```
Is Trigger = OFF
```

Điều này giúp Collider của Player hoạt động như vùng va chạm vật lý.

### Coin

Ví dụ:

```
Coin
├── SpriteRenderer
├── CircleCollider2D
└── Coin.cs
```

Trong Collider:

```
Is Trigger = ON
```

để `OnTriggerEnter2D()` có thể được dùng cho việc nhặt Coin.

---

## 3.6. Các thuộc tính Collider2D quan trọng

### Material

`Physics Material 2D` chứa các tính chất vật lý như ma sát và độ nảy.

Trong ảnh:

```
Material = None
```

---

### Is Trigger

```
OFF → Collider vật lý
ON  → Trigger
```

Đây là thuộc tính cần nhớ nhất khi làm Coin.

---

### Offset

Điều chỉnh vị trí Collider so với GameObject.

Ví dụ:

```
Offset X = 0
Offset Y = -0.2
```

Collider sẽ được dịch so với vị trí gốc của object.

---

### Size

Điều chỉnh kích thước Collider.

Trong ảnh CapsuleCollider2D:

```
Size X = 1
Size Y = 2
```

Có thể chỉnh Size để Collider ôm sát nhân vật hơn.

---

### Direction

Với CapsuleCollider2D, Direction xác định hướng chính của capsule.

Player đứng theo chiều dọc nên trong ảnh:

```
Direction = Vertical
```

---

# 4. Tổng kết

## Input System

```
Input Action
     ↓
Input value
     ↓
C# Script
```

## Rigidbody2D

```
C# Script
     ↓
Rigidbody2D
     ↓
Physics 2D
```

Có thể điều khiển Rigidbody2D bằng:

```
linearVelocity
AddForce()
MovePosition()
Rigidbody2D.position
```

## Collider2D

```
Collider2D
     ↓
Xác định vùng va chạm
     ↓
Collision / Trigger
```

Mối quan hệ tổng thể:

```
                 INPUT SYSTEM
                      ↓
                 Input Action
                      ↓
                 C# Script
                      ↓
                Rigidbody2D
                      ↓
                   Physics
                      ↓
                  Collider2D
                 ↙          ↘
           Collision       Trigger
```