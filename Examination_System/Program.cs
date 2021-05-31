using System;
using System.Collections.Generic;

namespace Examination_System
{
	class Program
	{
		class Question
		{
			#region props
			public string title { get; set; }
			public int number { get; set; }
			public string body { get; set; }
			public int mark { get; set; }
			public int right_answer { get; set; }
			#endregion
			#region ctors
			public Question(string title,int number,string body,int mark, int right_answer)
			{
				this.title = title;
				this.number = number;
				this.body = body;
				this.mark = mark;
				this.right_answer = right_answer;
			}
			#endregion
			#region methods
			public override string ToString()
			{
				return $"{title}:\n{number}-{body}?\t({mark} marks)";
			}
			#endregion
		}
		class Answer
		{
			#region props
			public int number { get; set; }
			public string answer { get; set; }
			public bool status { get; set; }
			
			#endregion
			#region ctors
			public Answer(int number,string answer,bool status) 
			{
				this.number = number;
				this.answer = answer;
				this.status = status;
				
			}
			#endregion
			#region methods
			public override string ToString()
			{
				return $"{number}-{answer}";
			}
			#endregion
		}
		abstract class Exam
		{
			#region props
			public int time { get; set; }
			public int NumberOfQuestion { get; set; }
			
			public Dictionary<Question, List<Answer>> fullexam { get; set; }
			#endregion

			#region ctor
			public Exam()
			{
				this.time = 0;
				this.NumberOfQuestion = 0;
				this.fullexam = null;
				
			}
			public Exam(int time,int NumberOfQuestion,Dictionary<Question, List<Answer>>fullexam )
			{
				this.time = time;
				this.NumberOfQuestion = NumberOfQuestion;
				this.fullexam = fullexam;
				
			}
			#endregion
			#region methods
			public abstract void Show();
			#endregion
		}
		class Practice_Exam : Exam, IComparable, ICloneable
		{
			public Practice_Exam(int time, int NumberOfQuestion, Dictionary<Question, List<Answer>> fullexam)
			{
				this.time = time;
				this.NumberOfQuestion = NumberOfQuestion;
				this.fullexam = fullexam;
				
			}

			public object Clone()
			{
				return new Practice_Exam(time, NumberOfQuestion, fullexam);
			}

			public int CompareTo(object obj)
			{
				Practice_Exam pe = obj as Practice_Exam;
				if (time.CompareTo(pe.time)==0)
				{
					return NumberOfQuestion.CompareTo(pe.NumberOfQuestion);
				}
				else
				{
					return time.CompareTo(pe.time);
				}
			}

			public override void Show()
			{
				Console.WriteLine($"Number of question: {NumberOfQuestion}\t\t\tTime: {time} Minutes");
				Console.WriteLine("-----------------------------------------------------------------------");
				foreach (KeyValuePair < Question, List < Answer >> item in fullexam)
				{
					Console.WriteLine(item.Key.ToString());
					foreach ( Answer item2 in item.Value )
					{
						Console.WriteLine(item2.ToString());
					}
					Console.WriteLine($"\n>>>Answer: {item.Key.right_answer}\n");
				Console.WriteLine("-----------------------------------------------------------------------");

				}
			}
		}
		class Final_Exam : Exam ,IComparable,ICloneable
		{
			int[] ans;
			int answers;
			int Marks;

			public Final_Exam(int time, int NumberOfQuestion, Dictionary<Question, List<Answer>> fullexam)
			{
				this.time = time;
				this.NumberOfQuestion = NumberOfQuestion;
				this.fullexam = fullexam;
				ans = new int[NumberOfQuestion];

			}

			public object Clone()
			{
				return new Final_Exam(time,NumberOfQuestion,fullexam);
			}

			public int CompareTo(object obj)
			{
				Final_Exam fe = obj as Final_Exam;
				if (time.CompareTo(fe.time) == 0)
				{
					return NumberOfQuestion.CompareTo(fe.NumberOfQuestion);
				}
				else
				{
					return time.CompareTo(fe.time);
				}
			}
		

