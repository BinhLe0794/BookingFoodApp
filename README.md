# Booking App

## Clone ứng dụng đặt đồ ăn gồm các chức năng sau: 
    
    + Loading danh sách đồ ăn: tất cả, theo danh mục
        + Hiển thị món ăn chi tiết: hình ảnh, giá, miêu tả ... 
    + Chức năng Authentication: Đăng nhập, đăng xuất và các thông tin liên quan
    + Chức năng giỏ hàng: lưu trữ local và đặt hàng

    #### Thư viện sử dụng
          pod "Kingfisher"
          pod 'ProgressHUD'
          pod "IQKeyboardManagerSwift"        

### Backend: 

    #### API - ASP.Net Core
    - Authentication: sử dụng JWT để bảo vệ API
        + Đăng nhập
        + Đăng Ký
        + Refresh Token
    - Dish: loading menu món ăn
        + Lấy tất cả món ăn
        + Lấy món ăn theo danh mục riêng
        + Lấy thông tin của món ăn
    - Order: giỏ hàng
        + Đặt đơn hàng
        + Lấy các đơn hàng theo người dùng
        + Lấy chi tiết các món ăn theo từng đơn hàng
        
    #### Trang quản trị viên
    - Authentication: CRUD với các tài khoản 
    - Dish: CRUD với món ăn
    - Order: Xem được tất cả đơn hàng, thông tin đơn hàng 
    - Trang quản trị các API của hệ thống
    
Dự án được publish API lên hosting https://phuocthaosolar.com/.
