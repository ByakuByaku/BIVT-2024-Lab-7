using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_1
    {
        public class Participant
        {


            //поля
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _numJump;



            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;
                    var copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public double TotalScore { get; private set; }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                TotalScore = 0;
                _numJump = 0;

            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs.Length == 4 && coefs != null && _coefs != null)
                {
                    Array.Copy(coefs, _coefs, coefs.Length);
                }
                else
                {
                    return;
                }
            }
            public void Jump(int[] marks)
            {
                if (marks.Length == 7 && _numJump < 4 && marks != null && _marks != null && _coefs != null)
                {
                    for (int i = 0; i < marks.Length; i++)
                    {
                        _marks[_numJump, i] = marks[i];
                    }
                    TotalScore += (marks.Sum() - marks.Min() - marks.Max()) * _coefs[_numJump];

                    _numJump++;
                }
                else
                    return;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;

                for (int i = 0, j = 1; i < array.Length;)
                {
                    if (i == 0 || array[i].TotalScore <= array[i - 1].TotalScore)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        var temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Имя {_name}");
                Console.WriteLine($"Фамилия {_surname}");


                Console.WriteLine("Коэффиценты");
                foreach (var coef in _coefs)
                {
                    Console.Write(coef + " ");
                }

                Console.WriteLine("Оценки");
                int row = _marks.GetLength(0);
                int col = _marks.GetLength(1);
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        Console.Write(_marks[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Результат {TotalScore}");
            }



        }















        public class Judge
        {
            private string _name;
            private int[] _marks;

            private int NumMark;
            public string Name => _name;
            
            public Judge(string name, int[] marks)
            {
                if (marks == null) _marks = null;
                _name = name;
                _marks = new int[marks.Length];
                Array.Copy(marks, _marks, marks.Length);
            }
            public int CreateMark()
            {
                if (_marks.Length==0 || _marks == null) 
                    return 0;
                if (NumMark==_marks.Length)
                {
                    NumMark = 0;
                }
                return _marks[NumMark++];
            }

            public void Print()
            {
                if (_marks == null)
                {
                    Console.WriteLine("Так не пойдет");
                    return;
                }
                Console.WriteLine($"Name: {_name}");
                for (int i = 0;i < _marks.Length;i++)
                {
                    Console.Write(_marks[i]+" ");
                }
            }

        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;

            public Competition(Judge[] judges)
            {
                _judges = judges != null ? new Judge[judges.Length] : null;
                if (judges != null)
                {
                    Array.Copy(judges, _judges, judges.Length);
                }
                _participants = new Participant[0];
            }
            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges==null) return;
                int[] res = new int[7];
                int index = 0;
                for (int i =0;i < _judges.Length;i++)
                {
                    res[index++] = _judges[i].CreateMark();
                    if (index == 7) break;
                }
                jumper.Jump(res);
            }
            public void Add(Participant jumper)
            {
                if (jumper == null || _participants==null) return;
                Array.Resize(ref _participants, _participants.Length+1);
                Evaluate(jumper);
                _participants[_participants.Length-1] = jumper;

            }
            public void Add(Participant[] jumper)
            {
                if (jumper==null || _participants == null) return;
                foreach (var jumper1 in jumper)
                {
                    if (jumper1 != null)
                    {
                        Evaluate(jumper1);
                        Array.Resize(ref _participants, _participants.Length + 1);
                        _participants[_participants.Length - 1] = jumper1;
                    }
                }


            }
            public void Sort()
            {
                if (_participants == null || _participants.Length <= 1)
                    return;
                Participant.Sort(_participants);


            }
        }

        

    }
}

