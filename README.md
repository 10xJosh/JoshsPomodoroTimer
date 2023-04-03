# JoshsPomodoroTimer
A simple pomodoro timer that can be used to help you keep track and stay focused on your work.

![pomodorotimer](https://user-images.githubusercontent.com/55113224/229462544-f7df4b6e-e893-4f39-aba7-e11d46930a10.gif)


To install, simply copy the repo or download the .exe thats found in the repo! Feel free to leave any comments or concerns!

<h2>Learning Takeaways (For those that are curious):</h2> 

 - UI elements such as labels cant be updated if a method is changing it
on a seperate thread. Use labelname.Dispatcher.BeginInvoke(new Action(() => {//code goes here};); to get around this.

- Becareful Instantiating a form object, they can lead to memory leaks
if they are not disposed of properly.

- Use cancellation tokens to cancel operations on different threads.
https://www.youtube.com/watch?v=TKc5A3exKBQ

- Using design patterns when making a program makes life a lot easier and simplier. I will be looking more into this topic.

- To access a directory within a project solution use AppDomain.CurrentDomain.BaseDirectory + "()"; so that when the code is published, it will always reference the correct files no matter where program is installed.


