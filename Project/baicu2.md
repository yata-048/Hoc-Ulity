1.Làm quen với Unity
Giao diện chính:
    + Hierarchy: Danh sách mọi object (GameObject) đang có mặt trong scene hiện tại.
    + Scene View & Game View: Scene là nơi kéo thả, sắp xếp đồ đạc; Game là góc nhìn thực tế của Camera khi người chơi chơi.
    + Inspector: Bảng thuộc tính, chứa các Component gắn trên GameObject được chọn (Transform, Collider, Rigidbody, Script...).
    + Project: Cây thư mục chứa toàn bộ tài nguyên (asset, script, prefab, sprite, sound).
    + Console: Nơi in log (Debug.Log()), bắn warning và quăng lỗi crash code.

Các kiểu dữ liệu: giống các ngôn ngữ khác nhưng có một vài lưu ý:
    khai báo mảng: int[] tenmang= new int[số lượng];
    float a=3.4f; (phải có chữ f đằng sau)
    map trong C#= Dictionary<Key, Value> tenbien = new Dictionary<Key, Value>();


Một số khái niệm:

    -GameObject: Đại diện cho một thực thể/vật thể tồn tại trong Scene (nhân vật, quái, cái cây, camera, UI...). Mọi thứ nhìn thấy hoặc hoạt động trong scene đều gắn với một GameObject.

    -Component: Class cha của tất cả các thành phần gắn vào GameObject. Một GameObject rỗng muốn có hình dáng, di chuyển hay có máu thì phải gắn các Component tương ứng vào.
        +Transform: Component bắt buộc phải có trên mọi GameObject. Dùng để lưu trữ và điều khiển vị trí (Position), góc xoay (Rotation), và tỉ lệ phóng to/thu nhỏ (Scale).
        +Rigidbody: Component vật lý giúp đối tượng chịu tác động của trọng lực, lực đẩy (AddForce), quán tính và va chạm.

    -Vector3 & Vector2:Struct biểu diễn tọa độ không gian hoặc vector hướng/vận tốc.

    -[SerializeField]: Cho phép hiển thị các biến private lên trên inspector và chỉnh ngay tại editor


Git: là hệ thống quản lý phiên bản phân tán (Distributed Version Control System)
    +Dùng để theo dõi toàn bộ lịch sử thay đổi của source code trong quá trình làm việc.
    +Cho phép quay lại các phiên bản code cũ nếu lỡ tay làm lỗi, tạo nhánh (branch) để thử nghiệm tính năng mới mà không sợ phá nát bản code chính.
    +Hỗ trợ làm việc nhóm mượt mà, kết hợp với các nền tảng lưu trữ từ xa như GitHub, GitLab hay Bitbucket.
    +các app git: source git, fork,...

markdown: cái đang viết.

Class C#: giống struct nhưng khác ở chỗ Class là tham chiếu còn struct là tham trị.
    +tham chiếu:Biến chỉ giữ địa chỉ vùng nhớ (con trỏ) dẫn đến nơi chứa dữ liệu thật. Khi gán b = a, cả hai biến cùng trỏ chung vào một đối tượng duy nhất.
    +tham trị:Biến nắm giữ chính xác giá trị của dữ liệu đó. Khi gán b = a, máy nhân bản (clone) một giá trị y hệt sang ô nhớ mới.

Constructor: Là hàm khởi tạo, có tác dụng tạo ra đối tượng mới
    +tên hàm bắt buộc giống class mà nó nằm trong
    +ko có kiểu trả về
    +một class có thể có nhiểu hàm khởi tạo
    ví dụ:
        class Player
        {
            int hp;
            int attack;
            public Player(int hp,int attack)
            {
                this.hp=hp;
                this.attack=attack;
            }
        }
        class Prg
        {
            void hello()
            {
                Player input = new Player(20 ,2);
            }
        }

Các từ khóa quan trọng trong C#:
    +var = auto trong c++;

    +const giống c++;
    +readonly khá giống const nhưng mình sẽ tự gắn vào sau
    phân biệt const và readonly
    public class Gun
    {
        public const int MAX_BULLETS = 30;     // Luôn cố định, gọi Gun.MAX_BULLETS
        public readonly float damage;          // Mỗi khẩu súng có damage khác nhau lúc spawn

        public Gun(float initDamage)
        {
            damage = initDamage; // Gán trong constructor hợp lệ
        }
    }

    +ref: dùng khi muốn truyền biến vào hàm, sẽ đọc dữ liệu được đưa vào để sửa
    ví dụ:
    void BuffSpeed(ref float speed)
    {
        speed += 5f; 
    }
    float playerSpeed = 10f; 
    BuffSpeed(ref playerSpeed); output =15f
    
    +out:giống ref nhưng chỉ nhận kết quả trong hàm trả về
    ví dụ:
    void GiveMeWeapon(out string weapon)
    {
        weapon = "Kiếm Lửa"; 
    }
    string myWeapon; //chưa có gì
    GiveMeWeapon(out myWeapon);
    Debug.Log(myWeapon); //lòi ra kiếm lửa
