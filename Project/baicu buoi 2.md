# C# và Unity — Kiến thức nền tảng

  

> **Mục tiêu:** Nắm được những khái niệm C# và Unity thường gặp khi bắt đầu xây dựng game, đặc biệt là cách script tương tác với GameObject và cách tổ chức logic trong Unity.

  

---

  

## 1. Class C# thuần và `MonoBehaviour`

  

Trong Unity, có thể chia script thành hai nhóm lớn:

  

| Loại | Đặc điểm | Cách sử dụng |

|---|---|---|

| **Class C# thuần** | Không kế thừa `MonoBehaviour` | Xử lý dữ liệu, thuật toán và logic độc lập |

| **`MonoBehaviour`** | Kế thừa từ `MonoBehaviour` | Gắn trực tiếp lên `GameObject` và sử dụng vòng đời của Unity |

  

### 1.1. Class C# thuần

  

**Class C# thuần** là class tiêu chuẩn của C#, không phụ thuộc trực tiếp vào vòng đời của Unity.

  

Thường dùng để:

  

- Chứa dữ liệu.

- Xử lý thuật toán.

- Tách các logic không cần tương tác trực tiếp với `GameObject`.

- Tạo các đối tượng bằng `new` khi cần.

  

Ví dụ:

  

```csharp

public class PlayerData

{

    public int health;

    public int mana;

}

```

  

Class này không thể được kéo trực tiếp vào một `GameObject` trong Scene như một Component.

  

### 1.2. `MonoBehaviour`

  

`MonoBehaviour` là class cơ sở cho các script Component của Unity.

  

```csharp

using UnityEngine;

  

public class Player : MonoBehaviour

{

}

```

  

Khi kế thừa `MonoBehaviour`, script có thể:

  

- Được gắn vào `GameObject`.

- Xuất dữ liệu ra **Inspector** thông qua cơ chế serialization của Unity.

- Sử dụng các hàm vòng đời như `Awake()`, `Start()`, `Update()`, `OnEnable()`...

- Tương tác trực tiếp với các Component khác trong Scene.

  

> **Lưu ý:** Không tạo Component `MonoBehaviour` bằng `new`.

Không nên:


> ```Player player = new Player(); ```

>

> Hãy để Unity quản lý Component thông qua `GameObject`, `AddComponent`, Prefab hoặc Scene.

  

---

  

# 2. Vòng đời của Script trong Unity

  

Các hàm như `Awake`, `Start`, `Update`... là một phần của **MonoBehaviour lifecycle**.

  

Có thể hình dung đơn giản:

  

```text

Script được tạo / nạp

        ↓

     Awake()

        ↓

    OnEnable()

        ↓

      Start()

        ↓

    ┌───────────────┐

    │   Game Loop   │

    │               │

    │ FixedUpdate() │ ← nhịp vật lý

    │ Update()      │ ← mỗi frame

    │ LateUpdate()  │ ← sau Update

    └───────────────┘

        ↓

   OnDisable()

        ↓

   OnDestroy()

```

  

> **Quan trọng:** Đây là cách hiểu tổng quát. Thứ tự thực tế giữa các object/script có thể phụ thuộc vào Execution Order của Unity.

  

## 2.1. `Awake()`

  

`Awake()` được Unity gọi khi instance của script được khởi tạo/nạp.

  

Thường dùng để:

  

- Khởi tạo dữ liệu nội bộ.

- Lấy Component cần thiết bằng `GetComponent`.

- Thiết lập các tham chiếu mà script cần ngay từ đầu.

  

Ví dụ:

  

```csharp

private Rigidbody2D rb;

  

private void Awake()

{

    rb = GetComponent<Rigidbody2D>();

}

```

  

### `Awake()` khác `Start()` như thế nào?

  

- `Awake()` phù hợp với **khởi tạo bản thân object**.

- `Start()` phù hợp với những công việc cần thực hiện khi object bắt đầu tham gia gameplay, đặc biệt khi cần liên kết với các object khác.

  

---

  

## 2.2. `OnEnable()`

  

`OnEnable()` được gọi mỗi khi `GameObject` hoặc Component chuyển từ trạng thái **tắt → bật**.

  

Ví dụ:

  

```csharp

private void OnEnable()

{

    Debug.Log("Object được bật");

}

```

  

Khác với `Awake()` và `Start()`, `OnEnable()` có thể được gọi **nhiều lần** nếu object bị tắt rồi bật lại.

  

---

  

## 2.3. `Start()`

  

`Start()` được Unity gọi trước frame đầu tiên mà script bắt đầu chạy.

  

