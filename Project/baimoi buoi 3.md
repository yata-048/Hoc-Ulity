# Unity 2D — New Input System & Physics 2D

> [!abstract] Mục tiêu  
> Hiểu luồng cơ bản:
> 
> **Input → Script → Rigidbody2D → Physics → Collision / Trigger / Raycast**

---

# 1. New Input System

## 1.1 New Input System là gì?

**New Input System** là hệ thống của Unity dùng để nhận và quản lý thao tác từ người chơi.

Ví dụ:

```text
Keyboard
Mouse
Controller
Touch
   ↓
Input System
   ↓
Game Code
```

Input System **chỉ cung cấp thông tin về Input**, không tự quyết định nhân vật phải làm gì.

Ví dụ:

```text
Nhấn D
  ↓
Input System nhận
  ↓
Move = (1, 0)
  ↓
Player Script
  ↓
Di chuyển Player
```

---

## 1.2 Input Action

**Input Action = một hành động mà game quan tâm.**

Ví dụ:

```text
Player
├── Move
├── Jump
├── Attack
└── Interact
```

Thay vì viết:

```text
D → chạy phải
A → chạy trái
```

ta tạo Action:

```text
Move
```

Sau đó gán các phím vào `Move`.

```text
Move
├── A / D
├── ← / →
└── Controller Stick
```

Game chỉ cần lấy giá trị của `Move`.

---

## 1.3 Binding

**Binding = Input cụ thể được gán cho một Action.**

Ví dụ:

```text
Move
├── A / D          ← Binding
├── Arrow Keys     ← Binding
└── Left Stick     ← Binding
```

Có thể hiểu:

```text
Action  = muốn làm gì?
Binding = dùng nút nào để làm?
```

---

## 1.4 Action Map

**Action Map = nhóm các Input Action theo một ngữ cảnh.**

Ví dụ:

```text
Player
├── Move
├── Jump
└── Attack

UI
├── Navigate
├── Submit
└── Cancel
```

Khi chơi:

```text
Player → Enable
UI     → Disable
```

Khi mở menu:

```text
Player → Disable
UI     → Enable
```

> **Action Map = nhóm Action.**

---

## 1.5 Input Actions Asset

Các Action thường được lưu trong một file:

```text
PlayerInputActions.inputactions
```

Ví dụ:

```text
PlayerInputActions
│
├── Player
│   ├── Move
│   ├── Jump
│   └── Attack
│
└── UI
    ├── Navigate
    ├── Submit
    └── Cancel
```

Đây là nơi cấu hình:

- Action Map
    
- Action
    
- Binding
    
- Loại dữ liệu Input
    

---

## 1.6 Action Type

### Button

Dùng cho hành động kiểu bật/tắt:

```text
Jump
Attack
Interact
```

Ví dụ:

```text
Space → Jump
```

---

### Value

Dùng khi Action cần trả về một giá trị.

Ví dụ:

```text
Move → Vector2
```

Kết quả:

```text
A → (-1, 0)
D → ( 1, 0)
```

Controller Stick có thể trả về:

```text
(0.7, 0.2)
```

> Movement 2D thường sử dụng **Value → Vector2**.

---

## 1.7 Vector2 và Movement

`Vector2` biểu diễn một hướng/giá trị trong không gian 2D:

```text
        Y
        ↑
        |
        |
--------+--------→ X
        |
```

Ví dụ:

```text
A → (-1, 0)
D → ( 1, 0)
W → ( 0, 1)
S → ( 0,-1)
```

Vì vậy:

```csharp
Vector2 moveInput;
```

có thể lưu hướng di chuyển.

---

## 1.8 PlayerInput

`PlayerInput` là Component giúp GameObject kết nối với Input Actions.

Có thể hình dung:

```text
Keyboard
    ↓
Input System
    ↓
Input Action
    ↓
PlayerInput
    ↓
Player Script
```

Một GameObject Player thường có:

```text
Player
├── SpriteRenderer
├── Rigidbody2D
├── Collider2D
└── PlayerInput
```

---

## 1.9 Callback của Input

Input Action có thể gọi code khi Input xảy ra.

Ví dụ:

```csharp
void OnMove(InputValue value)
{
    moveInput = value.Get<Vector2>();
}
```

Khi nhấn D:

```text
D
↓
Move Action
↓
OnMove()
↓
moveInput = (1, 0)
```

Đây gọi là **Callback**: hệ thống gọi hàm khi một sự kiện xảy ra.

---

## 1.10 Input và Movement

Input System **không trực tiếp di chuyển Player**.

Nó chỉ tạo ra dữ liệu:

```text
Move = (1, 0)
```

Script quyết định phải làm gì với dữ liệu đó:

```text
Input
 ↓
Move = (1, 0)
 ↓
Player Script
 ↓
Rigidbody2D
 ↓
Player di chuyển
```

Đây là tư duy quan trọng nhất khi học Input System.

---

# 2. Kiến thức nền — API

## 2.1 API là gì?

**API (Application Programming Interface)** = những công cụ/chức năng mà một hệ thống cung cấp để lập trình viên sử dụng.

