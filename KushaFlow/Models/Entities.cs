using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KushaFlow.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        public string ImgName { get; set; }

        public string ImgPath { get; set; }

        [Display(Name = "Институт")]
        public int? InstituteId { get; set; }
        [Display(Name = "Институт")]
        public Institute Institute { get; set; }
       
        [Display(Name = "Кафедра")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Кафедра")]
        public Departament Department { get; set; }
        
        [Display(Name = "Группа")]
        public string Group { get; set; }
        
        [Display(Name = "Курс")]
        public int? CourseId { get; set; }
        [Display(Name = "Курс")]
        public Course Course { get; set; }

        [Display(Name = "Достижения")]
        public string Achievements { get; set; }

        [Display(Name = "Instagram аккаунт")]
        public string InstagramAccount { get; set; }

        [Display(Name = "Facebook аккаунт")]
        public string FacebookAccount { get; set; }

    }

    public class Institute
    {
        public int Id { get; set; }

        [Display(Name = "Институт")]
        public string Name { get; set; }
    }

    public class Departament
    {
        public int Id { get; set; }

        [Display(Name = "Кафедра")]
        public string Name { get; set; }

        [Display(Name = "Институт")]
        public int? InstitutId { get; set; }

        [Display(Name = "Институт")]
        public Institute Institut { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Курс")]
        public int Num { get; set; }
    }
}