Thông thường `Start()` chỉ được gọi **một lần cho mỗi instance**.

  

Thường dùng để:

  

- Liên kết với các object khác.

- Thiết lập trạng thái ban đầu của gameplay.

- Thực hiện những khởi tạo không nhất thiết phải làm ngay trong `Awake()`.

  

Ví dụ:

  

```csharp

private void Start()

{

    // Khởi tạo logic gameplay

}

```

  

---

  

## 2.4. `FixedUpdate()`

  

`FixedUpdate()` chạy theo **khoảng thời gian cố định**, thay vì phụ thuộc trực tiếp vào FPS.

  

Thường dùng cho:

  

- `Rigidbody`.

- Lực.

- Vận tốc.

- Các thao tác liên quan đến hệ thống vật lý.

  

Ví dụ:

  

```csharp

private void FixedUpdate()

{

    rb.AddForce(Vector2.right * 10f);

}

```

  

> **Ghi nhớ:** Logic vật lý thường đặt trong `FixedUpdate()` thay vì `Update()`.

  

---

  

## 2.5. `Update()`

  

`Update()` được gọi một lần mỗi frame.

  

Tần suất gọi phụ thuộc vào tốc độ khung hình.

  

Thường dùng cho:

  

- Input của người chơi.

- Logic gameplay thông thường.

- Kiểm tra trạng thái liên tục.

- Điều khiển các hành vi không trực tiếp thuộc hệ thống vật lý.

  

Ví dụ:

  

```csharp

private void Update()

{

    // Đọc input và xử lý logic

}

```

  

---

  

## 2.6. `LateUpdate()`

  

`LateUpdate()` chạy sau khi các hàm `Update()` đã được xử lý.

  

Một trường hợp sử dụng phổ biến là **Camera bám theo nhân vật**.

  

Ví dụ:

  

```csharp

private void LateUpdate()

{

    transform.position = player.position;

}

```

  

Ý tưởng là:

  

```text

Player cập nhật vị trí

        ↓

Các object xử lý Update()

        ↓

Camera xử lý LateUpdate()

        ↓

Camera lấy vị trí mới của Player

```

  

---

  

## 2.7. `OnDisable()`

  

`OnDisable()` được gọi khi `GameObject` hoặc Component chuyển từ **bật → tắt**.

  

Ví dụ:

  

```csharp

private void OnDisable()

{

    // Dọn dẹp hoặc ngắt các đăng ký sự kiện

}

```

  

Giống `OnEnable()`, hàm này có thể được gọi nhiều lần trong suốt vòng đời object.

  

---

  

## 2.8. `OnDestroy()`

  

`OnDestroy()` được gọi khi Component hoặc `GameObject` bị Unity hủy.

  

Ví dụ:

  

```csharp

private void OnDestroy()

{

    // Dọn dẹp tài nguyên hoặc tham chiếu nếu cần

}

```

  

Thường gặp khi:

  

- Gọi `Destroy(gameObject)`.

- Object bị xóa khỏi Scene.

- Object bị hủy trong quá trình chuyển đổi hoặc quản lý Scene.

  

---

  

# 3. Các thao tác và khái niệm cơ bản trong Unity

  

## 3.1. Import Assets

  

**Asset** là tài nguyên được sử dụng trong game.

  

Ví dụ:

  

- Hình ảnh.

- Sprite.

- Audio.

- SFX.

- VFX.

- Model.

- Animation.

  

**Import Assets** là quá trình đưa các tài nguyên này vào project Unity để Unity có thể quản lý và sử dụng chúng.

  

---

  

## 3.2. `SpriteRenderer`

  

`SpriteRenderer` là Component dùng để **hiển thị Sprite trong game**, đặc biệt phổ biến trong game 2D.

  

Ví dụ:

  

```text

GameObject

    ├── Transform

    └── SpriteRenderer

```

  

`SpriteRenderer` có thể quyết định Sprite nào được hiển thị, cùng với các thiết lập như màu, flip và thứ tự vẽ.

  

---

  

## 3.3. Sorting Layer

  

**Sorting Layer** dùng để xác định thứ tự vẽ giữa các nhóm đối tượng 2D.

  

Ví dụ:

  

```text

Background

    ↓

Player

    ↓

Foreground

```

  

Object ở layer được vẽ phía sau sẽ bị object ở layer phía trước che lên.

  

---

  

## 3.4. Order in Layer

  

**Order in Layer** quyết định thứ tự hiển thị của các Sprite **trong cùng một Sorting Layer**.

  

Thông thường:

  

