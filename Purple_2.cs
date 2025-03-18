using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            //поля

            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;



            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null)
                    {
                        return null;
                    }
                    var copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int Result { get; private set; }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                Result = 0;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (distance == 0 || marks == null || marks.Length != 5 || distance < 0 || _marks == null)
                    return;
                _distance = distance;
                Array.Copy(marks, _marks, marks.Length);
                int Points = 60 + (_distance - 120) * 2;
                if (Points < 0) Points = 0;
                Result += marks.Sum() - marks.Max() - marks.Min() + Points;
                if (distance >= target)
                    Result += 60;



            }


            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;
                //for (int i = 0, j = 1;i< array.Length;)
                //{
                //    if (i==0 || array[i].Result <= array[i - 1].Result)
                //    {
                //        i = j;
                //        j++;
                //    }
                //    else
                //    {
                //        var temp = array[i];
                //        array[i] = array[i-1];
                //        array[i-1] = temp;
                //        i--;
                //    }
                //}
                var copy = array.OrderByDescending(x => x.Result).ToArray();
                Array.Copy(copy, array, copy.Length);
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {_name}    Фамилия: {_surname}    Результат: {Result}");
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;


            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (_participants == null) return;
                if (participants == null) return;
                
                _participants = _participants.Concat(participants).ToArray();
            }

            public void Jump(int distance, int[] marks)
            {
                int index = -1;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Marks.Sum() == 0 && _participants[i].Marks != null) { index = i; break; }
                }
                if (index < 0) return;

                _participants[index].Jump(distance, marks, _standard);
            }

            public void Print()
            {
                if (_participants == null || _name == null || _standard == 0) { Console.WriteLine("Так не пойдет"); return; }
                Console.WriteLine($"Название {_name}");
                Console.WriteLine($"Стандарт {_standard}");
                Console.WriteLine($"список участников");
                foreach (Participant participant in _participants)
                {
                    participant.Print();
                }
                Console.WriteLine();
            }

        }
        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}
