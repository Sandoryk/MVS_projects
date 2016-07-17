using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class PolinomHolder
    {
        Dictionary<int, double> polinom;
        public PolinomHolder()
        {
            polinom = new Dictionary<int, double>();
        }
        public PolinomHolder(Dictionary<int, double> inPolinom)
        {
            polinom = new Dictionary<int, double>();
            foreach (var member in inPolinom)
            {
                polinom.Add(member.Key,member.Value);
            }
        }
        public Dictionary<int, double> GetPolinomDictionary
        {
            get
            {
                return polinom;
            }
        }
        public int GetMemberCount
        { 
            get 
            { 
                return polinom.Count; 
            } 
        }
        public bool GetCoeficientByDegree(int degree, out double coef)
        {
            bool foundf = false;

            coef = 0;
            if (degree != 0)
            {
                if (polinom.ContainsKey(degree))
                {
                    coef = polinom[degree];
                    foundf = true;
                }  
            }
            return foundf;
        }
        public void AddMemberWithDegree(int degree,double coeficient)
        {
            if (degree != 0)
            {
                if (polinom.ContainsKey(degree))
                {
                    polinom[degree] = polinom[degree] + coeficient;
                }  
                else
                {
                    polinom.Add(degree, coeficient);
                }
            }
        }
        public void RemoveMemberWithDegree(int degree)
        {
            if (degree != 0)
            {
                if (polinom.ContainsKey(degree))
                {
                    polinom.Remove(degree);
                }
            }
        }
        public PolinomHolder ClonePolinom()
        {
            PolinomHolder newPoli = new PolinomHolder();
            foreach (var member in this.polinom)
            {
                newPoli.AddMemberWithDegree(member.Key,member.Value);
            }

            return newPoli;
        }
        public static PolinomHolder operator +(PolinomHolder pol1, PolinomHolder pol2)
        {
            PolinomHolder resultPoli = null;
            bool testf = true;

            if (pol1.GetMemberCount == 0 || pol2.GetMemberCount == 0)
                testf = false;
            if (pol1 == null || pol2 == null)
                testf = false;
            if (testf)
            {
                resultPoli = pol1.ClonePolinom();
                foreach (var pol2Member in pol2.polinom)
                {
                    if (resultPoli.polinom.ContainsKey(pol2Member.Key))
                    {
                        resultPoli.polinom[pol2Member.Key] = resultPoli.polinom[pol2Member.Key] + pol2Member.Value;
                    }
                    else
                    {
                        resultPoli.AddMemberWithDegree(pol2Member.Key, pol2Member.Value);
                    }
                }
            }

            return resultPoli;
        }
        public static PolinomHolder operator -(PolinomHolder pol1, PolinomHolder pol2)
        {
            PolinomHolder resultPoli = null;
            bool testf = true;

            if (pol1.GetMemberCount == 0 || pol2.GetMemberCount == 0)
                testf = false;
            if (pol1 == null || pol2 == null)
                testf = false;
            if (testf)
            {
                resultPoli = pol1.ClonePolinom();
                foreach (var pol2Member in pol2.polinom)
                {
                    if (resultPoli.polinom.ContainsKey(pol2Member.Key))
                    {
                        resultPoli.polinom[pol2Member.Key] = resultPoli.polinom[pol2Member.Key] - pol2Member.Value;
                    }
                    else
                    {
                        resultPoli.AddMemberWithDegree(pol2Member.Key, -pol2Member.Value);
                    }
                }
            }

            return resultPoli;
        }
        public string PolinomToString()
        {
            StringBuilder str = new StringBuilder();
            if (polinom.Count > 0)
            {
                IEnumerable<KeyValuePair<int, double>> result1  = polinom.OrderByDescending(t => t.Key);
                foreach (var memb in result1)
                {
                    if (str.Length==0)
	                {
                        str.Append(memb.Value + "(X^" + memb.Key + ")");
	                }
                    else
                    {
                        if (memb.Value>0)
                        {
                            str.Append("+" + memb.Value + "(X^" + memb.Key + ")");  
                        }
                        else
                        {
                            str.Append(memb.Value + "(X^" + memb.Key + ")");  
                        }
                    }
                }
            }
            return str.ToString();
        }
    }
}