```text

Order = -1   → phía sau

Order =  0   → ở giữa

Order =  1   → phía trước

```

  

Ví dụ:

  

- Player: `Order in Layer = 5`

- Enemy: `Order in Layer = 3`

  

Nếu cùng Sorting Layer thì Player sẽ được vẽ phía trước Enemy.

  

> **Ghi nhớ:** Có thể hiểu đơn giản:

>

> **Sorting Layer → chọn nhóm/lớp vẽ.**

>

> **Order in Layer → chọn thứ tự bên trong lớp đó.**

  

---

  

## 3.5. `Vector2` và `Vector3`

  

`Vector2` và `Vector3` là các kiểu dữ liệu thường dùng để biểu diễn thông tin trong không gian.

  

### `Vector2`

  

Có hai thành phần:

  

```text

(x, y)

```

  

Thường dùng trong game 2D.

  

Ví dụ:

  

```csharp

Vector2 direction = new Vector2(1, 0);

```

  

### `Vector3`

  

Có ba thành phần:

  

```text

(x, y, z)

```

  

Thường dùng trong không gian 3D và cũng xuất hiện trong nhiều API của Unity.

  

Ví dụ:

  

```csharp

Vector3 position = new Vector3(1, 2, 0);

```

  

Vector có thể được sử dụng để biểu diễn:

  

- Vị trí.

- Hướng.

- Độ dịch chuyển.

- Tỉ lệ/scale trong một số trường hợp.

  

---

  

## 3.6. `Time.deltaTime`

  

`Time.deltaTime` biểu diễn **thời gian đã trôi qua giữa hai frame**, tính bằng giây.

  

Nó thường được dùng khi tính chuyển động để tốc độ không phụ thuộc trực tiếp vào FPS.

  

Ví dụ:

  

```csharp

transform.position += Vector3.right * speed * Time.deltaTime;

```

  

Nếu:

  

```text

speed = 5

```

  

thì object di chuyển với tốc độ khoảng **5 đơn vị/giây**, thay vì 5 đơn vị mỗi frame.

  

> **Ghi nhớ:** Khi viết chuyển động dựa trên `Update()`, thường cần cân nhắc `Time.deltaTime`.

  

---

  

# 4. Một số công cụ và API thường dùng

  

## 4.1. `Mathf`

  

`Mathf` là class chứa nhiều hàm toán học tiện dụng của Unity.

  

Một số hàm quan trọng:

  

- `Mathf.Clamp()`

- `Mathf.Lerp()`

  

---

  

## 4.2. `Mathf.Clamp()`

  

`Clamp()` giới hạn một giá trị trong một khoảng xác định.

  

Cú pháp:

  

```csharp

Mathf.Clamp(value, min, max);

```

  

Ví dụ:

  

```csharp

health = Mathf.Clamp(health, 0, 100);

```

  

Kết quả:

  

```text

health < 0    → 0

0 ≤ health ≤ 100 → giữ nguyên

health > 100  → 100

```

  

**Ứng dụng:** giới hạn máu, mana, tốc độ, vị trí, giá trị slider...

  

---

  

## 4.3. `Mathf.Lerp()`

  

`Lerp()` thực hiện **nội suy tuyến tính** giữa hai giá trị.

  

Cú pháp:

  

```csharp

Mathf.Lerp(a, b, t);

```

  

Trong đó:

  

- `a`: giá trị bắt đầu.

- `b`: giá trị kết thúc.

- `t`: mức nội suy, thường từ `0` đến `1`.

  

Ví dụ:

  

```csharp

float value = Mathf.Lerp(0, 100, 0.5f);

```

  

Kết quả:

  

```text

value = 50

```

  

Có thể hình dung:

  

```text

0 -------------------- 100

          ↑

        50%

```

  

`Lerp()` thường được dùng cho:

  

- Di chuyển mượt.

- Thay đổi giá trị.

- Chuyển màu.

- Chuyển vị trí hoặc trạng thái từ A → B.

  

---

  

# 5. `Transform`

  

`Transform` là Component đặc biệt có trên mọi `GameObject`.

  

Nó lưu trữ các thông tin cơ bản về vị trí và hình dạng của object trong không gian:

  

- **Position** — vị trí.

- **Rotation** — góc xoay.

- **Scale** — tỉ lệ kích thước.

  

Ví dụ:

  

```csharp

transform.position = new Vector3(0, 2, 0);

transform.rotation = Quaternion.identity;

transform.localScale = Vector3.one;

```

  

Có thể xem `Transform` như thông tin mô tả:

  

> **Object đang ở đâu, xoay như thế nào và có kích thước bao nhiêu.**

  

