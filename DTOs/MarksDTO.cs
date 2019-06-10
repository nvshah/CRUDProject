using System.Collections.Generic;

namespace DTOs
{
    public class MarksDTO
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public Dictionary<string, int> Marks { get; set; }
    }
}
