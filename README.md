# Project-MVC6-angular2
Finall

Project được build trên nền tảng .NET CORE 1.0.0 preview 2
và angular 2 bẩn 2.0.0.rc4

Yêu cầu cài đặt:
.NET CORE preview 2

vơi Angular 2:
cài node js 5.x trở lên
và npm 3.x trở trở lên

Để build code thuận tiện thì nên cài đặt visual studio 2015 update 3

hoặc sau khi cài đặt các nên tẳng kể trên thì cài đặt visual code hoặcSublime text 3, 


***để bật cửa sổ cmd cho file =chuột phải+shift
sau khi mở cở sổ cmd cho ứng dụng
buid code: với visual code hay subline text
Bước 1: Restore thư viện: dotnet restore
Bước 1.1: build code sever
+ build code first trước tiên gõ vào cửa sổ dòng lệnh: "dotnet ef Migrations add Initials" **Lưu ý thông báo sinh ra khi build
        ----> sau đó gõ tiếp: donet ef database update **Lưu ý đường dẫn data base trong appsetting.json
  **Lưu ý, khi thành công thì sẽ thông báo thành công
Bước 2: Restore node_modules
Bước 2.1: gõ vào cử sổ cmd: npm install
Bước 2.2: gõ tiếp: cpm npm install typings


Bước 3: Build project:
Bước 3.1 gõ : dotnet build

**thành công khi trình duyệt tự động bật lên




