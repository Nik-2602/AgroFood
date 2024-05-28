# AgroFood
1. Kết nối với cơ sở dữ liệu
- Mở thư mục dữ liệu Copy 2 file vào thư mục data của SQL Server
- PATH: C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA
- Mở SQL Server Attach db
2- Mở project với Visual Studio
- vào file Web.config

3 Config lại kết nối từ Visual Studio đến Sql Server
- Tại thể <connectionString> </connectionString>
thay đổi tên server, tài khoản, mật khẩu kết nối đến sql server
tên server: data source
tài khoản : user id
mật khẩu : password

để tránh gặp lỗi thì có connecstring nào thì hãy thay đổi thành database của bạn
