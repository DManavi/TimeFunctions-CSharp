using System;

namespace TimeFunctions_CSharp
{
    struct TimeStruct
    {
        public int Hour;
        public int Minute;
        public int Second;
    }

    class Time
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
            Console.WriteLine("Enter time");

            TimeStruct output;

            var valid = false;

            int temp = 0;

            do
            {

                Console.Write("Hour: ");

                var input = Console.ReadLine();

                valid = int.TryParse(input, out temp);

            } while (!valid);

            output.Hour = temp;

            do
            {

                Console.Write("Minute: ");

                var input = Console.ReadLine();

                valid = int.TryParse(input, out temp);

            } while (!valid);

            output.Minute = temp;

            do
            {

                Console.Write("Second: ");

                var input = Console.ReadLine();

                valid = int.TryParse(input, out temp);

            } while (!valid);

            output.Second = temp;


            return output;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // create new instance of time class
            Time timeEmpty = new Time();

            // create new instance of time class with integer params
            Time timeWithIntParams = new Time(22, 65, 59);

            // create time
            TimeStruct t;

            // set hour
            t.Hour = 22;

            // set minute
            t.Minute = 65;

            // set second
            t.Second = 80;

            // create new instance of time class with time param
            Time timeWithTimeParam = new Time(t);

            // show prompt
            Console.WriteLine("Time is");

            Console.WriteLine();

            // show current time of the timeEmpty instance
            Console.WriteLine("time class with default time constructor: {0}", timeEmpty);

            // show time with int params
            Console.WriteLine("time class with int parameters: {0}", timeWithIntParams);


            // increment time (postfix)
            Console.WriteLine("Time after increment is (postfix): {0}", timeWithIntParams++);

            Console.WriteLine("Time after increment is: {0}", timeWithIntParams);

            Console.WriteLine("Time after decrement is (postfix): {0}", timeWithIntParams--);

            Console.WriteLine("Time after decrement is: {0}", timeWithIntParams);

            timeWithIntParams += 5;

            Console.WriteLine("Time after adding 5 seconds is: {0}", timeWithIntParams);

            timeWithIntParams -= 5;

            Console.WriteLine("Time after subtracting 5 seconds is: {0}", timeWithIntParams);


            Console.WriteLine("time class with time parameters: {0}", timeWithTimeParam);


            timeEmpty.SetTime(18, 35, 66);

            Console.WriteLine("Time has been updated from code: {0}", timeEmpty);

            // Get time from user input and update time

            Console.WriteLine("Trying to get time from user");

            // get user time from console
            TimeStruct userTime = timeEmpty.GetTime();

            // show user entered values
            Console.WriteLine("User entered: {0}:{1}:{2}", userTime.Hour.ToString("00"), userTime.Minute.ToString("00"), userTime.Second.ToString("00"));

            // update time
            timeEmpty.SetTime(userTime);

            Console.WriteLine("Time has been updated by the user. New time is: {0}", timeEmpty);

            // Add two times

            Console.WriteLine("Add two times");

            Console.WriteLine("Enter time 1");

            TimeStruct t1 = timeEmpty.GetTime();

            Console.WriteLine("Enter time 2");

            TimeStruct t2 = timeEmpty.GetTime();

            TimeStruct tResult = timeEmpty.Add(t1, t2);

            Console.WriteLine("Add result: {0}:{1}:{2}", tResult.Hour.ToString("00"), tResult.Minute.ToString("00"), tResult.Second.ToString("00"));

            Time time1 = new Time(t1);

            Time time2 = new Time(t2);

            Console.WriteLine("Add result (By operator): {0}", time1 + time2);

            // subtract two times

            Console.WriteLine("Subtract two times");

            Console.WriteLine("Enter time 1: ");

            t1 = timeEmpty.GetTime();

            Console.WriteLine("Enter time 2: ");

            t2 = timeEmpty.GetTime();

            // subtract two times
            tResult = timeEmpty.Subtract(t1, t2);

            time1 = new Time(tResult);

            // show subtract result
            Console.WriteLine("Subtract result: {0}", time1.ToString());

            time1 = new Time(t1);

            time2 = new Time(t2);

            Time operatorResult = time2 - time1;

            TimeStruct operatorTime = operatorResult.CurrentTime();

            // show subtract result
            Console.WriteLine("Subtract result (By operator): {0}:{1}:{2}", operatorTime.Hour.ToString("00"), operatorTime.Minute.ToString("00"), operatorTime.Second.ToString("00"));

            // compare two times
            Console.WriteLine("Compare two times");

            Console.WriteLine("Enter time 1: ");

            t1 = timeEmpty.GetTime();

            time1 = new Time(t1);

            Console.WriteLine("Enter time 2: ");

            t2 = timeEmpty.GetTime();

            time2 = new Time(t2);

            // equality check
            bool isEqual = time1 == time2;

            // inequality check
            bool inEqual = time1 != time2;

            // is left side greater than right side
            bool isLeftGtRight = time1 > time2;

            // is left side greater than or equal right side
            bool isLeftGtEqualRight = time1 >= time2;

            // is left side less than right side
            bool isLeftLtRight = time1 < time2;

            // is left side less than or equal right side
            bool isLeftLtEqualRight = time1 <= time2;

            Console.WriteLine("T1 == T2 = {0}", isEqual);

            Console.WriteLine("T1 != T2 = {0}", inEqual);

            Console.WriteLine("T1 > T2 = {0}", isLeftGtRight);

            Console.WriteLine("T1 >= T2 = {0}", isLeftGtEqualRight);

            Console.WriteLine("T1 < T2 = {0}", isLeftLtRight);

            Console.WriteLine("T1 <= T2 = {0}", isLeftLtEqualRight);

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }
    }
}