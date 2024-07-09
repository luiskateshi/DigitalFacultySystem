USE [uniDb]
GO
/****** Object:  StoredProcedure [dbo].[SP_EndExamSession]    Script Date: 10/05/2024 17:34:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_EndExamSession]
    @examSessionId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    -- Deactivate the exam session
    UPDATE ExamsSessions
    SET IsActive = 0
    WHERE Id = @examSessionId;

    -- Deactivate student records in StudentInCoursees with grade > 4 in exams of this session
    UPDATE StudentInCoursees 
    SET IsActive = 0
    WHERE id in (SELECT sic.id FROM StudentInCoursees sic
	JOIN Students s on s.id = sic.StudentId
	JOIN StudentsInExams sie on sie.StudentId = s.id
	JOIN Courses c on c.id = sic.CourseId
	JOIN Exams e on e.id = sie.ExamId
	JOIN StudyPlanSubjects sps on sps.Id = c.StudyPlanSubjectId and sps.id = e.PlanSubjectId
	WHERE sie.ExamGrade > 4
	AND e.ExamSessionId = @examSessionId);
    

    -- Deactivate records in examRetakeRequests
    UPDATE ExamRetakeRequests
    SET IsActive = 0
    WHERE id in (SELECT err.id FROM ExamRetakeRequests err
	JOIN Students s on s.id = err.StudentId
	JOIN StudentsInExams sie on sie.StudentId = s.id
	JOIN Exams e on e.id = sie.ExamId
	JOIN StudyPlanSubjects sps on sps.Id = e.PlanSubjectId
	WHERE e.ExamSessionId = @examSessionId);

    -- Deactivate all exams of this exam session
    UPDATE Exams
    SET IsActive = 0
    WHERE ExamSessionId = @examSessionId;

    -- Deactivate student records in StudentsInExams for exams of this session
    UPDATE StudentsInExams
    SET IsActive = 0
    WHERE ExamId IN (
        SELECT Id
        FROM Exams
        WHERE ExamSessionId = @examSessionId
    );

END;
