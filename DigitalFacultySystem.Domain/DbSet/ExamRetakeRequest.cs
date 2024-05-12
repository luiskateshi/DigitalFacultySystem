﻿using DigitalFacultySystem.Entities.DbSet;

namespace DigitalFacultySystem.Domain.Entities;

public class ExamRetakeRequest : BaseEntity
{
    public Guid? StudentId { get; set; }

    public Guid? PlanSubjectId { get; set; }

    public DateOnly? DateOfRequest { get; set; }

    public Guid? ExamSessionId { get; set; }

    public virtual StudyPlanSubject? StudyPlanSubject { get; set; }

    public virtual ExamsSession? ExamSession { get; set; }

    public virtual Student? Student { get; set; }
}
