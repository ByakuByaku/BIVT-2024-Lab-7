using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;


            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time)
            {
                if (time > 0 && _time == 0)
                {
                    _time = time;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {_name}    Фамилия: {_surname}    время: {_time}");
                Console.WriteLine();
            }
            public static void Sort(Sportsman[] array)
            {
                var copy = array.OrderBy(x => x._time).ToArray();
                Array.Copy(copy, array, copy.Length);
            }
        }
        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            
            public SkiMan(string name, string surname, double time) : base(name, surname) { Run(time); }
           
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }

            public SkiWoman(string name, string surname, double time) : base(name, surname) { Run(time); }

        }
        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    return _sportsmen;
                }
            }

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                _name = group.Name;
                var copy = group.Sportsmen;
                _sportsmen = new Sportsman[0];
                if (group.Sportsmen == null) _sportsmen = null;
                Array.Copy(copy, _sportsmen, _sportsmen.Length);

            }

            

            public void Split(out Sportsman[] men, out Sportsman[] women)
            { 
                men = null;
                women = null;


                if (_sportsmen==null) return;
                women = _sportsmen.Where(r => r is SkiWoman).ToArray();
                men = _sportsmen.Where(r => r is SkiMan).ToArray();
            }

            public void Shuffle()
            {
                if (_sportsmen == null) return;
                var M = new Sportsman[0];
                var W = new Sportsman[0];
                Sort();
                Split(out M, out W);
                if (M.Length == 0 || W.Length == 0) return;
                W = W.OrderBy(r => r.Time).ToArray();
                M = M.OrderBy(r => r.Time).ToArray();
                //var result = new Sportsman[M.Length+W.Length];
                int i = 0, j = 0, k = 0;
                bool flag = false;
                if (M[0].Time <= W[0].Time  )
                {
                    while(i<M.Length && j < W.Length)
                    {
                        _sportsmen[k++] = M[i++];


                        _sportsmen[k++] = W[j++];
                    }
                }
                else
                {
                    while (i < M.Length && j < W.Length)
                    {
                        _sportsmen[k++] = W[j++];
                        _sportsmen[k++] = M[i++];


                        
                    }
                }

                while (i < M.Length) _sportsmen[k++] = M[i++];

                while (j < W.Length) _sportsmen[k++] = W[j++];


            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;

            }
            public void Add(Sportsman[] sportsman)
            {
                if (_sportsmen == null || sportsman.Length == 0) return;
                int index = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsman.Length);
                int a = 0;
                for (int i = index; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = sportsman[a++];
                }

            }
            public void Add(Group group)
            {
                if (_sportsmen == null || group.Sportsmen == null)
                    return;
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                if (_sportsmen != null) Array.Copy(_sportsmen.OrderBy(r => r.Time).ToArray(), _sportsmen, _sportsmen.Length);
            }
            public static Group Merge(Group group1, Group group2)
            {
                var group = new Group("а");
                if (group1.Sportsmen == null || group2.Sportsmen == null)
                {
                    if (group1.Sportsmen != null)
                    {
                        group._sportsmen = group2._sportsmen;
                    }
                    else if (group2.Sportsmen != null)
                    {
                        group._sportsmen = group1._sportsmen;
                    }

                    return group;
                }
                Array.Resize(ref group._sportsmen, group1._sportsmen.Length + group2._sportsmen.Length);
                int i = 0, j = 0, k = 0;
                while (i < group1._sportsmen.Length && j < group2._sportsmen.Length)
                {
                    if (group1._sportsmen[i].Time <= group2._sportsmen[j].Time)
                        group._sportsmen[k++] = group1._sportsmen[i++];
                    else
                        group._sportsmen[k++] = group2._sportsmen[j++];
                }
                while (i < group1._sportsmen.Length)
                    group._sportsmen[k++] = group1._sportsmen[i++];
                while (j < group2._sportsmen.Length)
                    group._sportsmen[k++] = group2._sportsmen[j++];
                return group;



            }

            
            public void Print()
            {
                Console.WriteLine($"Name: {_name}");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Sportsmen");

                foreach (var sportsman in _sportsmen)
                {
                    sportsman.Print();
                }
            }
        }
    }
}
