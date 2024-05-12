USE [uniDb]
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertStudentsInCourse]    Script Date: 09/05/2024 19:09:36 ******/
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

	--temporary table
    CREATE TABLE #StudentStudyPlans (
        StudentId uniqueidentifier,
        StudyPlanId uniqueidentifier,
		CurrentSemesterOfStudies int,
    );

    INSERT INTO #StudentStudyPlans (StudentId, StudyPlanId, CurrentSemesterOfStudies)
    SELECT s.Id, gen.studyPlanId, gen.CurrentSemesterOfStudies
    FROM Students s
    JOIN StudentsInGroups sig ON sig.StudentId = s.Id
    JOIN Groups g ON g.id = sig.GroupId
    JOIN Generations gen ON gen.id = g.GenerationId;

	

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
            WHERE sic.StudentId = @studentId
            AND sic.CourseId = c.Id
        );

        INSERT INTO CourseAttendances (Id, StudentInCourseId, SeminarHoursAttended, LabHoursAttended, IsAttended, IsActive)
        SELECT NEWID(), sic.Id, 0, 0, 0, 1
        FROM StudentInCoursees sic
        WHERE sic.StudentId = @studentId
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


  --select s.Firstname, s.Lastname, c.Name, g.Name, gen.Name from Students s
  --join StudentsInGroups sig on sig.StudentId = s.id
  --join Groups g on g.Id = sig.GroupId
  --join Generations gen on gen.id = g.GenerationId
  --join StudentInCoursees sc on sc.StudentId = s.id
  --join courses c on c.id = sc.Course


  --update this SP so that it doesnt insert a record for a student in studentInCourse that inside studentsInExam table has a record with an examGrade more than 4.
