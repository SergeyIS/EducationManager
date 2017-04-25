using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Student
{
    public class ClassEditorViewModel
    {
        public ClassViewModel AboutClass { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
}