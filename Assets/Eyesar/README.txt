---Remoting---

客户端：
0. 新建项目用来定义协议
1. 为 EyesarSharedObject 添加抽象/非抽象属性(即协议字段), 或抽象/非抽象方法(交互接口)
2. 选中Remoting文件夹下的所有文件，右键选择'Compile Scripts'，导出dll程序集
3. 在正式项目中导入上一步中导出的程序集(而非引用Remoting下的源文件)和Remoting下的System.Runtime.Remoting.dll文件
4. 实现 EyesarClient 类, 实例化对象，调用类成员 EyesarClient.SharedObject 即可

服务端:
0. 引用客户端提供的.net程序集(Eyesar.dll和System.Runtime.Remoting.dll)
1. 实现 EyesarSharedObject 类 和 EyesarServer 类
2. 在启动幻眼应用进程前, 实例化上一步中实现的类
3. 启动幻眼进程；在幻眼运行期间，不要释放上一部中实例化的对象

交互:
客户端通过 EyesarClient.SharedObject 来获取/修改数据
服务端通过 EyesarServer.SharedObject 来获取/修改数据
无论哪一方修改数据，另一方取得的都是最新的结果
*在服务端施放资源之前，SharedObject永远为单例，即使幻眼客户端重启

---Commandline---
服务端传入格式: xxx.exe [-arg value]...
如: sample.exe -arg1 "This is string" -arg2 18 -p false -float 0.866
客户端调用CommandParser获取解析后的参数