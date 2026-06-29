using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.StudentPortals;

public class UpdateStudentGradeRequest
{
    public int StudentId { get; set; }
    public decimal GPA { get; set; }
}
