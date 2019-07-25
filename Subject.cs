using System;
using System.Collections.Generic;
using System.Text;

public class Subject
{
    public Subject(string name)
    {
        this._name = name;
    }

    public string GetName()
    {
        return _name;
    }

    public List<DateTime> getTimes()
    {
        return _time;
    }
    public Subject(string name, string time)
    {
        this._name = name;
        this._time.Add(Convert.ToDateTime(time));
    }

    public void AddTime(string time)
    {
        this._time.Add(Convert.ToDateTime(time));
    }

    private string _name;
    public List<DateTime> _time = new List<DateTime>();
}
