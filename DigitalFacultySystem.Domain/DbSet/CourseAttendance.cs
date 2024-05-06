using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

    public class CourseAttendance : BaseEntity
    {
        //will be filled in by the system when students are added to the course
        public int? SeminarHoursAttended { get; set; } = 0;

        public int? LabHoursAttended { get; set; } = 0;

        public bool isAttended { get; set; } = false;

        public Guid? StudentInCourseId { get; set; }

        public virtual StudentInCourse? StudentInCourse { get; set; }
    }