Unity cung cấp rất nhiều API:

```csharp
Rigidbody2D
Physics2D
Transform
Collider2D
```

Ví dụ:

```csharp
rb.AddForce(...)
rb.MovePosition(...)
rb.linearVelocity

Physics2D.Raycast(...)

transform.position
```

Không cần biết Unity triển khai bên trong như thế nào, chỉ cần biết **API dùng để làm gì và cách sử dụng**.

---

## 2.2 Các khái niệm cần biết

|Khái niệm|Ý nghĩa|Ví dụ|
|---|---|---|
|**Class**|Khuôn mẫu|`Rigidbody2D`|
|**Object**|Đối tượng cụ thể|Rigidbody của Player|
|**Property**|Dữ liệu/trạng thái|`position`|
|**Method**|Hành động|`AddForce()`|
|**Callback**|Hàm được hệ thống gọi|`OnCollisionEnter2D()`|
|**API**|Tập hợp công cụ được cung cấp|Unity API|

Ví dụ:

```csharp
rb.AddForce(...)
```

```text
rb
↓
Rigidbody2D cụ thể

AddForce()
↓
Method của Rigidbody2D
```

---

# 3. Collider 2D

**Collider2D = vùng va chạm của GameObject.**

```text
Player
├── SpriteRenderer → hình ảnh
└── Collider2D     → vùng va chạm
```

Các loại thường gặp:

- `BoxCollider2D`
    
- `CircleCollider2D`
    
- `CapsuleCollider2D`
    
- `PolygonCollider2D`
    

> **SpriteRenderer → hiển thị**  
> **Collider2D → vùng va chạm**

---

# 4. Rigidbody 2D

**Rigidbody2D = đưa GameObject vào hệ thống Physics 2D.**

Liên quan đến:

- Gravity
    
- Velocity
    
- Force
    
- Collision
    
- Movement
    

Ví dụ:

```text
Player
├── Rigidbody2D
└── Collider2D
```

> **Collider = vùng va chạm**  
> **Rigidbody = tham gia Physics**

---

# 5. Rigidbody2D — Body Type

|Body Type|Ý nghĩa|
|---|---|
|**Dynamic**|Chịu tác động của Physics|
|**Kinematic**|Chuyển động chủ yếu do code điều khiển|
|**Static**|Đứng yên|

### Dynamic

Chịu ảnh hưởng của:

- Gravity
    
- Force
    
- Velocity
    
- Collision
    

Ví dụ: Player, Ball.

### Kinematic

Chuyển động chủ yếu do code/gameplay.

Ví dụ: Moving Platform.

### Static

Không di chuyển.

Ví dụ: Ground, Wall.

> **Dynamic = Physics**  
> **Kinematic = Code**  
> **Static = Đứng yên**

---

# 6. Collision

**Collision = va chạm vật lý giữa các Collider.**

```text
Player → → → Wall
              ██
```

## Collision Callback

```csharp
OnCollisionEnter2D()  // bắt đầu va chạm
OnCollisionStay2D()   // đang va chạm
OnCollisionExit2D()   // kết thúc va chạm
```

Ví dụ:

```csharp
private void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Vừa va chạm!");
}
```

---

# 7. Điều kiện xảy ra Collision

Collision phụ thuộc vào nhiều yếu tố:

- Collider2D
    
- Rigidbody2D
    
- Body Type
    
- Layer Collision Matrix
    
- Physics Settings
    

Ví dụ:

```text
Player Layer × Ground Layer = ON
```

→ Hai Layer được phép tương tác.

Nếu:

```text
Player Layer × Ground Layer = OFF
```

→ Collision giữa hai Layer bị bỏ qua.

> Không nên chỉ nhớ **"có hai Collider là có Collision"**.

---

# 8. Trigger

**Trigger = vùng phát hiện thay vì vùng va chạm dùng để chặn vật thể.**

Bật:

```text
Collider2D
☑ Is Trigger
```

Ví dụ:

```text
Player → → → Coin
              ○
```

Player có thể đi xuyên qua vùng Trigger nhưng Unity vẫn phát hiện sự kiện.

## Callback

```csharp
OnTriggerEnter2D()
OnTriggerStay2D()
OnTriggerExit2D()
```

Ví dụ:

```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Đã đi vào Trigger!");
}
```

### Collision vs Trigger

|Collision|Trigger|
|---|---|
|Va chạm vật lý|Vùng phát hiện|
|Có thể chặn|Không dùng để chặn|
|`OnCollision...`|`OnTrigger...`|
|`Collision2D`|`Collider2D`|

Ví dụ:

```text
Wall        → Collision
Coin        → Trigger
Checkpoint  → Trigger
Damage Zone → Trigger
```

---

# 9. Raycast2D

**Raycast = bắn một tia để kiểm tra Collider trên đường đi.**

```text
Player
  |
  |
  ↓
======== Ground
```

Ví dụ:

```csharp
RaycastHit2D hit = Physics2D.Raycast(
    transform.position,
    Vector2.down,
    1f
);
```

