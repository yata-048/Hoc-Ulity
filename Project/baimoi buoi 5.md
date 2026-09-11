
# 1. Delegate

## 1.1. Delegate là gì?

`Delegate` là một kiểu dữ liệu có thể **lưu trữ một method**.

Nói đơn giản:

> Delegate giống như một biến, nhưng thay vì lưu `int`, `float`, `GameObject`..., nó lưu **method**.

Ví dụ:

```csharp
delegate void ShootFunction();

void Shoot()
{
    Debug.Log("Bắn!");
}
````

Ở đây:

```
delegate void ShootFunction();
```

Tạo ra một kiểu Delegate tên là `ShootFunction`.

Delegate này chỉ có thể chứa những method:

- Không có tham số
    
- Không trả về giá trị (`void`)
    

---

## 1.2. Sử dụng Delegate

```
delegate void ShootFunction();

void Shoot()
{
    Debug.Log("Bắn!");
}

ShootFunction shoot;

void Start()
{
    shoot = Shoot;

    shoot();
}
```

### API/cú pháp quan trọng

```
shoot = Shoot;
```

Gán method `Shoot()` vào Delegate.

```
shoot();
```

Gọi method đang được Delegate lưu.

### Kết quả

```
Bắn!
```

### Hiểu đơn giản

```
shoot
 ↓
Shoot()
```

Khi gọi:

```
shoot();
```

thì thực chất đang gọi:

```
Shoot();
```

---

# 2. Action

## 2.1. Action là gì?

`Action` cũng là Delegate.

Điểm khác:

> `Action` là Delegate được C# tạo sẵn cho mình.

Thay vì tự viết:

```
delegate void ShootFunction();
```

có thể viết:

```
Action shoot;
```

---

## 2.2. Ví dụ

```
using System;

Action shoot;

void Shoot()
{
    Debug.Log("Bắn!");
}

void Start()
{
    shoot = Shoot;

    shoot();
}
```

### `Action` có tác dụng gì?

```
Action shoot;
```

Tạo một biến `shoot` có thể chứa method.

```
shoot = Shoot;
```

Cho `shoot` trỏ tới method `Shoot`.

```
shoot();
```

Gọi method `Shoot`.

---

## 2.3. Action có tham số

`Action` có thể nhận tham số.

Ví dụ:

```
Action<int> TakeDamage;

void Damage(int damage)
{
    Debug.Log("Mất " + damage + " máu");
}

void Start()
{
    TakeDamage = Damage;

    TakeDamage(10);
}
```

Kết quả:

```
Mất 10 máu
```

### `Action<int>` nghĩa là gì?

```
Action<int>
```

Có nghĩa:

> Delegate này nhận **1 tham số kiểu `int`** và không trả về giá trị.

Ví dụ:

```
TakeDamage(10);
```

`10` được truyền vào:

```
Damage(int damage)
```

nên:

```
damage = 10;
```

---

# 3. Event

## 3.1. Event là gì?

`Event` được xây dựng dựa trên Delegate.

Nó thường được dùng khi muốn nói:

> "Một chuyện vừa xảy ra, những object nào quan tâm thì tự phản ứng."

Ví dụ:

```
Player bắn
   ↓
OnShoot
   ↓
├── Giảm đạn
├── Knockback
├── Phát âm thanh
└── Hiện hiệu ứng
```

---

# 3.2. Ví dụ Event

```
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnShoot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnShoot?.Invoke();
        }
    }
}
```

### API quan trọng

```
public event Action OnShoot;
```

Tạo một Event tên là `OnShoot`.

Event này không cần tham số.

---

### `?.Invoke()` làm gì?

```
OnShoot?.Invoke();
```

Có nghĩa:

> Gọi tất cả method đã đăng ký vào `OnShoot`.

Ví dụ có 3 method đăng ký:

```
OnShoot
 ↓
ReduceAmmo()
DoKnockback()
PlaySound()
```

Khi:

```
OnShoot?.Invoke();
```

được chạy thì cả 3 method sẽ được gọi.

---

# 3.3. Đăng ký Event

Ví dụ:

```
void ReduceAmmo()
{
    Debug.Log("Giảm đạn");
}

void Start()
{
    player.OnShoot += ReduceAmmo;
}
```

### `+=` có tác dụng gì?

```
player.OnShoot += ReduceAmmo;
```

Đăng ký:

> Khi `OnShoot` xảy ra → gọi `ReduceAmmo()`.

---

## 3.4. Hủy đăng ký

```
player.OnShoot -= ReduceAmmo;
```

`-=` có tác dụng:

> Xóa `ReduceAmmo()` khỏi danh sách method của Event.

---

# 4. Event trong Game

Ví dụ Player có:

```
public event Action OnShoot;
```

Ammo:

```
void ReduceAmmo()
{
    Ammo--;
}
```

Knockback:

```
void DoKnockback()
{
    rb.linearVelocity = new Vector2(-5f, rb.linearVelocityY);
}
```

Đăng ký:

```
player.OnShoot += ReduceAmmo;
player.OnShoot += DoKnockback;
```

Khi Player bắn:

```
OnShoot?.Invoke();
```

Kết quả:

```
Player bắn
    ↓
