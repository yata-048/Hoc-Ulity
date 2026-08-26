# Class C# thuần vs. Class MonoBehaviour

## Class C# thuần: Là class lập trình tiêu chuẩn của C#, không kế thừa từ Unity. Dùng để chứa cấu trúc dữ liệu, thuật toán nội bộ; phải khởi tạo thủ công bằng từ khóa new và không thể gắn trực tiếp vào GameObject trong Scene.

## MonoBehaviour: Là class cơ sở (base class) của Unity. Mọi script muốn gắn lên GameObject làm Component, muốn hiển thị trên Inspector và chạy theo vòng đời của game engine đều phải kế thừa từ class này. Tuyệt đối không khởi tạo bằng new.

# Vòng đời GameObject (Execution Order)

## Awake: Hàm được gọi ngay khi script instance được nạp vào bộ nhớ (kể cả khi script đang tắt tick Enabled). Dùng để khởi tạo biến nội bộ và lấy component (GetComponent).

## OnEnable: Hàm tự động kích hoạt mỗi khi GameObject hoặc Component chuyển từ trạng thái tắt sang bật (Active/Enabled).

## Start: Hàm chạy 1 lần duy nhất trước frame Update đầu tiên, chỉ khi script đang bật. Dùng để liên kết dữ liệu với các GameObject khác.

## FixedUpdate: Hàm lặp theo chu kỳ thời gian cố định độc lập với tốc độ khung hình (mặc định 0.02s/lần). Chuyên dùng để tính toán vật lý (Rigidbody, lực, vận tốc).

## Update: Hàm lặp lại sau mỗi khung hình (frame). Tần suất chạy phụ thuộc vào FPS của máy, dùng để bắt Input từ người chơi và cập nhật logic game thông thường.

## LateUpdate: Hàm chạy sau khi toàn bộ hàm Update của mọi object trong scene đã hoàn thành. Dùng phổ biến nhất để điều khiển Camera bám theo nhân vật.

## OnDisable: Hàm tự động kích hoạt mỗi khi GameObject hoặc Component bị tắt đi (Deactive/Disabled).

## OnDestroy: Hàm kích hoạt khi GameObject hoặc Component bị xóa bỏ hoàn toàn khỏi Scene (Destroy) hoặc khi chuyển Scene.


# Thao tác cơ bản trong Unity

## Import assets: đưa các tài nguyên bên ngoài(có thể là audio, sfx, vfx, ảnh,...) vào trong unity.

## Sprite renderer: component chịu trách nghiệm hiển thị hình ảnh 2d lên màn hình game

## sorting layer: phân layer vẽ 2D, trên đè dưới

## order in layer: chỉ số thứ tự hiển thị của các sprite nằm trong cùng 1 sorting layer, số lớn hơn đè số nhỏ hơn

## vector(2/3): cấu trúc dữ liệu biểu diễn hướng, vị trí hoặc tỉ lệ thu phóng trong không gian 2/3 D

## Time.deltaTime: Khoảng thời gian (tính bằng giây) để engine hoàn thành khung hình trước đó. Dùng làm hệ số nhân cho chuyển động để đảm bảo tốc độ vật thể đồng nhất trên mọi mức FPS.

# các hàm toán học:

## Mathf: thư viện toán học thông dụng:

### Mathf.Clamp: Hàm giới hạn một giá trị chỉ được phép dao động trong khoảng từ giá trị nhỏ nhất (min) đến lớn nhất (max).

### Mathf.Lerp: Hàm nội suy tuyến tính, tính toán một giá trị nằm giữa hai điểm mốc dựa theo tỉ lệ phần trăm cho trước.

## Transform: Component bắt buộc có trên mọi GameObject, lưu trữ dữ liệu về Vị trí (Position), Góc xoay (Rotation) và Tỉ lệ kích thước (Scale).

## Gizmos: Công cụ hỗ trợ trực quan hóa (vẽ đường thẳng, khối cầu, hình hộp) trong cửa sổ Scene nhằm phục vụ việc căn chỉnh, debug mà không hiển thị ra màn hình game của người chơi.

## C# Attributes: Các thẻ đánh dấu đặt trước biến hoặc class để thay đổi hành vi hoặc cách hiển thị của chúng trong Unity Editor.

### [SerializeField]: Ép một trường dữ liệu private hiển thị lên bảng Inspector để tinh chỉnh mà không cần đổi thành public.

### [Range]: Giới hạn giá trị của một biến số và biến ô nhập liệu trên Inspector thành một thanh trượt (slider).

### [ExecuteInEditMode]: Cho phép script thực thi các hàm vòng đời ngay trong chế độ chỉnh sửa (Edit Mode) mà không cần bấm nút Play.

# vòng for các thứ:

## if / else: Cấu trúc rẽ nhánh điều kiện, thực thi khối lệnh tương ứng khi biểu thức logic trả về true hoặc false

## switch - case: Cấu trúc rẽ nhánh theo nhiều trường hợp cụ thể của một biến đơn

## for: Vòng lặp với số lần xác định, quản lý thông qua một biến đếm và điều kiện dừng.

## foreach: Vòng lặp duyệt qua lần lượt từng phần tử trong một tập hợp (mảng, danh sách) mà không cần dùng chỉ số index

## while: Vòng lặp thực thi liên tục khối lệnh chừng nào điều kiện logic vẫn còn đúng