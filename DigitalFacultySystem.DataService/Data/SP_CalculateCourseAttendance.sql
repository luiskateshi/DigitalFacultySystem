CREATE PROCEDURE SP_CalculateCourseAttendance
    @courseId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ca
    SET ca.isAttended = CASE 
                            WHEN ca.SeminarHoursAttended >= 0.75 * sps.TotalHoursSeminars
                                AND ca.LabHoursAttended = sps.TotalHoursLab THEN 1
                            ELSE 0
                        END
    FROM CourseAttendances ca
    INNER JOIN StudentInCoursees sic ON ca.StudentInCourseId = sic.Id
    INNER JOIN Courses c ON sic.CourseId = c.Id
    INNER JOIN StudyPlanSubjects sps ON c.StudyPlanSubjectId = sps.Id
    WHERE c.Id = @courseId;
END;


--select s.Firstname, s.Lastname, c.Name, sps.TotalHoursSeminars, sps.TotalHoursLab, ca.SeminarHoursAttended, ca.LabHoursAttended, ca.isAttended from StudyPlanSubjects sps
--join Courses c on c.StudyPlanSubjectId = sps.Id
--join StudentInCoursees sic on sic.CourseId = c.id
--join CourseAttendances ca on ca.StudentInCourseId = sic.Id
--join Students s on s.id = sic.StudentId
--where c.id = 'E747CF8C-3043-48C2-BC88-2E38A196FF47'