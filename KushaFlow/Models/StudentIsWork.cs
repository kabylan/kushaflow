using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KushaFlow.Models
{
    public class StudentIsWork
    {
        public int Id { get; set; }

        [Display(Name ="Название")]
        public string Title { get; set; }
        
        [Display(Name ="Описание")]
        public string Category { get; set; }

        public string WorkName { get; set; }

        public string WorkPath { get; set; }

        [Display(Name = "Формат")]
        public string WorkFormat { get; set; }
        
        [Display(Name ="Дата")]
        public DateTime UploadDate { get; set; }
        
        public IEnumerable<WorkDownload> WorkDownloads { get; set; }

        [Display(Name = "Скачиваний")]
        public int DownloadsNum
        {

            get
            {
                try
                {
                    int x = WorkDownloads.ToList().Count;
                    return x;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {

            }
        }


    }
}