OnShoot.Invoke()
    ↓
ReduceAmmo()
    ↓
DoKnockback()
```

### Tác dụng của Event ở đây

Player **không cần biết**:

- Ammo nằm ở đâu
    
- Knockback xử lý thế nào
    
- Sound xử lý thế nào
    
- UI xử lý thế nào
    

Player chỉ cần thông báo:

```
OnShoot?.Invoke();
```

---

# 5. UnityEvent

## 5.1. UnityEvent là gì?

`UnityEvent` cũng là một hệ thống Event của Unity.

Điểm rất quan trọng:

> `UnityEvent` cho phép đăng ký method trực tiếp trong **Inspector**.

Ví dụ:

```
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] UnityEvent shootEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootEvent.Invoke();
        }
    }
}
```

---

## 5.2. `UnityEvent` có tác dụng gì?

```
[SerializeField] UnityEvent shootEvent;
```

Tạo một Event có thể nhìn thấy trong Inspector.

Sau đó trong Inspector có thể đăng ký:

```
Shoot Event

Player
 ├── ReduceAmmo()
 └── DoKnockBack()
```

Khi code chạy:

```
shootEvent.Invoke();
```

Unity sẽ gọi:

```
ReduceAmmo();
DoKnockBack();
```

---

# 5.3. `Invoke()` của UnityEvent

```
shootEvent.Invoke();
```

API:

```
Invoke()
```

có tác dụng:

> Kích hoạt Event → gọi tất cả method đã đăng ký trong Inspector.

---

# 5.4. UnityEvent vs C# Event

### C# Event

Đăng ký bằng code:

```
player.OnShoot += ReduceAmmo;
```

Kích hoạt:

```
OnShoot?.Invoke();
```

---

### UnityEvent

Đăng ký bằng Inspector.

Kích hoạt:

```
shootEvent.Invoke();
```

---

# 6. Coroutine

## 6.1. Coroutine là gì?

Coroutine cho phép một method:

> Chạy → tạm dừng → chờ → chạy tiếp.

Ví dụ bình thường:

```
void Test()
{
    Debug.Log("A");
    Debug.Log("B");
    Debug.Log("C");
}
```

Nó chạy gần như ngay lập tức:

```
A
B
C
```

Coroutine có thể làm:

```
A
↓
chờ 2 giây
↓
B
```

---

# 7. IEnumerator

## 7.1. `IEnumerator` là gì?

`IEnumerator` là kiểu thường được dùng làm **kiểu trả về của Coroutine**.

Ví dụ:

```
IEnumerator Test()
{
    Debug.Log("A");

    yield return new WaitForSeconds(2f);

    Debug.Log("B");
}
```

Ở đây:

```
IEnumerator Test()
```

cho Unity biết đây là một method có thể **tạm dừng bằng `yield return`**.

---

# 8. `yield return`

`yield return` là phần rất quan trọng của Coroutine.

Nó có tác dụng:

> Tạm dừng Coroutine tại đây.

---

## 8.1. `yield return null`

```
IEnumerator Test()
{
    Debug.Log("A");

    yield return null;

    Debug.Log("B");
}
```

### `yield return null` làm gì?

Tạm dừng Coroutine cho đến **frame tiếp theo**.

Ví dụ:

```
Frame 1:
A

Frame 2:
B
```

---

# 8.2. `WaitForSeconds`

```
yield return new WaitForSeconds(2f);
```

API:

```
WaitForSeconds(2f)
```

có tác dụng:

> Tạo thời gian chờ 2 giây.

Ví dụ:

```
IEnumerator Test()
{
    Debug.Log("Bắt đầu");

    yield return new WaitForSeconds(2f);

    Debug.Log("Sau 2 giây");
}
```

Kết quả:

```
Bắt đầu
↓
2 giây
↓
Sau 2 giây
```

---

# 9. StartCoroutine

Viết Coroutine thôi thì nó **chưa tự chạy**.

Ví dụ:

```
IEnumerator Test()
{
    Debug.Log("Hello");
}
```

Chỉ viết như vậy chưa đủ.

Phải:

```
StartCoroutine(Test());
```

### `StartCoroutine()` có tác dụng gì?

```
StartCoroutine(Test());
```

Nói với Unity:

> "Hãy bắt đầu chạy Coroutine `Test()`."

Ví dụ:

```
void Start()
{
    StartCoroutine(Test());
}
```

---

# 10. StopCoroutine

Nếu muốn dừng Coroutine đang chạy:

```
StopCoroutine(Test());
```

Tuy nhiên cách thường dùng là lưu Coroutine lại:

```
Coroutine myCoroutine;

