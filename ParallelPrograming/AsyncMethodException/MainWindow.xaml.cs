using System;
using System.Threading.Tasks;
using System.Windows;
/**
 * 1、使用异步方法时，不要返回void，异常捕获不到，应该返回Task或Task<T>;
 * 2、不要Task和异步方法一起使用，如：
 * await Task.Run(async ()=>
 * {
 *      await Method();
 * });
 * 
 * ****/

namespace AsyncMethodException
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //TestMethod0();
            //Application dispatcher catches an unhandled exception. 

            //TestMethod1();
            // Application dispatcher catches an unhandled exception. 

            //await Task.Run(() =>
            //{
            //    TestMethod1();
            //});
            // Current Domain catches an unhandled exception. 


            //TestMethod2();
            // Task Scheduler catches an unobserved task exception

            //await TestMethod2();
            // Application dispatcher catches an unhandled exception. 

            try
            {
                //TestMethod1();
                //uncatch

                // Task.Run(() =>
                //{
                //    TestMethod1();
                //});
                //uncatch

                //await Task.Run(() =>
                //{
                //    TestMethod1();
                //});
                //uncatch

                //TestMethod2();
                //uncatch

                //await TestMethod2();
                //catch
            }
            catch (Exception ex)
            {

            }

            // 避免async () =>{}无法捕获异常的处理方式
            OnRun(async () =>
            {
                await Task.Delay(1000);
                throw new Exception("error!");
            });

        }

        private void TestMethod0()
        {
            throw new Exception("TestMethod0 ex");
        }

        private async void TestMethod1()
        {
            throw new Exception("TestMethod1 ex");
        }

        private async Task TestMethod2()
        {
            throw new Exception("TestMethod2 ex");
        }



        private async void OnRun(Func<Task?> action)
        {
            try
            {
                await action();
            }
            catch (Exception e_)
            {
                Console.WriteLine(e_.Message);
            }
        }

        private void OnRun(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e_)
            {
                Console.WriteLine(e_.Message);
            }
        }
    }
}
