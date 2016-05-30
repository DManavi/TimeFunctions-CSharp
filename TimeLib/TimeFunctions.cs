using System;

namespace TimeLib
{
    public struct TimeStruct
    {
        public int Hour;
        public int Minute;
        public int Second;
    }

    public class Time
    {
        private int _Hour;
        private int _Minute;
        private int _Second;

        public Time()
        {
            // set hour, minute and second to 0
            _Hour = _Minute = _Second = 0;
        }

        public Time(int hour, int minute, int second)
        {
            // set time on construction
            SetTime(hour, minute, second);
        }

        public Time(TimeStruct time)
        {
            // set time on construction
            SetTime(time);
        }


        public void SetTime(TimeStruct time)
        {
            // normalize input time
            var t = NormalizeTime(time);

            // assign hour
            _Hour = t.Hour;

            // assign minute
            _Minute = t.Minute;

            // assign second
            _Second = t.Second;
        }

        public void SetTime(int hour, int minute, int second)
        {
            // create new time variable
            TimeStruct t;

            // assign hour
            t.Hour = hour;

            // assign minute
            t.Minute = minute;

            // assign second
            t.Second = second;

            // call set_time with time struct
            SetTime(t);
        }

        public int TotalSeconds()
        {
            return _Hour * 3600 + _Minute * 60 + _Second;
        }

        void ShowCurrentTime()
        {
            // show prompt
            Console.Write("Current system time is: ");

            // show time
            ShowCurrentTime(_Hour, _Minute, _Second);
        }

        void ShowCurrentTime(int hour, int minute, int second)
        {
            // print time
            Console.WriteLine("{0}:{1}:{2}", _Hour, _Minute, _Second);
        }

        public TimeStruct Add(TimeStruct t1, TimeStruct t2)
        {
            // create output variable
            TimeStruct output;

            // add hours
            output.Hour = t1.Hour + t2.Hour;

            // add minutes
            output.Minute = t1.Minute + t2.Minute;

            // add seconds
            output.Second = t1.Second + t2.Second;

            // normalize add result
            output = NormalizeTime(output);

            // return normalized time to the caller
            return output;
        }

        public TimeStruct Subtract(TimeStruct t1, TimeStruct t2)
        {
            // create output variable
            TimeStruct output;

            // subtract hours
            output.Hour = t2.Hour - t1.Hour;

            // subtract minutes
            output.Minute = t2.Minute - t1.Minute;

            // subtract seconds
            output.Second = t2.Second - t1.Second;

            while (output.Hour < 0 || output.Minute < 0 || output.Second < 0)
            {

                // if hour is negative
                if (output.Hour < 0)
                {
                    // fix negative hour
                    output.Hour += 24;
                }

                if (output.Minute < 0)
                {
                    // decrease hour
                    output.Hour--;

                    // add 60 minutes to current minute
                    output.Minute += 60;
                }

                if (output.Second < 0)
                {
                    // decrease minute
                    output.Minute--;

                    // add 60 seconds to current second
                    output.Second += 60;
                }
            }

            // normalize time again
            output = NormalizeTime(output);

            // return output to caller
            return output;
        }

        public TimeStruct CurrentTime()
        {
            TimeStruct tOutput;

            tOutput.Hour = _Hour;

            tOutput.Minute = _Minute;

            tOutput.Second = _Second;

            return tOutput;
        }

        private TimeStruct NormalizeTime(TimeStruct input)
        {
            // divide by 60 e.g. 75(s) / 60 = 1(int)
            int next = input.Second / 60;

            // get divide remaining and set as current second
            input.Second = input.Second % 60;

            // add calculated minutes to the time minute
            input.Minute += next;


            // divide by 60 e.g. 75(m) / 60 = 1(int)
            next = input.Minute / 60;

            // get divide remaining and set as current minute
            input.Minute = input.Minute % 60;

            while (input.Hour < 0 || input.Minute < 0 || input.Second < 0)
            {

                // add extra hours to current hour
                input.Hour += next;

                // normalize hour
                input.Hour = input.Hour % 24;

                // if second is negative
                if (input.Second < 0)
                {

                    // decrease minute
                    input.Minute--;

                    // add 60 seconds to second
                    input.Second += 60;
                }

                // if minute is negative
                if (input.Minute < 0)
                {

                    // decrease hour
                    input.Hour--;

                    // add 60 minutes to input
                    input.Minute += 60;
                }

                // if hour is negative
                if (input.Hour < 0)
                {

                    // add 24 hours to hour
                    input.Hour += 24;
                }
            }

            // return normalized value to the caller
            return input;
        }

        public static Time operator +(Time a, Time b)
        {
            a._Second += b._Second;

            a._Minute += b._Minute;

            a._Hour += b._Hour;

            a.SetTime(a._Hour, a._Minute, a._Second);

            return a;
        }

        public static Time operator +(Time a, int b)
        {
            a._Second += b;

            a.SetTime(a._Hour, a._Minute, a._Second);

            return a;
        }

        public static Time operator ++(Time a)
        {
            a._Second++;

            a.SetTime(a._Hour, a._Minute, a._Second);

            return a;
        }

        public static Time operator -(Time a, Time b)
        {
            a._Second -= b._Second;

            a._Minute -= b._Minute;

            a._Hour -= b._Hour;

            a.SetTime(a._Hour, a._Minute, a._Second);

            return a;
        }

        public static Time operator -(Time a, int b)
        {
            a._Second -= b;

            a.SetTime(a._Hour, a._Minute, a._Second);

            return a;
        }

        public static Time operator --(Time a)
        {
            a._Second--;

            a.SetTime(a._Hour, a._Minute, a._Second);

            return a;
        }

        public static bool operator <(Time a, Time b)
        {
            return a.TotalSeconds() < b.TotalSeconds();
        }

        public static bool operator <=(Time a, Time b)
        {
            return a.TotalSeconds() <= b.TotalSeconds();
        }

        public static bool operator >(Time a, Time b)
        {
            return a.TotalSeconds() > b.TotalSeconds();
        }

        public static bool operator >=(Time a, Time b)
        {
            return a.TotalSeconds() >= b.TotalSeconds();
        }

        public static bool operator ==(Time a, Time b)
        {
            if ((object)a == null || (object)b == null) { return false; }

            return a.TotalSeconds() == b.TotalSeconds();
        }

        public static bool operator !=(Time a, Time b)
        {
            return a.TotalSeconds() != b.TotalSeconds();
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", _Hour.ToString("00"), _Minute.ToString("00"), _Second.ToString("00"));
        }

        public TimeStruct GetTime()
        {
            TimeStruct time;

            time.Hour = _Hour;

            time.Minute = _Minute;

            time.Second = _Second;

            return time;
        }
    }
}