```text
transform.position → điểm bắt đầu
Vector2.down       → hướng
1f                 → độ dài
```

Kiểm tra:

```csharp
if (hit.collider != null)
{
    Debug.Log("Đã hit!");
}
```

### Raycast thường dùng để:

- Check Ground
    
- Check Wall
    
- Enemy phát hiện Player
    
- Kiểm tra vật phía trước
    
- Hit Detection
    

> **Collider = vùng va chạm**  
> **Raycast = phép kiểm tra**

---

# 10. Layer

**Layer = phân loại GameObject.**

Ví dụ:

```text
Player → Player Layer
Enemy  → Enemy Layer
Ground → Ground Layer
Coin   → Coin Layer
```

Layer giúp Physics biết Object thuộc nhóm nào.

---

# 11. LayerMask

**LayerMask = giới hạn phép kiểm tra vào những Layer được chọn.**

Ví dụ:

```text
GroundMask

☑ Ground
☐ Player
☐ Enemy
☐ Coin
```

Code:

```csharp
[SerializeField] LayerMask groundLayer;
```

Dùng với Raycast:

```csharp
RaycastHit2D hit = Physics2D.Raycast(
    transform.position,
    Vector2.down,
    1f,
    groundLayer
);
```

→ Raycast chỉ quan tâm Layer được chọn.

---

# 12. Các cách di chuyển nhân vật

## Transform

```csharp
transform.position += direction * speed * Time.deltaTime;
```

→ Thay đổi vị trí trực tiếp.

Đơn giản nhưng không phù hợp với mọi trường hợp khi Rigidbody đang quản lý Physics.

---

## Rigidbody2D — linearVelocity

```csharp
rb.linearVelocity = new Vector2(
    moveInput.x * speed,
    rb.linearVelocity.y
);
```

Ví dụ Platformer:

```text
X → Player điều khiển
Y → Gravity / Physics
```

---

## MovePosition

```csharp
rb.MovePosition(targetPosition);
```

→ Yêu cầu Rigidbody di chuyển đến vị trí mong muốn.

---

## AddForce

```csharp
rb.AddForce(Vector2.right * force);
```

→ Tác dụng lực lên Rigidbody.

Thường phù hợp với:

- Knockback
    
- Push
    
- Physics-based movement
    
- Một số kiểu Jump
    

---

# 13. Update và FixedUpdate

## Update

Chạy theo từng frame.

Thường dùng cho:

- Input
    
- Game Logic
    

```csharp
void Update()
{
    // đọc Input
}
```

## FixedUpdate

Chạy theo timestep của Physics.

Thường dùng cho:

- Rigidbody
    
- Physics Movement
    

```csharp
void FixedUpdate()
{
    // xử lý Physics
}
```

> **Update → Input / Logic**  
> **FixedUpdate → Physics**

---

# 14. Tổng kết — Luồng hoạt động

Đây là phần quan trọng nhất:

```text
                INPUT
                  ↓
          New Input System
                  ↓
             Input Action
                  ↓
            Player Script
                  ↓
            Rigidbody2D
                  ↓
               Physics
              ↙       ↘
        Collision     Raycast
            ↓             ↓
         Trigger       LayerMask
```

### Ví dụ Player chạy

```text
Bấm D
 ↓
Move Action
 ↓
Vector2 (1, 0)
 ↓
Player Script
 ↓
Rigidbody2D
 ↓
Physics
 ↓
Player chạy sang phải
```

### Gặp Wall

```text
Player → Wall
          ↓
      Collider2D
          ↓
       Collision
```

### Đi vào Coin

```text
Player → Coin
          ↓
      Is Trigger
          ↓
 OnTriggerEnter2D()
```

### Kiểm tra Ground

```text
Player
  ↓
Raycast2D
  ↓
LayerMask
  ↓
Ground?
```

---

# 15. Cheat Sheet

```text
NEW INPUT SYSTEM
→ Quản lý Input

Input Action
→ Hành động: Move / Jump / Attack

Action Map
→ Nhóm các Action

Binding
→ Phím / nút điều khiển Action

Button
→ Jump / Attack / Interact

Value
→ Dữ liệu như Vector2

PlayerInput
→ Kết nối Player với Input Actions

Callback
→ Hàm được hệ thống tự gọi

API
→ Bộ công cụ Unity cung cấp

Class
→ Khuôn mẫu

Property
→ Dữ liệu / trạng thái

Method
→ Hành động

Collider2D
→ Vùng va chạm

Rigidbody2D
→ Tham gia Physics

Dynamic
→ Chịu tác động của Physics

Kinematic
→ Chuyển động chủ yếu do Code

Static
→ Đứng yên

Collision
→ Va chạm vật lý

Trigger
→ Vùng phát hiện

Raycast2D
→ Bắn tia để kiểm tra

Layer
→ Phân loại Object

LayerMask
→ Lọc Layer

linearVelocity
→ Điều khiển vận tốc

MovePosition
→ Di chuyển Rigidbody

AddForce
→ Tác dụng lực

Update
→ Frame / Input / Logic

FixedUpdate
→ Physics
```

