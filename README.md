# JoshsPomodoroTimer
WORK IN PROGRESS

(For those that are curious)Key Takeaways:
- UI elements such as labels cant be updated if a method is changing it
on a seperate thread. Use labelname.Dispatcher.BeginInvoke(new Action(() => {//code goes here};); to get around this.

- Becareful Instantiating a form object, they can lead to memory leaks
if they are not disposed of properly.

- Use cancellation tokens to cancel operations on different threads.
https://www.youtube.com/watch?v=TKc5A3exKBQ

- Using design patterns when making a program makes life a lot easier and simplier. I will be looking more into this topic.

- To access a directory within a project solution use AppDomain.CurrentDomain.BaseDirectory + "()"; so that when the code is published, it will always reference the correct files no matter where program is installed.