---

  

# 6. Gizmos

  

**Gizmos** là công cụ hỗ trợ trực quan trong **Scene View**.

  

Nó cho phép vẽ các hình hỗ trợ như:

  

- Đường thẳng.

- Hình cầu.

- Hình hộp.

- Vùng kiểm tra.

- Các điểm hoặc hướng.

  

Gizmos thường dùng để:

  

- Căn chỉnh object.

- Debug.

- Hiển thị vùng hoạt động của hệ thống.

- Kiểm tra vị trí và phạm vi của gameplay.

  

Ví dụ:

  

```csharp

private void OnDrawGizmos()

{

    Gizmos.DrawWireSphere(transform.position, 2f);

}

```

  

> Gizmos chủ yếu phục vụ việc **quan sát và debug trong Editor**, không phải để hiển thị trực tiếp cho người chơi trong Game View.

  

---

  

# 7. C# Attributes trong Unity

  

**Attribute** là các thẻ đặt trước field, class hoặc thành phần code để cung cấp metadata hoặc thay đổi cách Unity xử lý/hiển thị chúng trong Editor.

  

Ví dụ:

  

```csharp

[SerializeField]

private int health;

```

  

Một số Attribute thường gặp:

  

- `[SerializeField]`

- `[Range]`

- `[ExecuteInEditMode]`

  

---

  

## 7.1. `[SerializeField]`

  

`[SerializeField]` cho phép một field `private` được Unity serialize và hiển thị trong **Inspector**.

  

Ví dụ:

  

```csharp

[SerializeField]

private int health = 100;

```

  

Field vẫn là `private` đối với code bên ngoài, nhưng có thể chỉnh giá trị trong Inspector.

  

### Tại sao hữu ích?

  

Thay vì:

  

```csharp

public int health;

```

  

có thể dùng:

  

```csharp

[SerializeField]

private int health;

```

  

Điều này giúp kiểm soát việc truy cập dữ liệu tốt hơn trong code.

  

---

  

## 7.2. `[Range]`

  

`[Range]` biến field số thành một **slider** trong Inspector và giới hạn giá trị nhập vào theo khoảng chỉ định.

  

Ví dụ:

  

```csharp

[Range(0, 100)]

public int health;

```

  

Inspector sẽ cho phép điều chỉnh `health` trong khoảng:

  

```text

0 → 100

```

  

---

  

## 7.3. `[ExecuteInEditMode]`

  

`[ExecuteInEditMode]` cho phép một số callback của `MonoBehaviour` được thực thi trong **Edit Mode**, tức là khi chưa nhấn Play.

  

Ví dụ:

  

```csharp

[ExecuteInEditMode]

public class Example : MonoBehaviour

{

}

```

  

Điều này hữu ích khi muốn một script hỗ trợ việc:

  

- Căn chỉnh object.

- Tạo công cụ trong Editor.

- Hiển thị hoặc cập nhật dữ liệu khi đang chỉnh Scene.

  

> **Lưu ý:** Đây là Attribute cũ trong hệ thống của Unity. Với các project Unity hiện đại, có thể gặp `ExecuteAlways` trong những trường hợp muốn script chạy cả khi đang chỉnh sửa và khi đang Play.

  

---

  

# 8. Các cấu trúc điều khiển cơ bản trong C#

  

Phần này là những cấu trúc dùng để **điều khiển luồng thực thi của chương trình**.

  

---

  

## 8.1. `if / else`

  

Dùng để rẽ nhánh chương trình dựa trên điều kiện `true` hoặc `false`.

  

Ví dụ:

  

```csharp

if (health > 0)

{

    // Nhân vật còn sống

}

else

{

    // Nhân vật đã hết máu

}

```

  

Có thể mở rộng thành:

  

```csharp

if (health > 50)

{

    // Máu cao

}

else if (health > 0)

{

    // Máu thấp

}

else

{

    // Hết máu

}

```

  

---

  

## 8.2. `switch / case`

  

Dùng khi cần xử lý nhiều trường hợp dựa trên **một giá trị cụ thể**.

  

Ví dụ:

  

```csharp

switch (state)

{

    case 0:

        // Idle

        break;

  

    case 1:

        // Run

        break;

  

    case 2:

        // Attack

        break;

}

```

  

Thường hữu ích khi xử lý:

  

- State.

- Loại vũ khí.

- Loại hành động.

- Menu hoặc lựa chọn có nhiều trạng thái.

  

---

  

## 8.3. `for`

  

`for` dùng khi biết hoặc có thể xác định cách lặp thông qua một biến đếm.

  

