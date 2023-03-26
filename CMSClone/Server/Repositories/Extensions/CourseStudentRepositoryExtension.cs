﻿using CMSClone.Server.Models;

namespace CMSClone.Server.Repositories.Extensions
{
    public static class CourseStudentRepositoryExtension
    {
        public static IQueryable<StudentsCourse> Search(this IQueryable<StudentsCourse> coursesStudent, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return coursesStudent;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return coursesStudent.Where(p => p.Course.CourseCode.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
