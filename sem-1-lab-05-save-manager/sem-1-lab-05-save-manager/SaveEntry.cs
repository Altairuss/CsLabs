using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace sem_1_lab_05_save_manager
{
    public class SaveEntry
    {
        public string Name { get; set; }
        [JsonProperty("file")]
        public string FileName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