Cấu trúc:

  

```csharp

for (khởi_tạo; điều_kiện; cập_nhật)

{

    // Code

}

```

  

Ví dụ:

  

```csharp

for (int i = 0; i < 10; i++)

{

    Debug.Log(i);

}

```

  

Kết quả:

  

```text

0

1

2

...

9

```

  

---

  

## 8.4. `foreach`

  

`foreach` dùng để duyệt lần lượt qua các phần tử trong một tập hợp như:

  

- Array.

- `List<T>`.

- Một số collection khác.

  

Ví dụ:

  

```csharp

foreach (GameObject enemy in enemies)

{

    enemy.SetActive(true);

}

```

  

Ưu điểm là không cần tự quản lý index.

  

So sánh:

  

```csharp

for (int i = 0; i < enemies.Count; i++)

{

    enemies[i].SetActive(true);

}

```

  

với:

  

```csharp

foreach (GameObject enemy in enemies)

{

    enemy.SetActive(true);

}

```

  

`foreach` thường dễ đọc hơn khi chỉ cần duyệt qua từng phần tử.

  

---

  

## 8.5. `while`

  

`while` lặp lại một khối code **chừng nào điều kiện còn đúng**.

  

Ví dụ:

  

```csharp

while (health > 0)

{

    // Thực hiện logic

}

```

  

Cấu trúc:

  

```csharp

while (điều_kiện)

{

    // Code

}

```

  

> **Cẩn thận:** Nếu điều kiện không bao giờ trở thành `false`, vòng `while` có thể trở thành **vòng lặp vô hạn**.

  

---

  

# 9. Bảng ghi nhớ nhanh

  

| Khái niệm | Dùng để làm gì? |

|---|---|

| `MonoBehaviour` | Cho script hoạt động như Component của Unity |

| Class C# thuần | Xử lý dữ liệu/logic độc lập |

| `Awake()` | Khởi tạo khi instance được nạp |

| `OnEnable()` | Chạy khi Component/GameObject được bật |

| `Start()` | Khởi tạo trước khi gameplay của script bắt đầu |

| `FixedUpdate()` | Logic theo nhịp cố định, đặc biệt là vật lý |

| `Update()` | Logic mỗi frame, thường dùng cho Input/gameplay |

| `LateUpdate()` | Logic sau `Update()`, thường gặp ở Camera |

| `OnDisable()` | Chạy khi Component/GameObject bị tắt |

| `OnDestroy()` | Chạy khi object bị hủy |

| `SpriteRenderer` | Hiển thị Sprite |

| Sorting Layer | Xác định nhóm/lớp vẽ 2D |

| Order in Layer | Xác định thứ tự vẽ trong cùng Sorting Layer |

| `Vector2` | Vector 2 chiều |

| `Vector3` | Vector 3 chiều |

| `Time.deltaTime` | Thời gian giữa các frame |

| `Mathf.Clamp()` | Giới hạn giá trị |

| `Mathf.Lerp()` | Nội suy giữa hai giá trị |

| `Transform` | Position, Rotation, Scale của GameObject |

| Gizmos | Vẽ công cụ hỗ trợ trong Scene View |

| `[SerializeField]` | Hiển thị field private trong Inspector |

| `[Range]` | Tạo slider và giới hạn giá trị |

| `if / else` | Rẽ nhánh theo điều kiện |

| `switch / case` | Rẽ nhánh theo nhiều trường hợp |

| `for` | Lặp bằng biến đếm |

| `foreach` | Duyệt từng phần tử trong collection |

| `while` | Lặp khi điều kiện còn đúng |

  

---

  

## 10. Cách tư duy khi học các khái niệm này

  

Không nên học thuộc từng hàm một cách rời rạc. Hãy liên kết chúng thành một chuỗi:

  

```text

GameObject

    ↓

Component

    ↓

MonoBehaviour

    ↓

Lifecycle

    ├── Awake()

    ├── OnEnable()

    ├── Start()

    ├── Update()

    ├── FixedUpdate()

    ├── LateUpdate()

    ├── OnDisable()

    └── OnDestroy()

```

  

Trong quá trình viết gameplay:

  

```text

C# logic

   ↓

Điều kiện / vòng lặp

   ↓

Tính toán với Vector / Mathf

   ↓

Thay đổi Transform / Component

   ↓

Unity cập nhật GameObject

   ↓

Game hiển thị kết quả

```

  

Đây mới là mối liên hệ quan trọng cần hiểu: **C# cung cấp logic, còn Unity cung cấp môi trường và các Component để logic đó tác động lên game.**