void Start()
{
    myCoroutine = StartCoroutine(Test());
}
```

Sau đó:

```
StopCoroutine(myCoroutine);
```

### `StopCoroutine()` có tác dụng gì?

> Dừng Coroutine đang chạy.

---

# 11. Ví dụ Coroutine trong Game

Ví dụ khi Player bắn:

```
IEnumerator HideEffect()
{
    triangle.SetActive(true);

    yield return new WaitForSeconds(0.5f);

    triangle.SetActive(false);
}
```

### `SetActive(true)`

```
triangle.SetActive(true);
```

Bật GameObject `triangle`.

### `WaitForSeconds`

```
yield return new WaitForSeconds(0.5f);
```

Chờ 0.5 giây.

### `SetActive(false)`

```
triangle.SetActive(false);
```

Tắt GameObject.

---

## Chạy Coroutine

```
StartCoroutine(HideEffect());
```

Toàn bộ quá trình:

```
Bắn
 ↓
triangle bật
 ↓
chờ 0.5 giây
 ↓
triangle tắt
```

---

# 12. Kết hợp Event + Coroutine

Ví dụ:

```
[SerializeField] UnityEvent shootEvent;
[SerializeField] GameObject triangle;

void Shoot()
{
    shootEvent.Invoke();

    StartCoroutine(HideEffect());
}

IEnumerator HideEffect()
{
    triangle.SetActive(true);

    yield return new WaitForSeconds(0.5f);

    triangle.SetActive(false);
}
```

Khi Player bắn:

```
Shoot()
 │
 ├── shootEvent.Invoke()
 │       │
 │       ├── ReduceAmmo()
 │       └── DoKnockBack()
 │
 └── StartCoroutine(HideEffect())
         │
         ├── Bật triangle
         │
         ├── Chờ 0.5 giây
         │
         └── Tắt triangle
```

---

# 13. Áp dụng vào Game hiện tại

Player đang có:

```
[SerializeField] UnityEvent shootEvent;
```

Khi bắn:

```
if (AttackAction.WasPressedThisFrame() && AmmoAmount > 0)
{
    shootEvent.Invoke();
}
```

`shootEvent.Invoke()` có tác dụng:

> Kích hoạt tất cả method đã đăng ký cho sự kiện bắn.

Trong Inspector có thể đăng ký:

```
Shoot Event

Player
 ├── ReduceAmmo()
 └── DoKnockBack()
```

Vì vậy:

```
shootEvent.Invoke();
```

sẽ làm:

```
AmmoAmount--
        +
Player bị knockback
```

---

# 14. Các API cần nhớ

|API / cú pháp|Tác dụng|
|---|---|
|`delegate`|Tự tạo một kiểu Delegate|
|`Action`|Delegate có sẵn của C#|
|`+=`|Đăng ký method vào Event/Delegate|
|`-=`|Hủy đăng ký method|
|`Invoke()`|Gọi các method đang được đăng ký|
|`UnityEvent`|Event của Unity, đăng ký được trong Inspector|
|`yield return null`|Chờ đến frame tiếp theo|
|`WaitForSeconds()`|Chờ một khoảng thời gian|
|`IEnumerator`|Kiểu thường dùng để viết Coroutine|
|`StartCoroutine()`|Bắt đầu Coroutine|
|`StopCoroutine()`|Dừng Coroutine|
|`SetActive(true)`|Bật GameObject|
|`SetActive(false)`|Tắt GameObject|

---

# 15. Cách nhớ nhanh

### Delegate

> "Tao muốn một biến có thể chứa method."

```
delegate void MyFunction();
```

### Action

> "Tao muốn Delegate nhưng không muốn tự định nghĩa."

```
Action myFunction;
```

### Event

> "Có chuyện xảy ra, báo cho những thằng đã đăng ký."

```
OnShoot?.Invoke();
```

### UnityEvent

> "Event nhưng muốn đăng ký method bằng Inspector."

```
shootEvent.Invoke();
```

### Coroutine

> "Chạy code nhưng có thể chờ rồi chạy tiếp."

```
IEnumerator Test()
{
    Debug.Log("A");

    yield return new WaitForSeconds(2f);

    Debug.Log("B");
}
```

### StartCoroutine

> "Bắt đầu chạy Coroutine."

```
StartCoroutine(Test());
```

### StopCoroutine

> "Dừng Coroutine."

```
StopCoroutine(myCoroutine);
```