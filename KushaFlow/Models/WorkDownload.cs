using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KushaFlow.Models
{
    public class WorkDownload
    {
        public int Id { get; set; }

        public int StudentIsWorkId { get; set; }

        public StudentIsWork StudentIsWork { get; set; }
        
        public DateTime DownloadDate { get; set; }
    }
}
