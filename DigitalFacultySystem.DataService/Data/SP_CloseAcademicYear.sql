USE [uniDb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CloseAcademicYear]
    @academicYearId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Deactivate the specified academic year
    UPDATE AcademicYears
    SET IsActive = 0
    WHERE Id = @academicYearId;

    -- Increment CurrentSemesterOfStudies for active generations
    -- and deactivate generations that are in the last semester
    UPDATE Generations
    SET CurrentSemesterOfStudies = CASE
        WHEN CurrentSemesterOfStudies < (SELECT StudyLength FROM DegreePrograms WHERE Id = Generations.DegreeId) THEN CurrentSemesterOfStudies + 1
        ELSE CurrentSemesterOfStudies
    END,
    IsActive = CASE
        WHEN CurrentSemesterOfStudies = (SELECT StudyLength FROM DegreePrograms WHERE Id = Generations.DegreeId) THEN 0
        ELSE IsActive
    END
    WHERE IsActive = 1;

    -- Process each student to check if they pass the year or not
    DECLARE @studentId UNIQUEIDENTIFIER;
    DECLARE @currentSemester INT;
    DECLARE @totalCredits INT;
    DECLARE @generationId UNIQUEIDENTIFIER;
    DECLARE @degreeProgramId UNIQUEIDENTIFIER;
    DECLARE @newGroupId UNIQUEIDENTIFIER;
    DECLARE @generationSemester INT;

    DECLARE student_cursor CURSOR FOR
    SELECT s.Id, s.CurrentSemester, gen.Id, gen.DegreeId, gen.CurrentSemesterOfStudies
    FROM Students s
    JOIN StudentsInGroups sig ON s.Id = sig.StudentId
    JOIN Groups g ON sig.GroupId = g.Id
    JOIN Generations gen ON g.GenerationId = gen.Id
    WHERE gen.IsActive = 1;

    OPEN student_cursor;

    FETCH NEXT FROM student_cursor INTO @studentId, @currentSemester, @generationId, @degreeProgramId, @generationSemester;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Calculate total credits for the student
        SELECT @totalCredits = SUM(sps.CreditsNo)
        FROM StudentInCoursees sic
        JOIN Courses c ON sic.CourseId = c.Id
        JOIN StudentsInExams sie ON sie.StudentId = sic.StudentId
        JOIN StudyPlanSubjects sps ON sps.Id = c.StudyPlanSubjectId
        WHERE sic.StudentId = @studentId AND sie.ExamGrade > 4;

        -- Determine if the student passes the year
        IF (@currentSemester = 2 AND @totalCredits >= 30) OR (@currentSemester = 4 AND @totalCredits >= 80)
        BEGIN
            -- The student passes the year, increment CurrentSemester by 2
            UPDATE Students
            SET CurrentSemester = CurrentSemester + 2
            WHERE Id = @studentId;
        END
        ELSE
        BEGIN
            -- The student does not pass the year
            -- Remove from current group
            DELETE FROM StudentsInGroups
            WHERE StudentId = @studentId;

            -- Find a new group in a generation that is 2 semesters lower and in the same degree program
            DECLARE @newGenerationId UNIQUEIDENTIFIER;
            SELECT TOP 1 @newGenerationId = g.Id
            FROM Generations g
            WHERE g.DegreeId = @degreeProgramId
              AND g.CurrentSemesterOfStudies = @generationSemester - 2
              AND g.IsActive = 1;

            IF @newGenerationId IS NOT NULL
            BEGIN
                -- Find a group in the new generation
                SELECT TOP 1 @newGroupId = grp.Id
                FROM Groups grp
                WHERE grp.GenerationId = @newGenerationId
                  AND grp.IsActive = 1;

                IF @newGroupId IS NOT NULL
                BEGIN
                    -- Insert the student into the new group
                    INSERT INTO StudentsInGroups (StudentId, GroupId, IsActive, CreatedAt)
                    VALUES (@studentId, @newGroupId, 1, GETDATE());
                END
            END
        END

        FETCH NEXT FROM student_cursor INTO @studentId, @currentSemester, @generationId, @degreeProgramId, @generationSemester;
    END

    CLOSE student_cursor;
    DEALLOCATE student_cursor;
END;
GO

/*
    This SP closes the academic year, deactivates the specified academic year record,
    increments the semester for active generations, and determines if students pass the year.

    Using this logic:

    1. Deactivate the specified academic year:
        - Sets the IsActive column to 0 for the specified academic year.

    2. Increment CurrentSemesterOfStudies for active generations:
        - Increments the CurrentSemesterOfStudies column by 1 for all active generations.
        - Deactivates generations that have reached the last semester.

    3. Determine if students pass the year:
        - For first-year students, they must have gained 30 ECTS credits to pass.
        - For second-year students, they must have gained 80 ECTS credits to pass.
        - If a student passes the year, their CurrentSemester is incremented by 2.
        - If a student does not pass the year:
            - Remove them from their current group.
            - Attempt to place them in a group of a generation that is 2 semesters lower within the same degree program.
            - If no such generation exists, only remove them from their current group.
*/
