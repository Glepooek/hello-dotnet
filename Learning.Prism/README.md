## Prism
该Demo基于Prism7.2开发！
Prism的设计思想是模块化编程！

1、Dialog
1)AlertDialog
2)ConfirmDialog
3)NotificationDialog

2、Log4net
可根据设定的level等级，记录对应级别的日志
日志等级：ALL、DEBUG、INFO、WARN、ERROR、FATAL、OFF

3、Dispatcher的主要作用是什么呢？
     Dispatcher的作用是管理（或调度）线程工作项队列。

     WPF应用程序，实际上都是一个进程，一个进程可以包含多个线程，其中有一个是主线程（UI线程），其余的是子线程。
     主线程负责接收输入、处理事件、绘制屏幕等工作。为了使主线程及时响应，防止假死，在开发过程中对一些耗时的操作、
     消耗资源比较多的操作，都会去创建一个或多个子线程去完成操作，比如大数据量的循环操作、后台下载。这样一来，
     由于UI界面是主线程创建的，所以子线程不能直接更新由主线程维护的UI界面。

参考文章：
Dispatcher：
https://www.cnblogs.com/chillsrc/p/4482691.html