//说明

项目中对数据库的使用分为两种情况：
1、通过定义数据库操作接口，工厂生产IDBAdaptor对象使用**Helper类，实现通用操作
2、使用Dapper ORM框架操作

App.config使用说明：
App.config配置文件要么使用xml，要么将配置写入调用此类库的config文件中

警告说明：
1、【已更换微软自带的System.Data.Oracle】oracle的连接驱动为Oracle.DataAccess.dll的64为版本，如果使用Oracle时需要将编译架构为x64平台


数据库连接说明：
1、sqlite数据库：由于使用的System.Data.SQLite.dll依赖于SQLite.Interop.dll，如果调用的工程报SQLite.Interop.dll无法找到的错误，则将x64和x86文件夹拷贝到lib同级的目录下