			public override void Show()
			{
				Console.WriteLine($"Number of question: {NumberOfQuestion}\t\t\tTime: {time}");
				Console.WriteLine("-----------------------------------------------------------------------");

				foreach (KeyValuePair<Question, List<Answer>> item in fullexam)
				{
					Console.WriteLine(item.Key.ToString());
					foreach (Answer item2 in item.Value)
					{
						Console.WriteLine(item2.ToString());
					}
					Console.Write("Enter your answer: ");
					answers = int.Parse(Console.ReadLine());
					Console.WriteLine("-----------------------------------------------------------------------");

					if (answers == item.Key.right_answer)
					{
						Marks += item.Key.mark;
					}
				}
			}
			public void ShowResult()
			{
				Console.WriteLine($"Your Mark : {Marks}");
			}
			

		}
		static void Main(string[] args)
		{
			Dictionary<Question, List<Answer>> exam1 = new Dictionary<Question, List<Answer>>();
			exam1.Add(new Question("MCQ", 1, "Which of the following is correct about C#", 10, 4),
				new List<Answer>(){new Answer (1, "It is component oriented.",false),
				new Answer (2, "It can be compiled on a variety of computer platforms.",false),
				new Answer (3, "It is a part of .Net Framework.",false),
				new Answer (4, " All of the above.",true) });
			    exam1.Add(new Question("T or F", 2, "Function overloading is a kind of static polymorphism.", 10, 1),
				new List<Answer>(){new Answer (1, " true.",true),
				new Answer (2, "false.",false) });
			Practice_Exam pExam = new Practice_Exam(20, 2, exam1);
			Practice_Exam pClone = pExam.Clone() as Practice_Exam;
			
			Final_Exam fExam = new Final_Exam(20, 2, exam1);
			Console.WriteLine("Choose the type of exam:\n\t1)Practical Exam\n\t2)Final Exam");
			Console.Write("Enter the type of exam: ");
			String choice = Console.ReadLine();
			Console.WriteLine();
			if (choice == "")
			{
				Console.WriteLine("Choose the type of exam:\n\t1)Practical Exam\n\t2)Final Exam");
				Console.Write("Enter the type of exam: ");
			    choice = Console.ReadLine();
				Console.WriteLine();
			}
			switch (choice)
			{
				case "1":
				case "Practical Exam":
				case "Practical":
				case "practical":
					pExam.Show();
					break;
				case "2":
				case "Final Exam":
				case "Final":
				case "final":
					fExam.Show();
					break;
			}
			Console.WriteLine($"Do you want to show your marks?\n\t1)Yes\n\t2)No");
			String show_result = Console.ReadLine();
			if (show_result == "")
			{
				Console.WriteLine($"Do you want to show your marks?\n\t1)Yes\n\t2)No");
			    show_result = Console.ReadLine();
			}
			switch (show_result)
			{ 
				case "1":
				case "yes":
				case "Yes":
				case "YES":
					fExam.ShowResult();
					Console.WriteLine("Thanks for your time.");
					break;
			
				case "2":
				case "no":
				case "No":
				case "NO":
					Console.WriteLine("Thanks for your time.");
					break;
			}
			Console.WriteLine($"Do you want to show answers?\n\t1)Yes\n\t2)No");
			String show_ans = Console.ReadLine();
			if (show_ans == "")
			{
				Console.WriteLine($"Do you want to show answers?\n\t1)Yes\n\t2)No");
				show_result = Console.ReadLine();
			}
			switch (show_ans)
			{
				case "1":
				case "yes":
				case "Yes":
				case "YES":
					pClone.Show();         //using clone
					Console.WriteLine("Thanks for your time.");
					break;

				case "2":
				case "no":
				case "No":
				case "NO":
					Console.WriteLine("Thanks for your time.");
					break;
			}
			Console.ReadKey();
		}
	}
}
