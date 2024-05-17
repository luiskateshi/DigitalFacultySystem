CREATE PROCEDURE [dbo].[SP_InsertExamsAndStudentsInExam]
    @examSessionId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @currentDate DATE = GETDATE(); 

    -- Insert exams only for those subjects of the study plan that:
		--still has active courses corresponding to the subjects the study plan
		--these courses till have at least one student that has attended the course and (didn't pass the exam OR passed the exam but made a request to improve it)
    INSERT INTO Exams (Id, Name, PlanSubjectId, ExamSessionId, Date, IsActive)
    SELECT NEWID(), sps.Name, sps.Id, @examSessionId, @currentDate, 1
    FROM Courses c
    JOIN StudyPlanSubjects sps ON c.StudyPlanSubjectId = sps.Id
    WHERE c.IsActive = 1
	AND NOT EXISTS (    --to not insert records twice just in case the generate button is clicked twice
        SELECT 1
        FROM Exams e
        WHERE e.PlanSubjectId = sps.Id
        AND e.ExamSessionId = @examSessionId
    )
    AND EXISTS (
        SELECT 1
        FROM CourseAttendances ca
        JOIN StudentInCoursees sic ON ca.StudentInCourseId = sic.Id
        WHERE sic.CourseId = c.Id
		AND ca.isAttended = 1 AND (sic.isActive = 1 OR EXISTS (
														SELECT 1
														FROM ExamRetakeRequests err 
                                                        JOIN Exams e ON e.PlanSubjectId = sps.Id
														WHERE err.StudentId = sic.StudentId
														AND err.ExamId = e.id))		
    );

    -- Insert students in exams only if they attended the course and (still haven't passed the exam of that subject or passed it once but made a request to improve the examGrade)
    INSERT INTO StudentsInExams (Id, ExamId, StudentId, IsActive)
    SELECT NEWID(), e.Id, sic.StudentId, 1
    FROM Exams e
    JOIN StudyPlanSubjects sps ON sps.Id = e.PlanSubjectId
    JOIN Courses c ON c.StudyPlanSubjectId = sps.Id
    JOIN StudentInCoursees sic ON sic.CourseId = c.Id
    JOIN CourseAttendances ca ON ca.StudentInCourseId = sic.Id
    WHERE e.ExamSessionId = @examSessionId
	AND ca.isAttended = 1
    AND (sic.IsActive = 1 OR EXISTS (
								SELECT 1
								FROM ExamRetakeRequests err 
								WHERE err.StudentId = sic.StudentId
								AND err.ExamId = e.id))
    AND NOT EXISTS (   --to not insert records twice just in case the generate button is clicked twice
        SELECT 1
        FROM StudentsInExams sie
        WHERE sie.ExamId = e.Id
        AND sie.StudentId = sic.StudentId
		AND e.ExamSessionId = @examSessionId
    );

END;

/*
	This SP inserts some new records in the exams table and studentsInExam. 
    Using this logic ... 

    Create all exams for a specific examSessionId for each course that:
        -is active
        -has at least one student that has attended the course and (didn't pass the exam OR passed the exam but made a request to improve it)
        -has not already an exam for this examSessionId.

    For each exam the studentInExam table has to be populated with only:
        -those students that have attended the course of this exam and (still haven't passed the exam of that subject or passed it once but made a request to improve the examGrade)
        -and have not already an exam for this examSessionId.
*/

