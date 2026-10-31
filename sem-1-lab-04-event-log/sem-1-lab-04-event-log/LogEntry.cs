using System;
using System.Collections.Generic;
using System.Text;

namespace sem_1_lab_04_event_log
{
    public class LogEntry //делаем свой класс в который будет передавать инфу по категориям
    {
        public DateTime Timestamp;
        public string Level;
        public string Category;
        public string Message;

    }
}
