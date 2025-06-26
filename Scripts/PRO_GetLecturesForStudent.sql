create  PROCEDURE GetLecturesForStudent
    @FingerID INT,
    @Day INT,
    @Time TIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Table variable to store subject IDs
    DECLARE @GetIdSub TABLE (
        SubId INT 
    ); 

    -- Get all subjects for the student with the specified FingerID
    INSERT INTO @GetIdSub (SubId)
    SELECT Sub_ID 
    FROM Students St 
    JOIN Studets_Subject SS ON St.ID = SS.St_ID 
    JOIN Subjects Sb ON SS.Sub_ID = Sb.ID  
    WHERE St.FingerID = @FingerID;
 
    -- Get lectures that match the criteria
    SELECT ID 
    FROM Lecture 
    WHERE Sub_ID IN (SELECT SubId FROM @GetIdSub)
    AND day = @Day 
    AND @Time BETWEEN FromTime AND ToTime;
END

go
-- Execute the stored procedure for FingerID 5, day 1, at 10:30 AM
EXEC GetLecturesForStudent 
    @FingerID = 5, 
    @Day = 1, 
    @Time = '10:30:00';