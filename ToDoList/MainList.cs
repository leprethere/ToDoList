using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoList
{
    class MainList
    {
        static void Main(string[] args)
        {
            ProcessesOfToDoList processesOfToDoList = new ProcessesOfToDoList();
            string filePath = "D:\\ToDoList.txt";
            string[] operations = { "Create", "Edit", "Task", "Delete", "Read", "Finish", "HIGH", "NORMAL", "LOW" };
            
            Console.WriteLine("Enter your command, please:");
            string command = Console.ReadLine();

                if (operations.Contains(command))
                {

                    if (command == "Create")
                    {
                        Console.WriteLine("Your new task is:");
                        processesOfToDoList.runToDoList(filePath);
                    }
                    else if (command == "Read")
                    {
                        Console.WriteLine(processesOfToDoList.readAllToDoList(filePath));
                    }
                    else if (command == "Task")
                    {
                        processesOfToDoList.readTaskFromToDoList(filePath);
                    }
                    else if (command == "Edit")
                    {
                        processesOfToDoList.editTaskFromToDoList(filePath);
                    }
                    else if (command == "Delete")
                    {
                        processesOfToDoList.deleteTaskToDoList(filePath);
                    }
                    else if (command == "HIGH" || command == "NORMAL" || command == "LOW")
                    {
                        Console.WriteLine(processesOfToDoList.priority(command, filePath));
                    }
                    else
                    {
                        Console.WriteLine("Wrong operation! Please, try again");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong operation! Please, try again");
                }
        }   
    }
    public class WorkingWithToDoList
    {
        public void NumerateToDoList(string[] filePathMassive)
        {
            Console.WriteLine("Your ToDoList:");

            for (int i = 0; i < filePathMassive.Length; i++)
            {
                filePathMassive[i] = $"{i}. {filePathMassive[i]}";
            }
            Console.WriteLine(string.Join("\n", filePathMassive));
        }

        public string YourCommand(string command)
        {
            return $"Which number of task do you want to {command}?";
        }

    }

    public class ProcessesOfToDoList : WorkingWithToDoList
    {
        public void runToDoList(string filePath)
        {
            string newTask = Console.ReadLine();
            using (StreamWriter writerTask = new StreamWriter(filePath, append: true))
            {
                writerTask.AutoFlush = true;
                writerTask.WriteLine(newTask);
            }
        }

        public string priority(string command, string filePath)
        {
            string toDoList = File.ReadAllText(filePath);
            if (toDoList.Contains($"{command}:")) 
            {
                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                List<string> priority = new List<string>();
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains($"{command}:"))
                    {
                        priority.Add(line);
                    }
                }
                return String.Join("\n", priority);
            } 
            else
            {
                return $"No {command} priority task in ToDoList";
            }
        }

        public string readAllToDoList(string filePath)
        {           
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public void readTaskFromToDoList(string filePath)
        {
            string[] toDoList = File.ReadAllLines(filePath);

            try
            {
                if (toDoList.Length == 0)
                {
                    Console.WriteLine("Your ToDoList is clear!");
                }
                else 
                {
                    Console.WriteLine(YourCommand("read"));
                    int task;
                    string numbTask = Console.ReadLine();
                    int.TryParse(numbTask, out task);

                    if (numbTask != null)
                    {
                        if (int.Parse(numbTask) >= 0 && int.Parse(numbTask) <= toDoList.Length)
                        {
                            Console.WriteLine(toDoList[task]);
                        }
                        else
                        {
                            Console.WriteLine("Task doesn't exist!");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wrong command, please write number!");
            }
        }
        public void editTaskFromToDoList(string filePath)
        {
            NumerateToDoList(File.ReadAllLines(filePath));
            string[] toDoList = File.ReadAllLines(filePath);

            Console.WriteLine(YourCommand("edit"));

            int task;
            string numbTask = Console.ReadLine();
            int.TryParse(numbTask, out task);

            if (numbTask != null)
            {
                try 
                {
                    if (int.Parse(numbTask) >= 0 && int.Parse(numbTask) <= toDoList.Length)
                    {
                        Console.WriteLine($"Write your new task instead '{toDoList[task]}':");
                        toDoList[task] = Console.ReadLine();
                        File.WriteAllLines(filePath, toDoList);
                        Console.WriteLine("Now you task is:\n"+readAllToDoList(filePath));
                    }
                    else
                    {
                        Console.WriteLine("ToDoList doesn't contain this task");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Wrong command '{numbTask}', please write number!");
                }
            }
            else if (numbTask != null || numbTask != "")
            {
                Console.WriteLine("Wrong command!");
            }
        }
        public void deleteTaskToDoList(string filePath)
        {
            NumerateToDoList(File.ReadAllLines(filePath));
            string[] toDoList = File.ReadAllLines(filePath);

            Console.WriteLine(YourCommand("delete"));

            int task;
            string numbTask = Console.ReadLine();
            int.TryParse(numbTask, out task);

            try
            {
                if (toDoList.Length == 0)
                {
                    Console.WriteLine("Your ToDoList is clear!");
                }
                else if (numbTask != null)
                {
                    if (int.Parse(numbTask) >= 0 && int.Parse(numbTask) <= toDoList.Length)
                    {
                        var listLines = File.ReadAllLines(filePath).ToList();
                        listLines.RemoveAt(task);
                        File.WriteAllLines(filePath, listLines);
                        Console.WriteLine($"Task '{task}' was deleted");
                    }
                    else
                    {
                        Console.WriteLine("ToDoList doesn't contain this task");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wrong command '{numbTask}', please write number!");
            }
        }
    }
}
