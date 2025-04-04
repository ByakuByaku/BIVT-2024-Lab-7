using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;


            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string charactertrait, string concept)
            {
                _animal = animal;
                _characterTrait = charactertrait;
                _concept = concept;
            }
            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (questionNumber < 1 || questionNumber > 3 || responses == null) return 0;

                var current = this;
                switch (questionNumber)
                {
                    case 1:
                        string answer = current.Animal;
                        return responses.Count(ans => ans.Animal == answer && ans.Animal != null);
                    case 2:
                        string answer2 = current.CharacterTrait;
                        return responses.Count(ans => ans.CharacterTrait == answer2 && ans.CharacterTrait != null);
                    case 3:
                        string answer3 = current.Concept;
                        return responses.Count(ans => ans.Concept == answer3 && ans.Concept != null);
                    default:
                        return 0;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Животное: {_animal} \t черта: {_characterTrait} \tВещь: {_concept} \t");
            }
        }
        public struct Research
        {
            private string _name;
            private Response[] _responses;


            public string Name => _name;

            public Response[] Responses
            {
                get
                {
                    if (_responses == null) return null;
                    return _responses;
                }
            }


            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];

            }

            public void Add(string[] answers)
            {
                if (answers == null || answers.Length <= 2 || _responses == null) return;

                var copy = new Response(answers[0], answers[1], answers[2]);

                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = copy;
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null || (question < 1 || question > 3)) return null;
                string[] Ans = new string[_responses.Length];
                int index = 0;
                foreach (var answers in _responses)
                {
                    string A = GetAnswer(answers, question);
                    if (A != null && A != "")
                    {

                        Ans[index++] = A;
                    }
                    else Array.Resize(ref Ans, Ans.Length - 1);


                }
                string[] Unique_ans = Ans.Distinct().ToArray();
                int[] Amount = new int[Unique_ans.Length];
                for (int i = 0; i < Unique_ans.Length; i++)
                {
                    Amount[i] = Ans.Count(r => r == Unique_ans[i]);
                }
                Sort(ref Unique_ans, ref Amount);
                if (Unique_ans.Length > 5)
                {
                    Array.Resize(ref Unique_ans, 5);
                }
                return Unique_ans;
            }
            private string GetAnswer(Response A, int question)
            {
                string ans;
                switch (question)
                {
                    case 1:
                        ans = A.Animal;
                        return ans;
                    case 2:
                        ans = A.CharacterTrait;
                        return ans;
                    case 3:
                        ans = A.Concept;
                        return ans;
                    default:
                        return null;
                }
            }
            private void Sort(ref string[] resp, ref int[] number)
            {
                for (int i = 0, j = 1; i < number.Length;)
                {
                    if (i == 0 || number[i - 1] >= number[i])
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        int temp = number[i];
                        number[i] = number[i - 1];
                        number[i - 1] = temp;
                        string temp2 = resp[i];
                        resp[i] = resp[i - 1];
                        resp[i - 1] = temp2;

                        i--;
                    }
                }
            }
            public void Print()
            {
                for (int i = 1; i <= 3; i++)
                {
                    string[] result = GetTopResponses(i);

                    for (int j = 0; j < result.Length; j++)
                    {
                        Console.Write(result[j] + " ");
                    }
                    Console.WriteLine();
                }

            }

        }
        public class Report
        {
            private Research[] _research;
            private static int _numbering;
            public Research[] Researches => _research;

            static Report()
            {
                _numbering = 1;
            }
            public Report()
            {
                _research = new Research[0];
            }
            public Research MakeResearch()
            {
                string name = $"No_{_numbering}_{DateTime.Now.ToString("MM/yy")}";
                Research research = new Research(name);
                Array.Resize(ref _research, _research.Length + 1);


                _research[_research.Length - 1] = research;


                _numbering++;
                return research;
            }


            public (string, double)[] GetGeneralReport(int question)
            {
                if (_research == null || question > 3 || question < 1) return null;
                //Response[] responses = new Response[0];
                //foreach (var research in _research)
                //{
                //    if (research.Responses != null)
                //    {
                //        foreach (var response in research.Responses)
                //        {
                //            string answer = GetAnswer(response, question);
                //            if (answer!="" && answer!=null)
                //            {
                //                Array.Resize(ref responses, responses.Length+1);
                //                responses[responses.Length-1] = answer;
                //            }
                //        }
                //    }
                //}
                Response[] responses = _research.SelectMany(r => r.Responses).Where(x => GetAnswer(x, question) != null).ToArray();
                if  (responses.Count() == 0) return null;
                //string[] unique = responses.Distinct().ToArray();

                //(string, double)[] res = new (string, double)[unique.Length];

                var res = responses.GroupBy(r => GetAnswer(r, question)).Select(g => (g.Key, (g.Count() * 100.0) / responses.Count())).ToArray();
                return res;
            }
            private static string GetAnswer(Response A, int question)
            {
                string ans;
                switch (question)
                {
                    case 1:
                        ans = A.Animal;
                        return ans;
                    case 2:
                        ans = A.CharacterTrait;
                        return ans;
                    case 3:
                        ans = A.Concept;
                        return ans;
                    default:
                        return null;
                }
            }
        }
    }
}
