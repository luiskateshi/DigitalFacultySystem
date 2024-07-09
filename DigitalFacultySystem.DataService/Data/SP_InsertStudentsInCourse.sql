USE [uniDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_InsertStudentsInCourse]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @studentId uniqueidentifier;
    DECLARE @studentStudyPlanId uniqueidentifier;
    DECLARE @currentSemester int;

    -- Temporary table
    CREATE TABLE #StudentStudyPlans (
        StudentId uniqueidentifier,
        StudyPlanId uniqueidentifier,
        CurrentSemesterOfStudies int
    );

    INSERT INTO #StudentStudyPlans (StudentId, StudyPlanId, CurrentSemesterOfStudies)
    SELECT s.Id, gen.StudyPlanId, gen.CurrentSemesterOfStudies
    FROM Students s
    JOIN StudentsInGroups sig ON sig.StudentId = s.Id
    JOIN Groups g ON g.Id = sig.GroupId
    JOIN Generations gen ON gen.Id = g.GenerationId
    WHERE s.IsActive = 1;

    DECLARE studentCursor CURSOR FOR
    SELECT Id
    FROM Students
    WHERE IsActive = 1;

    OPEN studentCursor;
    FETCH NEXT FROM studentCursor INTO @studentId;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @studentStudyPlanId = StudyPlanId, @currentSemester = CurrentSemesterOfStudies
        FROM #StudentStudyPlans
        WHERE StudentId = @studentId;

        -- Insert student into StudentInCoursees only if not already enrolled in another course for the same subject
        -- and the student hasn't already passed the course
        INSERT INTO StudentInCoursees (Id, StudentId, CourseId, IsActive)
        SELECT NEWID(), @studentId, c.Id, 1
        FROM Courses c
        JOIN StudyPlanSubjects sps ON c.StudyPlanSubjectId = sps.Id
        JOIN StudyPlans sp ON sps.StudyPlanId = sp.Id
        WHERE sp.Id = @studentStudyPlanId
        AND sps.Semester = @currentSemester
        AND NOT EXISTS (
            SELECT 1
            FROM StudentInCoursees sic
            JOIN Courses c2 ON sic.CourseId = c2.Id
            JOIN StudyPlanSubjects sps2 ON c2.StudyPlanSubjectId = sps2.Id
            WHERE sic.StudentId = @studentId
            AND sps2.SubjectId = sps.SubjectId
        )
        AND NOT EXISTS (
            SELECT 1
            FROM StudentsInExams sie
            JOIN Exams e ON sie.ExamId = e.Id
            JOIN StudyPlanSubjects sps2 ON e.PlanSubjectId = sps2.Id
            WHERE sie.StudentId = @studentId
            AND sps2.SubjectId = sps.SubjectId
            AND sie.ExamGrade > 4
        );

        -- Insert attendance records only for the new StudentInCoursees entries
        INSERT INTO CourseAttendances (Id, StudentInCourseId, SeminarHoursAttended, LabHoursAttended, IsAttended, IsActive)
        SELECT NEWID(), sic.Id, 0, 0, 0, 1
        FROM StudentInCoursees sic
        WHERE sic.StudentId = @studentId
        AND sic.IsActive = 1
        AND NOT EXISTS (
            SELECT 1
            FROM CourseAttendances ca
            WHERE ca.StudentInCourseId = sic.Id
        );

        FETCH NEXT FROM studentCursor INTO @studentId;
    END;

    CLOSE studentCursor;
    DEALLOCATE studentCursor;

    DROP TABLE #StudentStudyPlans;
END;


/*
1. Gather Student Information:

    +Collects the IDs, study plan IDs, and current semester of studies for all active students.
    +Stores this information in a temporary table for easy reference.

2. Iterate Over Each Active Student:

    +Uses a cursor to go through each active student one by one.

3. Determine Student's Study Plan and Current Semester:

    +For each student, retrieves their study plan ID and current semester from the temporary table.

4. Enroll Student in Courses:

    +Inserts the student into the StudentInCoursees table for courses that match their current semester and study plan, provided that:
    +The student is not already enrolled in another course for the same subject.
    +The student has not already passed the course (i.e., does not have a passing grade in the StudentsInExams table for the same subject).

5. Create Attendance Records:

    +Inserts records into the CourseAttendances table for the new StudentInCoursees entries, initializing attendance details.
*/