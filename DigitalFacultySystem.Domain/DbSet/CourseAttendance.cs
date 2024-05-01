using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

    public class CourseAttendance : BaseEntity
    {
        public int? SeminarHoursAttended { get; set; }

        public int? LabHoursAttended { get; set; }

        public Guid? StudentInCourseId { get; set; }

        public virtual StudentInCourse? StudentInCourse { get; set; }
    }
